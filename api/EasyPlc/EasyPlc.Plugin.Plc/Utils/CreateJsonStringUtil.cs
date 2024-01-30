/*=============================================================================================
*
*      *******    *******         **    **
*      **         **              **    **
*      **         **              **    **
*      *******    *******   **    ********
*           **    **              **    **
*           **    **              **    **
*      *******    **              **    **
*
* 创建者：韩顺发
* CLR版本：4.0.30319.42000
* 电子邮箱：shunfa.han@kstopa.com.cn
* 创建时间：2023/12/8 11:17:50
* 版本：v1.0.0
* 描述：
*
* ==============================================================================================
* 修改人：
* 修改时间：
* 修改说明：
* 版本：
*
===============================================================================================*/

namespace EasyPlc.Plugin.Plc.Utils;

/// <summary>
/// 创建JsonString工具类
/// </summary>
public static class CreateJsonStringUtil
{
    /// <summary>
    /// 根据PLC信息创建实时数据Json
    /// </summary>
    /// <param name="plc"></param>
    /// <returns></returns>
    public static string CreateRealtimeDataJsonString(this SiemensPlcInfo plc, EnumBody enumBody = EnumBody.ReadBody)
    {
        StringBuilder builder = new StringBuilder();

        int nIndentSpaces = 0;
        var nIS = nIndentSpaces;
        var strIs = GetIndentSpaces(nIS);
        //{
        var strIs_1 = strIs;
        builder.AppendLine(strIs_1 + "{");
        //  {
        nIS += 4;
        //var strIs_2 = GetIndentSpaces(nIS);
        ////PLC基础信息
        //builder.AppendLine(strIs_2 + $"\"PlcInfo\": {{");
        //nIS += 4;
        //var strIs_3 = GetIndentSpaces(nIS);
        //builder.AppendLine(strIs_3 + $"\"Name\": \"{plc.Name}\",");
        //builder.AppendLine(strIs_3 + $"\"IP\": \"{plc.IP}\",");
        //builder.AppendLine(strIs_3 + $"\"Port\": {plc.Port},");
        //builder.AppendLine(strIs_3 + $"\"Rack\": {plc.Rack},");
        //builder.AppendLine(strIs_3 + $"\"Slot\": {plc.Slot},");
        //builder.AppendLine(strIs_3 + $"\"Version\": \"{plc.Version}\",");

        //builder.AppendLine(strIs_3 + $"\"ReadTime\": \"{plc.PI.ReadTime.ToString("yy-MM-dd HH:mm:ss ffff")}\",");
        //plc.PI.SendTime = DateTime.Now;
        //builder.AppendLine(strIs_3 + $"\"SendTime\": \"{plc.PI.SendTime.ToString("yy-MM-dd HH:mm:ss ffff")}\"");
        ////  }
        //builder.AppendLine(strIs_2 + "},");
        ////公共区读内容
        //nIS -= 4;
        var strIs_4 = GetIndentSpaces(nIS);
        builder.AppendLine(
            enumBody == EnumBody.ReadBody ? plc.PI.ReadInfo.ListPr.CreateJson4TreeList(strIs_4) : plc.PI.WriterInfo.ListPr.CreateJson4TreeList(strIs_4)
            );

        //}
        builder.AppendLine(strIs_1 + "}");

        return builder.ToString();
    }

