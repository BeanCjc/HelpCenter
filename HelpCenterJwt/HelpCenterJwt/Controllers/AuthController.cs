using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//引用命名空间
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using HelpCenterJwt.Models;
using HelpCenterJwt.ViewModel;
using HelpCenter;
using HelpCenter.ActionService;
using Newtonsoft.Json;

namespace HelpCenterJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private JwtSettings _jwtSettings;

        public AuthController(IOptions<JwtSettings> _jwtSettingsAccesser)
        {
            _jwtSettings = _jwtSettingsAccesser.Value;
        }

        /// <summary>
        /// 请求登陆验证
        /// </summary>
        /// <param name="loginViewModel">登陆所需用户信息视图</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Token([FromBody]LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)//判断是否合法
            {
                try
                {
                    
                    HelpCenter.Model.Usr usrModel = null;

                    if (HelpCenter.BLL.Usr.CheckAccount(loginViewModel.User))
                    {
                        return Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_INFO_NO_FOUND });
                    }
                    if (!HelpCenter.BLL.Usr.Login(loginViewModel.User, loginViewModel.Password, out usrModel))//判断账号密码是否正确
                    {
                        return Ok(new { result = false, tips = ResponseMessageTips.MSG_USR_PWD_ERR });
                    }
                    HelpCenter.BLL.Usr.usrModel = usrModel;
                    if (usrModel.usrVerifyState == 0 || usrModel.usrState==0)
                    {
                        return Ok(new { result = false, tips = ResponseMessageTips.MSG_ACCOUNT_DISABLE });
                    }
                    var deptModel = HelpCenter.BLL.Dept.GetModel(usrModel.usrDeptId);

                    var claim = new Claim[]{
                        //this.User.Identities.First(u => u.IsAuthenticated).FindFirst("UsrId").Value
                        new Claim("UsrId",usrModel.usrId),
                        new Claim("UsrName",usrModel.usrName),
                        new Claim("UsrDeptId",string.IsNullOrEmpty(usrModel.usrDeptId)?"":usrModel.usrDeptId),
                        new Claim("UsrAccount",usrModel.usrAccount),
                        new Claim("UsrType",usrModel.usrType?.ToString()),
                        new Claim("UsrState",usrModel.usrState?.ToString()),
                        new Claim("UsrVerifyState",usrModel.usrVerifyState?.ToString()),
                        new Claim("DeptNO",string.IsNullOrEmpty(deptModel?.deptNO)?"":deptModel?.deptNO)
                    };

                    //对称秘钥
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

                    //签名证书(秘钥，加密算法)
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
                    JwtSecurityToken token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now, DateTime.Now.AddMinutes(600), creds);

                    return Ok(new { result = true, tips = ResponseMessageTips.MSG_AUTHORIZED_SUCCESS, token = new JwtSecurityTokenHandler().WriteToken(token), data = usrModel });
                }
                catch (Exception e)
                {
                    return Ok(new { result = false, tips = ResponseMessageTips.MSG_AUTHORIZED_FAILED + e.Message.ToString() });
                }
            }
            return Ok(new { result = false, tips = ResponseMessageTips.MSG_PROCESS_DATA_FORMAT_ERROR });
        }
    }
}