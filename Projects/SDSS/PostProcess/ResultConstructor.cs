using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using eZstd.Enumerable;

namespace SDSS.PostProcess
{
    /// <summary> 从 Abaqus 计算的结果文件中构造出各种不同的 Result 对象 </summary>
    internal static class ResultConstructor
    {

        public static Result ReadResult(string resultFilePath)
        {
            Result res = null;
            using (var sr = new StreamReader(resultFilePath))
            {
                var dict = ReadDictionary(sr);

                //var s = _sr.ReadLine();
                //MessageBox.Show(s);

                res = new Result(modelName: @"此结果对象的名称暂时还是随意设定的啦", items: dict);
            }
            return res;
        }

        #region ---   将 Result 文本的数据构造成一个 Dictionary

        private static Dictionary<string, ResultFileItem> ReadDictionary(StreamReader sr)
        {
            var dict = new Dictionary<string, ResultFileItem>();
            string strLine = sr.ReadLine();
            string pattern = @"T \* (.*) \* (.*) \* (.*)"; //  以 “liner-组名-”开头
            //

            Match match = Regex.Match(strLine, pattern, RegexOptions.IgnoreCase);
            while (match.Success)
            {
                string keyName = match.Groups[1].Value;
                ResultValueType valueType = GetResultValueType(match.Groups[2].Value);
                string description = match.Groups[3].Value;

                //
                switch (valueType)
                {
                    case ResultValueType.Vector:
                        double[] vec = null;
                        strLine = GetVector(sr, out vec);
                        dict.Add(keyName, new ResultFileItem(keyName, ResultValueType.Vector, value: vec, description: description));
                        break;
                    case ResultValueType.Number:
                        double num = 0;
                        strLine = GetNumber(sr, out num);
                        dict.Add(keyName, new ResultFileItem(keyName, ResultValueType.Number, value: num, description: description));
                        break;
                    case ResultValueType.Array2D:
                        double[,] arr2D;
                        strLine = GetArray2D(sr, out arr2D);
                        dict.Add(keyName, new ResultFileItem(keyName, ResultValueType.Array2D, value: arr2D, description: description));
                        break;
                    case ResultValueType.String:
                        string str;
                        strLine = GetString(sr, out str);
                        dict.Add(keyName, new ResultFileItem(keyName, ResultValueType.String, value: str, description: description));
                        break;
                }
                if (sr.EndOfStream) { break; }
                match = Regex.Match(strLine, pattern, RegexOptions.IgnoreCase);
            }
            return dict;
        }

        #region ---   读取不同类型的数据

        /// <summary> 从一行文本中提取一个单一的数值 </summary>
        /// <remarks></remarks>
        private static string GetString(StreamReader sr, out string str)
        {
            //
            string strLine = sr.ReadLine(); // 大致的结构为： 0.00529011
            str = strLine;
            //
            strLine = sr.ReadLine(); // 大致的结构为：T * maxLayerX * vector * 
            return strLine;
        }
        /// <summary> 从一行文本中提取一个单一的数值 </summary>
        /// <remarks></remarks>
        private static string GetNumber(StreamReader sr, out double number)
        {
            //
            string strLine = sr.ReadLine(); // 大致的结构为： 0.00529011
            number = double.Parse(strLine);
            //
            strLine = sr.ReadLine(); // 大致的结构为：T * maxLayerX * vector * 
            return strLine;
        }

        /// <summary> 提取一维向量数据 </summary>
        /// <remarks></remarks>
        private static string GetVector(StreamReader sr, out double[] array1D)
        {
            //
            string strLine = sr.ReadLine(); // 大致的结构为： [8.6999998, 9.0, 0.0]
            array1D = GetNumVector(strLine);
            //
            strLine = sr.ReadLine(); // 大致的结构为：T * maxLayerX * vector * 
            return strLine;
        }

        /// <summary> 提取二维数组数据 </summary>
        /// <remarks></remarks>
        private static string GetArray2D(StreamReader sr, out double[,] array2D)
        {
            List<double[]> arrList = new List<double[]>();
            //
            string strLine = sr.ReadLine(); // 大致的结构为： [8.6999998, 9.0, 0.0]
            double[] vector = GetNumVector(strLine);
            while (vector != null)
            {
                arrList.Add(vector);
                //
                strLine = sr.ReadLine();
                vector = GetNumVector(strLine);
            }
            array2D = ArrayConstructor.FromList2D(arrList);
            return strLine;
        }
        #endregion

        /// <summary> 对 [8.6999998, 9.0, 0.0] 这种格式的字符进行处理，得到其中对应的数值向量 </summary>
        /// <param name="line">这一行中的数值至少要有2个</param>
        /// <returns>如果不能正常地提取出数值，则返回 null </returns>
        private static double[] GetNumVector(string line)
        {
            double[] value = null;
            if (line != null)
            {
                var nums = line.Split(',');
                if (nums.Length > 1)
                {
                    value = new double[nums.Length];

                    // 对第一个数值与最后一个数值进行特殊处理
                    value[0] = double.Parse(nums[0].Substring(1));
                    for (int i = 1; i < nums.Length - 1; i++)
                    {
                        value[i] = double.Parse(nums[i]);
                    }
                    var ss = nums[nums.Length - 1];

                    value[nums.Length - 1] = double.Parse(ss.Substring(0, ss.Length - 1));
                }
            }
            return value;
        }

        private static ResultValueType GetResultValueType(string typeName)
        {
            if (string.Compare(typeName, "vector") == 0)
            {
                return ResultValueType.Vector;
            }
            else if (string.Compare(typeName, "number") == 0)
            {
                return ResultValueType.Number;
            }
            else if (string.Compare(typeName, "array2d") == 0)
            {
                return ResultValueType.Array2D;
            }
            // 默认项
            return ResultValueType.String;
        }
        #endregion
    }
}