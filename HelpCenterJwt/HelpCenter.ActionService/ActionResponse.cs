using System;

namespace HelpCenter.ActionService
{
    public class ActionResponse
    {
        public ActionResponse()
        {
            _resultType = ResultTypes.FAIL;
            _resultJSONData = ResponseMessageTips.MSG_PROCESS_EXCEPTION;
            _resultDataRowCount = 0;
        }

        public ActionResponse(ResultTypes resultType, string resultJSONData)
        {
            _resultType = resultType;
            _resultJSONData = resultJSONData;
            _resultDataRowCount = 0;
        }

        public ActionResponse(ResultTypes resultType, string resultJSONData, int resultDataRowCount)
        {
            _resultType = resultType;
            _resultJSONData = resultJSONData;
            _resultDataRowCount = resultDataRowCount;
        }

        /// <summary>
        /// 请求处理结果类型
        /// </summary>
        public enum ResultTypes
        {
            /// <summary>
            /// 处理成功
            /// </summary>
            SUCCESS = 1,

            /// <summary>
            /// 处理失败
            /// </summary>
            FAIL = 0
        }

        private ResultTypes _resultType;

        /// <summary>
        /// 请求处理结果类型
        /// </summary>
        public ResultTypes ResultType
        {
            get { return _resultType; }
            set { _resultType = value; }
        }

        private string _resultJSONData;

        /// <summary>
        /// 处理结果JSON数据
        /// </summary>
        public string ResultJSONData
        {
            get { return _resultJSONData; }
            set { _resultJSONData = value; }
        }

        private int _resultDataRowCount;

        /// <summary>
        /// 处理结果数据量
        /// </summary>
        public int ResultDataRowCount
        {
            get { return _resultDataRowCount; }
            set { _resultDataRowCount = value; }
        }
    }
}
