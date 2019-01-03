using Dapper;
using HelpCenter.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Drawing;
using HelpCenter.Common.DBUtility;
using System.Transactions;
using System.Linq;

namespace HelpCenter.Repository
{
    public class HelpDoc
    {

        private static DapperMySQLHelper mysql = new DapperMySQLHelper();

        public static bool Add(Model.HelpDoc helpDocModel, List<string> lstShareDeptIdList, bool isDefault,
            List<string> viewRoleList, List<string> editRoleList)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int rows = 0;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into helpdoc(");
                strSql.Append("docId,crdt,updt,docTypeId,docTitle,docContent,docFullText,docNum,docDeptId,docCount,crUsrId,upUsrId,docState,docAttachment)");
                strSql.Append(" values (");
                strSql.Append("@docId,@crdt,@updt,@docTypeId,@docTitle,@docContent,@docFullText,@docNum,@docDeptId,@docCount,@crUsrId,@upUsrId,@docState,@docAttachment)");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("?docId", helpDocModel.docId);
                parameters.Add("?crdt", helpDocModel.crdt);
                parameters.Add("?updt", helpDocModel.updt);
                parameters.Add("?docTypeId", helpDocModel.docTypeId);
                parameters.Add("?docTitle", helpDocModel.docTitle);
                parameters.Add("?docContent", helpDocModel.docContent);
                parameters.Add("?docFullText", helpDocModel.docFullText);
                parameters.Add("?docNum", helpDocModel.docNum);
                parameters.Add("?docDeptId", helpDocModel.docDeptId);
                parameters.Add("?docCount", helpDocModel.docCount);
                parameters.Add("?crUsrId", helpDocModel.crUsrId);
                parameters.Add("?upUsrId", helpDocModel.upUsrId);
                parameters.Add("?docState", helpDocModel.docState);
                parameters.Add("?docAttachment", helpDocModel.docAttachment);

                Model.DocDeptRole docDeptRole = new Model.DocDeptRole()
                {
                    Crdt = DateTime.Now,
                    CrUsrId = helpDocModel.crUsrId,
                    DocDeptRoleId = Guid.NewGuid().ToString(),
                    DocDeptState = 1,
                    Updt = DateTime.Now,
                    UpUsrId = helpDocModel.crUsrId,
                    DocId = helpDocModel.docId
                };

                DocDeptRole.Set(docDeptRole, lstShareDeptIdList);


                //有编辑权限即有查看权限
                foreach (var item in editRoleList)
                {
                    if (!viewRoleList.Contains(item))
                    {
                        viewRoleList.Add(item);
                    }
                }

                SaveDocRole(viewRoleList, helpDocModel.crUsrId, helpDocModel.docId, isDefault);

                SaveDocRoleCtrl(editRoleList, helpDocModel.crUsrId, helpDocModel.docId);

                rows = mysql.ExcuteNonQuery<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
                ts.Complete();
                return rows > 0;
            }
        }

        #region 处理文档对角色的可见权限
        private static void SaveDocRole(List<string> viewRoleList, string strCrUsrId, string strDocId, bool isDefault)
        {
            List<Model.DocUsrRole> viewDocRoleList = new List<Model.DocUsrRole>();
            //增加管理员权限
            Model.DocUsrRole docAdminRole = new Model.DocUsrRole()
            {
                crdt = DateTime.Now,
                crUsrId = strCrUsrId,
                docId = strDocId,
                docRoleId = Guid.NewGuid().ToString(),
                docRoleState = 1,
                roleId = "4be2ec6a-5ca6-4a73-8975-892189b72137",
                updt = DateTime.Now,
                upUsrId = strCrUsrId
            };
            //DocUsrRole.Add(docAdminRole);
            viewDocRoleList.Add(docAdminRole);

            //游客是否可见权限
            Model.DocUsrRole docUsrRole = new Model.DocUsrRole()
            {
                crdt = DateTime.Now,
                crUsrId = strCrUsrId,
                docId = strDocId,
                docRoleId = Guid.NewGuid().ToString(),
                docRoleState = 1,
                roleId = "4b5df8d4-e422-48b4-8138-7fd6806dd214",
                updt = DateTime.Now,
                upUsrId = strCrUsrId
            };
            //为文档增加默认游客可见权限
            if (isDefault)
            {
                //DocUsrRole.Add(docUsrRole);
                viewDocRoleList.Add(docUsrRole);
            }
            else
            {
                DocUsrRole.Delete(docUsrRole);
            }

            if (viewRoleList != null && viewRoleList.Count > 0)
            {
                //List<Model.DocUsrRole> viewDocRoleList = new List<Model.DocUsrRole>();
                foreach (var item in viewRoleList)
                {
                    Model.DocUsrRole _docUsrRole = new Model.DocUsrRole()
                    {
                        crdt = DateTime.Now,
                        crUsrId = strCrUsrId,
                        docId = strDocId,
                        docRoleId = Guid.NewGuid().ToString(),
                        docRoleState = 1,
                        roleId = item,
                        updt = DateTime.Now,
                        upUsrId = strCrUsrId
                    };
                    viewDocRoleList.Add(_docUsrRole);
                }
            }
            if (viewDocRoleList.Count > 0)
            {
                DocUsrRole.Add(viewDocRoleList);
            }
        }
        #endregion

