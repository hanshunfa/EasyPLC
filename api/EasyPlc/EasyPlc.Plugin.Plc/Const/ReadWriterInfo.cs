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
* 创建时间：2024/1/30 9:48:26
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlc.Plugin.Plc
{
    /// <summary>
    /// 读写信息基础类
    /// </summary>
    public class ReadWriterBaseInfo
    {
        /// <summary>
        /// 开始地址 列:Siemens DB10000.12
        /// </summary>
        public string StartAddr { get; set; }

        /// <summary>
        /// 数据长度字节数量
        /// </summary>
        public ushort Lenght { get; set; }

        /// <summary>
        /// 字节数组数据
        /// </summary>
        public byte[] Buffer { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        public Type ObjT { get; set; }

        /// <summary>
        /// 树形列表类型数据
        /// </summary>
        public List<PlcResource> ListPr { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime DoTime { get; set; }
    }

    /// <summary>
    /// 公共区读信息类
    /// </summary>
    public class PublicReadInfo : ReadWriterBaseInfo
    {
    }

    /// <summary>
    /// 公共区写信息类
    /// </summary>

    public class PublicWriterInfo : PublicReadInfo
    {
        /// <summary>
        /// 第一次写之前需要读取PLCDB内容
        /// </summary>
        public bool IsReset { get; set; } = true;
    }

    /// <summary>
    /// 事件区读信息类
    /// </summary>
    public class EventReadInfo : ReadWriterBaseInfo
    {
        /// <summary>
        /// SequenceId 防止事件重复处理使用
        /// </summary>
        public int SequenceId { get; set; } = -1;
    }

    public class EventWriterInfo : EventReadInfo
    {
        /// <summary>
        /// 第一次写之前需要读取PLCDB内容
        /// </summary>
        public bool IsReset { get; set; } = true;
    }
}