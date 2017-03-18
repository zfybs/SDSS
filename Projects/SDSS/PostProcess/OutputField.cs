namespace SDSS.PostProcess
{
    /// <summary>
    /// 输出结果的类型，比如轴力、弯矩等
    /// </summary>
    internal static class OutputField
    {
        public enum OutputFieldType
        {
            Displacement1,
            Displacement2,

            /// <summary>  水平与竖向位移的合成量 </summary>
            DisplacementM,
            AxialForce,
            ShearForce,
            BendingMoment,
        }

        /// <summary>  每一种输出类型在 Abaqus 中所对应的 Field 的名称 </summary>
        /// <param name="type"></param>
        public static string GetOutputFieldName(OutputFieldType type)
        {
            switch (type)
            {
                case OutputFieldType.Displacement1:
                    return "U1";
                case OutputFieldType.Displacement2:
                    return "U2";
                case OutputFieldType.DisplacementM:
                    return "UM";
                case OutputFieldType.AxialForce:
                    return "SF1";
                case OutputFieldType.ShearForce:
                    return "SF2";
                case OutputFieldType.BendingMoment:
                    return "SM";
                default:
                    return "UM";
            }
        }
        /// <summary>  每一种输出类型在 Abaqus 中所对应的 Field 的中文描述 </summary>
        /// <param name="type"></param>
        public static string GetOutputFieldDescription(OutputFieldType type)
        {

            switch (type)
            {
                case OutputFieldType.Displacement1:
                    return "水平位移";
                case OutputFieldType.Displacement2:
                    return "竖向位移";
                case OutputFieldType.DisplacementM:
                    return "总位移";
                case OutputFieldType.AxialForce:
                    return "轴力";
                case OutputFieldType.ShearForce:
                    return "剪力";
                case OutputFieldType.BendingMoment:
                    return "弯矩";
                default:
                    return "总位移";
            }
        }

        /// <summary>  每一种输出类型在 Abaqus 中所对应的 Field 的名称 </summary>
        /// <param name="outputFieldTypeName"></param>
        /// <returns>如果未匹配到，则返回 null </returns>
        public static OutputFieldType? GetOutputField(string outputFieldTypeName)
        {
            if (outputFieldTypeName == "U1")
            {
                return OutputFieldType.Displacement1;
            }
            else if (outputFieldTypeName == "U2")
            {
                return OutputFieldType.Displacement2;
            }
            else if (outputFieldTypeName == "UM")
            {
                return OutputFieldType.DisplacementM;
            }
            else if (outputFieldTypeName == "SF1")
            {
                return OutputFieldType.AxialForce;
            }
            else if (outputFieldTypeName == "SF2")
            {
                return OutputFieldType.ShearForce;
            }
            else if (outputFieldTypeName == "SM")
            {
                return OutputFieldType.BendingMoment;
            }
            else
            {
                return null;
            }
        }
    }
}