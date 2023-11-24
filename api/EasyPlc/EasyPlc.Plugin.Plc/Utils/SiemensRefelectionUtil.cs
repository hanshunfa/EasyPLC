using HslCommunication.Core;
using HslCommunication;

namespace EasyPlc.Plugin.Plc;

public static class SiemensRefelectionUtil
{
    public static T CreateObject1<T>(byte[] buffer, ref int startIndex, IByteTransform byteTransform) where T : class, new()
    {
        return (T)CreateObjectWithType(typeof(T), buffer, ref startIndex, byteTransform);
    }
    /// <summary>
    /// 创建对象，根据Type
    /// </summary>
    /// <param name="obj_type"></param>
    /// <param name="buffer"></param>
    /// <param name="startIndex"></param>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    public static object CreateObjectWithType(Type obj_type, byte[] buffer, ref int startIndex, IByteTransform byteTransform)
    {
        object t = null;

        PropertyInfo[] pis = obj_type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        t = Activator.CreateInstance(obj_type);

        foreach (var pi in pis)
        {
            object[] customAttributes = pi.GetCustomAttributes(typeof(KstopaStructAttribute), inherit: false);
            if (customAttributes.Length > 0)
            {
                //类属性
                if (pi.PropertyType.IsArray)
                {
                    //数组对象
                    //获取特性里面数组个数
                    KstopaStructAttribute kstopaStructAttribute = (KstopaStructAttribute)customAttributes[0];
                    int count = kstopaStructAttribute.Count;
                    object[] ret_obj = new object[4] { count, buffer, startIndex, byteTransform };
                    object ret_value = null;

                    string ret = RefelectionCallStaticMethod(typeof(SiemensRefelectionUtil), "CreateArraryObject", ref ret_obj, ref ret_value, pi.PropertyType.GetElementType());
                    startIndex = (int)ret_obj[2];
                    pi.SetValue(t, ret_value);
                }
                else
                {
                    object[] ret_obj = new object[3] { buffer, startIndex, byteTransform };
                    object ret_value = null;

                    RefelectionCallStaticMethod(typeof(SiemensRefelectionUtil), "CreateObject1", ref ret_obj, ref ret_value, pi.PropertyType.GetTypeInfo());
                    startIndex = (int)ret_obj[1];
                    pi.SetValue(t, ret_value);
                }
            }
            object[] customAttributes1 = pi.GetCustomAttributes(typeof(KstopaStructPropertyAttribute), inherit: false);
            if (customAttributes1.Length > 0)
            {

                KstopaStructPropertyAttribute kstopaStructPropertyAttribute = (KstopaStructPropertyAttribute)customAttributes1[0];
                int Length = kstopaStructPropertyAttribute.Lenght;


                // 1 获取属性类型
                Type propertyType = pi.PropertyType;
                if (propertyType == typeof(byte))
                {
                    pi.SetValue(t, buffer[startIndex], null);
                }
                else if (propertyType == typeof(byte[]))
                {
                    pi.SetValue(t, buffer.SelectMiddle(startIndex, Length), null);
                }
                else if (propertyType == typeof(short) || propertyType == typeof(Int16))
                {
                    pi.SetValue(t, byteTransform.TransInt16(buffer, startIndex), null);
                }
                else if (propertyType == typeof(short[]) || propertyType == typeof(Int16[]))
                {
                    pi.SetValue(t, byteTransform.TransInt16(buffer, startIndex, Length / 2), null);
                }
                else if (propertyType == typeof(ushort))
                {
                    pi.SetValue(t, byteTransform.TransUInt16(buffer, startIndex), null);
                }
                else if (propertyType == typeof(ushort[]))
                {
                    pi.SetValue(t, byteTransform.TransUInt16(buffer, startIndex, Length / 2), null);
                }
                else if (propertyType == typeof(int))
                {
                    pi.SetValue(t, byteTransform.TransInt32(buffer, startIndex), null);
                }
                else if (propertyType == typeof(int[]))
                {
                    pi.SetValue(t, byteTransform.TransInt32(buffer, startIndex, Length / 4), null);
                }
                else if (propertyType == typeof(uint))
                {
                    pi.SetValue(t, byteTransform.TransUInt32(buffer, startIndex), null);
                }
                else if (propertyType == typeof(uint[]))
                {
                    pi.SetValue(t, byteTransform.TransUInt32(buffer, startIndex, Length / 4), null);
                }
                else if (propertyType == typeof(long))
                {
                    pi.SetValue(t, byteTransform.TransInt64(buffer, startIndex), null);
                }
                else if (propertyType == typeof(long[]))
                {
                    pi.SetValue(t, byteTransform.TransInt64(buffer, startIndex, Length / 8), null);
                }
                else if (propertyType == typeof(ulong))
                {
                    pi.SetValue(t, byteTransform.TransUInt64(buffer, startIndex), null);
                }
                else if (propertyType == typeof(ulong[]))
                {
                    pi.SetValue(t, byteTransform.TransUInt64(buffer, startIndex, Length / 8), null);
                }
                else if (propertyType == typeof(float))
                {
                    pi.SetValue(t, byteTransform.TransSingle(buffer, startIndex), null);
                }
                else if (propertyType == typeof(float[]))
                {
                    pi.SetValue(t, byteTransform.TransSingle(buffer, startIndex, Length / 4), null);
                }
                else if (propertyType == typeof(double))
                {
                    pi.SetValue(t, byteTransform.TransDouble(buffer, startIndex), null);
                }
                else if (propertyType == typeof(double[]))
                {
                    pi.SetValue(t, byteTransform.TransDouble(buffer, startIndex, Length / 8), null);
                }
                else if (propertyType == typeof(string))
                {
                    if (kstopaStructPropertyAttribute.Encoding == "ASCII")
                    {
                        int read_len = buffer[startIndex + 1];

                        string s = byteTransform.TransString(buffer, startIndex + 2, read_len, Encoding.ASCII);
                        pi.SetValue(t, s, null);
                    }
                    else if (kstopaStructPropertyAttribute.Encoding == "UNICODE")
                    {
                        int read_len = buffer[startIndex + 2] << 8 + buffer[startIndex + 3];

                        string s = byteTransform.TransString(buffer, startIndex + 4, read_len, Encoding.Unicode);
                        pi.SetValue(t, s, null);
                    }
                }
                else if (propertyType == typeof(bool))
                {
                    pi.SetValue(t, byteTransform.TransBool(buffer, startIndex * 8), null);
                }
                else if (propertyType == typeof(bool[]))
                {
                    bool[] array2 = byteTransform.TransBool(buffer, startIndex * 8, Length * 8);

                    pi.SetValue(t, array2, null);
                }
                startIndex += Length;
            }
        }

        return t;
    }

