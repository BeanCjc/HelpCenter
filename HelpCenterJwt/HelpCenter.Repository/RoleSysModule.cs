using Dapper;
using HelpCenter.Common.DBUtility;
using HelpCenter.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HelpCenter.Repository
{
    public class RoleSysModule
    {
        private static DapperMySQLHelper mysql = new DapperMySQLHelper();

        public static bool Add(Model.RoleSysModule roleSysModule)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into roleSysModule (");
            strSql.Append("roleSysModuleId,sysModuleId,roleId,roleSysModuleState,crdt,crUsrId,updt,upUsrId) ");
            strSql.Append(" values (");
            strSql.Append("?roleSysModuleId,?sysModuleId,?roleId,?roleSysModuleState,?crdt,?crUsrId,?updt,?upUsrId)");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleSysModuleId", roleSysModule.RoleSysModuleId);
            parameters.Add("?sysModuleId", roleSysModule.SysModuleId);
            parameters.Add("?roleId", roleSysModule.RoleId);
            parameters.Add("?roleSysModuleState", roleSysModule.RoleSysModuleState);
            parameters.Add("?crdt", roleSysModule.Crdt);
            parameters.Add("?crUsrId", roleSysModule.CrUsrId);
            parameters.Add("?updt", roleSysModule.Updt);
            parameters.Add("?upUsrId", roleSysModule.UpUsrId);

            int rows = mysql.ExcuteNonQuery<Model.RoleSysModule>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);

            return rows > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Model.RoleSysModule roleSysModule)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update roleSysModule set ");
            strSql.Append("sysModuleId=?sysModuleId,");
            strSql.Append("roleId=?roleId,");
            strSql.Append("roleSysModuleState=?roleSysModuleState");
            strSql.Append("updt=?updt");
            strSql.Append("upUsrId=?upUsrId");
            strSql.Append(" where roleSysModuleId=?roleSysModuleId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleSysModuleId", roleSysModule.RoleSysModuleId);
            parameters.Add("?sysModuleId", roleSysModule.SysModuleId);
            parameters.Add("?roleId", roleSysModule.RoleId);
            parameters.Add("?roleSysModuleState", roleSysModule.RoleSysModuleState);
            parameters.Add("?updt", roleSysModule.Updt);
            parameters.Add("?upUsrId", roleSysModule.UpUsrId);

            int rows = mysql.ExcuteNonQuery<Model.RoleSysModule>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool Delete(string strRoleSysModuleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update roleSysModule set roleSysModuleState=0 ");
            strSql.Append(" where roleSysModuleId=?roleSysModuleId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleSysModuleId", strRoleSysModuleId);

            int rows = mysql.ExcuteNonQuery<Model.RoleSysModule>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool Set(Model.RoleSysModule roleSysModule, string[] roleSysModuleList)
        {
            List<CmdParams> parameters = new List<CmdParams>();

            /*先清空原有关系*/
            CmdParams cpDel = new CmdParams
            {
                cmd = "delete from roleSysModule where roleId=?roleId",
                param = new DynamicParameters()
            };
            cpDel.param.Add("?roleId", roleSysModule.RoleId);

            parameters.Add(cpDel);

            /*批量更新到最新关系*/
            foreach (var item in roleSysModuleList)
            {
                CmdParams cp = new CmdParams
                {
                    cmd = "insert into roleSysModule(roleSysModuleId,sysModuleId,roleId,roleSysModuleState,crdt,crUsrId,updt,upUsrId) values (?roleSysModuleId,?sysModuleId,?roleId,?roleSysModuleState,?crdt,?crUsrId,?updt,?upUsrId)",
                    param = new DynamicParameters()
                };

                cp.param.Add("?roleSysModuleId", Guid.NewGuid().ToString());
                cp.param.Add("?sysModuleId", item);
                cp.param.Add("?roleId", roleSysModule.RoleId);
                cp.param.Add("?roleSysModuleState", roleSysModule.RoleSysModuleState);
                cp.param.Add("?crdt", roleSysModule.Crdt);
                cp.param.Add("?crUsrId", roleSysModule.CrUsrId);
                cp.param.Add("?updt", roleSysModule.Updt);
                cp.param.Add("?upUsrId", roleSysModule.UpUsrId);

                parameters.Add(cp);
            }

            /*调用事务处理*/
            int rows = mysql.ExcuteNonQueryTransaction<Model.RoleSysModule>(DapperMySQLHelper.ConnectionString, parameters, false);

            return rows > 0;
        }

        /// <summary>
        /// 获取角色模块关系列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.RoleSysModule> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "roleSysModuleId,sysModuleId,roleId,roleSysModuleState,crdt,crUsrId,updt,upUsrId");
            parameters.Add("_tables", "roleSysModule");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " updt desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.RoleSysModule>(DapperMySQLHelper.ConnectionString, "SP_PAGER", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 根据角色模块ID获取一个角色模块角色关系实体对象
        /// </summary>
        /// <param name="strRoleId">角色模块ID</param>
        /// <returns></returns>
        public static IList<Model.RoleSysModule> GetModelListByRoleId(string strRoleId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select roleSysModuleId,sysModuleId,roleId,roleSysModuleState,crdt,crUsrId,updt,upUsrId from RoleSysModule ");

            strSql.AppendFormat(" where roleId= '{0}' ", strRoleId);

            return mysql.FindToList<Model.RoleSysModule>(DapperMySQLHelper.ConnectionString, strSql.ToString(), null, false);
        }


    }
}
