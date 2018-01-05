using System;

namespace Model.Dmgl
{
    /// <summary>
    /// 党校考核状态代码Dm_khzt
    /// </summary>
    public class Dm_khzt
    {
        #region 构造函数

        public Dm_khzt()
        {
            _Pkid = "";
            _Dm = "";
            _Mc = "";
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

        private string _Dm;
        /// <summary>
        /// 考核状态代码
        /// </summary>
        public string Dm
        {
            get { return _Dm; }
            set { _Dm = value; }
        }

        private string _Mc;
        /// <summary>
        /// 考核状态名称（0：不合格，1：合格，2：优秀）
        /// </summary>
        public string Mc
        {
            get { return _Mc; }
            set { _Mc = value; }
        }

        #endregion
    }
}