    private static string RefelectionCallStaticMethod(this Type method_class, string method_name, ref object[] obj, ref object ret_value, params Type[] generic_type)
    {
        string ret = "OK";

        do
        {
            try
            {
                if (generic_type == null)
                {
                    ret = "generic_type is null";
                    break;
                }

                MethodInfo Queryable_Method = method_class.GetMethod(method_name);

                if (Queryable_Method == null)
                {
                    ret = "Queryable_Method is null";
                    break;
                }

                MethodInfo Generic_Method = Queryable_Method.MakeGenericMethod(generic_type);

                ret_value = Generic_Method.Invoke(null, obj);

            }
            catch (Exception ep)
            {
                Exception inner_ep = (ep.InnerException == null ? ep : ep.InnerException);
                ret = inner_ep.Message;
                break;
            }

        } while (false);

        return ret;
    }

    public static T[] CreateArraryObject<T>(int count, byte[] buff, ref int startIdx, IByteTransform byteTransform) where T : class, new()
    {
        T[] array_obj = new T[count];
        for (int i = 0; i < count; i++)
        {
            array_obj[i] = CreateObject1<T>(buff, ref startIdx, byteTransform);
        }
        return array_obj;
    }
    /// <summary>
    /// 解析对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="buffer"></param>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    public static T PraseStructContent<T>(byte[] buffer, IByteTransform byteTransform) where T : class, new()
    {
        //定义开始位置
        int idx = 0;
        return CreateObject1<T>(buffer, ref idx, byteTransform);
    }
    /// <summary>
    /// 解析对象
    /// </summary>
    /// <param name="obj_type"></param>
    /// <param name="buffer"></param>
    /// <param name="byteTransform"></param>
    /// <returns></returns>
    public static object PraseStructContent(Type obj_type, byte[] buffer, IByteTransform byteTransform)
    {
        //定义开始位置
        int idx = 0;
        return CreateObjectWithType(obj_type, buffer, ref idx, byteTransform);
    }
    /// <summary>
    /// 获取对象字节长度
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ushort GetObjectLenght<T>() where T : class, new()
    {
        ushort objLenght = 0;
        CreateObjectL<T>(ref objLenght);
        return objLenght;
    }
    /// <summary>
    /// 获取对象字节长度
    /// </summary>
    /// <param name="obj_type"></param>
    /// <returns></returns>
    public static ushort GetObjectLenghtWithType(Type obj_type)
    {
        ushort objLenght = 0;
        CreateObjectLWithType(obj_type, ref objLenght);
        return objLenght;
    }

