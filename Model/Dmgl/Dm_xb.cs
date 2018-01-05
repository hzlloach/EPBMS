using System;

namespace Model.Dmgl
{
    /// <summary>
    /// 性别代码Dm_xb
    /// </summary>
    public class Dm_xb
    {
        #region 构造函数

        public Dm_xb()
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
        /// 性别代码
        /// </summary>
        public string Dm
        {
            get { return _Dm; }
            set { _Dm = value; }
        }

        private string _Mc;
        /// <summary>
        /// 性别名称（0：男，1：女）
        /// </summary>
        public string Mc
        {
            get { return _Mc; }
            set { _Mc = value; }
        }

        #endregion
    }
}
