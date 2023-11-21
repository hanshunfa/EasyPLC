

namespace EasyPlc.Application;

public static class LabelUtil
{
    /// <summary>
    /// 标签解析
    /// </summary>
    /// <param name="modelParams"></param>
    /// <param name="batch">批次</param>
    /// <param name="lsh">流水号</param>
    /// <param name="isInit">是否初始生成记录</param>
    /// <returns></returns>
    public static ParseLaserResult ParseLabelValue(this List<string> ps, string batch, int lsh, DateTime dateTime, bool isInit = true)
    {
        /*
         * 匹配关键字
         * {name(code)} 提取变量内容 依据code
         * {serialNu(d)}流水号 d表示 有效位数
         * {batch()} 批次号订单获取
         * {date(yyMMdd hh:mm:ss)} 提取当前时间关键字 时间转换格式 yyMMdd hh:mm:ss
         * {null} 空可忽略
         */

        var result = new ParseLaserResult();
        try
        {
            ps.ForEach(param =>
            {
                var str = param;
                //提取{}
                var dkhList = GetDkh(param);
                //匹配方法
                dkhList.ForEach(x =>
                {
                    var str1 = string.Empty;
                    var func = x.Trim();

                    if (func.ToLower().Contains("serialnu") && !isInit)
                    {
                        var p = GetXkh(func);
                        //判断流水号是否用完
                        var nr = p.ToInt();
                        if (lsh.ToString().Length > nr)
                        {
                            result.IsSucceed = false;
                            result.Msg = "";
                        }
                        str1 = lsh.ToString().PadLeft(nr, '0');

                        //替换{}里面内容
                        str = str.Replace("{" + x + "}", str1);
                    }
                    if (func.ToLower().Contains("batch"))
                    {
                        str1 = batch;
                        //替换{}里面内容
                        str = str.Replace("{" + x + "}", str1);
                    }
                    if (func.ToLower().Contains("date") && !isInit)
                    {
                        var p = GetXkh(func);
                        str1 = dateTime.ToString(p);

                        //替换{}里面内容
                        str = str.Replace("{" + x + "}", str1);
                    }
                });
                //替换

                result.ResultList.Add(str);
            });
        }
        catch (Exception ex)
        {
            result.IsSucceed = false;
            result.Msg = ex.Message;
        }
        return result;
    }
    public class ParseLaserResult
    {
        public List<string> ResultList { get; set; } = new List<string>();
        public bool IsSucceed { get; set; } = true;
        public string Msg { get; set; }
    }
    /// <summary>
    /// 提取{}
    /// </summary>
    public static List<string> GetDkh(string str, int idx = 0)
    {
        var result = new List<string>();
        //提取{}
        int n1 = idx;
        int n2 = idx;
        var d1 = str.IndexOf("{", n1);
        if(d1 == -1)
        {
            //找不到
            return result;
        }
        n1 = d1;
        var d2 = str.IndexOf("}", n1);
        if(d2 == -1)
        {
            //异常，暂时抛弃处理  { 没有成对出现
            return result;
        }
        n2 = d2;
        result.Add(str.Substring(n1 + "{".Length, n2 - n1 - "{".Length));

        if(str.IndexOf("{", n2) == -1 )
        {
            return result;
        }
        else
        {
            result.AddRange(GetDkh(str, n2));
        }
        
        return result;
    }

    public static string GetXkh(string str)
    {
        string result = string.Empty;
        var d1 = str.IndexOf("(", 0);
        if (d1 == -1)
        {
            //找不到
            return result;
        }
        var d2 = str.IndexOf(")", d1);
        if (d2 == -1)
        {
            return result;
        }
        result = str.Substring(d1 + "(".Length, d2 - d1 - "(".Length);

        return result;
    }
}
