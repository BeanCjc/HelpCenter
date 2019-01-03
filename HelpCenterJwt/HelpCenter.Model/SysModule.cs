using System;
using System.Collections.Generic;

namespace HelpCenter.Model
{
	/// <summary>
	/// sys_module:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysModule
	{
		public SysModule()
		{}
		#region Model
		private string _moduleid;
		private string _modulename;
        private string _moduleCode;
        private int _moduleNum;
        private string _moduleImgPath;
        private string _moduleUrlPath;
        private string _premoduleid;
		private string _moduleremark;
		private int? _modulestate;
		private DateTime? _crdt;
		private string _crusrid;
		private DateTime? _updt;
		private string _upusrid;

        private List<SysModule> _subs;

		/// <summary>
		/// 
		/// </summary>
		public string moduleId
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string moduleName
		{
			set{ _modulename=value;}
			get{return _modulename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string preModuleId
		{
			set{ _premoduleid=value;}
			get{return _premoduleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string moduleRemark
		{
			set{ _moduleremark=value;}
			get{return _moduleremark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? moduleState
		{
			set{ _modulestate=value;}
			get{return _modulestate;}
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

        public string ModuleImgPath { get => _moduleImgPath; set => _moduleImgPath = value; }
        public string ModuleUrlPath { get => _moduleUrlPath; set => _moduleUrlPath = value; }
        public string ModuleCode { get => _moduleCode; set => _moduleCode = value; }
        /// <summary>
        /// 子模块列表
        /// </summary>
        public List<SysModule> subs { get => _subs; set => _subs = value; }
        /// <summary>
        /// 排序号码，越小的排越前
        /// </summary>
        public int ModuleNum { get => _moduleNum; set => _moduleNum = value; }
        #endregion Model

    }
}

