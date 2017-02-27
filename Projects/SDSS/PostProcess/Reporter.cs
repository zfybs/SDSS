using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using SDSS.Utility;

namespace SDSS.PostProcess
{
    internal class Reporter : WordWriter
    {
        private Range _range;

        public Reporter() : base()
        {
        }

        public override bool NewDocument(string templatePath = null)
        {
            bool succ = base.NewDocument(templatePath);
            if (succ)
            {
                _range = Doc.Range(0, 0);
            }
            return succ;
        }
        
    }
}
