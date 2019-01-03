using Dapper.Contrib.Extensions;
using System;
namespace HelpCenter.Model
{
	/// <summary>
	/// docusrrole:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DocUsrRole
    {
		public DocUsrRole()
		{}
		#region Model
		private string _docroleid;
		private string _docid;
		private string _roleid;
		private int? _docrolestate;
		private DateTime? _crdt;
		private string _crusrid;
		private DateTime? _updt;
		private string _upusrid;
		/// <summary>
		/// 
		/// </summary>
        [Key]
		public string docRoleId
		{
			set{ _docroleid=value;}
			get{return _docroleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string docId
		{
			set{ _docid=value;}
			get{return _docid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string roleId
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? docRoleState
		{
			set{ _docrolestate=value;}
			get{return _docrolestate;}
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
		#endregion Model

	}
}

