using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DownFiles()
        {
            return View();
        }
        public ActionResult bootsharpuploadfiles()
        {
            return View();
        }
        public ActionResult Bootstrapfileinput()
        {
            return View();
        }
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }


        public ActionResult SaveDownload(string uri, string filefullpath)
        {
            int size=1024;
            bool savesuccessornot = HttpDownLoadFiles(uri, filefullpath, size);
            if (savesuccessornot == true)
            {
                return Json(new { Message = "success" });
            }else
            {
                return Json(new { Message = "fail" });
            }
        }
        /// <summary>
        /// Http下载文件支持断点续传
        /// </summary>
        /// <param name="uri">下载地址</param>
        /// <param name="filefullpath">存放完整路径（含文件名）</param>
        /// <param name="size">每次多的大小</param>
        /// <returns>下载操作是否成功</returns>
        public static bool HttpDownLoadFiles(string uri, string filefullpath, int size = 1024)
        {
            try
            {
                string fileDirectory = System.IO.Path.GetDirectoryName(filefullpath);
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
                string fileFullPath = filefullpath;
                string fileTempFullPath = filefullpath;
                if (System.IO.File.Exists(fileFullPath))
                {
                    return true;
                }
                else
                {
                    if (System.IO.File.Exists(fileTempFullPath))
                    {
                        FileStream fs = new FileStream(fileTempFullPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        byte[] buffer = new byte[512];
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                        request.Timeout = 10000;
                        request.AddRange((int)fs.Length);
                        Stream ns = request.GetResponse().GetResponseStream();
                        long contentLength = request.GetResponse().ContentLength;
                        int length = ns.Read(buffer, 0, buffer.Length);
                        while (length > 0)
                        {
                            fs.Write(buffer, 0, length);
                            buffer = new byte[512];
                            length = ns.Read(buffer, 0, buffer.Length);
                        }
                        fs.Close();
                        System.IO.File.Move(fileTempFullPath, fileFullPath);
                    }
                    else
                    {
                        FileStream fs = new FileStream(fileTempFullPath, FileMode.Create);
                        byte[] buffer = new byte[512];
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                        request.Timeout = 10000;
                        request.AddRange((int)fs.Length);
                        Stream ns = request.GetResponse().GetResponseStream();
                        long contentLength = request.GetResponse().ContentLength;
                        int length = ns.Read(buffer, 0, buffer.Length);
                        while (length > 0)
                        {
                            fs.Write(buffer, 0, length);
                            buffer = new byte[512];
                            length = ns.Read(buffer, 0, buffer.Length);
                        }
                        fs.Close();
                        System.IO.File.Move(fileTempFullPath, fileFullPath);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
    }
}
