using System;

namespace Model.Jcgl
{
    /// <summary>
    /// 班级Jd_bj
    /// </summary>
    public class Jd_bj
    {
        #region 构造函数

        public Jd_bj()
        {
            _Pkid = "";
            _Bmbh = "";
            _Dzbbh = "";
            _Zybh = "";
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

        private string _Dzbbh;
        /// <summary>
        /// 党支部编号
        /// </summary>
        public string Dzbbh
        {
            get { return _Dzbbh; }
            set { _Dzbbh = value; }
        }

        private string _Zybh;
        /// <summary>
        /// 专业编号
        /// </summary>
        public string Zybh
        {
            get { return _Zybh; }
            set { _Zybh = value; }
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
