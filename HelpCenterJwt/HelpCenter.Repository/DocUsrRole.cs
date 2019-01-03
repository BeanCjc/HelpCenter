using Dapper;
using HelpCenter.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HelpCenter.Common.DBUtility;
using System.Linq;
using System.Transactions;

namespace HelpCenter.Repository
{
    public class DocUsrRole
    {

        private static DapperMySQLHelper mysql = new DapperMySQLHelper();

        public static bool Delete(Model.DocUsrRole docUsrRole)
        {

            /*先清空原有关系*/
            CmdParams cpDel = new CmdParams
            {
                cmd = "delete from docUsrRole where roleId=?roleId and docId=?docId",
                param = new DynamicParameters()
            };
            cpDel.param.Add("?roleId", docUsrRole.roleId);
            cpDel.param.Add("?docId", docUsrRole.docId);


            /*调用事务处理*/
            int rows = mysql.ExcuteNonQuery<Model.DocUsrRole>(DapperMySQLHelper.ConnectionString, cpDel.cmd, cpDel.param, false);

            return rows > 0;
        }

        public static bool Add(Model.DocUsrRole docUsrRole)
        {
            List<CmdParams> parameters = new List<CmdParams>();

            /*先清空原有关系*/
            CmdParams cpDel = new CmdParams
            {
                cmd = "delete from docUsrRole where roleId=?roleId and docId=?docId",
                param = new DynamicParameters()
            };
            cpDel.param.Add("?roleId", docUsrRole.roleId);
            cpDel.param.Add("?docId", docUsrRole.docId);

            //parameters.Add(cpDel);
            mysql.ExcuteNonQuery<Model.DocUsrRole>(DapperMySQLHelper.ConnectionString, cpDel.cmd, cpDel.param, false);

            CmdParams cp = new CmdParams
            {
                cmd = "insert into docUsrRole(docRoleId,docId,roleId,docRoleState,crdt,crUsrId,updt,upUsrId) values (?docRoleId,?docId,?roleId,?docRoleState,?crdt,?crUsrId,?updt,?upUsrId)",
                param = new DynamicParameters()
            };

            cp.param.Add("?docRoleId", Guid.NewGuid().ToString());
            cp.param.Add("?roleId", docUsrRole.roleId);
            cp.param.Add("?docId", docUsrRole.docId);
            cp.param.Add("?docRoleState", 1);
            cp.param.Add("?crdt", docUsrRole.crdt);
            cp.param.Add("?crUsrId", docUsrRole.crUsrId);
            cp.param.Add("?updt", docUsrRole.updt);
            cp.param.Add("?upUsrId", docUsrRole.upUsrId);

            // parameters.Add(cp);
            int rows = mysql.ExcuteNonQuery<Model.DocUsrRole>(DapperMySQLHelper.ConnectionString, cp.cmd, cp.param, false);

            /*调用事务处理*/
            //int rows = mysql.ExcuteNonQueryTransaction<Model.DocUsrRole>(DapperMySQLHelper.ConnectionString, parameters, false);

            return rows > 0;
        }
        public static bool Add(List<Model.DocUsrRole> docUsrRoleList)
        {
            int rows = 0;
            List<CmdParams> parameters = new List<CmdParams>();
            using (var tran = new TransactionScope())
            {
                /*先清空原有关系*/
                CmdParams cpDel = new CmdParams
                {
                    cmd = "delete from docUsrRole where /*roleId=?roleId and */docId=?docId",
                    param = new DynamicParameters()
                };
                //cpDel.param.Add("?roleId", item.roleId);
                cpDel.param.Add("?docId", docUsrRoleList[0].docId);
                parameters.Add(cpDel);
                mysql.ExcuteNonQuery<Model.DocUsrRole>(DapperMySQLHelper.ConnectionString, cpDel.cmd, cpDel.param, false);
                foreach (var item in docUsrRoleList)
                {


                    CmdParams cp = new CmdParams
                    {
                        cmd = "insert into docUsrRole(docRoleId,docId,roleId,docRoleState,crdt,crUsrId,updt,upUsrId) values (?docRoleId,?docId,?roleId,?docRoleState,?crdt,?crUsrId,?updt,?upUsrId)",
                        param = new DynamicParameters()
                    };

                    cp.param.Add("?docRoleId", Guid.NewGuid().ToString());
                    cp.param.Add("?roleId", item.roleId);
                    cp.param.Add("?docId", item.docId);
                    cp.param.Add("?docRoleState", 1);
                    cp.param.Add("?crdt", item.crdt);
                    cp.param.Add("?crUsrId", item.crUsrId);
                    cp.param.Add("?updt", item.updt);
                    cp.param.Add("?upUsrId", item.upUsrId);

                    //parameters.Add(cp);
                    rows = mysql.ExcuteNonQuery<Model.DocUsrRole>(DapperMySQLHelper.ConnectionString, cp.cmd, cp.param, false);
                }
                tran.Complete();
            }

            /*调用事务处理*/
            //int rows = mysql.ExcuteNonQueryTransaction<Model.DocUsrRole>(DapperMySQLHelper.ConnectionString, parameters, false);

            return rows > 0;
        }

        public static bool Set(Model.DocUsrRole docUsrRole, string[] docIdList)
        {
            List<CmdParams> parameters = new List<CmdParams>();

            /*先清空原有关系*/
            CmdParams cpDel = new CmdParams
            {
                cmd = "delete from docUsrRole where roleId=?roleId",
                param = new DynamicParameters()
            };
            cpDel.param.Add("?roleId", docUsrRole.roleId);

            parameters.Add(cpDel);

            /*批量更新到最新关系*/
            foreach (var item in docIdList)
            {
                CmdParams cp = new CmdParams
                {
                    cmd = "insert into docUsrRole(docRoleId,docId,roleId,docRoleState,crdt,crUsrId,updt,upUsrId) values (?docRoleId,?docId,?roleId,?docRoleState,?crdt,?crUsrId,?updt,?upUsrId)",
                    param = new DynamicParameters()
                };

                cp.param.Add("?docRoleId", Guid.NewGuid().ToString());
                cp.param.Add("?roleId", docUsrRole.roleId);
                cp.param.Add("?docId", item);
                cp.param.Add("?docRoleState", 1);
                cp.param.Add("?crdt", docUsrRole.crdt);
                cp.param.Add("?crUsrId", docUsrRole.crUsrId);
                cp.param.Add("?updt", docUsrRole.updt);
                cp.param.Add("?upUsrId", docUsrRole.upUsrId);

                parameters.Add(cp);
            }

            /*调用事务处理*/
            int rows = mysql.ExcuteNonQueryTransaction<Model.DocUsrRole>(DapperMySQLHelper.ConnectionString, parameters, false);

            return rows > 0;
        }

        public static List<string> GetListByDocId(string docId)
        {
            try
            {
                using (IDbConnection conn = DbSwitcher.GetOpenConnection())
                {
                    List<string> vs = new List<string>();
                    var lst = conn.GetList<Model.DocUsrRole>(new { DocId = docId }).ToList();
                    if (lst != null && lst.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            if (!vs.Contains(item.roleId))
                            {
                                vs.Add(item.roleId);
                            }
                        }
                    }
                    return vs;
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return new List<string>();
        }

        public static Model.DocUsrRole QueryExist(string docId, string roleId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from docusrrole where docId=?docId and roleId=?roleId ");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("?roleId", roleId);
            parameters.Add("?docId", docId);

            return mysql.FindOne<Model.DocUsrRole>(DapperMySQLHelper.ConnectionString, strSql.ToString(), parameters, false);
        }
    }
}