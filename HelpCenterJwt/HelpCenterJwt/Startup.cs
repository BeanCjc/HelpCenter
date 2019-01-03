
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HelpCenterJwt.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace HelpCenterJwt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Swagger文档
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "帮助中心接口文档",
                    Description = "RESTful API for HelpCenter。<br/>"
                                  + "约定：所有请求需要先通过调用/api/auth获取token令牌，<br/>"
                                  + "然后通过其它接口，将返回的token值前加上“bearer（这里一个空格）token值”作为请求头headers的Authorization值。<br/>"
                                  + "所有请求结果的返回值，除了http(s)本身的请求状态，如200，404，500以外，均以json格式返回，以result的布尔值判断请求结果，tips为请求结果的相关提示信息，data为相关数据"
                });

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "请输入带有bearer的Token",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                //Json Token认证方式，此方式为全局添加
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() }
                });

                //Set the comments path for the swagger json and ui.
                options.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "HelpCenterApiDoc.xml"));
                //options.OperationFilter<Swagger.HttpHeaderOperation>(); // 添加httpHeader参数
                options.OperationFilter<Swagger.SwaggerFileHeaderParameter>();//文件上传
            });
            #endregion

            #region Jwt认证
            //将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            //由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
            //将配置绑定到JwtSettings实例中
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);

            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //主要是jwt  token参数设置
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    //Token颁发机构
                    ValidIssuer = jwtSettings.Issuer,
                    //颁发给谁
                    ValidAudience = jwtSettings.Audience,
                    //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    //ValidateIssuerSigningKey=true,
                    ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    //ValidateLifetime=true,
                    ////允许的服务器时间偏移量
                    //ClockSkew=TimeSpan.Zero

                };
            });

            #endregion Jwt认证

            #region 设置接收文件长度的最大值
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                             /*统一设置JsonResult中的日期格式*/
                             .AddJsonOptions(json => { json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            // 在这里面可以注入
            app.UseSwaggerUI(options =>
            {
                //options.InjectOnCompleteJavaScript("/swagger/ui/zh_CN.js"); // 加载中文包
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "HelpCenter API v1");
            });
            app.UseStaticFiles(new StaticFileOptions {
                ServeUnknownFileTypes = true
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