    /// <summary>
    /// 根据PLC信息创建事件数据Json
    /// </summary>
    /// <param name="plc"></param>
    /// <returns></returns>
    public static string CreateEventDataJsonString(this SiemensPlcInfo plc, int eventIdx, EnumBody enumBody = EnumBody.ReadBody)
    {
        StringBuilder builder = new StringBuilder();

        int nIndentSpaces = 0;
        var nIS = nIndentSpaces;
        var strIs = GetIndentSpaces(nIS);
        //{
        var strIs_1 = strIs;
        builder.AppendLine(strIs_1 + "{");
        //  {
        nIS += 4;
        //var strIs_2 = GetIndentSpaces(nIS);
        ////PLC基础信息
        //builder.AppendLine(strIs_2 + $"\"PlcInfo\": {{");
        //nIS += 4;
        //var strIs_3 = GetIndentSpaces(nIS);
        //builder.AppendLine(strIs_3 + $"\"Name\": \"{plc.Name}\",");
        //builder.AppendLine(strIs_3 + $"\"IP\": \"{plc.IP}\",");
        //builder.AppendLine(strIs_3 + $"\"Port\": {plc.Port},");
        //builder.AppendLine(strIs_3 + $"\"Rack\": {plc.Rack},");
        //builder.AppendLine(strIs_3 + $"\"Slot\": {plc.Slot},");
        //builder.AppendLine(strIs_3 + $"\"Version\": \"{plc.Version}\",");

        //builder.AppendLine(strIs_3 + $"\"ReadTime\": \"{plc.EIs[eventIdx].ReadTime.ToString("yy-MM-dd HH:mm:ss ffff")}\",");
        //plc.EIs[eventIdx].SendTime = DateTime.Now;//发送时间赋值
        //builder.AppendLine(strIs_3 + $"\"SendTime\": \"{plc.EIs[eventIdx].SendTime.ToString("yy-MM-dd HH:mm:ss ffff")}\"");
        ////  }
        //builder.AppendLine(strIs_2 + "},");
        ////事件区读内容
        //nIS -= 4;
        var strIs_4 = GetIndentSpaces(nIS);
        builder.AppendLine(
            enumBody == EnumBody.ReadBody ? plc.EIs[eventIdx].ReadInfo.ListPr.CreateJson4TreeList(strIs_4) : plc.EIs[eventIdx].WriteInfo.ListPr.CreateJson4TreeList(strIs_4));

        //}
        builder.AppendLine(strIs_1 + "}");

        return builder.ToString();
    }

    private static string GetIndentSpaces(int num)
    {
        if (num <= 0)
            return "";
        else
        {
            string str = "";
            for (int i = 0; i < num; i++)
            {
                str += " ";
            }
            return str;
        }
    }

    private static string CreateJson4TreeList(this List<PlcResource> resources, string space = "")
    {
        StringBuilder builder = new StringBuilder();
        resources.ForEach(p =>
        {
            var douhao = resources.LastOrDefault() != p ? "," : "";
            if (p.Category == "STRUCTDATA")
            {
                //有可能是结构数组内部结构
                if (p.Code.Contains("[") && p.Code.Contains("]"))
                {
                    builder.AppendLine(space + $"{{");
                }
                else
                {
                    builder.AppendLine(space + $"\"{p.Code}\": {{");
                }

                if (p.Children.Count > 0)
                {
                    builder.AppendLine(p.Children.CreateJson4TreeList(space + "    "));
                }
                builder.AppendLine(space + $"}}{douhao}");
            }
            else if (p.Category == "ARRDATA")
            {
                builder.AppendLine(space + $"\"{p.Code}\": [");
                if (p.Children.Count > 0)
                {
                    builder.AppendLine(p.Children.CreateJson4TreeList(space + "    "));
                }
                builder.AppendLine(space + $"]{douhao}");
            }
            else if (p.Category == "BASEDATA")
            {
                string strValue = string.Empty;
                switch (p.ValueType)
                {
                    case "Int16" or "Int32" or "Float":
                        strValue = p.Value + "";
                        break;

                    case "Bool[]" or "Int16[]" or "Int32[]" or "Float[]":
                        {
                            var arr = p.Value as Array;
                            string v = string.Empty;
                            v += "[";
                            for (int i = 0; i < arr.Length; i++)
                            {
                                if (i != 0) v += ", ";
                                if (p.ValueType == "Bool[]")
                                {
                                    v += arr.GetValue(i)?.ToString()?.ToLower();
                                }
                                else
                                {
                                    v += arr.GetValue(i);
                                }
                            }
                            v += "]";
                            strValue = v;
                            break;
                        }
                    case "String" or "WString":
                        strValue = "\"" + p.Value + "\"";
                        break;
                }
                builder.AppendLine(space + $"\"{p.Code}\": {strValue}{douhao}");
            }
            //加添逗号
        });
        return builder.ToString();
    }

    public enum EnumBody
    {
        ReadBody = 1,
        WriteBody
    }
}