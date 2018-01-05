using System;

namespace Model.Jcgl
{
    /// <summary>
    /// 党支部V_jd_dzb
    /// </summary>
    public class V_jd_dzb
    {
        #region 构造函数

        public V_jd_dzb()
        {
            _Pkid = "";
            _Bmbh = "";
            _Bmdm = "";
            _Bmmc = "";
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

        #endregion
    }
}
