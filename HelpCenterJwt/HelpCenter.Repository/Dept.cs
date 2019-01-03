using Dapper;
using HelpCenter.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HelpCenter.ActionService;
using System.Linq;

namespace HelpCenter.Repository
{
    public class Dept
    {

        private static DapperMySQLHelper mysql = new DapperMySQLHelper();

        public static bool Add(Model.Dept deptModel, Model.DeptAccount deptAccountModel)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_deptId", deptModel.deptId);
            parameters.Add("_deptNO", deptModel.deptNO);
            parameters.Add("_deptName", deptModel.deptName);
            parameters.Add("_deptState", deptModel.deptState);
            parameters.Add("_preDeptId", deptModel.preDeptId);
            parameters.Add("_deptAccountId", deptAccountModel.deptAccountId);
            parameters.Add("_deptAccount", deptAccountModel.deptAccount);
            parameters.Add("_deptPsw", deptAccountModel.deptPsw);
            parameters.Add("_crUsrId", deptAccountModel.crUsrId);
            parameters.Add("_crdt", deptAccountModel.crdt);
            parameters.Add("_updt", deptAccountModel.updt);
            parameters.Add("_upUsrId", deptAccountModel.upUsrId);
            parameters.Add("isErr", dbType: DbType.Int32, direction: ParameterDirection.Output);

            /*调用存储过程进行事务处理*/
            mysql.ExcuteNonQuery<Model.Dept>(DapperMySQLHelper.ConnectionString, "SP_ADD_DEPT_ACCOUNT", parameters, true);

            return parameters.Get<int>("isErr") == 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Model.Dept deptModel, Model.DeptAccount deptAccountModel, Model.Usr usrModel)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_deptId", deptModel.deptId);
            parameters.Add("_deptName", deptModel.deptName);
            parameters.Add("_deptAccount", deptModel.deptAccount);
            parameters.Add("_deptPsw", usrModel.usrPsw);
            parameters.Add("_preDeptId", deptModel.preDeptId);
            parameters.Add("_updt", deptModel.updt);
            parameters.Add("_upUsrId", deptModel.upUsrId);
            parameters.Add("isErr", dbType: DbType.Int32, direction: ParameterDirection.Output);

