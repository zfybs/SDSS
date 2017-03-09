using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSS.PostProcess
{
    internal class Result
    {
        public string Name;
        public Dictionary<string, ResultItem> Items;
        public Result(string name, Dictionary<string, ResultItem> items)
        {
            Name = name;
            Items = items;
        }
    }
}
