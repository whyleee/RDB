using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace RDB.Api.Business.Swagger
{
    internal class SwaggerUiFileProvider : IFileProvider
    {
        private readonly PhysicalFileProvider _fileProvider;
        private readonly SwaggerUiOptions _swaggerUiOptions;

        public SwaggerUiFileProvider(string root, SwaggerUiOptions swaggerUiOptions)
        {
            _fileProvider = new PhysicalFileProvider(root);
            _swaggerUiOptions = swaggerUiOptions;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (subpath == "/index.html")
            {
                var filePath = Path.Combine(_fileProvider.Root, subpath.TrimStart('/'));
                return new SwaggerUiIndexFileInfo(filePath, _swaggerUiOptions);
            }

            return _fileProvider.GetFileInfo(subpath);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return _fileProvider.GetDirectoryContents(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return _fileProvider.Watch(filter);
        }
    }
}
