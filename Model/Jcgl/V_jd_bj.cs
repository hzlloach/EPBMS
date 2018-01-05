using System;

namespace Model.Jcgl
{
    /// <summary>
    /// 班级V_jd_bj
    /// </summary>
    public class V_jd_bj
    {
        #region 构造函数

        public V_jd_bj()
        {
            _Pkid = "";
            _Bmbh = "";
            _Bmmc = "";
            _Dzbbh = "";
            _Dzbmc = "";
            _Bjmc = "";
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

        private string _Dzbmc;
        /// <summary>
        /// 党支部名称
        /// </summary>
        public string Dzbmc
        {
            get { return _Dzbmc; }
            set { _Dzbmc = value; }
        }

        private string _Bjmc;
        /// <summary>
        /// 班级名称
        /// </summary>
        public string Bjmc
        {
            get { return _Bjmc; }
            set { _Bjmc = value; }
        }

        #endregion
    }
}
