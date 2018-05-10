using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataConsole
{
    public class Parameters: IParameter
    {
       public string Name { get; set; }
       public string In { get; set; }
       public string Description { get; set; }
       public bool Required { get; set; }
       public Dictionary<string, object> Extensions { get; }
    }
}
