using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.ViewModel
{
    public class SysModuleViewModel
    {
        /// <summary>
        /// 系统模块名称
        /// </summary>
        public string ModuleName;
        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode;
        /// <summary>
        /// 模块图标路径
        /// </summary>
        public string ModuleImgPath;
        /// <summary>
        /// 模块跳转路径
        /// </summary>
        public string ModuleUrlPath;
        /// <summary>
        /// 上级模块ID
        /// </summary>
        public string PreModuleId;
        /// <summary>
        /// 模块说明
        /// </summary>
        public string ModuleRemark;
        /// <summary>
        /// 模块状态，1为启用，0为停用
        /// </summary>
        public string ModuleState;
    }
}
