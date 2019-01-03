using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.BLL
{
    public class Role
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="strRoleName">角色名称</param>
        /// <param name="strRoleState">启用状态，1为启用，0为停用</param>
        /// <param name="strRoleRemark">角色说明</param>
        /// <returns></returns>
        public static bool Add(string strRoleName, int strRoleState, string strRoleRemark, string strCrUsrId)
        {
            Model.Role roleModel = new Model.Role()
            {
                roleId = Guid.NewGuid().ToString(),
                roleName = strRoleName,
                roleState = strRoleState,
                roleRemark = strRoleRemark,
                crdt = DateTime.Now,
                crUsrId = strCrUsrId,
                updt = DateTime.Now,
                upUsrId = strCrUsrId
            };

            return Repository.Role.Add(roleModel);
        }

        /// <summary>
        /// 获取所有角色清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.Role> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount)
        {
            IList<Model.Role> roleList = Repository.Role.GetList(pageIndex, rowCount, out totalCount, out pageCount);
            return roleList;
        }

        public static bool Update(string strRoleId, string strRoleName, string strRoleRemark, int strRoleState, string strUpUsrId)
        {
            Model.Role roleModel = new Model.Role()
            {
                roleId = strRoleId,
                roleName = strRoleName,
                roleState = strRoleState,
                roleRemark = strRoleRemark,
                updt = DateTime.Now,
                upUsrId = strUpUsrId
            };
            return Repository.Role.Update(roleModel);
        }

        public static bool Delete(string strRoleId)
        {
            return Repository.Role.Delete(strRoleId);
        }

        public static bool Enable(string strDeptId, int roleState)
        {
            return Repository.Role.Enable(strDeptId, roleState);
        }
        /// <summary>
        /// 获取指定角色信息详情
        /// </summary>
        /// <param name="strRoleId">角色ID</param>
        /// <returns></returns>
        public static Model.Role GetModel(string strRoleId)
        {
            var role = Repository.Role.GetModel(strRoleId);
            return role;
        }
        /// <summary>
        /// 获取指定角色信息详情
        /// </summary>
        /// <returns></returns>
        public static IList<Model.Role> GetAllList(bool isEnable = false)
        {
            var role = Repository.Role.GetAllList(isEnable);
            return role;
        }

    }
}
