using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSS.PostProcess
{
    /// <summary> Result 文本文件中，每一个数据项，整个 Result 文本就是由大量的这些数据项组合起来的 </summary>
    public class ResultFileItem
    {
        public string Name { get; private set; }
        public ResultValueType ValueType { get; private set; }
        public object Value { get; private set; }

        /// <summary> 对此项结果的描述，比如“整个矩形框架中的每一层的最大位移” </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="valueType"></param>
        /// <param name="value"></param>
        /// <param name="description">对此项结果的描述，比如“整个矩形框架中的每一层的最大位移”</param>
        public ResultFileItem(string name, ResultValueType valueType, object value, string description)
        {
            Name = name;
            ValueType = valueType;
            Value = value;
            Description = description;
        }

        /// <summary> 将此项数据转换为对应的字符 </summary>
        /// <returns></returns>
        public string GetValueString()
        {
            switch (ValueType)
            {
                case ResultValueType.String:
                    return Value.ToString();

                case ResultValueType.Number:
                    return Value.ToString();

                case ResultValueType.Vector:
                    var value = Value as double[];
                    string v = value[0].ToString();
                    for (int i = 1; i < value.Length; i++)
                    {
                        v += "\t" + value[i].ToString();
                    }
                    return v;

                case ResultValueType.Array2D:

                    var value2d = Value as double[,];
                    var sb = new StringBuilder();
                    string s = "";
                    for (int r = 0; r < value2d.GetLength(0); r++)
                    {
                        s = value2d[r, 0].ToString();
                        for (int c = 1; c < value2d.GetLength(1); c++)
                        {
                            s += "\t" + value2d[r, c].ToString();
                        }
                        sb.AppendLine(s);
                    }
                    return sb.ToString();
            }
            return Value.ToString();
        }

        public override string ToString()
        {
            return this.Description;
        }
    }

    /// <summary> Result 文本文件中，不同的数据类型 </summary>
    public enum ResultValueType
    {
        /// <summary> 一个字符串 </summary>
        @String = 0,
        /// <summary> 一个单独的数值，比如 -12.3 </summary>
        Number,
        /// <summary> 一个行向量，比如 [9.0, 15.0, 0.0]，此向量中至少有两个元素 </summary>
        Vector,
        /// <summary> 一个二维数组，至少有两行，而且在 Result 文本文件中，每一行所对应的行向量的元素个数相同。 比如 
        /// [9.0, 4.0, 0.0]
        /// [9.0, 9.0, 0.0]
        /// </summary>
        Array2D,
    }
}