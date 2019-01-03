using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.ViewModel
{
    /// <summary>
    /// 角色与系统模块关系视图模型
    /// </summary>
    public class RoleSysModuleViewModel
    {
        /// <summary>
        /// 角色对应的系统模块ID列表
        /// </summary>
        public string[] SysModuleIdList;
        /// <summary>
        /// 关系状态，1为启用，0为停用
        /// </summary>
        public int? RoleSysModuleState;
    }
}
