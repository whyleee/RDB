using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileProviders;

namespace RDB.Api.Business.Swagger
{
    internal class SwaggerUiIndexFileInfo : IFileInfo
    {
        private readonly string _filePath;
        private readonly SwaggerUiOptions _swaggerUiOptions;
        private byte[] _resultBytes;
        private DateTimeOffset _lastModified;

        public SwaggerUiIndexFileInfo(string filePath, SwaggerUiOptions swaggerUiOptions)
        {
            _filePath = filePath;
            _swaggerUiOptions = swaggerUiOptions;
        }

        public bool Exists => true;
        public long Length
        {
            get
            {
                EnsureResultBytesCreated();
                return _resultBytes.Length;
            }
        }
        public string PhysicalPath => null;
        public string Name => "index.html";
        public DateTimeOffset LastModified => _lastModified;
        public bool IsDirectory => false;

        public Stream CreateReadStream()
        {
            EnsureResultBytesCreated();
            return new MemoryStream(_resultBytes);
        }

        private void EnsureResultBytesCreated()
        {
            if (_resultBytes != null)
            {
                return;
            }

            _resultBytes = CreateResultBytes();
            _lastModified = DateTimeOffset.UtcNow;
        }

        private byte[] CreateResultBytes()
        {
            var fileText = File.ReadAllText(_filePath);

            if (!string.IsNullOrEmpty(_swaggerUiOptions.Title))
            {
                fileText = Regex.Replace(fileText, @"<title>[^<]*</title>", $"<title>{_swaggerUiOptions.Title}</title>");
            }
            if (!string.IsNullOrEmpty(_swaggerUiOptions.Url))
            {
                fileText = Regex.Replace(fileText, @"url:.*", $"url: window.location.origin + '{_swaggerUiOptions.Url}',");
            }
            foreach (var cssUrl in _swaggerUiOptions.CustomCssUrls)
            {
                fileText = Regex.Replace(fileText, @"</body>", $"<link rel=\"stylesheet\" href=\"{cssUrl}\">\n</body>");
            }
            if (!string.IsNullOrEmpty(_swaggerUiOptions.FaviconUrl))
            {
                fileText = Regex.Replace(fileText, @"\./favicon[^.]*.png", _swaggerUiOptions.FaviconUrl);
            }

            return Encoding.UTF8.GetBytes(fileText);
        }
    }
}
