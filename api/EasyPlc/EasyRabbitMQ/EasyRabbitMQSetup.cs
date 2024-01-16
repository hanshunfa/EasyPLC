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
* 创建时间：2023/12/5 10:49:12
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


using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyRabbitMQ
{
    /// <summary>
    /// RabbitMQ客户端扩展类
    /// </summary>
    public static class EasyRabbitMQSetup
    {
        public static void AddRabbitMQManager(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            // 获取当前程序集
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                // 获取所有类型
                Type[] types = assembly.GetTypes();

                // 遍历所有类型
                foreach (Type type in types)
                {
                    // 判断类型是否实现了指定接口
                    if (typeof(IRabbitReceived).IsAssignableFrom(type) && type.IsClass)
                    {
                        // 创建对象实例
                        object obj = Activator.CreateInstance(type);
                        //注册到容器中
                        services.AddSingleton(x => obj as IRabbitReceived);
                    }
                }
                
            }
            services.AddSingleton<IRabbitMQManager, RabbitMQManager>();
        }
    }
}
