using System;

namespace Model.Jcgl
{
    /// <summary>
    /// 联系人V_jc_lxr
    /// </summary>
    public class V_jc_lxr
    {
        #region 构造函数

        public V_jc_lxr()
        {
            _Pkid = "";
            _Bmbh = "";
            _Bmdm = "";
            _Bmmc = "";
            _Dzbbh = "";
            _Dzbdm = "";
            _Dzbmc = "";
            _Gh = "";
            _Xm = "";
            _Sjhm = "";
            _Lbdm = "";
            _Jslb = "";
            _Issj = "0";
            _Sj = "";
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

        private string _Bmdm;
        /// <summary>
        /// 部门代码
        /// </summary>
        public string Bmdm
        {
            get { return _Bmdm; }
            set { _Bmdm = value; }
        }

        private string _Bmmc;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Bmmc
        {
            get { return _Bmmc; }
            set { _Bmmc = value; }
        }

        private string _Dzbbh;
        /// <summary>
        /// 党支部编号
        /// </summary>
        public string Dzbbh
        {
            get { return _Dzbbh; }
            set { _Dzbbh = value; }
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

        private string _Gh;
        /// <summary>
        /// 工号
        /// </summary>
        public string Gh
        {
            get { return _Gh; }
            set { _Gh = value; }
        }

        private string _Xm;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Xm
        {
            get { return _Xm; }
            set { _Xm = value; }
        }

        private string _Sjhm;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Sjhm
        {
            get { return _Sjhm; }
            set { _Sjhm = value; }
        }
        
        private string _Lbdm;
        /// <summary>
        /// 角色类别代码（0:党支部,1:教师,2:学生）
        /// </summary>
        public string Lbdm
        {
            get { return _Lbdm; }
            set { _Lbdm = value; }
        }

        private string _Jslb;
        /// <summary>
        /// 角色类别
        /// </summary>
        public string Jslb
        {
            get { return _Jslb; }
            set { _Jslb = value; }
        }

        private string _Issj;
        /// <summary>
        /// 是否支部书记
        /// </summary>
        public string Issj
        {
            get { return _Issj; }
            set { _Issj = value; }
        }

        private string _Sj;
        /// <summary>
        /// 支部书记（是/否）
        /// </summary>
        public string Sj
        {
            get { return _Sj; }
            set { _Sj = value; }
        }

        #endregion
    }
}