    public static object CreateObjectLWithType(Type obj_type, ref ushort len)
    {
        object t = null;
        int idx = 0;


        PropertyInfo[] pis = obj_type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        t = Activator.CreateInstance(obj_type);

        foreach (var pi in pis)
        {
            object[] customAttributes = pi.GetCustomAttributes(typeof(KstopaStructAttribute), inherit: false);
            if (customAttributes.Length > 0)
            {
                //类属性
                if (pi.PropertyType.IsArray)
                {

                    //数组对象
                    //获取特性里面数组个数
                    KstopaStructAttribute kstopaStructAttribute = (KstopaStructAttribute)customAttributes[0];
                    int count = kstopaStructAttribute.Count;
                    object[] ret_obj = new object[2] { count, len };
                    object ret_value = null;


                    string ret = RefelectionCallStaticMethod(typeof(SiemensRefelectionUtil), "CreateArraryObjectL", ref ret_obj, ref ret_value, pi.PropertyType.GetElementType());

                    len = (ushort)ret_obj[1];

                    //  pi.SetValue(t, ret_value);
                }
                else
                {
                    ////遇到结构体 索引向后偏移两个字节 差想是指针
                    // len += (ushort)((len % 4) != 0 ? (4 - (len % 4)) : 0);
                    //单对象
                    object[] ret_obj = new object[1] { len };
                    object ret_value = null;


                    RefelectionCallStaticMethod(typeof(SiemensRefelectionUtil), "CreateObjectL", ref ret_obj, ref ret_value, pi.PropertyType.GetTypeInfo());
                    len = (ushort)ret_obj[0];
                    //  pi.SetValue(t, ret_value);
                }
            }
            object[] customAttributes1 = pi.GetCustomAttributes(typeof(KstopaStructPropertyAttribute), inherit: false);
            if (customAttributes1.Length > 0)
            {
                KstopaStructPropertyAttribute kstopaStructPropertyAttribute = (KstopaStructPropertyAttribute)customAttributes1[0];
                // 1 获取属性类型
                Type propertyType = pi.PropertyType;


                len += (ushort)kstopaStructPropertyAttribute.Lenght;
                idx += kstopaStructPropertyAttribute.Lenght;
            }
        }

        return t;

    }

    public static void CreateObjectL<T>(ref ushort len) where T : class, new()
    {
         CreateObjectLWithType(typeof(T), ref len);
    }

    public static void CreateArraryObjectL<T>(int count, ref ushort len) where T : class, new()
    {
        for (int i = 0; i < count; i++)
        {
            CreateObjectL<T>(ref len);
        }
    }

