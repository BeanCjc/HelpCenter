using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.Model.DbModel
{
    [Serializable]
    public partial class DbUserModel
    {
        #region Model
        private string _usrid;
        private string _roleId;
        private string _roleName;
        private string _usrphonenum;
        private string _usraccount;
        private string _usrpsw;
        private string _usrname;
        private string _usrdeptid;
        private int? _usrverifystate;
        private int? _usrtype;
        private int? _usrstate;
        private DateTime? _crdt;
        private string _crusrid;
        private DateTime? _updt;
        private string _upusrid;
        /// <summary>
        /// 用户ID
        /// </summary>
        public string usrId
        {
            set { _usrid = value; }
            get { return _usrid; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string usrPhoneNum
        {
            set { _usrphonenum = value; }
            get { return _usrphonenum; }
        }
        /// <summary>
        /// 账号
        /// </summary>
        public string usrAccount
        {
            set { _usraccount = value; }
            get { return _usraccount; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string usrPsw
        {
            set { _usrpsw = value; }
            get { return _usrpsw; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string usrName
        {
            set { _usrname = value; }
            get { return _usrname; }
        }
        /// <summary>
        /// 所在部门ID
        /// </summary>
        public string usrDeptId
        {
            set { _usrdeptid = value; }
            get { return _usrdeptid; }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int? usrVerifyState
        {
            set { _usrverifystate = value; }
            get { return _usrverifystate; }
        }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int? usrType
        {
            set { _usrtype = value; }
            get { return _usrtype; }
        }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int? usrState
        {
            set { _usrstate = value; }
            get { return _usrstate; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? crdt
        {
            set { _crdt = value; }
            get { return _crdt; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string crUsrId
        {
            set { _crusrid = value; }
            get { return _crusrid; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? updt
        {
            set { _updt = value; }
            get { return _updt; }
        }
        /// <summary>
        /// 最后更新用户ID
        /// </summary>
        public string upUsrId
        {
            set { _upusrid = value; }
            get { return _upusrid; }
        }
        #endregion Model

    }
}
