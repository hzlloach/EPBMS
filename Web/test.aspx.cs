using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace Web
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.OnClientClick = Confirm.GetShowReference("确认删除？", String.Empty, MessageBoxIcon.Warning, "delImage(3);return false;", String.Empty);
        }
    }
}