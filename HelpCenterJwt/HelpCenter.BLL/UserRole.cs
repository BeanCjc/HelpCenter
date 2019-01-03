using Dapper;
using HelpCenter.Common.DBUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.BLL
{
    public class UserRole
    {
        /// <summary>
        /// 新增用户角色关系
        /// </summary>
        /// <param name="strUsrRoleId"></param>
        /// <param name="strUsrId"></param>
        /// <param name="strRoleIdList"></param>
        /// <param name="usrRoleState"></param>
        /// <param name="strCrUsrId"></param>
        /// <returns></returns>
        public static bool Set(string strUsrRoleId, string strUsrId, List<string> strRoleIdList, int? usrRoleState,string strCrUsrId)
        {
            Model.UsrRole usrRoleModel = new Model.UsrRole()
            {
                usrId = strUsrId,
                usrRoleId = Guid.NewGuid().ToString(),
                usrRoleState = 1,
                crdt = DateTime.Now,
                crUsrId = strCrUsrId,
                updt = DateTime.Now,
                upUsrId = strCrUsrId
            };
            return Repository.UsrRole.Set(usrRoleModel, strRoleIdList);
        }

        /// <summary>
        /// 获取所有用户角色关系清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.UsrRole> GetList(int pageIndex,int rowCount, out int totalCount, out int pageCount)
        {
            IList<Model.UsrRole> deptList = Repository.UsrRole.GetList(pageIndex,rowCount,out totalCount,out pageCount);
            return deptList;
        }

        public static bool Update(string strUsrRoleId, string strUsrId, List<string> RoleIdList, int? usrRoleState, string strCrUsrId)
        {

            Model.UsrRole usrRoleModel = new Model.UsrRole()
            {
                usrId = strUsrId,
                usrRoleId = strUsrRoleId,
                updt = DateTime.Now,
                upUsrId = string.Empty
            };
            return Repository.UsrRole.Set(usrRoleModel, RoleIdList);
        }

        public static bool Delete(string strUsrId)
        {
            return Repository.UsrRole.Delete(strUsrId);
        }

        /// <summary>
        /// 获取指定用户ID 关系信息详情
        /// </summary>
        /// <param name="strUsrId">用户ID</param>
        /// <returns></returns>
        public static IList<Model.UsrRole> GetModelByUserId(string strUsrId)
        {
            var usrRole = Repository.UsrRole.GetModel(strUsrId);

            return usrRole;
        }

    }
}
