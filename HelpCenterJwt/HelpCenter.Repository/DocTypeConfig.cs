using Dapper;
using HelpCenter.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HelpCenter.Repository
{
    public class DocTypeConfig
    {

        private static DapperMySQLHelper mysql = new DapperMySQLHelper();

        public static bool Add(Model.DocTypeConfig docTypeConfig)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into doctypeconfig(");
            strSql.Append("docTypeId,docTypeNum,docPreTypeId,docTypeName,docTypeDeptId,docTypeState,crdt,crUsrId,updt,upUsrId)");
            strSql.Append(" values (");
            strSql.Append("?docTypeId,?docTypeNum,?docPreTypeId,?docTypeName,?docTypeDeptId,?docTypeState,?crdt,?crUsrId,?updt,?upUsrId)");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?docTypeId", docTypeConfig.docTypeId);
            parameters.Add("?docTypeNum", docTypeConfig.docTypeNum);
            parameters.Add("?docPreTypeId", docTypeConfig.docPreTypeId);
            parameters.Add("?docTypeName", docTypeConfig.docTypeName);
            parameters.Add("?docTypeDeptId", docTypeConfig.docTypeDeptId);
            parameters.Add("?docTypeState", docTypeConfig.docTypeState);
            parameters.Add("?crdt", docTypeConfig.crdt);
            parameters.Add("?crUsrId", docTypeConfig.crUsrId);
            parameters.Add("?updt", docTypeConfig.updt);
            parameters.Add("?upUsrId", docTypeConfig.upUsrId);

            int rows = mysql.ExcuteNonQuery<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Model.DocTypeConfig docTypeConfig)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update doctypeconfig set ");
            strSql.Append("docTypeNum=?docTypeNum,");
            strSql.Append("docPreTypeId=?docPreTypeId,");
            strSql.Append("docTypeName=?docTypeName,");
            strSql.Append("docTypeDeptId=?docTypeDeptId,");
            strSql.Append("docTypeState=?docTypeState,");
            strSql.Append("updt=?updt,");
            strSql.Append("upUsrId=?upUsrId");
            strSql.Append(" where docTypeId=?docTypeId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?docTypeId", docTypeConfig.docTypeId);
            parameters.Add("?docTypeNum", docTypeConfig.docTypeNum);
            parameters.Add("?docPreTypeId", docTypeConfig.docPreTypeId);
            parameters.Add("?docTypeName", docTypeConfig.docTypeName);
            parameters.Add("?docTypeDeptId", docTypeConfig.docTypeDeptId);
            parameters.Add("?docTypeState", docTypeConfig.docTypeState);
            parameters.Add("?updt", docTypeConfig.updt);
            parameters.Add("?upUsrId", docTypeConfig.upUsrId);

            int rows = mysql.ExcuteNonQuery<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static bool Delete(string docTypeId,out string strTips)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(1) from helpdoc where docTypeId=?docTypeId");

            using (IDbConnection conn = DbSwitcher.GetOpenConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("?docTypeId", docTypeId);
                int.TryParse(conn.ExecuteScalar(strSql.ToString(), parameters)?.ToString(),out int result);
                if (result > 0)
                {
                    strTips = ActionService.ResponseMessageTips.MSG_DOCTYPE_HAS_BIND_DOC;
                    return false;
                }
                strSql = new StringBuilder();
                strSql.Append("delete from doctypeconfig ");
                strSql.Append(" where docTypeId=?docTypeId");

                int rows = mysql.ExcuteNonQuery<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
                strTips = rows > 0
                    ? ActionService.ResponseMessageTips.MSG_DOCTYPE_DELETE_SUCCESS
                    : ActionService.ResponseMessageTips.MSG_DOCTYPE_DELETE_FAIL;
                return rows > 0;
            }

        }

        /// <summary>
        /// 获取文档类型列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount,string strDeptId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_paramsDeptID", strDeptId);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString, "SP_PAGER_DOCTYPECONFIG_DEPTNO", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 获取文档类型列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetListByDeptId(int pageIndex, int rowCount, out int totalCount, out int pageCount, string strDeptId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "docTypeId,docTypeNum,docPreTypeId,FN_DOCTYPECONFIG_DOCTYPENAME(docPreTypeId) docPreTypeName,docTypeName,docTypeDeptId,FN_DEPT_PREDEPTNAME(docTypeDeptId) docTypeDeptName,FN_DOCTYPECONFIG_CHECKCHILD(docTypeId) childCount,docTypeState,crdt,crUsrId,updt,upUsrId");
            parameters.Add("_tables", "doctypeconfig");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " updt desc,docTypeNum asc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_paramsDeptID", strDeptId);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString, "SP_PAGER_DOCTYPECONFIG_DEPTID", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 获取文档类型列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetListByDocTypeId(int pageIndex, int rowCount, out int totalCount, out int pageCount, string strPreTypeID)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "docTypeId,docTypeNum,docPreTypeId,FN_DOCTYPECONFIG_DOCTYPENAME(docPreTypeId) docPreTypeName,docTypeName,docTypeDeptId,FN_DEPT_PREDEPTNAME(docTypeDeptId) docTypeDeptName,FN_DOCTYPECONFIG_CHECKCHILD(docTypeId) childCount,docTypeState,crdt,crUsrId,updt,upUsrId");
            parameters.Add("_tables", "doctypeconfig");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " updt desc,docTypeNum asc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_paramsPreTypeID", strPreTypeID);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString, "SP_PAGER_DOCTYPECONFIG_PRETYPEID", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 获取文档类型列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetListByDocTypeName(int pageIndex, int rowCount, out int totalCount, out int pageCount, string strDocTypeName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "docTypeId,docTypeNum,docPreTypeId,FN_DOCTYPECONFIG_DOCTYPENAME(docPreTypeId) docPreTypeName,docTypeName,docTypeDeptId,FN_DEPT_PREDEPTNAME(docTypeDeptId) docTypeDeptName,FN_DOCTYPECONFIG_CHECKCHILD(docTypeId) childCount,docTypeState,crdt,crUsrId,updt,upUsrId");
            parameters.Add("_tables", "doctypeconfig");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " updt desc,docTypeNum asc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_docTypeName", strDocTypeName);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString, "SP_PAGER_DOCTYPECONFIG_DOCTYPENAME", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 获取顶级文档类型列表
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetTopList()
        {
            return mysql.FindToListAsPage<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString,
            #region SQL
                "SELECT "+
                    "docTypeId,"+
                    "docTypeNum,"+
                    "docPreTypeId," +
                    "FN_DOCTYPECONFIG_DOCTYPENAME(docPreTypeId) docPreTypeName," +
                    "docTypeName," +
                    "docTypeDeptId," +
                    "FN_DEPT_PREDEPTNAME(docTypeDeptId) docTypeDeptName," +
                    "FN_DOCTYPECONFIG_CHECKCHILD(docTypeId) childCount," +
                    "docTypeState," +
                    "crdt," +
                    "crUsrId," +
                    "updt," +
                    "upUsrId" +
                " FROM" +
                   " doctypeconfig" +
                " WHERE" +
                    " (docPreTypeId = '' OR docPreTypeId IS NULL)" +
                " ORDER BY "+
                    " updt DESC, "+
                    " docTypeNum ASC"
            #endregion
                , null, false);
        }

        /// <summary>
        /// 获取文档类型列表
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetAllList()
        {
            return mysql.FindToListAsPage<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString,
            #region SQL
                "SELECT "+
                   " docTypeId, "+
                   " docTypeNum, "+
                   " docPreTypeId, "+
                   " FN_DOCTYPECONFIG_DOCTYPENAME(docPreTypeId) docPreTypeName, "+
                   " docTypeName, " +
                   " docTypeDeptId, " +
                   " docTypeImg, " +
                   " FN_DEPT_PREDEPTNAME(docTypeDeptId) docTypeDeptName, " +
                   " FN_DOCTYPECONFIG_CHECKCHILD(docTypeId) childCount, "+
                   " docTypeState, "+
                   " crdt, "+
                   " crUsrId, "+
                   " updt, "+
                   " upUsrId " +
               " FROM " +
                   " doctypeconfig " +
               " ORDER BY " +
                   " docTypeNum ASC"
            #endregion
                   , null, false);
        }

        /// <summary>
        /// 获取指定文档类型的子类型列表
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetChildNodes(string strDocPreTypeId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?docPreTypeId", strDocPreTypeId);

            return mysql.FindToListAsPage<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString,
            #region SQL
                "SELECT "+
                   " docTypeId, " +
                   " docTypeNum, " +
                   " docPreTypeId, " +
                   " FN_DOCTYPECONFIG_DOCTYPENAME(docPreTypeId) docPreTypeName, " +
                   " docTypeName, " +
                   " docTypeDeptId, " +
                   " FN_DEPT_PREDEPTNAME(docTypeDeptId) docTypeDeptName, " +
                   " FN_DOCTYPECONFIG_CHECKCHILD(docTypeId) childCount, " +
                   " docTypeState, " +
                   " crdt, " +
                   " crUsrId, " +
                   " updt, " +
                   " upUsrId " +
               " FROM "+
                   " doctypeconfig "+
               " WHERE "+
                   " docPreTypeId =?docPreTypeId " +
               " ORDER BY "+
                   " updt DESC, "+
                   " docTypeNum ASC"
            #endregion
                   , parameters, false);
        }
        /// <summary>
        /// 根据编号获取一个文档类型实体对象
        /// </summary>
        /// <param name="docTypeId">文档ID</param>
        /// <returns></returns>
        public static Model.DocTypeConfig GetModel(string docTypeId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append(
            #region SQL
                            "SELECT " +
                               " docTypeId, " +
                               " docTypeNum, " +
                               " docPreTypeId, " +
                               " FN_DOCTYPECONFIG_DOCTYPENAME(docPreTypeId) docPreTypeName, " +
                               " docTypeName, " +
                               " docTypeDeptId, " +
                               " FN_DEPT_PREDEPTNAME(docTypeDeptId) docTypeDeptName, " +
                               " FN_DOCTYPECONFIG_CHECKCHILD(docTypeId) childCount, " +
                               " docTypeState, " +
                               " crdt, " +
                               " crUsrId, " +
                               " updt, " +
                               " upUsrId " +
                           " FROM " +
                               " doctypeconfig " +
                           " WHERE " +
                               " docTypeId =?docTypeId"
            #endregion
                               );

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?docTypeId", docTypeId);

            return mysql.FindOne<Model.DocTypeConfig>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }
    }
}
