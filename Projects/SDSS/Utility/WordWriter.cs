using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace SDSS.Utility
{
    internal class WordWriter : IDisposable
    {
        private Document _doc;
        private Application _app;

        #region ---   构造函数

        public WordWriter()
        {
            _app = new Application
            {
                Visible = false
            };
        }

        ~WordWriter()
        {
            Dispose();
        }

        public void Dispose()
        {
            try
            {
                if (_doc != null)
                {
                    _doc.Close();
                }
                if (_app != null)
                {
                    _app.Quit();
                }
            }
            catch (Exception)
            {

                // ignored;
            }

        }

        #endregion

        #region ---   Application 操作

        public void SetVisibility(bool visible)
        {
            _app.Visible = visible;
        }

        #endregion

        #region ---   Document 操作

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templatePath"> word 模块的名称，空则表示默认的 Normal 模板。</param>
        public void NewDocument(string templatePath = null)
        {
            if (!File.Exists(templatePath))
            {
                templatePath = "";
            }
            if (_doc != null)
            {
                _doc.Close(SaveChanges: false);
            }
            _doc = _app.Documents.Add(Template: templatePath);
        }

        public void SaveDocument(string filePath)
        {
            if (_doc != null)
            {
                _doc.SaveAs(FileName: filePath);
            }
        }

        #endregion
    }
}
