using System;

namespace Model.Dmgl
{
    /// <summary>
    /// 结果状态代码Dm_jgzt
    /// </summary>
    public class Dm_jgzt
    {
        #region 构造函数

        public Dm_jgzt()
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
        /// 结果状态代码
        /// </summary>
        public string Dm
        {
            get { return _Dm; }
            set { _Dm = value; }
        }

        private string _Mc;
        /// <summary>
        /// 结果状态名称（0：未通过，1：通过）
        /// </summary>
        public string Mc
        {
            get { return _Mc; }
            set { _Mc = value; }
        }

        #endregion
    }
}
