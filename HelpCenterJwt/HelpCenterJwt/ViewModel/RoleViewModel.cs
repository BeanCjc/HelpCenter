using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.ViewModel
{
    public class RoleViewModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        public string RoleName;
        /// <summary>
        /// 角色说明
        /// </summary>
        public string RoleRemark;
        /// <summary>
        /// 启用状态，1为启用，0为停用
        /// </summary>
        [Required]
        public int RoleState;
    }
}
