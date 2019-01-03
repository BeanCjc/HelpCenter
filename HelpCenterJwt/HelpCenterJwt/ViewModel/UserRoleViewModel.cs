using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.ViewModel
{
    public class UserRoleViewModel
    {
        private string userId;
        private List<string> roleIdList;

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get => userId; set => userId = value; }
        /// <summary>
        /// 角色ID列表
        /// </summary>
        public List<string> RoleIdList { get => roleIdList; set => roleIdList = value; }
    }
}