    public static void Object2Buffer<T>(T t, byte[] buffer) where T : class, new()
    {
        //定义开始位置
        int idx = 0;
        var byteTransform = new ReverseBytesTransform();
        CreateObject(t, buffer, ref idx, byteTransform);
    }
    public static void CreateObject<T>(T t, byte[] buffer, ref int startIndex, IByteTransform byteTransform) where T : class, new()
    {
        Type obj_type = t.GetType();
        int idx = 0;

        PropertyInfo[] pis = obj_type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (var pi in pis)
        {
            object[] customAttributes = pi.GetCustomAttributes(typeof(KstopaStructAttribute), inherit: false);
            if (customAttributes.Length > 0)
            {
                //类属性
                if (pi.PropertyType.IsArray)
                {
                    //数组对象
                    //获取特性里面数组个数
                    KstopaStructAttribute kstopaStructAttribute = (KstopaStructAttribute)customAttributes[0];
                    int count = kstopaStructAttribute.Count;
                    //获取对象
                    object obj = pi.GetValue(t, null);
                    object[] ret_obj = new object[] { obj, count, buffer, startIndex, byteTransform };
                    object ret_value = null;

                    string ret = RefelectionCallStaticMethod(typeof(SiemensRefelectionUtil), "CreateArraryObject", ref ret_obj, ref ret_value, pi.PropertyType.GetElementType());

                    startIndex = (int)ret_obj[3];
                }
                else
                {
                    ////遇到结构体 索引向后偏移两个字节 差想是指针
                    //  startIndex += ((startIndex % 4) != 0 ? (4 - (startIndex % 4)) : 0);
                    //单对象
                    //获取对象
                    object obj = pi.GetValue(t, null);
                    object[] ret_obj = new object[] { obj, buffer, startIndex, byteTransform };
                    object ret_value = null;

                    RefelectionCallStaticMethod(typeof(SiemensRefelectionUtil), "CreateObject", ref ret_obj, ref ret_value, pi.PropertyType.GetTypeInfo());

                    startIndex = (int)ret_obj[2];
                }
            }
            object[] customAttributes1 = pi.GetCustomAttributes(typeof(KstopaStructPropertyAttribute), inherit: false);
            if (customAttributes1.Length > 0)
            {

                KstopaStructPropertyAttribute kstopaStructPropertyAttribute = (KstopaStructPropertyAttribute)customAttributes1[0];
                int Length = kstopaStructPropertyAttribute.Lenght;


                // 1 获取属性类型
                Type propertyType = pi.PropertyType;
                if (propertyType == typeof(byte))
                {
                    var v = (byte)pi.GetValue(t, null);
                    
                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(byte[]))
                {
                    var v = (byte[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v, 0, Length);
                        buffer.SetMiddle(startIndex, bs);
                    }
                    pi.SetValue(t, buffer.SelectMiddle(startIndex, Length), null);
                }
                else if (propertyType == typeof(short) || propertyType == typeof(Int16))
                {
                    var v = (short)pi.GetValue(t, null);
                    
                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(short[]) || propertyType == typeof(Int16[]))
                {
                    var v = (short[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v);
                        buffer.SetMiddle(startIndex, bs);
                    }
                }
                else if (propertyType == typeof(ushort))
                {
                    var v = (ushort)pi.GetValue(t, null);
                   
                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(ushort[]))
                {
                    var v = (ushort[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v);
                        buffer.SetMiddle(startIndex, bs);
                    }
                }
                else if (propertyType == typeof(int))
                {
                    var v = (int)pi.GetValue(t, null);
                    
                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(int[]))
                {
                    var v = (int[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v);
                        buffer.SetMiddle(startIndex, bs);
                    }
                }
                else if (propertyType == typeof(uint))
                {
                    var v = (uint)pi.GetValue(t, null);
                    
                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(uint[]))
                {
                    var v = (uint[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v);
                        buffer.SetMiddle(startIndex, bs);
                    }
                }
                else if (propertyType == typeof(long))
                {
                    var v = (long)pi.GetValue(t, null);
                   
                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(long[]))
                {
                    var v = (long[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v);
                        buffer.SetMiddle(startIndex, bs);
                    }
                }
                else if (propertyType == typeof(ulong))
                {
                    var v = (ulong)pi.GetValue(t, null);
                    
                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(ulong[]))
                {
                    var v = (ulong[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v);
                        buffer.SetMiddle(startIndex, bs);
                    }
                }
                else if (propertyType == typeof(float))
                {
                    var v = (float)pi.GetValue(t, null);
                    
                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(float[]))
                {
                    var v = (float[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v);
                        buffer.SetMiddle(startIndex, bs);
                    }
                }
                else if (propertyType == typeof(double))
                {
                    var v = (double)pi.GetValue(t, null);
                    
                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(double[]))
                {
                    var v = (double[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v);
                        buffer.SetMiddle(startIndex, bs);
                    }
                }
                else if (propertyType == typeof(string))
                {
                    if (kstopaStructPropertyAttribute.Encoding == "ASCII")//英文
                    {
                        var v = (string)pi.GetValue(t, null);
                        if (v != null)
                        {
                            //防呆处理，字符串长度大于设置值时，需要自动裁断，string类型不支持中文，中文使用WString类型，暂时没有该功能。即string 类型中传递中文PLC是获取不到的，
                            var strLen = v.Length;
                            if(strLen > Length - 2) strLen = Length - 2;
                            var bs = byteTransform.TransByte(v, strLen, Encoding.ASCII);
                            byte[] ret_bs = new byte[bs.Length + 2];
                            ret_bs[0] = (byte)(Length - 2);
                            ret_bs[1] = (byte)strLen;

                            for (int i = 0; i < bs.Length; i++)
                            {
                                ret_bs[i + 2] = bs[i];
                            }
                            buffer.SetMiddle(startIndex, ret_bs);
                        }
                    }
                    else if (kstopaStructPropertyAttribute.Encoding == "UNICODE")//中文
                    {
                        var v = (string)pi.GetValue(t, null);
                        if (v != null)
                        {
                            //防呆处理，字符串长度大于设置值时，需要自动裁断，string类型不支持中文，中文使用WString类型，暂时没有该功能。即string 类型中传递中文PLC是获取不到的，
                            var strLen = v.Length;
                            if (v.Length > (Length -4) / 2)
                            {
                                strLen = (Length -4) / 2;
                                v = v.Substring(0, strLen);
                            }
                            var bs = byteTransform.TransByte(v, strLen * 2, Encoding.Unicode);
                            byte[] ret_bs = new byte[bs.Length + 4];
                            ret_bs[0] = (byte)(((Length -4)/ 2) >> 8);
                            ret_bs[1] = (byte)((Length -4 )/ 2);
                            ret_bs[2] = (byte)(strLen >> 8);
                            ret_bs[3] = (byte)strLen;

                            for (int i = 0; i < bs.Length; i++)
                            {
                                if (i % 2 == 0)
                                    ret_bs[i + 4] = bs[i + 1];
                                else
                                    ret_bs[i + 4] = bs[i - 1];
                            }
                            buffer.SetMiddle(startIndex, ret_bs);
                        }
                    }
                }
                else if (propertyType == typeof(bool))
                {
                    var v = (bool)pi.GetValue(t, null);

                    var bs = byteTransform.TransByte(v);
                    buffer.SetMiddle(startIndex, bs);
                }
                else if (propertyType == typeof(bool[]))
                {
                    var v = (bool[])pi.GetValue(t, null);
                    if (v != null)
                    {
                        var bs = byteTransform.TransByte(v);
                        buffer.SetMiddle(startIndex, bs);
                    }
                }

                startIndex += Length;
                idx += Length;
            }
        }
    }
    public static void CreateArraryObject<T>(T[] ts, int count, byte[] buff, ref int startIdx, IByteTransform byteTransform) where T : class, new()
    {
        for (int i = 0; i < count; i++)
        {
            CreateObject(ts[i], buff, ref startIdx, byteTransform);
        }
    }

