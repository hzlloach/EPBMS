using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TU = TStar.Utility;

namespace TStar.Web
{
    /// <summary>
    /// Web页的基类
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            Globals.Account.CheckAuthority();
            base.OnPreInit(e);
        }
        
        #region 自定义属性

        protected string Pkid
        {
            get { return TU.Globals.GetParaValue("pkid", ""); }
        }
        protected bool IsAdd
        {
            get { return String.IsNullOrEmpty(Pkid); }
        }

        #endregion
    }
}
