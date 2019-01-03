using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HelpCenterJwt.Swagger
{
    public class HttpHeaderOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<IParameter>();
            var info = context.MethodInfo;
            context.ApiDescription.TryGetMethodInfo(out info);
            try
            {
                Attribute attribute = info.GetCustomAttribute(typeof(AuthorizeAttribute));
                if (attribute != null)
                {
                    operation.Parameters.Add(new BodyParameter
                    {
                        Name = "Authorization",
                        @In = "header",
                        Description = "access_token",
                        Required = true
                    });
                }

            }
            catch
            { }
        }
    }
}
