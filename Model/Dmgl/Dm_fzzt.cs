using System;

namespace Model.Dmgl
{
    /// <summary>
    /// 党员发展状态代码Dm_fzzt
    /// </summary>
    public class Dm_fzzt
    {
        #region 构造函数

        public Dm_fzzt()
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
        /// 发展状态代码
        /// </summary>
        public string Dm
        {
            get { return _Dm; }
            set { _Dm = value; }
        }

        private string _Mc;
        /// <summary>
        /// 发展状态名称（1：入党申请人，2：入党积极分子，3：拟发展对象，4：发展对象，5：预备党员，6：正式党员）
        /// </summary>
        public string Mc
        {
            get { return _Mc; }
            set { _Mc = value; }
        }

        #endregion
    }
}
