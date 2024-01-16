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
* 创建时间：2023/12/5 10:53:30
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

namespace EasyRabbitMQ
{
    /// <summary>
    /// rabbitMQ客户端配置
    /// </summary>
    public class RabbitMQClientConfig
    {
        /// <summary>
        /// 主机地址
        /// </summary>
        public string Host { get; set; } = "127.0.0.1";

        /// <summary>
        /// 端口，默认5672
        /// </summary>
        public int Port { get; set; } = 5672;
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string User { get; set; } = "shunfa";
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "123";

        /// <summary>
        /// 超时，默认5000ms
        /// </summary>
        public int Timeout { get; set; } = 5000;
    }
}
