﻿using Minio;
using Minio.Exceptions;

namespace EasyPlc.System;

/// <summary>
/// Minio功能类
/// </summary>
public class MinioUtils : ITransient
{
    public MinioClient minioClient;
    private string defaultBucketName;
    private string defaultEndPoint;
    private string defaultPrefix = "http://";
    private readonly IConfigService _configService;

    public MinioUtils(IConfigService configService)
    {
        this._configService = configService;
        InitClient();
    }

    /// <summary>
    /// 初始化操作
    /// </summary>
    /// <returns></returns>
    private async void InitClient()
    {
        var configs = await _configService.GetListByCategory(CateGoryConst.Config_FILE_MINIO);//获取minio配置
        var accessKey = configs.Where(it => it.ConfigKey == DevConfigConst.FILE_MINIO_ACCESS_KEY).FirstOrDefault();//MINIO文件AccessKey
        var secretKey = configs.Where(it => it.ConfigKey == DevConfigConst.FILE_MINIO_SECRET_KEY).FirstOrDefault();//MINIO文件SecetKey
        var endPoint = configs.Where(it => it.ConfigKey == DevConfigConst.FILE_MINIO_END_POINT).FirstOrDefault();//MINIO文件EndPoint
        var bucketName = configs.Where(it => it.ConfigKey == DevConfigConst.FILE_MINIO_DEFAULT_BUCKET_NAME).FirstOrDefault();//MINIO文件默认存储桶
        if (accessKey == null || secretKey == null || endPoint == null || bucketName == null)
        {
            throw Oops.Oh($"MINIO客户端未正确配置");
        }
        try
        {
            //默认值赋值
            defaultBucketName = bucketName.ConfigValue;
            defaultEndPoint = endPoint.ConfigValue;
            if (defaultEndPoint.ToLower().StartsWith("http"))
            {
                var point = defaultEndPoint.Split("//").ToList();//分割、
                defaultPrefix = $"{point[0]}//";
                defaultEndPoint = point[1];
            }
            this.minioClient = new MinioClient().WithEndpoint(defaultEndPoint).WithCredentials(accessKey.ConfigValue, secretKey.ConfigValue).Build();//初始化monio对象
            this.minioClient.WithTimeout(5000);//超时时间
        }
        catch (Exception ex)
        {
            throw Oops.Oh("MINIO客户端启动失败", ex);
        }
    }

    /// <summary>
    /// 上传文件 - 返回Minio文件完整Url
    /// </summary>
    /// <param name="objectName">存储桶里的对象名称,例:/mnt/photos/island.jpg</param>
    /// <param name="file">文件</param>
    /// <param name="contentType">文件的Content type，默认是"application/octet-stream"</param>
    /// <returns></returns>
    public async Task<string> PutObjectAsync(string objectName, IFormFile file, string contentType = "application/octet-stream")
    {
        try
        {
            using var fileStream = file.OpenReadStream();//获取文件流
            PutObjectArgs putObjectArgs = new PutObjectArgs().WithBucket(defaultBucketName).WithObject(objectName).WithStreamData(fileStream).WithObjectSize(file.Length).WithContentType(contentType);
            await minioClient.PutObjectAsync(putObjectArgs);
            return $"{defaultPrefix}{defaultEndPoint}/{defaultBucketName}/{objectName}";//默认http
        }
        catch (MinioException e)
        {
            throw Oops.Oh($"上传文件失败!", e);
        }
        catch (Exception e)
        {
            throw Oops.Oh($"上传文件失败!", e);
        }
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="objectName"></param>
    /// <returns></returns>
    public async Task<MemoryStream> DownloadFileAsync(string objectName)
    {
        var stream = new MemoryStream();
        try
        {
            var getObjectArgs = new GetObjectArgs().WithBucket(defaultBucketName)
                                                   .WithObject(objectName)
                                                   .WithCallbackStream(cb =>
                                                   {
                                                       cb.CopyTo(stream);
                                                   });
            await minioClient.GetObjectAsync(getObjectArgs);

            //System.InvalidOperationException: Response Content-Length mismatch: too few bytes written (0 of 30788)
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
            return stream;
        }
        catch (MinioException e)
        {
            throw Oops.Oh($"下载文件失败!", e);
        }
        catch (Exception e)
        {
            throw Oops.Oh($"下载文件失败!", e);
        }
    }
}