    public static void SetMiddle(this byte[] buff, int idx, byte[] setBuf)
    {
        for (int i = 0; i < setBuf.Length; i++)
        {
            buff[i + idx] = setBuf[i];
        }
    }

    public static Type GetReadClassType(this PublicInfo p)
    {
        var namespaceName = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
        return Type.GetType($"{namespaceName}.{p.ReadClassName}");
    }
    public static Type GetWriteClassType(this PublicInfo p)
    {
        var namespaceName = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
        return Type.GetType($"{namespaceName}.{p.WriteClassName}");
    }
    public static Type GetReadClassType(this EventInfo ei)
    {
        var namespaceName = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
        return Type.GetType($"{namespaceName}.{ei.ReadClassName}");
    }

    public static Type GetWriteClassType(this EventInfo ei)
    {
        var namespaceName = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
        return Type.GetType($"{namespaceName}.{ei.WriteClassName}");
    }
    public static void ResetReadClassLenth(this PublicInfo p)
    {
        p.ReadLen = GetObjectLenghtWithType(p.GetReadClassType());
    }
    public static void ResetReadClassLenth(this EventInfo ei)
    {
        ei.ReadLen = GetObjectLenghtWithType(ei.GetReadClassType());
    }
    public static void UpdateReadContent(this PublicInfo p, byte[] read_buffer)
    {
        //p.ObjR = PraseStructContent(p.GetReadClassType(), read_buffer, new ReverseBytesTransform());
    }
    public static void UpdateReadContent(this EventInfo ei, byte[] read_buffer)
    {
        //ei.ObjR = PraseStructContent(ei.GetReadClassType(), read_buffer, new ReverseBytesTransform());
    }
    public static void ResetWriteClassLenth(this PublicInfo p)
    {
        p.WriteLen = GetObjectLenghtWithType(p.GetWriteClassType());
        //p.WriteBuffer = new byte[p.WriteLen];
    }
    public static void ResetWriteClassLenth(this EventInfo ei)
    {
        ei.WriteLen = GetObjectLenghtWithType(ei.GetWriteClassType());
        ei.WriteBuffer = new byte[ei.WriteLen];
    }
    public static void UpdateWriteContent(this PublicInfo p, byte[] read_buffer)
    {
        //p.ObjW = PraseStructContent(p.GetWriteClassType(), read_buffer, new ReverseBytesTransform());
    }
    public static void UpdateWriteContent(this EventInfo ei, byte[] write_buffer)
    {
        //ei.ObjW = PraseStructContent(ei.GetWriteClassType(), write_buffer, new ReverseBytesTransform());
    }


