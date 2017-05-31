using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*IntegralRecordDomain integralRecord = new IntegralRecordDomain();
            integralRecord.userName = "test";
            integralRecord.storeId = 6396;
            string LCSWeiXinUrl = ConfigurationManager.AppSettings["LCSCallBackUrl"];
            string paras = string.Format("storeId={0}&userName={1}", 6396, "test");
            htttpPostStr(LCSWeiXinUrl + "/api/open/weixin/weixin/WeiXinReceipt", paras);*/
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult NewTest()
        {
            return View();
        }

        public ActionResult Upload(int storeId = 0)
        {
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;

            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string imgPath = "";
            if (hfc.Count > 0)
            {
                imgPath = "/testUpload" + hfc[0].FileName;
                string PhysicalPath = Server.MapPath(imgPath);
                hfc[0].SaveAs(PhysicalPath);
            }
            //注意要写好后面的第二第三个参数
            return Json(new { Id = 1, name = 222, imgPath1 = imgPath }, "text/html", JsonRequestBehavior.AllowGet);
        }

        public static string htttpPostStr(string url, string data, int timeout = 15000)
        {
            string strResult = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            try
            {
                Stream reqstream = request.GetRequestStream();
                reqstream.Write(bytes, 0, bytes.Length);
                //读数据
                request.Timeout = timeout;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
                strResult = streamReader.ReadToEnd();

                //关闭流
                reqstream.Close();
                streamReader.Close();
                streamReceive.Close();
                request.Abort();
                response.Close();
            }
            catch (WebException)
            {
            }
            return strResult;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}