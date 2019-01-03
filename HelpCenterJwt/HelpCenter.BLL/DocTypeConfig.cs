using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HelpCenter.BLL
{
    public class DocTypeConfig
    {
        /// <summary>
        /// 新增文档类型
        /// </summary>
        /// <param name="strDocTypeName"></param>
        /// <param name="strDocTypeState"></param>
        /// <param name="strDocPreTypeId"></param>
        /// <param name="strDocTypeDeptId"></param>
        /// <param name="strDocTypeNum"></param>
        /// <returns></returns>
        public static bool Add(string strDocTypeName, int strDocTypeState, string strDocPreTypeId, string strDocTypeDeptId, int? strDocTypeNum,string strCrUsrId)
        {
            Model.DocTypeConfig roleModel = new Model.DocTypeConfig()
            {
                docTypeId = Guid.NewGuid().ToString(),
                docTypeName = strDocTypeName,
                docTypeState = strDocTypeState,
                docPreTypeId = strDocPreTypeId,
                docTypeDeptId = strDocTypeDeptId,
                docTypeNum = strDocTypeNum,
                crdt = DateTime.Now,
                crUsrId = strCrUsrId,
                updt = DateTime.Now,
                upUsrId = strCrUsrId
            };

            return Repository.DocTypeConfig.Add(roleModel);
        }

        /// <summary>
        /// 获取所有文档类型清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetList(int pageIndex,int rowCount, out int totalCount, out int pageCount,string strDeptId)
        {
            IList<Model.DocTypeConfig> roleList = Repository.DocTypeConfig.GetList(pageIndex,rowCount,out totalCount,out pageCount, strDeptId);
            foreach (var item in roleList)
            {
                item.child = item.childCount == 0 ? null : (new string[0]);
            }
            return roleList;
        }

        /// <summary>
        /// 获取所有文档类型清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetListByDeptId(int pageIndex,int rowCount, out int totalCount, out int pageCount,string strDeptId)
        {
            IList<Model.DocTypeConfig> roleList = Repository.DocTypeConfig.GetListByDeptId(pageIndex,rowCount,out totalCount,out pageCount, strDeptId);
            foreach (var item in roleList)
            {
                item.child = item.childCount == 0 ? null : (new string[0]);
            }
            return roleList;
        }

        /// <summary>
        /// 获取所有文档类型清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetListByDocTypeId(int pageIndex,int rowCount, out int totalCount, out int pageCount,string strPreTypeID)
        {
            IList<Model.DocTypeConfig> roleList = Repository.DocTypeConfig.GetListByDocTypeId(pageIndex,rowCount,out totalCount,out pageCount, strPreTypeID);
            foreach (var item in roleList)
            {
                item.child = item.childCount == 0 ? null : (new string[0]);
            }
            return roleList;
        }

        /// <summary>
        /// 获取所有文档类型清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetListByDocTypeName(int pageIndex,int rowCount, out int totalCount, out int pageCount,string strDocTypeName)
        {
            IList<Model.DocTypeConfig> roleList = Repository.DocTypeConfig.GetListByDocTypeName(pageIndex,rowCount,out totalCount,out pageCount, strDocTypeName);
            foreach (var item in roleList)
            {
                item.child = item.childCount == 0 ? null : (new string[0]);
            }
            return roleList;
        }

        /// <summary>
        /// 获取所有顶级文档类型清单
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetTopList()
        {
            var docTypeConfigList = Repository.DocTypeConfig.GetTopList();
            if (docTypeConfigList != null)
            {
                foreach (var item in docTypeConfigList)
                {
                    item.child = item.childCount == 0 ? null : (new string[0]);
                }
            }
            return docTypeConfigList;
        }

        /// <summary>
        /// 获取首页文档类型清单，并提供每个类型部分文档列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.DocTypeConfig> GetHomePageList(bool isLogin)
        {
            var docTypeConfigList = Repository.DocTypeConfig.GetAllList();
            List<Model.DocTypeConfig> docTypeConfigs = new List<Model.DocTypeConfig>();
            if (docTypeConfigList != null)
            {
                var docTypeConfigListResult =
                    (from rst in docTypeConfigList where string.IsNullOrEmpty(rst.docPreTypeId) select rst).ToList();

                foreach (var item in docTypeConfigListResult)
                {
                    item.child = item.childCount == 0 ? null : (new string[0]);
                    //是否显示一级分类的文档
                    //item.DocList= isLogin
                    //    ? 
                    //    Repository.HelpDoc.GetListByDocTypeId(1, 8, item.docTypeId, out int _totalCount, out int _pageCount)
                    //    :
                    //    Repository.HelpDoc.GetVisitorListByDocTypeId(1, 8, item.docTypeId, out int _vTotalCount, out int _vPageCount)
                    //    ;
                    docTypeConfigs.Add(item);
                    //当前层级下标定义，默认顶层1
                    if (item.childCount > 0)
                    {
                        var lst = from rst in docTypeConfigList where rst.docPreTypeId == item.docTypeId select rst;
                        if (lst != null)
                        {
                            if (item.subs == null)
                            {
                                item.subs = new List<Model.DocTypeConfig>();
                            }
                            foreach (var _item in lst)
                            {
                                //添加二级分类文档
                                _item.DocList = isLogin
                                    ?
                                    Repository.HelpDoc.GetListByDocTypeId(1, 8, _item.docTypeId, out int _totalCount, out int _pageCount).ToList()
                                    :
                                    Repository.HelpDoc.GetVisitorListByDocTypeId(1, 8, _item.docTypeId, out int _vTotalCount, out int _vPageCount).ToList()
                                    ;
                                item.subs.Add(_item);
                                _item.child = _item.childCount == 0 ? null : (new string[0]);
                                if (_item.childCount > 0)
                                {
                                    lst = from rst in docTypeConfigList where rst.docPreTypeId == _item.docTypeId select rst;
                                    //进行二级处理
                                    AddDocTypeConfigDocList(_item, _item, lst, docTypeConfigList, isLogin);
                                }
                            }
                        }
                    }
                }
            }
            return docTypeConfigs;
        }

        /// <summary>
        /// 递归加载文章
        /// </summary>
        /// <param name="docTypeConfigFix">固定的二级</param>
        /// <param name="docTypeConfig">当前文档类型</param>
        /// <param name="docTypeConfigLst">当前文档子类型</param>
        /// <param name="allList">所有类型</param>
        /// <param name="isLogin">是否登录</param>
        private static void AddDocTypeConfigDocList(Model.DocTypeConfig docTypeConfigFix, Model.DocTypeConfig docTypeConfig, IEnumerable<Model.DocTypeConfig> docTypeConfigLst, IList<Model.DocTypeConfig> allList, bool isLogin)
        {
            foreach (var item in docTypeConfigLst)
            {
                var docList = isLogin
                    ?
                    Repository.HelpDoc.GetListByDocTypeId(1, 8, item.docTypeId, out int _totalCount, out int _pageCount)
                    :
                    Repository.HelpDoc.GetVisitorListByDocTypeId(1, 8, item.docTypeId, out int _vTotalCount, out int _vPageCount)
                    ;

                if (docTypeConfigFix.DocList == null)
                {
                    docTypeConfigFix.DocList = new List<Model.HelpDoc>();
                }
                //处理二级文档列表
                foreach (var doc in docList)
                {
                    if (docTypeConfigFix.DocList.Count == 8)
                    {
                        break;
                    }
                    docTypeConfigFix.DocList.Add(doc);
                }

                var lst = from rst in allList where rst.docPreTypeId == item.docTypeId select rst;
                if (lst != null)
                {
                    AddDocTypeConfigDocList(docTypeConfigFix, item, lst, allList, isLogin);
                }
            }
        }

        /// <summary>
        /// 获取所有文档类型清单
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetAllList()
        {
            var docTypeConfigList = Repository.DocTypeConfig.GetAllList();
            foreach (var item in docTypeConfigList)
            {
                item.child = item.childCount == 0 ? null : (new string[0]);
            }
            return docTypeConfigList;
        }

        private static void AddDocTypeConfig(Model.DocTypeConfig docTypeConfig, IEnumerable<Model.DocTypeConfig> docTypeConfigLst, IList<Model.DocTypeConfig> allList)
        {
            docTypeConfig.subs = new List<Model.DocTypeConfig>();
            foreach (var item in docTypeConfigLst)
            {
                docTypeConfig.subs.Add(item);

                var lst = from rst in allList where rst.docPreTypeId == item.docTypeId select rst;
                if (lst != null)
                {
                    AddDocTypeConfig(item, lst, allList);
                }
            }
            if (docTypeConfig.subs.Count == 0)
            {
                docTypeConfig.subs = null;
            }
        }

        /// <summary>
        /// 获取所有文档类型清单
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetAllListTree()
        {
            var docTypeConfigList = Repository.DocTypeConfig.GetAllList();
            List<Model.DocTypeConfig> docTypeConfigs = new List<Model.DocTypeConfig>();
            if (docTypeConfigList != null)
            {
                var docTypeConfigListResult = from rst in docTypeConfigList where string.IsNullOrEmpty(rst.docPreTypeId) select rst;

                foreach (var item in docTypeConfigListResult)
                {
                    item.child = item.childCount == 0 ? null : (new string[0]);
                    docTypeConfigs.Add(item);

                    if (item.childCount > 0)
                    {
                        var lst = from rst in docTypeConfigList where rst.docPreTypeId == item.docTypeId select rst;
                        if (lst != null)
                        {
                            AddDocTypeConfig(item, lst, docTypeConfigList);
                        }
                    }
                }
            }
            return docTypeConfigs;
        }

        /// <summary>
        /// 获取指定文档类型信息详情
        /// </summary>
        /// <param name="docTypeId">文档类型编号</param>
        /// <returns></returns>
        public static IList<Model.DocTypeConfig> GetChildNodes(string docTypeId)
        {
            var docType = Repository.DocTypeConfig.GetChildNodes(docTypeId);
            foreach (var item in docType)
            {
                item.child = item.childCount == 0 ? null : (new string[0]);
            }
            return docType;
        }

        public static bool Update(string strDocTypeId, string strDocTypeName, int strDocTypeState, string strDocPreTypeId, string strDocTypeDeptId, int? strDocTypeNum,string strUpUsrId)
        {
            Model.DocTypeConfig roleModel = new Model.DocTypeConfig()
            {
                docTypeId = strDocTypeId,
                docTypeName = strDocTypeName,
                docTypeState = strDocTypeState,
                docPreTypeId = strDocPreTypeId,
                docTypeDeptId = strDocTypeDeptId,
                docTypeNum = strDocTypeNum,
                updt = DateTime.Now,
                upUsrId = strUpUsrId
            };
            return Repository.DocTypeConfig.Update(roleModel);
        }

        public static bool Delete(string strDocTypeId,out string strTips)
        {
            if (GetChildNodes(strDocTypeId).Count > 0)
            {
                strTips = ActionService.ResponseMessageTips.MSG_DOCTYPE_HAS_CHILD;
                return false;
            }
            else
            {
                return Repository.DocTypeConfig.Delete(strDocTypeId, out strTips);
            }
        }

        /// <summary>
        /// 获取指定文档类型信息详情
        /// </summary>
        /// <param name="strDocTypeId">文档类型ID</param>
        /// <returns></returns>
        public static Model.DocTypeConfig GetModel(string strDocTypeId)
        {
            var role = Repository.DocTypeConfig.GetModel(strDocTypeId);
            role.child = role.childCount == 0 ? null : (new string[0]);
            return role;
        }

    }
}
