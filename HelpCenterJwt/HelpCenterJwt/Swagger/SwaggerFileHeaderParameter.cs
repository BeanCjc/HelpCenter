﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpCenterJwt.Swagger
{
    public class SwaggerFileHeaderParameter : IOperationFilter
    {
        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<IParameter>();

            if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
                !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var fileParameters = context.ApiDescription.ActionDescriptor.Parameters.Where(n => n.ParameterType == typeof(IFormFile)).ToList();

            if (fileParameters.Count < 0)
            {
                return;
            }

            foreach (var fileParameter in fileParameters)
            {
                if (fileParameter.BindingInfo == null)
                {
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Name = fileParameter.Name,
                        In = "formData",
                        Description = "文件上传",
                        Required = true,
                        Type = "file"
                    });
                    operation.Consumes.Add("multipart/form-data");
                }
                else
                {
                    var parameter = operation.Parameters.Single(n => n.Name == fileParameter.Name);
                    operation.Parameters.Remove(parameter);
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Name = parameter.Name,
                        In = "formData",
                        Description = parameter.Description,
                        Required = parameter.Required,
                        Type = "file"
                    });
                    operation.Consumes.Add("multipart/form-data");
                }

            }
        }

    }
}
