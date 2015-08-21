using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SampleLibrary
{
    public class DataController : ApiController
    {
        private readonly string _dependency;

        public DataController(Func<string> dependency)
        {
            _dependency = dependency();
        }
        public IEnumerable<string> Get()
        {
            return new String[] { "hello", "world", _dependency };
        }
    }
}
