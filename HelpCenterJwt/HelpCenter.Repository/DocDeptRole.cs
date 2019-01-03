using Dapper;
using HelpCenter.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HelpCenter.Common.DBUtility;

namespace HelpCenter.Repository
{
    public class DocDeptRole
    {

        private static DapperMySQLHelper mysql = new DapperMySQLHelper();
        public static bool Set(Model.DocDeptRole docDeptRole, List<string> shareDeptList)
        {
            int rows = 0;
            List<CmdParams> parameters = new List<CmdParams>();

            /*先清空原有关系*/
            CmdParams cpDel = new CmdParams
            {
                cmd = "delete from docdeptrole where docId=?docId",
                param = new DynamicParameters()
            };
            cpDel.param.Add("?docId", docDeptRole.DocId);

            mysql.ExcuteNonQuery<Model.DocDeptRole>(DapperMySQLHelper.ConnectionString, cpDel.cmd, cpDel.param,false);

            /*批量更新到最新关系*/
            foreach (var item in shareDeptList)
            {
                rows = 0;
                CmdParams cp = new CmdParams
                {
                    cmd = "insert into docdeptrole(docDeptRoleId,docId,deptId,docDeptState,crdt,crUsrId,updt,upUsrId) values (?docDeptRoleId,?docId,?deptId,?docDeptState,?crdt,?crUsrId,?updt,?upUsrId)",
                    param = new DynamicParameters()
                };

                cp.param.Add("?docDeptRoleId", docDeptRole.DocDeptRoleId);
                cp.param.Add("?docId", docDeptRole.DocId);
                cp.param.Add("?deptId", item);
                cp.param.Add("?docDeptState", docDeptRole.DocDeptState);
                cp.param.Add("?crdt", docDeptRole.Crdt);
                cp.param.Add("?crUsrId", docDeptRole.CrUsrId);
                cp.param.Add("?updt", docDeptRole.Updt);
                cp.param.Add("?upUsrId", docDeptRole.CrUsrId);

               rows = mysql.ExcuteNonQuery<Model.DocDeptRole>(DapperMySQLHelper.ConnectionString, cp.cmd, cp.param, false);
            }

            return rows > 0;
        }

        /// <summary>
        /// 检查指定部门下是否绑定文档
        /// </summary>
        /// <param name="strDeptId">部门编号</param>
        /// <returns></returns>
        public static Model.Dept GetModel(string strDeptId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from docdeptrole ");
            strSql.Append(" where deptId=?deptId ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?deptId", strDeptId);

            var result = mysql.FindOne<Model.Dept>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return result;
        }

        /// <summary>
        /// 查询文档对应的共享关系
        /// </summary>
        /// <param name="strDocId">文档编号</param>
        /// <returns></returns>
        public static IList<Model.DocDeptRole> GetDocDeptRoleModelList(string strDocId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from docdeptrole ");
            strSql.Append(" where docId=?docId ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?docId", strDocId);

            var result = mysql.FindToListByPage<Model.DocDeptRole>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
            return result;
        }

    }
}
