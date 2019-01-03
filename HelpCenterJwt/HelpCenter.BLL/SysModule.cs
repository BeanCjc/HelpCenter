using System;
using System.Collections.Generic;
using System.Text;
using HelpCenter;
using Newtonsoft.Json;
using System.Linq;

namespace HelpCenter.BLL
{
    public class SysModule
    {
        public static bool Add(string strModuleName,string strModuleCode, string strModuleImgPath, string strModuleUrlPath, 
            string strPreModuleId, string strModuleRemark, int? strModuleState, string crUsrId)
        {
            
            Model.SysModule sysModule = new Model.SysModule()
            {
                moduleId = Guid.NewGuid().ToString(),
                moduleName = strModuleName,
                ModuleCode = strModuleCode,
                ModuleImgPath = strModuleImgPath,
                ModuleUrlPath = strModuleUrlPath,
                preModuleId = strPreModuleId,
                moduleRemark = strModuleRemark,
                moduleState = strModuleState,
                crdt = DateTime.Now,
                crUsrId = crUsrId,
                updt = DateTime.Now,
                upUsrId = crUsrId
            };

            return Repository.SysModule.Add(sysModule);
        }

        public static bool Update(string strModuleId, string strModuleName, string strModuleImgPath, string strModuleUrlPath,
            string strPreModuleId, string strModuleRemark, int? strModuleState, string strUpUsrId)
        {

            Model.SysModule sysModule = new Model.SysModule()
            {
                moduleId = strModuleId,
                moduleName = strModuleName,
                ModuleImgPath = strModuleImgPath,
                ModuleUrlPath = strModuleUrlPath,
                preModuleId = strPreModuleId,
                moduleRemark = strModuleRemark,
                moduleState = strModuleState,
                updt = DateTime.Now,
                upUsrId = strUpUsrId
            };

            return Repository.SysModule.Update(sysModule);
        }

        public static bool Delete(string strModuleId)
        {
            return Repository.SysModule.Delete(strModuleId);
        }

        public static Model.SysModule GetDetail(string strModuleId)
        {
            var sysModule = Repository.SysModule.GetModel(strModuleId);
            return sysModule;
        }

        /// <summary>
        /// 获取所有顶级模块清单
        /// </summary>
        /// <returns></returns>
        public static IList<Model.SysModule> GetTopList()
        {
            IList<Model.SysModule> sysModuleList = Repository.SysModule.GetTopList();
            return sysModuleList;
        }

        /// <summary>
        /// 获取所有顶级模块清单
        /// </summary>
        /// <returns></returns>
        public static List<Model.SysModule> GetList()
        {
            List<Model.SysModule> sysModuleList = Repository.SysModule.GetList().ToList();

            List<Model.SysModule> sysModules = new List<Model.SysModule>();

            if (sysModuleList != null && sysModuleList.Count > 0)
            {
                var sysModuleListResult = (from rst in sysModuleList where string.IsNullOrEmpty(rst.preModuleId) select rst).ToList();

                foreach (var item in sysModuleListResult)
                {
                    sysModules.Add(item);

                    var subs = (from s in sysModuleList where s.preModuleId == item.moduleId select s).ToList();

                    if (subs != null && subs.Count > 0)
                    {
                        AddSubSysModule(item, subs, sysModuleList);
                    }
                }
            }

            return sysModules;
        }

        private static void AddSubSysModule(Model.SysModule sysModule,List<Model.SysModule> sysModulelst, List<Model.SysModule> allLst)
        {
            sysModule.subs = new List<Model.SysModule>();
            foreach (var item in sysModulelst)
            {
                sysModule.subs.Add(item);

                var lst = (from rst in allLst where rst.preModuleId == item.moduleId select rst).ToList();
                if (lst != null && lst.Count>0)
                {
                    AddSubSysModule(item, lst, allLst);
                }
            }
            if (sysModule.subs.Count == 0)
            {
                sysModule.subs = null;
            }
        }
        /// <summary>
        /// 获取指定模块信息详情
        /// </summary>
        /// <param name="sysModuleId">模块ID</param>
        /// <returns></returns>
        public static IList<Model.SysModule> GetChildNodes(string sysModuleId)
        {
            var ysModule = Repository.SysModule.GetChildNodes(sysModuleId);
            return ysModule;
        }

        /// <summary>
        /// 获取清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.SysModule> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount)
        {
            IList<Model.SysModule> usrList = Repository.SysModule.GetList(pageIndex, rowCount, out totalCount, out pageCount);
            return usrList;
        }

    }
}
