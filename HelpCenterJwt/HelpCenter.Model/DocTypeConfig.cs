using System;
using System.Collections.Generic;

namespace HelpCenter.Model
{
    /// <summary>
    /// doctypeconfig:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class DocTypeConfig
    {
        public DocTypeConfig()
        { }
        #region Model
        private string _doctypeid;
        private int? _doctypenum;
        private string _docpretypeid;
        private string _docPreTypeName;
        private string _doctypename;
        private string _doctypedeptid;
        private string _docTypeDeptName;
        private string _docTypeImg;
        private int _childCount;
        private int? _doctypestate;
        private DateTime? _crdt;
        private string _crusrid;
        private DateTime? _updt;
        private string _upusrid;
        private List<Model.HelpDoc> _docList;
        private List<Model.DocTypeConfig> _subs;

        /// <summary>
        /// 子节点列表
        /// </summary>
        public string[] child { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string docTypeId
        {
            set { _doctypeid = value; }
            get { return _doctypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? docTypeNum
        {
            set { _doctypenum = value; }
            get { return _doctypenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string docPreTypeId
        {
            set { _docpretypeid = value; }
            get { return _docpretypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string docTypeName
        {
            set { _doctypename = value; }
            get { return _doctypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string docTypeDeptId
        {
            set { _doctypedeptid = value; }
            get { return _doctypedeptid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? docTypeState
        {
            set { _doctypestate = value; }
            get { return _doctypestate; }
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
        /// 上级文档名称
        /// </summary>
        public string DocPreTypeName { get => _docPreTypeName; set => _docPreTypeName = value; }
        /// <summary>
        /// 子节点数量
        /// </summary>
        public int childCount { get => _childCount; set => _childCount = value; }
        /// <summary>
        /// 文档归属部门
        /// </summary>
        public string DocTypeDeptName { get => _docTypeDeptName; set => _docTypeDeptName = value; }

        /// <summary>
        /// 该分类下的文档列表
        /// </summary>
        public List<HelpDoc> DocList { get => _docList; set => _docList = value; }
        /// <summary>
        /// 子文档类型列表
        /// </summary>
        public List<DocTypeConfig> subs { get => _subs; set => _subs = value; }
        /// <summary>
        /// 文档图标
        /// </summary>
        public string DocTypeImg { get => _docTypeImg; set => _docTypeImg = value; }
        #endregion Model

    }
}

