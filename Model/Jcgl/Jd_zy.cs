using System;

namespace Model.Jcgl
{
    /// <summary>
    /// 专业Jd_zy
    /// </summary>
    public class Jd_zy
    {
        #region 构造函数

        public Jd_zy()
        {
            _Pkid = "";
            _Bmbh = "";
            _Dzbbh = "";
            _Zydm = "";
            _Zymc = "";
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

        private string _Zydm;
        /// <summary>
        /// 专业代码
        /// </summary>
        public string Zydm;
        //{
        //    get { return _Zydm; }
        //    set { _Zydm = value; }
        //}

        private string _Zymc;
        /// <summary>
        /// 专业名称
        /// </summary>
        public string Zymc
        {
            get { return _Zymc; }
            set { _Zymc = value; }
        }

        #endregion
    }
}
