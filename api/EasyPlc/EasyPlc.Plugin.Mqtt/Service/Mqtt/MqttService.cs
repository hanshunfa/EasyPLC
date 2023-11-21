﻿using Furion.DataEncryption;
using Microsoft.AspNetCore.Http;
using SimpleTool;

namespace EasyPlc.Plugin.Mqtt;

/// <summary>
/// <inheritdoc cref="IMqttService"/>
/// </summary>
public class MqttService : IMqttService
{
    private readonly ISimpleCacheService _simpleCacheService;
    private readonly ISysUserService _sysUserService;
    private readonly IConfigService _configService;

    public MqttService(ISimpleCacheService simpleCacheService, ISysUserService sysUserService, IConfigService configService)
    {
        this._simpleCacheService = simpleCacheService;
        this._sysUserService = sysUserService;
        this._configService = configService;
    }

    /// <inheritdoc/>
    public async Task<MqttParameterOutput> GetWebLoginParameter()
    {
        var user = await _sysUserService.GetUserById(UserManager.UserId);//获取用户信息
        var token = JWTEncryption.GetJwtBearerToken((DefaultHttpContext)App.HttpContext); // 获取当前token
        //获取mqtt配置
        var mqttconfig = await _configService.GetListByCategory(CateGoryConst.Config_MQTT_BASE);
        //地址
        var url = mqttconfig.Where(it => it.ConfigKey == DevConfigConst.MQTT_PARAM_URL).Select(it => it.ConfigValue).FirstOrDefault();
        //用户名
        var userName = mqttconfig.Where(it => it.ConfigKey == DevConfigConst.MQTT_PARAM_USERNAME).Select(it => it.ConfigValue).FirstOrDefault();
        //密码
        var password = mqttconfig.Where(it => it.ConfigKey == DevConfigConst.MQTT_PARAM_PASSWORD).Select(it => it.ConfigValue).FirstOrDefault();

        #region 用户名特殊处理

        if (userName.ToLower() == "$username")
            userName = user.Account;
        else if (userName.ToLower() == "$userid")
            userName = user.Id.ToString();

        #endregion 用户名特殊处理

        #region 密码特殊处理

        if (password.ToLower() == "$username")
            password = token; // 当前token作为mqtt密码

        #endregion 密码特殊处理

        var clientId = $"{user.Id}_{RandomHelper.CreateLetterAndNumber(5)}";//客户端ID
        _simpleCacheService.Set(CacheConst.Cache_MqttClientUser + clientId, token, TimeSpan.FromMinutes(1));//将该客户端ID对应的token插入redis后面可以根据这个判断是哪个token登录的
        return new MqttParameterOutput
        {
            ClientId = clientId,
            Password = password,
            Url = url,
            UserName = userName,
            Topics = new List<string> { MqttConst.Mqtt_TopicPrefix + user.Id }//默认监听自己
        };
    }

    /// <inheritdoc/>
    public async Task<MqttAuthOutput> Auth(MqttAuthInput input)
    {
        var user = await _sysUserService.GetUserByAccount(input.Username);
        MqttAuthOutput mqttAuthOutput = new MqttAuthOutput { Is_superuser = false, Result = "deny" };

        //获取用户token
        var tokens = _simpleCacheService.HashGetOne<List<TokenInfo>>(CacheConst.Cache_UserToken, user.Id.ToString());
        if (tokens != null)
        {
            if (tokens.Any(it => it.Token == input.Password))//判断是否有token
                mqttAuthOutput.Result = "allow";//允许登录
        }
        return mqttAuthOutput;
    }

    #region 方法


    #endregion 方法
}