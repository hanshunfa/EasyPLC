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
* 创建时间：2023/11/17 16:02:47
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


using EasyPlc.Plugin.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using TouchSocket.Sockets;

namespace EasyPlc.Plugin.Plc;

/// <summary>
/// plc组件
/// </summary>
public sealed class PlcCompoment : IServiceComponent
{
    /// <summary>
    /// ConfigureServices中不能解析服务，比如App.GetService()，尤其是不能在ConfigureServices中获取诸如缓存等数据进行初始化，应该在Configure中进行
    /// 服务都还没初始化完成，会导致内存中存在多份 IOC 容器！！
    /// 正确应该在 Configure 中，这个时候服务（IServiceCollection 已经完成 BuildServiceProvider() 操作了
    /// </summary>
    /// <param name="services"></param>
    /// <param name="componentContext"></param>
    public void Load(IServiceCollection services, ComponentContext componentContext)
    {
        Console.WriteLine("注册plc插件");

    }
}

/// <summary>
/// plc组件
/// </summary>
public sealed class PlcApplicationCompoment : IApplicationComponent
{
    public async void Load(IApplicationBuilder app, IWebHostEnvironment env, ComponentContext componentContext)
    {
        Console.WriteLine("启用SiemensPLC");
        //初始化工厂
        var siemensFac = App.GetService<ISiemensPlcFactoryService>();
        siemensFac.Use();
        var pluginSettings = App.GetOptions<PluginSettingsOptions>();
        if (pluginSettings.SiemensPlc.IsInitFactory)
        {
            Console.WriteLine("初始化SiemensPLC工厂");
            await siemensFac.InitFactory();
            Console.WriteLine("连接所有SiemensPLC");
            siemensFac.StartPLC();
        }
    }
}
