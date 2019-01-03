using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.Model.DTO
{

    /// <summary>
    /// usr:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserDto
    {
        public UserDto()
        { }
        #region Model
        private string _usrid;
        private string _usrphonenum;
        private string _usraccount;
        private string _usrpsw;
        private string _usrname;
        private string _usrdeptid;
        private string _deptName;
        private int? _usrverifystate;
        private int? _usrtype;
        private int? _usrstate;
        private DateTime? _crdt;
        private string _crusrid;
        private DateTime? _updt;
        private string _upusrid;
        private string _roleName;

        private IList<string> _usrRoleIdLst;
        /// <summary>
        /// 
        /// </summary>
        public string usrId
        {
            set { _usrid = value; }
            get { return _usrid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string usrPhoneNum
        {
            set { _usrphonenum = value; }
            get { return _usrphonenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string usrAccount
        {
            set { _usraccount = value; }
            get { return _usraccount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string usrPsw
        {
            set { _usrpsw = value; }
            get { return _usrpsw; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string usrName
        {
            set { _usrname = value; }
            get { return _usrname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string usrDeptId
        {
            set { _usrdeptid = value; }
            get { return _usrdeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? usrVerifyState
        {
            set { _usrverifystate = value; }
            get { return _usrverifystate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? usrType
        {
            set { _usrtype = value; }
            get { return _usrtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? usrState
        {
            set { _usrstate = value; }
            get { return _usrstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? crdt
        {
            set { _crdt = value; }
            get { return _crdt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string crUsrId
        {
            set { _crusrid = value; }
            get { return _crusrid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? updt
        {
            set { _updt = value; }
            get { return _updt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string upUsrId
        {
            set { _upusrid = value; }
            get { return _upusrid; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get => _roleName; set => _roleName = value; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get => _deptName; set => _deptName = value; }
        /// <summary>
        /// 用户权限列表
        /// </summary>
        public IList<string> UsrRoleIdLst { get => _usrRoleIdLst; set => _usrRoleIdLst = value; }
        #endregion Model
    }
}
