using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;

namespace Web.Xtgl
{
    public partial class Download : System.Web.UI.Page
    {
        public string Param
        {
            get { return TU.Globals.GetParaValue("param", "").Replace("@", "+"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(Param)) return;

                string file = TU.Globals.TripleDESDecrypt(Param);
                DownloadFile(file);
            }
            catch (Exception err)
            {
                Alert.ShowInTop(err.Message, "下载失败", MessageBoxIcon.Error);
            }
        }

        private void DownloadFile(string fullFilePath)
        {
            if (!File.Exists(fullFilePath)) return;

            //以字符流的形式下载文件  
            FileStream fs = new FileStream(fullFilePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream"; // 未知文件类型

            //通知浏览器下载文件而不是打开  
            string filename = fullFilePath.Substring(fullFilePath.LastIndexOf('\\') + 1);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);//HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        private void DownloadFile2(string fullFilePath)
        {
            if (!File.Exists(fullFilePath)) return;

            string filename = fullFilePath.Substring(fullFilePath.LastIndexOf('\\') + 1);
            const long ChunkSize = 409600;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力  
            byte[] buffer = new byte[ChunkSize];

            Response.Clear();
            FileStream iStream = File.OpenRead(fullFilePath);
            long dataLengthToRead = iStream.Length;//获取下载的文件总大小  
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);//HttpUtility.UrlEncode(filename));
            while (dataLengthToRead > 0 && Response.IsClientConnected)
            {
                int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小  
                Response.OutputStream.Write(buffer, 0, lengthRead);
                Response.Flush();
                dataLengthToRead = dataLengthToRead - lengthRead;
            }
            Response.Close();
        }
    }
}