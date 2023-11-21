﻿
using Furion;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Reflection;
using EasyPlc.Core.UserInfo;

namespace EasyPlc.SqlSugar;

/// <summary>
/// 数据库上下文对象
/// </summary>
public class DbContext
{
    /// <summary>
    /// 读取配置文件中的 ConnectionStrings:Sqlsugar 配置节点
    /// </summary>
    public readonly static List<SqlSugarConfig> DbConfigs = App.GetConfig<List<SqlSugarConfig>>("SqlSugarSettings:ConnectionStrings");

    /// <summary>
    /// SqlSugar 数据库实例
    /// </summary>
    public readonly static SqlSugarScope Db = new (DbConfigs.Adapt<List<ConnectionConfig>>(), db =>
    {
        //遍历配置的数据库
        DbConfigs.ForEach(it =>
        {
            var sqlsugarScope = db.GetConnectionScope(it.ConfigId);//获取当前库
            MoreSetting(sqlsugarScope);//更多设置
            ExternalServicesSetting(sqlsugarScope, it);//实体拓展配置
            AopSetting(sqlsugarScope);//aop配置
            FilterSetting(sqlsugarScope);//过滤器配置
        });
    });

    /// <summary>
    /// 实体拓展配置,自定义类型多库兼容
    /// </summary>
    /// <param name="db"></param>
    /// <param name="config"></param>
    private static void ExternalServicesSetting(SqlSugarScopeProvider db, SqlSugarConfig config)
    {
        db.CurrentConnectionConfig.ConfigureExternalServices = new ConfigureExternalServices
        {
            // 处理表
            EntityNameService = (type, entity) =>
            {
                if (config.IsUnderLine && !entity.DbTableName.Contains('_'))
                    entity.DbTableName = UtilMethods.ToUnderLine(entity.DbTableName);// 驼峰转下划线
            },
            //自定义类型多库兼容
            EntityService = (c, p) =>
            {
                //如果是mysql并且是varchar(max) 已弃用
                //if (config.DbType == SqlSugar.DbType.MySql && (p.DataType == SqlsugarConst.NVarCharMax))
                //{
                //    p.DataType = SqlsugarConst.LongText;//转成mysql的longtext
                //}
                //else if (config.DbType == SqlSugar.DbType.Sqlite && (p.DataType == SqlsugarConst.NVarCharMax))
                //{
                //    p.DataType = SqlsugarConst.Text;//转成sqlite的text
                //}
                //默认不写IsNullable为非必填
                //if (new NullabilityInfoContext().Create(c).WriteState is NullabilityState.Nullable)
                //    p.IsNullable = true;
                if (config.IsUnderLine && !p.IsIgnore && !p.DbColumnName.Contains('_'))
                    p.DbColumnName = UtilMethods.ToUnderLine(p.DbColumnName);// 驼峰转下划线
            }
        };
    }

    /// <summary>
    /// Aop设置
    /// </summary>
    /// <param name="db"></param>
    public static void AopSetting(SqlSugarScopeProvider db)
    {
        var config = db.CurrentConnectionConfig;

        // 设置超时时间
        db.Ado.CommandTimeOut = 30;

        // 打印SQL语句
        db.Aop.OnLogExecuting = (sql, pars) =>
        {
            //如果不是开发环境就打印sql
            if (App.HostEnvironment.IsDevelopment())
            {
                if (sql.StartsWith("SELECT"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    WriteSqlLog($"查询{config.ConfigId}库操作");
                }
                if (sql.StartsWith("UPDATE") || sql.StartsWith("INSERT"))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    WriteSqlLog($"修改{config.ConfigId}库操作");
                }
                if (sql.StartsWith("DELETE"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteSqlLog($"删除{config.ConfigId}库操作");
                }
                Console.WriteLine(UtilMethods.GetSqlString(config.DbType, sql, pars));
                WriteSqlLog($"{config.ConfigId}库操作结束");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        };
        //异常
        db.Aop.OnError = (ex) =>
        {
            //如果不是开发环境就打印日志
            if (App.HostEnvironment.IsDevelopment())
            {
                if (ex.Parametres == null) return;
                Console.ForegroundColor = ConsoleColor.Red;
                var pars = db.Utilities.SerializeObject(((SugarParameter[])ex.Parametres).ToDictionary(it => it.ParameterName, it => it.Value));
                WriteSqlLog($"{config.ConfigId}库操作异常");
                Console.WriteLine(UtilMethods.GetSqlString(config.DbType, ex.Sql, (SugarParameter[])ex.Parametres) + "\r\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        };
        //插入和更新过滤器
        db.Aop.DataExecuting = (oldValue, entityInfo) =>
        {
            // 新增操作
            if (entityInfo.OperationType == DataFilterType.InsertByObject)
            {
                // 主键(long类型)且没有值的---赋值雪花Id
                if (entityInfo.EntityColumnInfo.IsPrimarykey && entityInfo.EntityColumnInfo.PropertyInfo.PropertyType == typeof(long))
                {
                    var id = entityInfo.EntityColumnInfo.PropertyInfo.GetValue(entityInfo.EntityValue);
                    if (id == null || (long)id == 0)
                        entityInfo.SetValue(CommonUtils.GetSingleId());
                }
                if (entityInfo.PropertyName == nameof(BaseEntity.CreateTime))
                    entityInfo.SetValue(DateTime.Now);

                if (LoginUser.IsLogin)
                {
                    //创建人和创建机构ID
                    if (entityInfo.PropertyName == nameof(BaseEntity.CreateUserId))
                        entityInfo.SetValue(LoginUser.UserId);
                    if (entityInfo.PropertyName == nameof(BaseEntity.CreateUser))
                        entityInfo.SetValue(LoginUser.Account);
                    if (entityInfo.PropertyName == nameof(DataEntityBase.CreateOrgId))
                        entityInfo.SetValue(LoginUser.OrgId);
                }
            }
            // 更新操作
            if (entityInfo.OperationType == DataFilterType.UpdateByObject)
            {
                //更新时间
                if (entityInfo.PropertyName == nameof(BaseEntity.UpdateTime))
                    entityInfo.SetValue(DateTime.Now);
                //更新人
                if (LoginUser.IsLogin)
                {
                    if (entityInfo.PropertyName == nameof(BaseEntity.UpdateUserId))
                        entityInfo.SetValue(LoginUser.UserId);
                    if (entityInfo.PropertyName == nameof(BaseEntity.UpdateUser))
                        entityInfo.SetValue(LoginUser.Account);
                }
            }
        };

        //查询数据转换
        db.Aop.DataExecuted = (value, entity) =>
        {
        };
    }

    /// <summary>
    /// 实体更多配置
    /// </summary>
    /// <param name="db"></param>
    private static void MoreSetting(SqlSugarScopeProvider db)
    {
        db.CurrentConnectionConfig.MoreSettings = new ConnMoreSettings
        {
            SqlServerCodeFirstNvarchar = true//设置默认nvarchar
        };
    }

    /// <summary>
    /// 过滤器设置
    /// </summary>
    /// <param name="db"></param>
    public static void FilterSetting(SqlSugarScopeProvider db)
    {
        // 假删除过滤器
        //LogicDeletedEntityFilter(db);
    }

    /// <summary>
    /// 假删除过滤器
    /// </summary>
    /// <param name="db"></param>
    private static void LogicDeletedEntityFilter(SqlSugarScopeProvider db)
    {
    }

    private static void WriteSqlLog(string msg)
    {
        Console.WriteLine($"=============={msg}==============");
    }
}