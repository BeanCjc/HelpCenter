using System;
namespace HelpCenter.Model
{
	/// <summary>
	/// usrrole:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UsrRole
	{
		public UsrRole()
		{}
		#region Model
		private string _usrroleid;
		private string _usrid;
		private string _roleId;
		private int? _usrrolestate;
		private DateTime? _crdt;
		private string _crusrid;
		private DateTime? _updt;
		private string _upusrid;
        private string _roleName;
        /// <summary>
        /// 
        /// </summary>
        public string usrRoleId
		{
			set{ _usrroleid=value;}
			get{return _usrroleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string usrId
		{
			set{ _usrid=value;}
			get{return _usrid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string roleId
		{
			set{ _roleId = value;}
			get{return _roleId; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int? usrRoleState
		{
			set{ _usrrolestate=value;}
			get{return _usrrolestate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? crdt
		{
			set{ _crdt=value;}
			get{return _crdt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string crUsrId
		{
			set{ _crusrid=value;}
			get{return _crusrid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? updt
		{
			set{ _updt=value;}
			get{return _updt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string upUsrId
		{
			set{ _upusrid=value;}
			get{return _upusrid;}
		}

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get => _roleName; set => _roleName = value; }
        #endregion Model

    }
}

