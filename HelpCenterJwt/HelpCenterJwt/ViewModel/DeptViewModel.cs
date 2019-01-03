using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.ViewModel
{
    public class DeptViewModel
    {
        /// <summary>
        /// 上级部门编号
        /// </summary>
        [Required]
        public string PreDeptId;
        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        public string DeptName;
        /// <summary>
        /// 部门账号
        /// </summary>
        [Required]
        public string DeptAccount;
        /// <summary>
        /// 部门密码
        /// </summary>
        [Required]
        public string DeptPsw;
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptNO;
    }
}
