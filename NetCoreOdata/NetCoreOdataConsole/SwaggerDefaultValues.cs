using Microsoft.AspNet.OData;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor.FilterDescriptors.Any(a => a.Filter.GetType() == typeof(EnableQueryAttribute)))
            {
                Dictionary<string, string> odataParameters = new Dictionary<string, string>()
                {
                    { "$expand", "Causes related entities to be included inline in the response"},
                    { "$filter", "A function that must evaluate to true for a record to be returned"},
                    { "$select", "Specifies a subset of properties to return"},
                    { "$orderby", "Determines what values are used to order a collection of records"},
                    { "$top", "The max number of records"},
                    { "$skip", "The number of records to skip"}
                };

                if (operation.Parameters == null)
                {
                    operation.Parameters = new List<IParameter>();
                }

                foreach (var pair in odataParameters)
                {
                    operation.Parameters.Add(new Parameters
                    {
                        Name = pair.Key,
                        Required = false,
                        In = "query",
                        Description = pair.Value
                    });
                }
            }

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = context.ApiDescription
                                         .ParameterDescriptions
                                         .First(p => p.Name == parameter.Name);

                var t = context.ApiDescription.ActionAttributes();

                var routeInfo = description.RouteInfo;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.Default == null)
                {
                    parameter.Default = routeInfo.DefaultValue;
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}
