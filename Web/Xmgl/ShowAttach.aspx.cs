using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TU = TStar.Utility;

namespace Web.Xmgl
{
    public partial class ShowAttach : TStar.Web.BasePage
    {
        private string Url
        {
            get
            {
                string url = TU.Globals.GetParaValue("url", "").ToLower();
                return TStar.Utility.Globals.DecodeUrl(url);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Url;
            if (url.EndsWith(".pdf"))
            {
                this.pnlImg.Hidden = true;
                this.Panel.IFrameUrl = url;
            }
            else
            {
                this.img.ImageUrl = url;
            }
        }
    }
}