using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HelpCenter.BLL
{
    public class HelpDoc
    {
        /// <summary>
        /// 新增文档
        /// </summary>
        /// <param name="strDocTypeId"></param>
        /// <param name="strDocTitle"></param>
        /// <param name="strDocContent"></param>
        /// <param name="strDocFullText"></param>
        /// <param name="strDocNum"></param>
        /// <param name="strDocDeptId"></param>
        /// <param name="strCrUsrId"></param>
        /// <param name="strUpUsrId"></param>
        /// <param name="strDocState"></param>
        /// <returns></returns>
        public static bool Add(string strDocTypeId, string strDocTitle, string strDocContent, string strDocFullText,
            int? strDocNum, string strDocDeptId, string strCrUsrId, int? strDocState, List<string> lstShareDeptIdList, string strDocAttachment, bool isDefault,
            List<string> viewRoleList, List<string> editRoleList)
        {
            string strDocId = Guid.NewGuid().ToString();
            Model.HelpDoc helpDocModel = new Model.HelpDoc()
            {
                docContent = strDocContent,
                docCount = 0,
                docDeptId = strDocDeptId,
                docFullText = strDocFullText,
                docId = strDocId,
                docNum = strDocNum,
                docState = 1,
                docTitle = strDocTitle,
                docTypeId = strDocTypeId,
                crdt = DateTime.Now,
                crUsrId = strCrUsrId,
                updt = DateTime.Now,
                docAttachment = strDocAttachment,
                upUsrId = strCrUsrId
            };

            return Repository.HelpDoc.Add(helpDocModel, lstShareDeptIdList, isDefault, viewRoleList, editRoleList);
        }

        /// <summary>
        /// 获取所有文档清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.HelpDoc> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount, ClaimsPrincipal user, string strKeyword)
        {
            IList<Model.HelpDoc> helpDocList =
               user.Identity.IsAuthenticated ?
               Repository.HelpDoc.GetList(pageIndex, rowCount, out totalCount, out pageCount, user.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value, strKeyword) :
               Repository.HelpDoc.VisitorSearch(pageIndex, rowCount, strKeyword, out totalCount, out pageCount);

            if (user.Identity.IsAuthenticated)
            {
                string strUsrId = user.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value;
                var roleList = Repository.UsrRole.GetModel(strUsrId).ToList();
                if (helpDocList != null && helpDocList.Count > 0)
                {
                    foreach (var doc in helpDocList)
                    {
                        foreach (var role in roleList)
                        {
                            if (Repository.DocRoleCtrl.Get(doc.docId, role.roleId).Count > 0)
                            {
                                doc.editRole = true;
                                break;
                            }
                        }
                    }
                }
            }
            return helpDocList;
        }


        /// <summary>
        /// 所有文档清单
        /// </summary>
        /// <returns></returns>
        public static IList<Model.HelpDoc> GetAllList()
        {
            IList<Model.HelpDoc> helpDocList = Repository.HelpDoc.GetAllList();
            return helpDocList;
        }

        /// <summary>
        /// 根据部门ID获取部门下所有文档清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.HelpDoc> GetListByDeptId(int pageIndex, int rowCount, out int totalCount, out int pageCount, string strDeptId)
        {
            IList<Model.HelpDoc> helpDocList = Repository.HelpDoc.GetListByDeptId(pageIndex, rowCount, out totalCount, out pageCount, strDeptId);
            return helpDocList;
        }

        /// <summary>
        /// 根据文档类型ID获取所有文档清单（游客分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.HelpDoc> GetVisitorListByDocTypeId(int pageIndex, int rowCount, string strDocTypeId, out int totalCount, out int pageCount)
        {
            IList<Model.HelpDoc> helpDocList = Repository.HelpDoc.GetVisitorListByDocTypeId(pageIndex, rowCount, strDocTypeId, out totalCount, out pageCount);
            return helpDocList;
        }

        /// <summary>
        /// 根据文档类型ID获取所有文档清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.HelpDoc> GetListByDocTypeId(int pageIndex, int rowCount, string strDocTypeId, bool isRecursion, out int totalCount, out int pageCount, ClaimsPrincipal user)
        {
            IList<Model.HelpDoc> helpDocListTemp =
                user.Identity.IsAuthenticated
                ? Repository.HelpDoc.GetListByDocTypeId(1, int.MaxValue, strDocTypeId, out totalCount, out pageCount, isRecursion)
                : Repository.HelpDoc.GetVisitorListByDocTypeId(1, int.MaxValue, strDocTypeId, out totalCount, out pageCount, isRecursion);

            var helpDocList = new List<Model.HelpDoc>();
            //2018.12.17 17:52:00 针对文档的编辑权限加的代码
            if (user.Identity.IsAuthenticated)
            {
                string strUsrId = user.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value;
                var roleList = Repository.UsrRole.GetModel(strUsrId).ToList();
                if (helpDocListTemp != null && helpDocListTemp.Count > 0)
                {
                    foreach (var doc in helpDocListTemp)
                    {
                        var displayflag = false;
                        foreach (var role in roleList)
                        {
                            if (Repository.DocRoleCtrl.Get(doc.docId, role.roleId).Count > 0)
                            {
                                doc.editRole = true;
                                break;
                            }
                        }
                        //用户无编辑权限时判断是否有可读的权限，如果没有，则过滤改文档
                        if (!doc.editRole)
                        {
                            doc.viewRoleList = DocUsrRole.GetListByDocId(doc.docId);
                            foreach (var view in doc.viewRoleList)
                            {
                                foreach (var userrole in roleList)
                                {
                                    if (view == userrole.roleId)
                                    {
                                        displayflag = true;
                                        break;
                                    }
                                }
                                if (displayflag)
                                {
                                    break;
                                }
                            }
                        }
                        if (displayflag || doc.editRole)
                        {
                            helpDocList.Add(doc);

                        }
                    }
                }
                totalCount = helpDocList.Count;
                pageCount = totalCount / rowCount;
                helpDocList = helpDocList.Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();
            }
            else
            {
                helpDocList = helpDocListTemp.ToList();
                helpDocList = helpDocList.Skip((pageIndex - 1) * rowCount).Take(rowCount).ToList();
            }

            return helpDocList;
        }

        /// <summary>
        /// 获取所有文档清单（分页）
        /// </summary>
        /// <returns></returns>
        //public static IList<Model.HelpDoc> Search(int pageIndex,int rowCount,string strKeyWord, out int totalCount, out int pageCount, ClaimsPrincipal user)
        //{

        //    IList<Model.HelpDoc> helpDocList =
        //        user.Identity.IsAuthenticated ? Repository.HelpDoc.Search(pageIndex,rowCount, strKeyWord,out totalCount,out pageCount, user.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value)
        //        : Repository.HelpDoc.VisitorSearch(pageIndex, rowCount, strKeyWord, out totalCount, out pageCount);
        //    return helpDocList;
        //}

        public static bool Update(string strDocId, string strDocTypeId, string strDocTitle, string strDocContent, string strDocFullText,
            int? strDocNum, string strDocDeptId, string strUpUsrId, int? strDocState, List<string> strShareDeptIdList, string strDocAttachment, bool isDefault,
            List<string> viewRoleList, List<string> editRoleList)
        {
            Model.HelpDoc helpDocModel = new Model.HelpDoc()
            {
                docContent = strDocContent,
                docDeptId = strDocDeptId,
                docFullText = strDocFullText,
                docId = strDocId,
                docNum = strDocNum,
                docState = 1,
                docTitle = strDocTitle,
                docTypeId = strDocTypeId,
                updt = DateTime.Now,
                docAttachment = strDocAttachment,
                upUsrId = strUpUsrId
            };

            return Repository.HelpDoc.Update(helpDocModel, isDefault, viewRoleList, editRoleList);
        }

        public static bool Delete(string strDocId)
        {
            return Repository.HelpDoc.Delete(strDocId);
        }

        /// <summary>
        /// 获取指定文档信息详情
        /// </summary>
        /// <param name="strDocId">文档编号</param>
        /// <returns></returns>
        public static Model.HelpDoc GetModel(string strDocId)
        {
            Model.HelpDoc doc = Repository.HelpDoc.GetModel(strDocId);
            if (doc != null)
            {
                doc.IsDefaultRole = DocUsrRole.QueryExist(strDocId, "4b5df8d4-e422-48b4-8138-7fd6806dd214");
                doc.docFullText = null;
                doc.viewRoleList = DocUsrRole.GetListByDocId(strDocId);
                doc.editRoleList = DocRoleCtrl.GetEditRoleList(strDocId);
            }
            return doc;
        }

    }
}
