using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDSS.Models;

namespace SDSS.PostProcess
{
    internal class Result
    {
        public string ModelName;
        public Dictionary<string, ResultFileItem> Items;

        public Result(string modelName, Dictionary<string, ResultFileItem> items)
        {
            ModelName = modelName;
            Items = items;
        }
    }
}
