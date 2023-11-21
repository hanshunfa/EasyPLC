
using NewLife;
using Org.BouncyCastle.Crypto.Tls;
using StackExchange.Profiling.Internal;
using System.Reactive.Linq;

namespace EasyPlc.Plugin.ScrewGun;

public static class KwUtil
{
    /*
        OK  PR:1 PLUTO3   TARGET: 1.15 Nm  S: 600rpm ST: 0.1s  Screw:01/01 Seq:1/1 T: 1.19 Nm A:   31deg 06/04/89           22:09:55 Program end
        NOK PR:1 PLUTO3   TARGET: 1.15 Nm  S: 600rpm ST: 2.8s  Screw:00/01 Seq:1/1 T:---.-        A: 9720deg 06/04/89 22:11:04 Error ang. max
     * */
    /// <summary>
    /// 解析康沃螺丝枪
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static KwScrewGunReturnInfo PaseKw(byte[] bytes)
    {
        var str = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        var result = new KwScrewGunReturnInfo();
        try
        {
            //分类提取
            //01 结果
            if(str.Substring(0, 2) == "OK")
            {
                result.Result = true;
            }
            else if(str.Substring(0, 3) == "NOK")
            {
                result.Result = false;
            }
            else
            {
                result.PaseResult = false;
                result.PaseMsg = "结果解析不符合定义(OK|NOK)";
            }
            //02 程序号
            var nPR = str.IndexOf("PR:");
            var proNum = str.Substring(nPR + "PR:".Length, 1).ToInt();
            result.ProNum = proNum;
            //03 设定扭力
            //TARGET:
            var nTarget = str.IndexOf("TARGET:");
            //S:
            var nS = str.IndexOf("S:");
            var setTarget = str.Substring(nTarget + "TARGET:".Length, nS - (nTarget + "TARGET:".Length));
            var st1 = setTarget.Trim();//去首尾
            var st1sps = st1.Split(" ");
            var setT  = Convert.ToSingle(st1sps[0]);
            result.SetTorque = setT;
            //04 设定角度
            //ST:
            var nST = str.IndexOf("ST:");
            var setAngle = str.Substring(nS + "S:".Length, nST - (nS + "S:".Length));
            var setA1 = setAngle.Trim();//去首尾
            var nrpm = setA1.IndexOf("rpm");
            var setA = setA1.Remove(nrpm);
            result.SetAngle = Convert.ToSingle(setA);

            //05 运行时间
            //Screw:
            var nScrew = str.IndexOf("Screw:");
            var rt = str.Substring(nST + "ST:".Length, nScrew - (nST + "ST:".Length));
            var rtt = rt.Trim();
            var s = rtt.IndexOf("s");
            rtt = rtt.Remove(s);
            result.RunTimeS = Convert.ToSingle(rtt);
            //06 结果扭力
            //T:
            var nt = str.IndexOf("T:", nST + "ST:".Length);
            //A:
            var na = str.IndexOf("A:");

            var t = str.Substring(nt + "T:".Length, na - (nt + "T:".Length));
            //有一种是没有扭力
            if (t.Contains("-.-"))
            {
                result.Torque = 0f;
            }
            else
            {
                //去掉Nm
                var nnm = t.IndexOf("Nm");
                t = t.Remove(nnm);
                t = t.Trim();
                result.Torque = Convert.ToSingle(t);
            }
            //07 结果角度
            var strA = str.Substring(na + "A:".Length);
            strA = strA.Trim();
            var a = strA.Split(" ")[0];
            //去掉deg
            var ndeg = a.IndexOf("deg");
            a = a.Remove(ndeg);
            result.Angle = Convert.ToSingle(a);

            result.PaseResult = true;
            result.PaseMsg = "成功";
        }
        catch (Exception e)
        {
            result.PaseResult = false;
            result.PaseMsg =$"{str}:" + e.Message;
        }

        return result;
    }
}
