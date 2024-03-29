﻿/*=============================================================================================
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
* 创建时间：2023/11/23 9:52:37
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


using AngleSharp.Css.Values;
using HslCommunication;
using HslCommunication.Core;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Org.BouncyCastle.Utilities;
using System.Resources;

namespace EasyPlc.Plugin.Plc.Utils;

/// <summary>
/// 西门子PLC地址映射工具
/// </summary>
public static class SiemensPaseUtil
{
    public static List<PlcResource> ReadTree(this List<PlcResource> resources, byte[] buffer)
    {
        int startIndex = 0;
        return resources.ReadTreeWithChildren(buffer,ref startIndex, new ReverseBytesTransform());
    }
    public static List<PlcResource> ReadTreeWithChildren(this List<PlcResource> resources, byte[] buffer, ref int startIndex, IByteTransform byteTransform)
    {
        if(resources != null && resources.Count > 0)
        {
            for (int i = 0; i < resources.Count; i++)
            {
                var r = resources[i];
                if (r.Category == "BASEDATA")
                {
                    int len = r.ByteCount;//所暂用的字节数量
                    if (r.ValueType.ToLower() == "byte")
                    {
                        r.Value = buffer[startIndex];
                    }
                    else if (r.ValueType.ToLower() == "byte[]")
                    {
                        buffer.SelectMiddle(startIndex, len);
                    }
                    else if (r.ValueType.ToLower() == "bool")
                    {
                        r.Value = byteTransform.TransBool(buffer, startIndex * 8);
                    }
                    else if (r.ValueType.ToLower() == "bool[]")
                    {
                        r.Value = byteTransform.TransBool(buffer, startIndex * 8, len * 8);
                    }
                    else if (r.ValueType.ToLower() == "int16")
                    {
                        r.Value = byteTransform.TransInt16(buffer, startIndex);
                    }
                    else if (r.ValueType.ToLower() == "int16[]")
                    {
                        r.Value = byteTransform.TransInt16(buffer, startIndex, len / 2);
                    }
                    else if (r.ValueType.ToLower() == "uint16")
                    {
                        r.Value = byteTransform.TransUInt16(buffer, startIndex);
                    }
                    else if (r.ValueType.ToLower() == "uint16[]")
                    {
                        r.Value = byteTransform.TransUInt16(buffer, startIndex, len / 2);
                    }
                    else if (r.ValueType.ToLower() == "int32")
                    {
                        r.Value = byteTransform.TransInt32(buffer, startIndex);
                    }
                    else if (r.ValueType.ToLower() == "int32[]")
                    {
                        r.Value = byteTransform.TransInt32(buffer, startIndex, len / 4);
                    }
                    else if (r.ValueType.ToLower() == "uint32")
                    {
                        r.Value = byteTransform.TransUInt32(buffer, startIndex);
                    }
                    else if (r.ValueType.ToLower() == "uint32[]")
                    {
                        r.Value = byteTransform.TransUInt32(buffer, startIndex, len / 4);
                    }
                    else if (r.ValueType.ToLower() == "float")
                    {
                        r.Value = byteTransform.TransSingle(buffer, startIndex);
                    }
                    else if (r.ValueType.ToLower() == "float[]")
                    {
                        r.Value = byteTransform.TransSingle(buffer, startIndex, len / 4);
                    }
                    else if (r.ValueType.ToLower() == "string")
                    {
                        int read_len = buffer[startIndex + 1];

                        string s = byteTransform.TransString(buffer, startIndex + 2, read_len, Encoding.ASCII);
                        r.Value = s;
                    }
                    else if (r.ValueType.ToLower() == "wstring")
                    {
                        int read_len = buffer[startIndex + 2] << 8 + buffer[startIndex + 3];

                        string s = byteTransform.TransString(buffer, startIndex + 4, read_len, Encoding.Unicode);
                        r.Value = s;
                    }
                    //........后续新增
                    startIndex += len;
                }
                else if(r.Category == "STRUCTDATA" || r.Category == "ARRDATA")//结构体
                {
                    //准备递归
                    if (r.Children != null && r.Children.Count > 0)
                    {
                        r.Children.ReadTreeWithChildren(buffer, ref startIndex, byteTransform);
                    }
                }
            }
        }

        return resources;
    }

    public static byte[] WriteBuffer(this List<PlcResource> resources)
    {
        return resources.WriteBufferWithTree( new ReverseBytesTransform()).ToArray();
    }
    public static List<byte> WriteBufferWithTree(this List<PlcResource> resources, IByteTransform byteTransform)
    {
        var list = new List<byte>();
        if (resources != null || resources.Count > 0)
        {
            for (int i = 0; i < resources.Count; i++)
            {
                var r = resources[i];

                if (r.Category == "STRUCTDATA")//结构体
                {
                    continue;
                }
                else if (r.Category == "ARRDATA")//数组
                {
                    //后续处理。。。
                }
                else if (r.Category == "BASEDATA")
                {
                    int len = r.ValueLength;
                    if (r.ValueType.ToLower() == "byte")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "byte[]")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value, 0, len));
                    }
                    else if (r.ValueType.ToLower() == "bool")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "bool[]")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "int16")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "int16[]")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "uint16")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "uint16[]")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "int32")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "int32[]")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "uint32")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "uint32[]")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "float")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "float[]")
                    {
                        list.AddRange(byteTransform.TransByte(r.Value));
                    }
                    else if (r.ValueType.ToLower() == "string")
                    {
                        //防呆处理，字符串长度大于设置值时，需要自动裁断，string类型不支持中文，中文使用WString类型，暂时没有该功能。即string 类型中传递中文PLC是获取不到的，
                        var strLen = r.Value.Length;
                        if (strLen > len - 2) strLen = len - 2;
                        var bs = byteTransform.TransByte(r.Value, strLen, Encoding.ASCII);
                        byte[] ret_bs = new byte[bs.Length + 2];
                        ret_bs[0] = (byte)(len - 2);
                        ret_bs[1] = (byte)strLen;

                        for (int j = 0; j < bs.Length; j++)
                        {
                            ret_bs[j + 2] = bs[j];
                        }
                        list.AddRange(ret_bs);
                    }
                    else if (r.ValueType.ToLower() == "wstring")
                    {
                        //防呆处理，字符串长度大于设置值时，需要自动裁断，string类型不支持中文，中文使用WString类型，暂时没有该功能。即string 类型中传递中文PLC是获取不到的，
                        var strLen = r.Value.Length;
                        if (strLen > (len - 4) / 2)
                        {
                            strLen = (len - 4) / 2;
                            r.Value = r.Value.Substring(0, strLen);
                        }
                        var bs = byteTransform.TransByte(r.Value, strLen * 2, Encoding.Unicode);
                        byte[] ret_bs = new byte[bs.Length + 4];
                        ret_bs[0] = (byte)(((len - 4) / 2) >> 8);
                        ret_bs[1] = (byte)((len - 4) / 2);
                        ret_bs[2] = (byte)(strLen >> 8);
                        ret_bs[3] = (byte)strLen;

                        for (int j = 0; j < bs.Length; j++)
                        {
                            if (j % 2 == 0)
                                ret_bs[j + 4] = bs[j + 1];
                            else
                                ret_bs[j + 4] = bs[j - 1];
                        }
                        list.AddRange(ret_bs);
                    }
                }
                //准备递归
                if (r.Children != null && r.Children.Count > 0)
                {
                    list.AddRange(r.Children.WriteBufferWithTree(byteTransform));
                }
            }
        }
        return list;
    }

    public static PlcResource GetResourceWithEventTrigger(this List<PlcResource> resources)
    {
        return resources.GetResourceWithKey("EventTrigger");
    }
    public static PlcResource GetResourceWithSequenceId(this List<PlcResource> resources)
    {
        return resources.GetResourceWithKey("SequenceID");
    }

    private static PlcResource GetResourceWithKey(this IReadOnlyList<PlcResource> resources, string key)
    {
        if(resources != null && resources.Count > 0)
        {
            for (int i = 0; i < resources.Count; i++)
            {
                if (resources[i].Code == key)
                {
                    return resources[i];
                }
                //递归查找
                var r = resources[i].Children.GetResourceWithKey(key);
                if(r != null) return r;
            }
            return null;
        }
        else
        {
            return null;
        }
    }
    private static dynamic GetDynamicWithKey(this IReadOnlyList<PlcResource> resources, string key)
    {
        if (resources != null && resources.Count > 0)
        {
            for (int i = 0; i < resources.Count; i++)
            {
                if (resources[i].Code == key)
                {
                    return resources[i].Value;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 通过Tree创建TypeObj对象
    /// </summary>
    /// <param name="resources"></param>
    /// <param name="objType"></param>
    public static object GetTypeObj4Tree(this List<PlcResource> resources, Type objType)
    {
        //创建实列对象
        var t = Activator.CreateInstance(objType);
        //获取实列对象属性
        var pis = objType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        for (int i =0; i < pis.Count(); i ++)
        {
            var pi = pis[i];
            var piName = pi.Name;
            if (
                pi.PropertyType == typeof(bool[])
                || pi.PropertyType == typeof(short)
                || pi.PropertyType == typeof(short[])
                || pi.PropertyType == typeof(int)
                || pi.PropertyType == typeof(int[])
                || pi.PropertyType == typeof(float)
                || pi.PropertyType == typeof(float[])
                || pi.PropertyType == typeof(string)
                )
            {
                pi.SetValue(t, resources.GetDynamicWithKey(piName));
            }
            else
            {
                //类对象
                //类属性
                if (pi.PropertyType.IsArray)//结构数组对象
                {
                    //数组Count
                    var arrCount = resources[i].Children.Count;
                    var classType = pi.PropertyType.GetElementType();
                    var arr = Array.CreateInstance(classType, arrCount);
                    for (int j = 0;j < arrCount; j ++)
                    {
                        arr.SetValue(resources[i].Children[j].Children.GetTypeObj4Tree(classType), j);
                    }
                    pi.SetValue(t, arr);
                }
                else//结构对象
                {
                    pi.SetValue(t, resources[0].Children.GetTypeObj4Tree(pi.PropertyType.GetTypeInfo()));
                }
            }
        }
        return t;
    }
}
