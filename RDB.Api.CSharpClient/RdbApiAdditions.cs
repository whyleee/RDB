using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RDB
{
    // Custom additions to RdbApi.
    public partial class RdbApi
    {
        /// <summary>
        /// Initializes a new instance of the RdbApi class.
        /// </summary>
        /// <param name="httpClient">HttpClient to be used</param>
        /// <param name="disposeHttpClient">true: Will dispose the supplied httpClient on calling Dispose(). False: will not dispose</param>
        public RdbApi(HttpClient httpClient, bool disposeHttpClient = true) : base(httpClient, disposeHttpClient)
        {
            Initialize();
        }
    }
}
