using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class RedPackHelper
    {

        private static string GetCode(int num)
        {
            string a = "0123456789";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length - 1)]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 发送红包
        /// </summary>
        /// <param name="acid">项目编号</param>
        /// <param name="hid">红包编号</param>
        /// <param name="openid">用户openid</param>
        /// <param name="orderid">订单编号（yyyyMMdd+10位一天内不能重复的数字）</param>
        /// <param name="money">红包金额(1元:100,最小100)</param>
        /// <param name="dt">当前时间戳</param>
        /// <returns></returns>
        public result send(int acid, int hid, string openid, string orderid, int money, string ckey, string hkey)
        {
            result returndata = new result();
             
            string vkey = "ZZCXXCZ090115";
            string dt = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            //string openid = "";
            //orderid = DateTime.Now.ToString("yyyyMMddh") + GetCode(10);//yyyymmdd+10位一天内不能重复的数字;
            //orderid = DateTime.Now.ToString("yyyyMMdd") + GetCode(10);//yyyymmdd+10位一天内不能重复的数字;
            //int money = 1;
            //int acid =1;
            //int hid = 1;
            //string ckey = "OIM1u72sz01";//项目密钥
            //string hkey = "olskeg891LW";//红包密钥

            string sign1 = Common.getMD5.MD5(vkey + dt + openid + orderid).ToUpper();
            string sign = Common.getMD5.MD5(vkey + ckey + hkey + dt + openid + orderid + acid + hid + money).ToUpper();
            //string url = "http://wxhb.esmartwave.com/redpack/yh_send.aspx?sign=" + sign + "&sign1=" + sign1 + "&openid=" + openid + "&orderid=" + orderid + "&acid=" + acid + "&hid=" + hid + "&money=" + money + "&dt=" + dt;
            string url = "http://redpack.esmartwave.com/Controller/RedPackAPI.ashx?sign=" + sign + "&sign1=" + sign1 + "&openid=" + openid + "&orderid=" + orderid + "&acid=" + acid + "&hid=" + hid + "&money=" + money + "&dt=" + dt;

            //String message=do=Xml.DocXml(userName, MD5Encode(password), msgid, phone, content, sign, subcode, sendtime);

            string data="";
            try
            {
                data = WebNet.doPost(url, "");
                returndata = Common.JsonHelper.JsonDeserialize<result>(data);
            }
            catch
            {
                returndata.SendStatus = false;
                returndata.STATUS = "CustomFail";
                returndata.MSG = "系统错误-" + data;
            }
            //("{\"MSG\" : 项目错误,\"STATUS\" : \"ERROR\"}");//项目错误
            //("{\"MSG\" : 红包错误,\"STATUS\" : \"ERROR\"}");//红包项目错误
            //("{\"MSG\" : S_ERR,\"STATUS\" : \"ERROR\"}");//密钥失效
            //("{\"MSG\" : M_MAX,\"STATUS\" : \"ERROR\"}");//超过红包最大金额
            //("{\"MSG\" : H_MAX,\"STATUS\" : \"ERROR\"}");//超过项目设定个人红包总数
            //("{\"MSG\" : C_MAX,\"STATUS\" : \"ERROR\"}");//超过项目设定红包总数
            //("{\"MSG\" : 发送成功,\"STATUS\" : \"SUCCESS\"}");
            //("{\"MSG\" : DATA_ERR,\"STATUS\" : \"ERROR\"}");//订单插入失败
            //("{\"MSG\" : P_UNLINE,\"STATUS\" : \"ERROR\"}");//项目已下线
            //("{\"MSG\" : 发送成功,\"STATUS\" : \"SUCCESS\"}");
            //("{\"MSG\" : 发送失败,\"STATUS\" : \"ERROR\"}");

            if (returndata.STATUS == "SUCCESS")
            {
                //Model.RedPack_LogModel repkdel = new Model.RedPack_LogModel();
                //repkdel.Acid = acid + "";
                //repkdel.Ctime = DateTime.Now;
                //repkdel.Hid = hid + "";
                //repkdel.Money = "" + money;
                //repkdel.Openid = openid;
                //repkdel.Orderid = orderid;
                //repkdel.Note = "";

                //Db.RedPack_LogDal repkdal = new Db.RedPack_LogDal();
                //repkdal.Add(repkdel);

                returndata.SendStatus = true;
            }
            else
            {
                returndata.SendStatus = false;

                if (returndata.MSG == "订单号格式错误")
                { 
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "签名格式错误")
                { 
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "订单签名错误")
                { 
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "项目错误")
                { 
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "红包错误")
                { 
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "红包签名错误")
                { 
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "项目已下线")
                {
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "超过红包最大金额")
                {
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "超过项目设定个人红包总数")
                {
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "超过项目设定红包总数")
                {
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "金额与订单不符")
                { 
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "订单已取消")
                { 
                    returndata.STATUS = "CustomFail";
                }
                if (returndata.MSG == "微信订单插入失败，请稍后重试")
                { 
                    returndata.STATUS = "CustomFail";
                } 
                if (returndata.STATUS == "CustomFail")
                {
                    EmailTool.sendEmail(ConfigurationManager.AppSettings["LogErrorEmailTo"], HttpContext.Current.Request.Url.Host, returndata.MSG + ",红包订单号：" + orderid + ",Openid：" + openid, "");
                } 
            }

            return returndata;
        }

        public class result
        {

            public bool SendStatus { get; set; }
            public string MSG { get; set; }
            public string STATUS { get; set; }

        }
    }

    

}   
