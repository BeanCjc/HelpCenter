using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpCenter.Common.DBUtility
{
    public class CmdParams
    {
        public string cmd;
        public DynamicParameters param;
    }
}
