using Dapper;
using HelpCenter.Common.DBUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HelpCenter.BLL
{
    public class RoleSysModule
    {
        /// <summary>
        /// 新增用户角色关系
        /// </summary>
        /// <param name="strRoleId"></param>
        /// <param name="strUsrId"></param>
        /// <param name="strRoleIdList"></param>
        /// <param name="iRoleSysModuleState"></param>
        /// <param name="strCrUsrId"></param>
        /// <returns></returns>
        public static bool Add(string strRoleId, int? iRoleSysModuleState,string strSysModuleId, string strCrUsrId)
        {
            Model.RoleSysModule roleSysModule = new Model.RoleSysModule()
            {
                Crdt = DateTime.Now,
                CrUsrId = strCrUsrId,
                RoleSysModuleId = Guid.NewGuid().ToString(),
                RoleId = strRoleId,
                RoleSysModuleState=iRoleSysModuleState,
                SysModuleId = strSysModuleId,
                Updt = DateTime.Now,
                UpUsrId = strCrUsrId
            };
            return Repository.RoleSysModule.Add(roleSysModule);
        }

        /// <summary>
        /// 获取所有用户角色关系清单（分页）
        /// </summary>
        /// <returns></returns>
        public static string GetList(int pageIndex,int rowCount, out int totalCount, out int pageCount)
        {
            IList<Model.RoleSysModule> deptList = Repository.RoleSysModule.GetList(pageIndex,rowCount,out totalCount,out pageCount);
            return null == deptList ? string.Empty : JsonConvert.SerializeObject(deptList);
        }


        public static bool Delete(string strRoleId)
        {
            return Repository.RoleSysModule.Delete(strRoleId);
        }

        /// <summary>
        /// 获取指定角色ID列表 关系信息详情
        /// </summary>
        /// <param name="roleId">角色ID列表</param>
        /// <returns></returns>
        public static IList<Model.RoleSysModule> GetModelListByRoleId(string roleId)
        {
            var usrRole = Repository.RoleSysModule.GetModelListByRoleId(roleId);
            return usrRole;
        }

        /// <summary>
        /// 获取指定用户ID模块信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public static List<Model.SysModule> GetModelListByUserId(string userId)
        {
            var userRoleModelList = UserRole.GetModelByUserId(userId);
            List<string> lstRoleId = new List<string>();
            foreach (var item in userRoleModelList)
            {
                lstRoleId.Add(item.roleId);
            }
            if (lstRoleId.Count == 0)
            {
                return null;
            }
            var usrRole = Repository.SysModule.GetModelListByRoleIdList(lstRoleId);
            var usrRoleResult = from rst in usrRole where string.IsNullOrEmpty(rst.preModuleId) select rst;

            List<Model.SysModule> sysModule = new List<Model.SysModule>();

            if (usrRoleResult != null)
            {
                foreach (var usrRoleResultItem in usrRoleResult)
                {
                    var lst = from rst in usrRole where rst.preModuleId == usrRoleResultItem.moduleId select rst;
                    if (lst != null)
                    {
                        AddSysModule(usrRoleResultItem, lst, usrRole);
                    }
                }
                sysModule.AddRange(usrRoleResult);
            }

            return sysModule;
        }

        private static void AddSysModule(Model.SysModule sysModule,IEnumerable<Model.SysModule> sysModulesLst,IList<Model.SysModule> allList)
        {
            sysModule.subs = new List<Model.SysModule>();
            foreach (var item in sysModulesLst)
            {
                sysModule.subs.Add(item);
                var lst = from rst in allList where rst.preModuleId == item.moduleId select rst;
                if (lst != null)
                {
                    AddSysModule(item, lst, allList);
                }
            }
            if (sysModule.subs.Count == 0)
            {
                sysModule.subs = null;
            }
        }


        public static bool Set(string strRoleId, int? iRoleSysModuleState, string strCrUsrId,string[] sysModuleList)
        {
            Model.RoleSysModule roleSysModule = new Model.RoleSysModule()
            {
                Crdt = DateTime.Now,
                CrUsrId = strCrUsrId,
                RoleSysModuleId = Guid.NewGuid().ToString(),
                RoleId = strRoleId,
                RoleSysModuleState = iRoleSysModuleState,
                Updt = DateTime.Now,
                UpUsrId = strCrUsrId
            };

            return Repository.RoleSysModule.Set(roleSysModule, sysModuleList);
        }

    }
}
