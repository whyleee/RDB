using System;
using System.Collections.Generic;
using System.Linq;

namespace RDB.Api.Business.Swagger
{
    public class SwaggerUiOptions
    {
        public string Url { get; set; }
        public IEnumerable<string> CustomCssUrls { get; set; }
    }
}