        #region 处理文档对角色的控制权限
        private static void SaveDocRoleCtrl(List<string> editRoleList, string strCrUsrId, string strDocId)
        {
            List<Model.DocRoleCtrl> docRoleCtrls = new List<Model.DocRoleCtrl>();
            Model.DocRoleCtrl docAdminRoleCtrl = new Model.DocRoleCtrl
            {
                Crdt = DateTime.Now,
                Updt = DateTime.Now,
                CrUsrId = strCrUsrId,
                UpUsrId = strCrUsrId,
                CtrlType = 1,
                DocId = strDocId,
                RoleId = "4be2ec6a-5ca6-4a73-8975-892189b72137"
            };
            docRoleCtrls.Add(docAdminRoleCtrl);
            if (editRoleList != null && editRoleList.Count > 0)
            {
                foreach (var item in editRoleList)
                {
                    Model.DocRoleCtrl docRoleCtrl = new Model.DocRoleCtrl
                    {
                        Crdt = DateTime.Now,
                        Updt = DateTime.Now,
                        CrUsrId = strCrUsrId,
                        UpUsrId = strCrUsrId,
                        CtrlType = 1,
                        DocId = strDocId,
                        RoleId = item
                    };
                    docRoleCtrls.Add(docRoleCtrl);
                }
            }
            if (docRoleCtrls.Count > 0)
            {
                DocRoleCtrl.Add(docRoleCtrls);
            }
        }
        #endregion

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Model.HelpDoc helpDocModel, bool isDefault,
            List<string> viewRoleList, List<string> editRoleList)
        {

            var roleList = UsrRole.GetModel(helpDocModel.upUsrId).ToList();
            bool checkRole = false;
            //检查文档修改人是否有权限更新
            foreach (var item in roleList)
            {
                if (DocRoleCtrl.Get(helpDocModel.docId, item.roleId).Count > 0)
                {
                    checkRole = true;
                    break;
                }
            }
            if (!checkRole)
            {
                return false;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update helpdoc set ");
                strSql.Append("docTypeId=?docTypeId,");
                strSql.Append("docTitle=?docTitle,");
                strSql.Append("docContent=?docContent,");
                strSql.Append("docFullText=?docFullText,");
                strSql.Append("docNum=?docNum,");
                strSql.Append("docDeptId=?docDeptId,");
                strSql.Append("docState=?docState,");
                strSql.Append("updt=?updt,");
                strSql.Append("updt=?updt,");
                strSql.Append("docAttachment=?docAttachment");
                strSql.Append(" where docId=?docId");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("?docTypeId", helpDocModel.docTypeId);
                parameters.Add("?docTitle", helpDocModel.docTitle);
                parameters.Add("?docContent", helpDocModel.docContent);
                parameters.Add("?docFullText", helpDocModel.docFullText);
                parameters.Add("?docNum", helpDocModel.docNum);
                parameters.Add("?docDeptId", helpDocModel.docDeptId);
                parameters.Add("?docState", helpDocModel.docState);
                parameters.Add("?updt", helpDocModel.updt);
                parameters.Add("?upUsrId", helpDocModel.upUsrId);
                parameters.Add("?docId", helpDocModel.docId);
                parameters.Add("?docAttachment", helpDocModel.docAttachment);

                //有编辑权限即有查看权限
                foreach (var item in editRoleList)
                {
                    if (!viewRoleList.Contains(item))
                    {
                        viewRoleList.Add(item);
                    }
                }

                SaveDocRole(viewRoleList, helpDocModel.crUsrId, helpDocModel.docId, isDefault);

                SaveDocRoleCtrl(editRoleList, helpDocModel.crUsrId, helpDocModel.docId);

                int rows = mysql.ExcuteNonQuery<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
                ts.Complete();
                return rows > 0;
            }
        }

        public static bool Delete(string strHelpDocId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from helpdoc ");
            strSql.Append(" where docId=?docId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?docId", strHelpDocId);

            int rows = mysql.ExcuteNonQuery<Model.Usr>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return rows > 0;
        }

        public static IList<Model.HelpDoc> GetAllList()
        {
            var list = mysql.FindToList<Model.HelpDoc>(DapperMySQLHelper.ConnectionString,
                "select docId,crdt,updt,docTypeId,FN_DOCTYPECONFIG_DOCTYPENAME(docTypeId) docTypeName,docTitle,docContent,docFullText,docNum,docDeptId,FN_DEPT_PREDEPTNAME(docDeptId) docDeptName,docCount,crUsrId,upUsrId,docState,docAttachment from helpdoc"
                , null, false);
            return list;
        }

        /// <summary>
        /// 获取文档列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.HelpDoc> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount, string strUserId, string strKeyword)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_usrId", strUserId);
            parameters.Add("_paramsKeyWord", strKeyword == null ? "" : strKeyword);
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, "SP_PAGER_HELPDOC_LOGIN", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 获取文档列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.HelpDoc> GetListByDeptId(int pageIndex, int rowCount, out int totalCount, out int pageCount, string strDeptId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", " docId,crdt,updt,docTypeId,FN_DOCTYPECONFIG_DOCTYPENAME(docTypeId) docTypeName,docTitle,docContent,docFullText,docNum,docDeptId,docCount,crUsrId,upUsrId,docState,docAttachment ");
            parameters.Add("_tables", " helpdoc ");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " docCount desc,updt desc ");
            parameters.Add("_paramsDeptID", strDeptId);
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, "SP_PAGER_HELPDOC_DEPTID", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 获取游客文档列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static IList<Model.HelpDoc> GetVisitorList(int pageIndex, int rowCount, out int totalCount, out int pageCount)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "*");
            parameters.Add("_tables", "v_doc_visitor");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", "");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, "SP_PAGER", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }


        /// <summary>
        /// 获取文档列表，全文检索
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="strKeyword"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        //public static IList<Model.HelpDoc> Search(int pageIndex, int rowCount,string strKeyword, out int totalCount, out int pageCount)
        //{

        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("_fields", "docId,crdt,updt,docTypeId,docTitle,docContent,docNum,docDeptId,docCount,crUsrId,upUsrId,docState");
        //    parameters.Add("_tables", "helpdoc");
        //    parameters.Add("_where", "  docState!=0 and MATCH (docTitle, docFullText) ");
        //    parameters.Add("_orderby", " docCount desc,updt desc ");
        //    parameters.Add("_pageindex", pageIndex);
        //    parameters.Add("_pageSize", rowCount);
        //    parameters.Add("_paramsKeyWord", strKeyword);
        //    parameters.Add("_sumfields", string.Empty);
        //    parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //    parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //    parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

        //    var list = mysql.FindToListByPage<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, "SP_PAGER_HELPDOC_KeyWord", parameters, true);
        //    totalCount = parameters.Get<int>("_totalcount");
        //    pageCount = parameters.Get<int>("_pagecount");
        //    return list;
        //}
        //public static IList<Model.HelpDoc> Search(int pageIndex, int rowCount,string strKeyword, out int totalCount, out int pageCount,string strUsrId)
        //{

        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("_fields", " * ");
        //    parameters.Add("_tables", "helpdoc");
        //    parameters.Add("_where", "  1=1 and MATCH (docTitle, docFullText) ");
        //    parameters.Add("_orderby", " docCount desc,updt desc ");
        //    parameters.Add("_pageindex", pageIndex);
        //    parameters.Add("_pageSize", rowCount);
        //    parameters.Add("_usrId", strUsrId);
        //    parameters.Add("_paramsKeyWord", strKeyword);
        //    parameters.Add("_sumfields", string.Empty);
        //    parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //    parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //    parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

        //    var list = mysql.FindToListByPage<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, "SP_PAGER_HELPDOC_KEYWORD_LOGIN", parameters, true);
        //    totalCount = parameters.Get<int>("_totalcount");
        //    pageCount = parameters.Get<int>("_pagecount");
        //    return list;
        //}

        /// <summary>
        /// 获取文档列表，全文检索
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="rowCount"></param>
        /// <param name="strKeyword"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        public static IList<Model.HelpDoc> VisitorSearch(int pageIndex, int rowCount, string strKeyword, out int totalCount, out int pageCount)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "*  ");
            parameters.Add("_tables", "v_doc_visitor");
            parameters.Add("_where", " ");
            parameters.Add("_orderby", string.IsNullOrEmpty(strKeyword) ? " docCount desc " : " score desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_paramsKeyWord", string.IsNullOrEmpty(strKeyword) ? "" : strKeyword);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, "SP_PAGER_HELPDOC_KEYWORD_LOGOUT", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        /// <summary>
        /// 根据编号获取一个文档实体对象
        /// </summary>
        /// <param name="helpDocId">文档编号</param>
        /// <returns></returns>
        public static Model.HelpDoc GetModel(string helpDocId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update helpdoc set docCount=docCount+1 where docId=?docId");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?docId", helpDocId);

            mysql.ExcuteNonQuery<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);

            strSql.Clear();
            strSql.Append("select docId,crdt,updt,docTypeId,docTitle,docContent,docFullText,docNum,docDeptId,docCount,crUsrId,upUsrId,docState,docAttachment,FN_USR_USRNAME(crUsrId) crUsrName from helpdoc ");
            strSql.Append(" where docId=?docId ");

            return mysql.FindOne<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }

        public static IList<Model.HelpDoc> GetVisitorListByDocTypeId(int pageIndex, int rowCount, string strDocTypeID, out int totalCount, out int pageCount, bool isRecursion = false)
        {
            if (isRecursion)
            {
                strDocTypeID = mysql.ExecuteScalar<StringBuilder>(DapperMySQLHelper.ConnectionString, "select FN_queryChildrenAreaInfo('" + strDocTypeID + "')", null, false).ToString();
            }
            else
            {
                strDocTypeID = "'" + strDocTypeID + "'";
            }
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "*");
            parameters.Add("_tables", "v_doc_visitor");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " docCount desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_paramsDocTypeID", strDocTypeID);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, "SP_PAGER_HELPDOC_DOCTYPEID", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }

        public static IList<Model.HelpDoc> GetListByDocTypeId(int pageIndex, int rowCount, string strDocTypeID, out int totalCount, out int pageCount, bool isRecursion = false)
        {
            if (isRecursion)
            {
                strDocTypeID = mysql.ExecuteScalar<StringBuilder>(DapperMySQLHelper.ConnectionString, "select FN_queryChildrenAreaInfo('" + strDocTypeID + "')", null, false).ToString();
            }
            else
            {
                strDocTypeID = "'" + strDocTypeID + "'";
            }
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("_fields", "* ,FN_DEPT_PREDEPTNAME(docDeptId) docDeptName,FN_DOCTYPECONFIG_DOCTYPENAME(docTypeId) docTypeName");
            parameters.Add("_tables", "helpdoc");
            parameters.Add("_where", " 1=1 ");
            parameters.Add("_orderby", " docCount desc ");
            parameters.Add("_pageindex", pageIndex);
            parameters.Add("_pageSize", rowCount);
            parameters.Add("_paramsDocTypeID", strDocTypeID);
            parameters.Add("_sumfields", string.Empty);
            parameters.Add("_totalcount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_pagecount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("_sumResult", dbType: DbType.String, direction: ParameterDirection.Output);

            var list = mysql.FindToListByPage<Model.HelpDoc>(DapperMySQLHelper.ConnectionString, "SP_PAGER_HELPDOC_DOCTYPEID", parameters, true);
            totalCount = parameters.Get<int>("_totalcount");
            pageCount = parameters.Get<int>("_pagecount");
            return list;
        }
    }
}