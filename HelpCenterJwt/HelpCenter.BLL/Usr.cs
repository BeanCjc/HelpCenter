using System;
using System.Collections.Generic;
using System.Text;
using HelpCenter;
using Newtonsoft.Json;

namespace HelpCenter.BLL
{
    public class Usr
    {
        public static Model.Usr usrModel = null;
        public static Model.DTO.UserDto usrDtoModel = null;

        public static bool Add(string strUsrPhoneNum, string strUsrAccount, string strPassWord, string strUserName, string strUsrDeptId,int usrState, string strOperUserId,int iUsrType,int? iUsrVerifyState,List<string> strRoleIdList)
        {
            string strUsrId = Guid.NewGuid().ToString();
            Model.Usr usrModel = new Model.Usr()
            {
                usrId = strUsrId,
                usrPhoneNum = strUsrPhoneNum,
                usrAccount = strUsrAccount,
                usrPsw = strPassWord,
                usrName = strUserName,
                usrDeptId = strUsrDeptId,
                /*默认0为未审核通过，1为审核通过*/
                usrVerifyState = iUsrVerifyState,
                /*默认1为部门账号，2为正式人员，3为试用人员，4为普通用户*/
                usrType = iUsrType,
                /*用户数据状态0为标识删除，1为新增，2为修改*/
                usrState = 1,
                crdt = DateTime.Now,
                crUsrId = strOperUserId,
                updt = DateTime.Now,
                upUsrId = strOperUserId
            };

            UserRole.Set(Guid.NewGuid().ToString(), strUsrId, strRoleIdList, 1, strOperUserId);



            return Repository.Usr.Add(usrModel);
        }

        public static bool Update(string strUsrId, string strUsrPhoneNum, string strUsrAccount, string strUsrPsw, string strUsrName, string strUsrDeptId,int usrState, string strUpUsrId,int? _usrType,int? _usrVerifyState, List<string> strRoleIdList)
        {
            Model.Usr usrModel = new Model.Usr()
            {
                usrId = strUsrId,
                usrPhoneNum = strUsrPhoneNum,
                usrAccount = strUsrAccount,
                usrPsw = strUsrPsw,
                usrName = strUsrName,
                usrDeptId = strUsrDeptId,
                usrState = usrState,
                updt = DateTime.Now,
                upUsrId = strUpUsrId,
                usrType = _usrType,
                usrVerifyState = _usrVerifyState
            };

            UserRole.Set(Guid.NewGuid().ToString(), strUsrId, strRoleIdList, 1, strUpUsrId);
            return Repository.Usr.Update(usrModel);
        }

        public static bool UpdatePassWord(string strUsrAccount, string strUsrPsw)
        {
            return Repository.Usr.UpdatePassWord(strUsrAccount, strUsrPsw);
        }

        public static bool UpdateUsrVerifyState(string strUsrId, int usrVerifyState)
        {
            return Repository.Usr.UpdateUsrVerifyState(strUsrId, usrVerifyState);
        }
        public static bool UpdateUserState(string strUsrId, int usrState)
        {
            return Repository.Usr.UpdateUserState(strUsrId, usrState);
        }
        
        public static bool CheckAccount(string account)
        {
            return null==Repository.Usr.CheckAccount(account);
        }

        public static bool Delete(string strUsrId)
        {
            return Repository.Usr.Delete(strUsrId);
        }

        public static bool Login(string strUserName, string strPassWord, out Model.Usr usrInfoModel)
        {
            usrInfoModel = Repository.Usr.GetModel(strUserName, strPassWord);
            if (usrInfoModel != null)
            {
                usrInfoModel.UsrRoleLst = UserRole.GetModelByUserId(usrInfoModel.usrId);
            }
            return null != usrInfoModel;
        }

        public static Model.Usr GetDetail(string strUsrId)
        {
            var modelUsr = Repository.Usr.GetModel(strUsrId);
            return modelUsr;
        }

        public static Model.DTO.UserDto GetUserDtoDetail(string strUsrId)
        {
            var modelUsr = Repository.Usr.GetUserDtoModel(strUsrId);
            return modelUsr;
        }


        /// <summary>
        /// 获取用户清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.Usr> GetList(int pageIndex, int rowCount, out int totalCount, out int pageCount, string usrType)
        {
            IList<Model.Usr> usrList = Repository.Usr.GetList(pageIndex, rowCount, out totalCount, out pageCount, usrType);
            return usrList;
        }

        /// <summary>
        /// 根据用户名搜索，获取用户清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.Usr> GetListByUsrName(int pageIndex, int rowCount, out int totalCount, out int pageCount,string strUsrName,string usrType, string strDeptID)
        {
            IList<Model.Usr> usrList = Repository.Usr.GetListByUsrName(pageIndex, rowCount, out totalCount, out pageCount, strUsrName, usrType,strDeptID);
            return usrList;
        }


        /// <summary>
        /// 根据用户名搜索，获取用户清单（分页）
        /// </summary>
        /// <returns></returns>
        public static IList<Model.DbModel.DbDeptUserModel> GetListByDeptId(int pageIndex, int rowCount, out int totalCount, out int pageCount,string strDeptId)
        {
            IList<Model.DbModel.DbDeptUserModel> usrList = Repository.Usr.GetListByDeptId(pageIndex, rowCount, out totalCount, out pageCount, strDeptId);
            return usrList;
        }
    }
}
