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
    public partial class SysModule
    {
        private static DapperMySQLHelper mysql = new DapperMySQLHelper();

        public SysModule()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(Model.SysModule model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sys_Module(");
            strSql.Append("moduleId,moduleName,moduleImgPath,moduleUrlPath,preModuleId,moduleRemark,moduleState,crdt,crUsrId,updt,upUsrId)");
            strSql.Append(" values (");
            strSql.Append("?moduleId,?moduleName,?moduleImgPath,?moduleUrlPath,?preModuleId,?moduleRemark,?moduleState,?crdt,?crUsrId,?updt,?upUsrId)");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?moduleId", model.moduleId);
            parameters.Add("?moduleName", model.moduleName);
            parameters.Add("?moduleImgPath", model.ModuleImgPath);
            parameters.Add("?moduleUrlPath", model.ModuleUrlPath);
            parameters.Add("?preModuleId", model.preModuleId);
            parameters.Add("?moduleRemark", model.moduleRemark);
            parameters.Add("?moduleState", model.moduleState);
            parameters.Add("?crdt", model.crdt);
            parameters.Add("?crUsrId", model.crUsrId);
            parameters.Add("?updt", model.updt);
            parameters.Add("?upUsrId", model.upUsrId);

            int rows = mysql.ExcuteNonQuery<Model.SysModule>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }


        public static bool Update(Model.SysModule model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sys_Module set ");
            strSql.Append("moduleName=?moduleName,");
            strSql.Append("moduleImgPath=?moduleImgPath,");
            strSql.Append("moduleUrlPath=?moduleUrlPath,");
            strSql.Append("preModuleId=?preModuleId,");
            strSql.Append("moduleRemark=?moduleRemark,");
            strSql.Append("moduleState=?moduleState,");
            strSql.Append("upUsrId=?upUsrId,");
            strSql.Append("updt=?updt,");
            strSql.Append(" where moduleId=?moduleId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?moduleId", model.moduleId);
            parameters.Add("?moduleName", model.moduleName);
            parameters.Add("?moduleImgPath", model.ModuleImgPath);
            parameters.Add("?moduleUrlPath", model.ModuleUrlPath);
            parameters.Add("?preModuleId", model.preModuleId);
            parameters.Add("?moduleRemark", model.moduleRemark);
            parameters.Add("?moduleState", model.moduleState);
            parameters.Add("?updt", model.updt);
            parameters.Add("?upUsrId", model.upUsrId);

            int rows = mysql.ExcuteNonQuery<Model.SysModule>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool Delete(string strSysModuleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sys_Module ");
            strSql.Append(" where moduleId=?moduleId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?moduleId", strSysModuleId);

            int rows = mysql.ExcuteNonQuery<Model.SysModule>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        /// <summary>
        /// 获取顶级模块列表
        /// </summary>
        /// <returns></returns>
        public static IList<Model.SysModule> GetTopList()
        {
            return mysql.FindToList<Model.SysModule>(DapperMySQLHelper.ConnectionString,
                "SELECT moduleId,moduleName,moduleImgPath,moduleUrlPath,preModuleId,moduleRemark,moduleState,crdt,crUsrId,updt,upUsrId from Sys_Module WHERE (preModuleId='' or preModuleId is null)  ", null, false);
        }

        /// <summary>
        /// 获取顶级模块列表
        /// </summary>
        /// <returns></returns>
        public static IList<Model.SysModule> GetList()
        {
            return mysql.FindToList<Model.SysModule>(DapperMySQLHelper.ConnectionString,
                "SELECT moduleId,moduleName,moduleImgPath,moduleUrlPath,preModuleId,moduleRemark,moduleState,crdt,crUsrId,updt,upUsrId from Sys_Module where moduleState!=0 ", null, false);
        }

        /// <summary>
        /// 获取指定模块的子模块列表
        /// </summary>
        /// <returns></returns>
        public static IList<Model.SysModule> GetChildNodes(string strPreModuleId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?preModuleId", strPreModuleId);

            return mysql.FindToList<Model.SysModule>(DapperMySQLHelper.ConnectionString,
                "select moduleId,moduleName,moduleImgPath,moduleUrlPath,preModuleId,moduleRemark,moduleState,crdt,crUsrId,updt,upUsrId from Sys_Module WHERE preModuleId=?preModuleId and moduleState!=0 ", parameters, false);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.SysModule GetModel(string strModuleId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select moduleId,moduleName,moduleImgPath,moduleUrlPath,preModuleId,moduleRemark,moduleState,crdt,crUsrId,updt,upUsrId from Sys_Module ");
            strSql.Append(" where moduleId=?moduleId and moduleState!=0 ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?moduleId", strModuleId);

            return mysql.FindOne<Model.SysModule>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }

        public static IList<Model.SysModule> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("_fields", "moduleId,moduleName,moduleImgPath,moduleUrlPath,preModuleId,moduleRemark,moduleState,crdt,crUsrId,updt,upUsrId");
            parameters.Add("_tables", " Sys_Module");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " updt desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.SysModule>(DapperMySQLHelper.ConnectionString, "SP_PAGER", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 根据角色模块ID获取一个角色模块角色关系实体对象
        /// </summary>
        /// <param name="strRoleId">角色模块ID</param>
        /// <returns></returns>
        public static IList<Model.SysModule> GetModelListByRoleIdList(List<string> strRoleIdList)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT sm.moduleId, sm.moduleCode, sm.moduleName, sm.moduleState, sm.moduleImgPath , sm.preModuleId ,sm.moduleNum ");

            StringBuilder strList = new StringBuilder();
            foreach (var item in strRoleIdList)
            {
                strList.AppendFormat("'{0}',", item);
            }
            strList.Remove(strList.Length - 1, 1);
            strSql.AppendFormat(" FROM rolesysmodule rsm INNER JOIN sys_module sm ON rsm.sysModuleId = sm.moduleId AND roleId in ({0}) AND sm.moduleState = 1 and rsm.roleSysModuleState=1 order by sm.moduleName desc ", strList);

            return mysql.FindToList<Model.SysModule>(DapperMySQLHelper.ConnectionString, strSql.ToString(), null, false);
        }
    }
}
