using System;
using System.Collections.Generic;
using System.Linq;

namespace RDB.Api.Business.Swagger
{
    // Custom options to customize swagger-ui-dist index.html file
    public class SwaggerUiOptions
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public IEnumerable<string> CustomCssUrls { get; set; }
        public string FaviconUrl { get; set; }
    }
}
