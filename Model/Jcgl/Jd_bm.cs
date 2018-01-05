using System;

namespace Model.Jcgl
{
    /// <summary>
    /// 部门Jd_bm
    /// </summary>
    public class Jd_bm
    {
        #region 构造函数

        public Jd_bm()
        {
            _Pkid = "";
            _Bmdm = "";
            _Bmmc = "";
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

        #endregion
    }
}
