using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using HelpCenter;
using HelpCenter.DBUtility;

namespace HelpCenter.Repository
{
    public partial class Usr
    {
        private static DapperMySQLHelper mysql = new DapperMySQLHelper();

        public Usr()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(Model.Usr model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into usr(");
            strSql.Append("usrId,usrPhoneNum,usrAccount,usrPsw,usrName,usrDeptId,usrVerifyState,usrType,usrState,crdt,crUsrId,updt,upUsrId)");
            strSql.Append(" values (");
            strSql.Append("?usrId,?usrPhoneNum,?usrAccount,?usrPsw,?usrName,?usrDeptId,?usrVerifyState,?usrType,?usrState,?crdt,?crUsrId,?updt,?upUsrId)");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrId", model.usrId);
            parameters.Add("?usrPhoneNum", model.usrPhoneNum);
            parameters.Add("?usrAccount", model.usrAccount);
            parameters.Add("?usrPsw", model.usrPsw);
            parameters.Add("?usrName", model.usrName);
            parameters.Add("?usrDeptId", model.usrDeptId);
            parameters.Add("?usrVerifyState", model.usrVerifyState);
            parameters.Add("?usrType", model.usrType);
            parameters.Add("?usrState", model.usrState);
            parameters.Add("?crdt", model.crdt);
            parameters.Add("?crUsrId", model.crUsrId);
            parameters.Add("?updt", model.updt);
            parameters.Add("?upUsrId", model.upUsrId);

            int rows = mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }


        public static bool Update(Model.Usr model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update usr set ");
            strSql.Append("usrPhoneNum=?usrPhoneNum,");
            strSql.Append("usrAccount=?usrAccount,");
            if (!string.IsNullOrEmpty(model.usrPsw)) { strSql.Append("usrPsw=?usrPsw,"); };
            strSql.Append("usrName=?usrName,");
            strSql.Append("usrDeptId=?usrDeptId,");
            strSql.Append("usrVerifyState=?usrVerifyState,");
            strSql.Append("usrType=?usrType,");
            strSql.Append("usrState=?usrState,");
            strSql.Append("updt=?updt,");
            strSql.Append("upUsrId=?upUsrId");
            strSql.Append(" where usrId=?usrId ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrId", model.usrId);
            parameters.Add("?usrPhoneNum", model.usrPhoneNum);
            parameters.Add("?usrAccount", model.usrAccount);
            if (!string.IsNullOrEmpty(model.usrPsw)) { parameters.Add("?usrPsw", model.usrPsw); };
            parameters.Add("?usrName", model.usrName);
            parameters.Add("?usrDeptId", model.usrDeptId);
            parameters.Add("?usrVerifyState", model.usrVerifyState);
            parameters.Add("?usrType", model.usrType);
            parameters.Add("?usrState", model.usrState);
            parameters.Add("?updt", model.updt);
            parameters.Add("?upUsrId", model.upUsrId);

            int rows = mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool UpdatePassWord(string strUsrAccount, string strUsrPsw)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update usr set usrPsw=?usrPsw ");
            strSql.Append(" where usrAccount=?usrAccount ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrPsw", strUsrPsw);
            parameters.Add("?usrAccount", strUsrAccount);

            int rows = mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool UpdateUsrVerifyState(string strUsrId, int usrVerifyState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update usr set usrVerifyState=?usrVerifyState ");
            strSql.Append(" where usrId=?usrId ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrVerifyState", usrVerifyState);
            parameters.Add("?usrId", strUsrId);

            int rows = mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool UpdateUserState(string strUsrId, int usrState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update usr set usrState=?usrState ");
            strSql.Append(" where usrId=?usrId ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrState", usrState);
            parameters.Add("?usrId", strUsrId);

            int rows = mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool Delete(string strUsrId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_usrId", strUsrId);
            parameters.Add("isErr", dbType: DbType.Int32, direction: ParameterDirection.Output);

            mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, "SP_DEL_USR_ACCOUNT", parameters, true);
                return parameters.Get<int>("isErr") == 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.Usr GetModel(string usrId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,FN_USR_ALL_ROLENAME(usrId) roleName from v_usr_detail WHERE usrId =?usrId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrId", usrId);

            return mysql.FindOne<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.DTO.UserDto GetUserDtoModel(string usrId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,FN_USR_ALL_ROLENAME(usrId) roleName from v_usr_detail WHERE usrId =?usrId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrId", usrId);

            return mysql.FindOne<Model.DTO.UserDto>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.Usr CheckAccount(string account)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select usrId from usr ");
            strSql.Append(" where usrAccount=?usrAccount ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrAccount", account);

            return mysql.FindOne<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.Usr GetModel(string usrId, string usrPsw)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select usrId,usrPhoneNum,usrAccount,usrName,usrDeptId,usrVerifyState,usrType,usrState,crdt,crUsrId,updt,upUsrId,FN_USR_ALL_ROLENAME(usrId) roleName from usr ");
            strSql.Append(" where usrAccount=?usrAccount and usrPsw=?usrPsw ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrAccount", usrId);
            parameters.Add("?usrPsw", usrPsw);

            return mysql.FindOne<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }

        public static IList<Model.Usr> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount,string usrType)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("_fields", "usrId,usrPhoneNum,usrAccount,usrName,usrDeptId,usrVerifyState,usrType,usrState,crdt,crUsrId,updt,upUsrId,FN_USR_ALL_ROLENAME(usrId) roleName ");
            parameters.Add("_tables", "usr");
            parameters.Add("_where", " 1=1 and usr.usrType in (" + usrType+")");
            parameters.Add("_orderby", " usr.updt desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.Usr>(DapperMySQLHelper.ConnectionString, "SP_PAGER", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 按用户名搜索，分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="strUsrName"></param>
        /// <returns></returns>
        public static IList<Model.Usr> GetListByUsrName(int pageIndex, int rowCount, out int totalCount, out int pageCount, string strUsrName,string usrType,string strDeptID)
        {
            var deptInfo = mysql.FindOne<Model.Dept>(DapperMySQLHelper.ConnectionString, "SELECT DEPTNO FROM DEPT WHERE DEPTID='" + strDeptID + "'", null, false);

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("_tables", " SELECT sql_calc_found_rows * FROM (SELECT usr.usrId,usr.usrPhoneNum,usr.usrAccount,usr.usrName,usr.usrDeptId,FN_DEPT_PREDEPTNAME(usr.usrDeptId) deptName,"
                + "usr.usrVerifyState,usr.usrType,usr.usrState,usr.crdt,usr.crUsrId,usr.updt,usr.upUsrId,FN_USR_ALL_ROLENAME(usr.usrId) roleName FROM "
                + " usr LEFT JOIN dept ON usr.usrDeptId = dept.deptId " + (string.IsNullOrEmpty(strDeptID) ? "" : "AND dept.deptNO LIKE '" + deptInfo.deptNO + "%' "));
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " updt desc) t where usrType in (" + usrType + ") ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_paramsUsrName", strUsrName);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.Usr>(DapperMySQLHelper.ConnectionString, "SP_PAGER_USR_USRNAME", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 列出指定部门下的所有用户，分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="strDeptId"></param>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptUserModel> GetListByDeptId(int pageIndex, int rowCount, out int totalCount, out int pageCount, string strDeptId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("_where", " and usr.usrType in (2,3)");
            parameters.Add("_orderby", " usr.updt desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_paramsDeptID", strDeptId);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.DbModel.DbDeptUserModel>(DapperMySQLHelper.ConnectionString, "SP_PAGER_USR_DEPTID", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 获取指定部门下的所有员工
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static IList<Model.Usr> GetUsrsByDeptId(string deptId)
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from usr where usrDeptId=?deptId");
            var param = new DynamicParameters();
            param.Add("?deptId", deptId);
            return mysql.FindToList<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), param, false);
        }
    }
}
