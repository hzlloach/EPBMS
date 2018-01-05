using System;

namespace Model.Jcgl
{
    /// <summary>
    /// 党支部Jd_dzb
    /// </summary>
    public class Jd_dzb
    {
        #region 构造函数

        public Jd_dzb()
        {
            _Pkid = "";
            _Bmbh = "";
            _Dzbdm = "";
            _Dzbmc = "";
        }

        #endregion

        #region 属性

        private string _Pkid;
        /// <summary>
        /// 序号
        /// </summary>
        public string Pkid
        {
            get { return _Pkid; }
            set { _Pkid = value; }
        }

        private string _Bmbh;
        /// <summary>
        /// 部门编号
        /// </summary>
        public string Bmbh
        {
            get { return _Bmbh; }
            set { _Bmbh = value; }
        }

        private string _Dzbdm;
        /// <summary>
        /// 党支部代码
        /// </summary>
        public string Dzbdm
        {
            get { return _Dzbdm; }
            set { _Dzbdm = value; }
        }

        private string _Dzbmc;
        /// <summary>
        /// 党支部名称
        /// </summary>
        public string Dzbmc
        {
            get { return _Dzbmc; }
            set { _Dzbmc = value; }
        }

        /// <summary>
        /// 登录帐号名
        /// </summary>
        public string UserID;

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password;

        #endregion
    }
}
