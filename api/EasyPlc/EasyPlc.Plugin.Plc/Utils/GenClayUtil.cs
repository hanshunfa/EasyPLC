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
* 创建时间：2023/11/22 10:24:22
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

using System.Security.Cryptography;

namespace EasyPlc.Plugin.Plc.Utils;

/// <summary>
/// 粘土对象生成工具
/// </summary>
public static class GenClayUtil
{
    public static dynamic GenClay(List<PlcResource> resources)
    {
        // 创建一个空的粘土对象
        dynamic clay = new Clay();

        clay.A = resources[0];
        clay.B = 100f;
        clay.C = (short)10;
        clay.D = 10;
        clay.E = (float)10;

        return clay.Solidify<dynamic>();
    }
}
