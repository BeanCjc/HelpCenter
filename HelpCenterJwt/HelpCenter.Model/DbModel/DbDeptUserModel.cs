using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.Model.DbModel
{
    /// <summary>
    /// 部门下的用户模型
    /// </summary>
    public class DbDeptUserModel
    {
        private DateTime? crdt;
        private string usrid;
        private string usrDeptId;
        private string usrName;
        private int? usrType;
        private int? usrVerifystate;
        private string usrAccount;
        private string deptName;
        private int? _usrState;

        /// <summary>
        /// 用户创建时间
        /// </summary>
        public DateTime? Crdt { get => crdt; set => crdt = value; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Usrid { get => usrid; set => usrid = value; }
        /// <summary>
        /// 用户所属部门
        /// </summary>
        public string UsrDeptId { get => usrDeptId; set => usrDeptId = value; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UsrName { get => usrName; set => usrName = value; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int? UsrType { get => usrType; set => usrType = value; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int? UsrVerifystate { get => usrVerifystate; set => usrVerifystate = value; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UsrAccount { get => usrAccount; set => usrAccount = value; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string DeptName { get => deptName; set => deptName = value; }
        public int? UsrState { get => _usrState; set => _usrState = value; }
    }
}
