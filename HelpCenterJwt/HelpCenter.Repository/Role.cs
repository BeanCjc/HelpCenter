using Dapper;
using HelpCenter.DBUtility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace HelpCenter.Repository
{
    public class Role
    {

        private static DapperMySQLHelper mysql = new DapperMySQLHelper();

        public static bool Add(Model.Role roleModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into role(");
            strSql.Append("roleId,roleName,roleState,roleRemark,crdt,crUsrId,updt,upUsrId)");
            strSql.Append(" values (");
            strSql.Append("?roleId,?roleName,?roleState,?roleRemark,?crdt,?crUsrId,?updt,?upUsrId)");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleId", roleModel.roleId);
            parameters.Add("?roleName", roleModel.roleName);
            parameters.Add("?roleState", roleModel.roleState);
            parameters.Add("?roleRemark", roleModel.roleRemark);
            parameters.Add("?crdt", roleModel.crdt);
            parameters.Add("?crUsrId", roleModel.crUsrId);
            parameters.Add("?updt", roleModel.updt);
            parameters.Add("?upUsrId", roleModel.upUsrId);

            int rows = mysql.ExcuteNonQuery<Model.Role>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Model.Role roleModel)
        {
            #region 更新角色信息的语句
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update role set ");
            strSql.Append("roleName=?roleName,");
            strSql.Append("roleState=?roleState,");
            strSql.Append("roleRemark=?roleRemark,");
            strSql.Append("updt=?updt,");
            strSql.Append("upUsrId=?upUsrId");
            strSql.Append(" where roleId=?roleId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleName", roleModel.roleName);
            parameters.Add("?roleState", roleModel.roleState);
            parameters.Add("?roleRemark", roleModel.roleRemark);
            parameters.Add("?updt", roleModel.updt);
            parameters.Add("?upUsrId", roleModel.upUsrId);
            parameters.Add("?roleId", roleModel.roleId);
            #endregion

            #region 更新用户角色表的状态的语句
            var strSqlUpdateusrrole = "update usrrole set usrrolestate=?roleState where roleId=?roleId";
            var param = new DynamicParameters();
            param.Add("?roleState", roleModel.roleState);
            param.Add("?roleId", roleModel.roleId);

            #endregion

            #region 更新角色模块表的状态的语句
            var strSqlUpdaterolesysmodule = "update rolesysmodule set roleSysModuleState=?roleState where roleId=?roleId";
            var param1 = new DynamicParameters();
            param1.Add("?roleState", roleModel.roleState);
            param1.Add("?roleId", roleModel.roleId);
            #endregion

            #region 更新角色文档表的状态的语句
            var strSqlUpdatedocusrrole = "update docusrrole set docRoleState=?roleState where roleId=?roleId";
            var param2 = new DynamicParameters();
            param2.Add("?roleState", roleModel.roleState);
            param2.Add("?roleId", roleModel.roleId);
            #endregion

            var flag = false;
            #region 事物处理

            using (var tran = new TransactionScope())
            {
                using (var db = new MySqlConnection(DapperMySQLHelper.ConnectionString))
                {
                    db.Execute(strSql.ToString(), parameters, null, null, CommandType.Text);
                    db.Execute(strSqlUpdateusrrole.ToString(), param, null, null, CommandType.Text);
                    db.Execute(strSqlUpdaterolesysmodule, param1, null, null, CommandType.Text);
                    db.Execute(strSqlUpdatedocusrrole, param2, null, null, CommandType.Text);
                    tran.Complete();
                    flag = true;
                }
            }
            #endregion

            //int rows = mysql.ExcuteNonQuery<Model.Role>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return flag;
        }

        public static bool Delete(string strRoleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from role ");
            strSql.Append(" where roleId=?roleId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleId", strRoleId);

            int rows = mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool Enable(string strRoleId, int roleState)
        {
            #region 更新角色状态的语句
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update role set roleState=@roleState ");
            strSql.Append(" where roleId=?roleId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleState", roleState);
            parameters.Add("?roleId", strRoleId);
            #endregion

            #region 更新用户角色表的状态的语句
            var strSqlUpdateusrrole = "update usrrole set usrrolestate=?roleState where roleId=?roleId";
            var param = new DynamicParameters();
            param.Add("?roleState", roleState);
            param.Add("?roleId", strRoleId);

            #endregion

            #region 更新角色模块表的状态的语句
            var strSqlUpdaterolesysmodule = "update rolesysmodule set roleSysModuleState=?roleState where roleId=?roleId";
            var param1 = new DynamicParameters();
            param1.Add("?roleState", roleState);
            param1.Add("?roleId", strRoleId);
            #endregion

            #region 更新角色文档表的状态的语句
            var strSqlUpdatedocusrrole = "update docusrrole set docRoleState=?roleState where roleId=?roleId";
            var param2 = new DynamicParameters();
            param2.Add("?roleState", roleState);
            param2.Add("?roleId", strRoleId);
            #endregion

            var flag = false;
            #region 事物处理

            using (var tran = new TransactionScope())
            {
                using (var db = new MySqlConnection(DapperMySQLHelper.ConnectionString))
                {
                    db.Execute(strSql.ToString(), parameters, null, null, CommandType.Text);
                    db.Execute(strSqlUpdateusrrole.ToString(), param, null, null, CommandType.Text);
                    db.Execute(strSqlUpdaterolesysmodule, param1, null, null, CommandType.Text);
                    db.Execute(strSqlUpdatedocusrrole, param2, null, null, CommandType.Text);
                    tran.Complete();
                    flag = true;
                }
            }
            #endregion

            //int rows = mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return flag;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.Role> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "roleId,roleName,roleState,roleRemark,crdt,crUsrId,FN_USR_USRNAME(crUsrId) crUsrName,updt,upUsrId,FN_USR_USRNAME(upUsrId) upUsrName");
            parameters.Add("_tables", "role");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " updt desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.Role>(DapperMySQLHelper.ConnectionString, "SP_PAGER", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }


        /// <summary>
        /// 根据编号获取一个角色实体对象
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public static Model.Role GetModel(string roleId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select roleId,roleName,roleState,roleRemark,crdt,crUsrId,updt,upUsrId from role ");
            strSql.Append(" where roleId=?roleId ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleId", roleId);

            return mysql.FindOne<Model.Role>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }

        /// <summary>
        /// 根据编号获取一个角色实体对象
        /// </summary>
        /// <returns></returns>
        public static IList<Model.Role> GetAllList(bool isEnable = false)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            if (isEnable)
            {
                strSql.Append("select * from role where rolestate<>0");
            }
            else
            {
                strSql.Append("select * from role ");
            }
            return mysql.FindToList<Model.Role>(DapperMySQLHelper.ConnectionString, strSql.ToString(), null, false);
        }

    }
}
