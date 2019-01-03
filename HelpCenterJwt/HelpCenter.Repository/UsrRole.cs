using Dapper;
using HelpCenter.Common.DBUtility;
using HelpCenter.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HelpCenter.Repository
{
    public class UsrRole
    {

        private static DapperMySQLHelper mysql = new DapperMySQLHelper();

        public static bool Set(Model.UsrRole usrRoleModel, List<string> RoleIdList)
        {
            List<CmdParams> parameters = new List<CmdParams>();

            /*先清空原有关系*/
            CmdParams cpDel = new CmdParams
            {
                cmd = "delete from usrrole where usrId=?usrId",
                param = new DynamicParameters()
            };
            cpDel.param.Add("?usrId", usrRoleModel.usrId);

            parameters.Add(cpDel);
            if (RoleIdList == null)
            {
                RoleIdList = new List<string>();
            }
            /*默认游客角色编号*/
            string strDefaultRole = "4b5df8d4-e422-48b4-8138-7fd6806dd214";
            if (!RoleIdList.Contains(strDefaultRole))
            {
                /*添加默认游客角色权限*/
                RoleIdList.Add(strDefaultRole);
            }

            /*循环赋予角色*/
            foreach (string strRoleId in RoleIdList)
            {
                if (HelpCenter.Repository.Role.GetModel(strRoleId) == null)
                {
                    //不存在的角色不允许添加到数据表里
                    //修改人CJC，2018.12.18.13:29:00
                    continue;
                }
                CmdParams cp = new CmdParams
                {
                    cmd = "insert into usrrole(usrRoleId,usrId,roleId,usrRoleState,crdt,crUsrId,updt,upUsrId) values (?usrRoleId,?usrId,?roleId,?usrRoleState,?crdt,?crUsrId,?updt,?upUsrId)",
                    param = new DynamicParameters()
                };
                cp.param.Add("?usrRoleId", Guid.NewGuid().ToString());
                cp.param.Add("?usrId", usrRoleModel.usrId);
                cp.param.Add("?roleId", strRoleId);
                cp.param.Add("?usrRoleState", usrRoleModel.usrRoleState);
                cp.param.Add("?crdt", usrRoleModel.crdt);
                cp.param.Add("?crUsrId", usrRoleModel.crUsrId);
                cp.param.Add("?updt", usrRoleModel.updt);
                cp.param.Add("?upUsrId", usrRoleModel.upUsrId);
                parameters.Add(cp);
            }

            int rows = mysql.ExcuteNonQueryTransaction<Model.UsrRole>(DapperMySQLHelper.ConnectionString, parameters, false);

            return rows > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Model.UsrRole usrRoleModel)
        {

            StringBuilder strRoleIdList = new StringBuilder();
            foreach (var strRoleId in usrRoleModel.roleId)
            {
                strRoleIdList.Append(strRoleId + ",");
            }

            strRoleIdList = strRoleIdList.Length > 0 ? strRoleIdList.Remove(strRoleIdList.Length - 1, 1) : strRoleIdList;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update usrrole set ");
            strSql.Append("roleId=?roleId,");
            strSql.Append("updt=?updt,");
            strSql.Append("upUsrId=?upUsrId");
            strSql.Append(" where usrRoleId=?usrRoleId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleId", strRoleIdList);
            parameters.Add("?updt", usrRoleModel.updt);
            parameters.Add("?upUsrId", usrRoleModel.upUsrId);
            parameters.Add("?usrRoleId", usrRoleModel.usrRoleId);

            int rows = mysql.ExcuteNonQuery<Model.UsrRole>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool Delete(string usrId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from usrrole ");
            strSql.Append(" where usrId=?usrId ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrId", usrId);

            int rows = mysql.ExcuteNonQuery<Model.UsrRole>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        /// <summary>
        /// 获取用户角色关系列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.UsrRole> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "usrRoleId,usrId,roleId,usrRoleState,crdt,crUsrId,updt,upUsrId,FN_ROLE_ROLENAME(usrId) roleName");
            parameters.Add("_tables", "usrrole");
            parameters.Add("_where", " 1=1  ");
            parameters.Add("_orderby", " updt desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.UsrRole>(DapperMySQLHelper.ConnectionString, "SP_PAGER", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 根据用户ID获取一个用户角色关系实体对象
        /// </summary>
        /// <param name="usrId">用户ID</param>
        /// <returns></returns>
        public static IList<Model.UsrRole> GetModel(string usrId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select usrRoleId,usrId,roleId,usrRoleState,crdt,crUsrId,updt,upUsrId,FN_ROLE_ROLENAME(usrId) roleName from usrrole ");
            strSql.Append(" where usrId=?usrId ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?usrId", usrId);

            return mysql.FindToList<Model.UsrRole>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }
    }
}