    public static Type GetTypeByName(string name)
    {
        var namespaceName = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
        return Type.GetType($"{namespaceName}.{name}");
    }

    /// <summary>
    /// 将一个对象转换为指定类型
    /// </summary>
    /// <param name="obj">待转换的对象</param>
    /// <param name="type">目标类型</param>
    /// <returns>转换后的对象</returns>
    public static object ConvertToObject(object obj, Type type)
    {
        if (type == null) return obj;
        if (obj == null) return type.IsValueType ? Activator.CreateInstance(type) : null;

        Type underlyingType = Nullable.GetUnderlyingType(type);
        if (type.IsAssignableFrom(obj.GetType())) // 如果待转换对象的类型与目标类型兼容，则无需转换
        {
            return obj;
        }
        else if ((underlyingType ?? type).IsEnum) // 如果待转换的对象的基类型为枚举
        {
            if (underlyingType != null && string.IsNullOrEmpty(obj.ToString())) // 如果目标类型为可空枚举，并且待转换对象为null 则直接返回null值
            {
                return null;
            }
            else
            {
                return Enum.Parse(underlyingType ?? type, obj.ToString());
            }
        }
        else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type)) // 如果目标类型的基类型实现了IConvertible，则直接转换
        {
            try
            {
                return Convert.ChangeType(obj, underlyingType ?? type, null);
            }
            catch
            {
                return underlyingType == null ? Activator.CreateInstance(type) : null;
            }
        }
        else
        {
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            if (converter.CanConvertFrom(obj.GetType()))
            {
                return converter.ConvertFrom(obj);
            }
            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            if (constructor != null)
            {
                object o = constructor.Invoke(null);
                PropertyInfo[] propertys = type.GetProperties();
                Type oldType = obj.GetType();
                foreach (PropertyInfo property in propertys)
                {
                    PropertyInfo p = oldType.GetProperty(property.Name);
                    if (property.CanWrite && p != null && p.CanRead)
                    {
                        property.SetValue(o, ConvertToObject(p.GetValue(obj, null), property.PropertyType), null);
                    }
                }
                return o;
            }
        }
        return obj;
    }

}

public class KstopaStructAttribute : Attribute
{
    public int Count { get; set; }
    public KstopaStructAttribute(int count = 1)
    {
        Count = count;
    }
}
public class KstopaStructPropertyAttribute : Attribute
{
    public int Lenght { get; set; }
    public string Encoding { get; set; }
    public KstopaStructPropertyAttribute()
    {
        this.Lenght = 1;
    }
    public KstopaStructPropertyAttribute(int lenght, string encoding = "ASCII")
    {
        this.Lenght = lenght;
        this.Encoding = encoding;
    }
}
