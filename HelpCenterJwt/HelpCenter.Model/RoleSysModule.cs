using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.Model
{
    public class RoleSysModule
    {
        private string roleSysModuleId;
        private string sysModuleId;
        private string roleId;
        private int? roleSysModuleState;
        private DateTime? crdt;
        private string crUsrId;
        private DateTime? updt;
        private string upUsrId;

        /// <summary>
        /// 角色模块ID
        /// </summary>
        public string RoleSysModuleId { get => roleSysModuleId; set => roleSysModuleId = value; }
        /// <summary>
        /// 模块ID
        /// </summary>
        public string SysModuleId { get => sysModuleId; set => sysModuleId = value; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get => roleId; set => roleId = value; }
        /// <summary>
        /// 角色模块状态
        /// </summary>
        public int? RoleSysModuleState { get => roleSysModuleState; set => roleSysModuleState = value; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Crdt { get => crdt; set => crdt = value; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CrUsrId { get => crUsrId; set => crUsrId = value; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? Updt { get => updt; set => updt = value; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public string UpUsrId { get => upUsrId; set => upUsrId = value; }
    }
}
