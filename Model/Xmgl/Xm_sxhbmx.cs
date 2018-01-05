using System;

namespace Model.Xmgl
{
    /// <summary>
    /// 思想汇报明细Xm_sxhbmx
    /// </summary>
    public class Xm_sxhbmx
    {
        #region 构造函数

        public Xm_sxhbmx()
        {
            _Pkid = "";
            _Hbbh = "";
            _Hbnr = "";
            _Pyyj = "";
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

        private string _Hbbh;
        /// <summary>
        /// 汇报编号
        /// </summary>
        public string Hbbh
        {
            get { return _Hbbh; }
            set { _Hbbh = value; }
        }

        private string _Hbnr;
        /// <summary>
        /// 汇报内容
        /// </summary>
        public string Hbnr
        {
            get { return _Hbnr; }
            set { _Hbnr = value; }
        }

        private string _Pyyj;
        /// <summary>
        /// 评阅意见
        /// </summary>
        public string Pyyj
        {
            get { return _Pyyj; }
            set { _Pyyj = value; }
        }

        #endregion
    }
}
