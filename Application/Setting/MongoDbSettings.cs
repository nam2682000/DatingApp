using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Setting
{
    public class MongoDbSettings
    {
        public required string MongoDB { get; set; }
        public required string DatabaseName { get; set; }
    }
}