            mysql.ExcuteNonQuery<Model.Dept>(DapperMySQLHelper.ConnectionString, "SP_UPDATE_DEPT_ACCOUNT", parameters, true);
            return parameters.Get<int>("isErr") == 0;
        }

        public static bool UpdatePassWord(string strDeptId, string strDeptPsw, string strUpUsrId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_deptPsw", strDeptPsw);
            parameters.Add("_deptId", strDeptId);
            parameters.Add("_upUsrId", strUpUsrId);
            parameters.Add("_updt", DateTime.Now);
            parameters.Add("isErr", dbType: DbType.Int32, direction: ParameterDirection.Output);

            mysql.ExcuteNonQuery<Model.Dept>(DapperMySQLHelper.ConnectionString, "SP_UPDATE_DEPT_PSW", parameters, true);
            return parameters.Get<int>("isErr") == 0;
        }

        public static bool Delete(string strDeptId, out string tips)
        {
            //有子部门不允许删除
            var nodes = GetChildNodes(strDeptId);
            if (null != nodes && nodes.Count > 0)
            {
                tips = ResponseMessageTips.MSG_DEPT_HAS_CHILD_DEL_FAIL;
                return false;
            }

            //有绑定文档关系不允许删除
            var checkExist = DocDeptRole.GetModel(strDeptId);
            if (null != checkExist)
            {
                tips = ResponseMessageTips.MSG_DEPT_HAS_BIND_DOC_DEL_FAIL;
                return false;
            }

            //部门下有人员
            var checkUser = Usr.GetUsrsByDeptId(strDeptId);
            if (checkUser != null && checkUser.Count > 0)
            {
                tips = ResponseMessageTips.MSG_DEPT_HAS_BIND_USER_DEL_FAIL;
                return false;
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_deptId", strDeptId);
            parameters.Add("isErr", dbType: DbType.Int32, direction: ParameterDirection.Output);

            mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, "SP_DEL_DEPT_ACCOUNT", parameters, true);
            if (parameters.Get<int>("isErr") == 0)
            {
                tips = ResponseMessageTips.MSG_DEPT_DELETE_SUCCESS;
                return true;
            }
            else
            {
                tips = ResponseMessageTips.MSG_DEPT_DELETE_FAIL;
                return false;
            }
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptModel> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount, string deptNO)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "deptId,deptNO,deptName,deptState,preDeptId,FN_DEPT_PREDEPTNAME(preDeptId) preDeptName,dept.crUsrId,dept.crdt,dept.updt,dept.upUsrId,FN_DEPT_CHECKCHILD(DEPTID) childCount,usr.usrAccount deptAccount,usr.usrType,usr.usrState");
            parameters.Add("_tables", " dept INNER JOIN usr on dept.deptId=usr.usrDeptId and usr.usrType=1 and dept.deptNO like '" + deptNO + "%'");
            parameters.Add("_where", "");
            parameters.Add("_orderby", " dept.updt desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToList<Model.DbModel.DbDeptModel>(DapperMySQLHelper.ConnectionString, "SP_PAGER", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptModel> GetListByDeptName(int pageIndex, int rowCount, string strDeptName, out int totalCount, out int pageCount, string deptNO)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "deptId,deptNO,deptName,deptState,preDeptId,FN_DEPT_PREDEPTNAME(preDeptId) preDeptName,dept.crUsrId,dept.crdt,dept.updt,dept.upUsrId,FN_DEPT_CHECKCHILD(DEPTID) childCount,usr.usrAccount deptAccount,usr.usrType,usr.usrState");
            parameters.Add("_tables", " dept INNER JOIN usr on dept.deptId=usr.usrDeptId and usr.usrType=1 and dept.deptNO like '" + deptNO + "%'");
            parameters.Add("_orderby", " dept.updt desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_paramsDeptName", strDeptName);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            IList<Model.DbModel.DbDeptModel> list = mysql.FindToList<Model.DbModel.DbDeptModel>(DapperMySQLHelper.ConnectionString, "SP_PAGER_DEPT_DEPTNAME", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptModel> GetTopList()
        {
            return mysql.FindToList<Model.DbModel.DbDeptModel>(DapperMySQLHelper.ConnectionString,
                "SELECT deptId,deptNO,deptName,deptState,preDeptId,FN_DEPT_PREDEPTNAME(preDeptId) preDeptName,dept.crUsrId,dept.crdt,dept.updt,dept.upUsrId,FN_DEPT_CHECKCHILD(DEPTID) childCount,usr.usrAccount deptAccount,usr.usrType,usr.usrState "
                + " FROM dept INNER JOIN usr on dept.deptId=usr.usrDeptId and usr.usrType=1 and  (dept.preDeptId= '' or dept.preDeptId is null)  ", null, false);
        }
        /// <summary>
        /// 获取指定部门的子部门列表
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptModel> GetChildNodes(string strPreDeptId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?preDeptId", strPreDeptId);

            return mysql.FindToList<Model.DbModel.DbDeptModel>(DapperMySQLHelper.ConnectionString,
                "SELECT deptId,deptNO,deptName,deptState,preDeptId,FN_DEPT_PREDEPTNAME(preDeptId) preDeptName,dept.crUsrId,dept.crdt,dept.updt,dept.upUsrId,FN_DEPT_CHECKCHILD(DEPTID) childCount,usr.usrAccount deptAccount,usr.usrType,usr.usrState "
                + " FROM dept INNER JOIN usr on dept.deptId=usr.usrDeptId and usr.usrType=1 and  dept.preDeptId= ?preDeptId  ",
                parameters, false);
        }

        /// <summary>
        /// 根据编号获取一个部门实体对象
        /// </summary>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        public static Model.Dept GetModel(string deptId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select _dept.*,usr.usrAccount deptAccount, usr.usrPsw deptPsw, usr.usrState from( SELECT deptId, deptNO, deptName, deptState,preDeptId,FN_DEPT_PREDEPTNAME(preDeptId) preDeptName,crUsrId,crdt,updt,upUsrId,FN_DEPT_CHECKCHILD ( DEPTID )  childCount  ");
            strSql.Append(" FROM dept WHERE deptId = ?deptId ) _dept LEFT JOIN usr on _dept.deptId=usr.usrDeptId and usr.usrType=1 ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?deptId", deptId);

            return mysql.FindOne<Model.Dept>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }

        /// <summary>
        /// 根据编号获取一个部门实体对象
        /// </summary>
        /// <param name="deptId">部门编号</param>
        /// <returns></returns>
        public static IList<Model.Dept> GetModelListByDeptId(string deptId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select _dept.*,usr.usrAccount deptAccount from( SELECT deptId, deptNO, deptName, deptState,preDeptId,FN_DEPT_PREDEPTNAME(preDeptId) preDeptName,crUsrId,crdt,updt,upUsrId,FN_DEPT_CHECKCHILD ( DEPTID )  childCount  ");
            strSql.Append(" FROM dept WHERE deptId = ?deptId ) _dept LEFT JOIN usr on _dept.deptId=usr.usrDeptId and usr.usrType=1 ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?deptId", deptId);

            return mysql.FindToList<Model.Dept>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }

        public static string GetMaxDeptNumByPreDeptId(string preDeptId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(deptNO) from dept where preDeptId=?preDeptId");
            //DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("?preDeptId", preDeptId);

            using (IDbConnection conn = DbSwitcher.GetOpenConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("?preDeptId", preDeptId);
                return conn.ExecuteScalar(strSql.ToString(), parameters)?.ToString();
            }
            //return mysql.FindOne<Model.Dept>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);

        }
    }
}
