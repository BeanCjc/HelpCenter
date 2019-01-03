using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.ViewModel
{
    public class UserViewModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string UsrPhoneNum { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        [Required]
        public string UsrAccount { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UsrPsw { get; set; }
        /// <summary>
        /// 默认1为部门账号，2为正式人员，3为试用人员，4为普通用户
        /// </summary>
        [Required]
        public int UsrType { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UsrName { get; set; }
        /// <summary>
        /// 所属部门ID
        /// </summary>
        public string UsrDeptId { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public List<string> RoleIdList { get; set; }
        /// <summary>
        /// 用户数据状态，1为启用，0为停用
        /// </summary>
        [Required]
        public int UsrState { get; set; }
        /// <summary>
        /// 用户审核状态，1为通过，0为未通过
        /// </summary>
        [Required]
        public int? UsrVerifyState { get; set; }
    }
}
