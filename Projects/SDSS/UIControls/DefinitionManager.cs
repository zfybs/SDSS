using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using SDSS.Definitions;
using SDSS.Utility;

namespace SDSS.UIControls
{
    /// <summary> 对整个系统中所有的土层或者桩截面进行管理 </summary>
    /// <typeparam name="T"></typeparam>
    public partial class DefinitionManager<T> : Form where T : Definition, new()
    {
        private readonly BindingList<T> _definitions;

        /// <summary>构造函数</summary>
        /// <param name="definitions"> 进行管理的集合</param>
        public DefinitionManager(IList<T> definitions)
        {
            InitializeComponent();
            //
            _definitions = new BindingList<T>(definitions);

            // 绑定到集合
            listBox1.DisplayMember = "Name";
            listBox1.DataSource = _definitions;
        }

        #region ---   窗口的打开与关闭

        private void DefinitionManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region ---   添加、移除 与 编辑

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //
            object newDefIns = GetInitialDefinitionObject();
            EditDefinition formAddDefinition = new EditDefinition(newDefIns);
            //
            var res = formAddDefinition.ShowDialog();
            T def = (T)formAddDefinition.Instance;
            if (res == DialogResult.OK)
            {
                StringBuilder errorMessage = new StringBuilder();
                bool succ = AddDefinition(def, ref errorMessage);
                if (!succ)
                {
                    MessageBox.Show(errorMessage.ToString(), "添加参数定义出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary> 构造一个默认的定义对象 </summary>
        private Definition GetInitialDefinitionObject()
        {
            Definition d;
            if (typeof(T) == typeof(Profile))
            {
                d = new Rectangular("Rec1", 1, 0.8);
            }
            else if (typeof(T) == typeof(Material))
            {
                d = new Material("Mat1", density: 1.9e3, elastic: 200e6, poissonRatio: 0.3, type: MaterialType.Elastic);
            }
            else
            {
                throw new ArgumentException();
            }
            return d;
        }


        /// <summary> 添加一个定义到总的定义集合中去 </summary>
        /// <param name="def"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool AddDefinition(Definition def, ref StringBuilder errorMessage)
        {
            if (CheckAddDefinition(_definitions, def, ref errorMessage))
            {
                _definitions.Add((T)def);
                return true;
            }
            return false;
        }

        /// <summary> 检查添加后的定义集合是否符合命名的唯一性规范 </summary>
        private bool CheckAddDefinition(BindingList<T> originaldefinitions, Definition defToAdd, ref StringBuilder errorMessage)
        {
            if (string.IsNullOrEmpty(defToAdd.Name))
            {
                errorMessage.AppendLine( "必须为添加的参数定义指定一个名称。");
                return false;
            }
            if (sdUtils.StringHasNonEnglish(defToAdd.Name))
            {
                errorMessage .AppendLine( "定义的命名不能包含非英文的字符。");
                return false;
            }
            // 检查有没有重复的名称
            int namesCount = originaldefinitions.Count + 1; // 如果添加成功的话，那整个集合中应该有这么多名称
            SortedSet<string> names = new SortedSet<string>();
            foreach (var def in originaldefinitions)
            {
                names.Add(def.Name);
            }
            names.Add(defToAdd.Name);
            if (names.Count < namesCount)
            {
                errorMessage.AppendLine("新添加的参数定义与现有集合中的定义重名。");
                return false;
            }
            errorMessage.AppendLine("成功");
            return true;
        }

        #region ---   编辑

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                EditItem((T)listBox1.SelectedItem);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                EditItem((T)listBox1.SelectedItem);
            }
        }

        private void EditItem(T item)
        {
            T itemClone = (T)item.Clone();
            //
            EditDefinition formAddDefinition = new EditDefinition(itemClone);
            var res = formAddDefinition.ShowDialog();
            if (res == DialogResult.OK)
            {
                string errorMessage;
                if (CheckEditDefinition(_definitions, item, itemClone, out errorMessage))
                {
                    _definitions[listBox1.SelectedIndex] = (T)formAddDefinition.Instance;
                }
                else
                {
                    MessageBox.Show(errorMessage, "编辑参数定义出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary> 检查编辑后的定义集合是否符合命名的唯一性规范 </summary>
        /// <param name="originaldefinitions"></param>
        /// <param name="defToEdit">被编辑的定义，此定义是位于 originaldefinitions 集合中的</param>
        /// <param name="editedDef">被编辑后的新的定义</param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool CheckEditDefinition(BindingList<T> originaldefinitions, Definition defToEdit, Definition editedDef,
            out string errorMessage)
        {
            if (string.IsNullOrEmpty(editedDef.Name))
            {
                errorMessage = "必须为当前参数定义指定一个名称。";
                return false;
            }
            // 先将新名称替换掉旧名称
            var index = originaldefinitions.IndexOf((T)defToEdit);
            var namesT = originaldefinitions.Select(r => r.Name).ToList();
            namesT[index] = editedDef.Name;
            //

            // 检查有没有重复的名称
            SortedSet<string> names = new SortedSet<string>();

            foreach (var n in namesT)
            {
                names.Add(n);
            }
            if (names.Count < namesT.Count)
            {
                errorMessage = "新添加的参数定义与现有集合中的定义重名。";
                return false;
            }
            errorMessage = "成功";
            return true;
        }

        #endregion

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                _definitions.RemoveAt(listBox1.SelectedIndex);
            }
        }

        #endregion

        #region ---   导入定义

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (typeof(T) == typeof(Profile))
            {
                string filePath = sdUtils.ChooseOpenProfiles("导入截面");
                if (filePath != null)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    bool succeeded;
                    var profiles = sdUtils.ImportFromXml(filePath, typeof(List<Profile>),
                        out succeeded, ref errorMessage) as List<Profile>;
                    if (succeeded)
                    {
                        foreach (Profile p in profiles)
                        {
                            try
                            {
                                bool succ = AddDefinition(p, ref errorMessage);
                                if (!succ)
                                {
                                    MessageBox.Show(errorMessage.ToString(), "添加参数定义出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            else if (typeof(T) == typeof(Material))
            {
                string filePath = sdUtils.ChooseOpenMaterials("导入材料");
                if (filePath != null)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    bool succeeded;
                    var materials = sdUtils.ImportFromXml(filePath, typeof(List<Material>),
                        out succeeded, ref errorMessage) as List<Material>;
                    if (succeeded)
                    {
                        foreach (Material m in materials)
                        {
                            try
                            {
                                bool succ = AddDefinition(m, ref errorMessage);
                                if (!succ)
                                {
                                    MessageBox.Show(errorMessage.ToString(), "添加参数定义出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        #endregion

        #region ---   导出定义

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (typeof(T) == typeof(Profile))
            {
                string filePath = sdUtils.ChooseSaveProfiles("导出截面");
                if (filePath != null)
                {
                    bool succ = ExportToXml(filePath, _definitions.ToList() as List<Profile>);
                    if (succ)
                    {
                    }
                }
            }
            else if (typeof(T) == typeof(Material))
            {
                string filePath = sdUtils.ChooseSaveMaterials("导出材料");
                if (filePath != null)
                {
                    bool succ = ExportToXml(filePath, _definitions.ToList() as List<Material>);
                    if (succ)
                    {
                    }
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private bool ExportToXml(string xmlFilePath, IEnumerable<Definition> definitions)
        {
            StreamWriter fs = null;
            try
            {
                Type tp = definitions.GetType();

                fs = new StreamWriter(xmlFilePath, append: false);
                XmlSerializer s = new XmlSerializer(tp);
                s.Serialize(fs, definitions);
                //
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        #endregion
    }
}