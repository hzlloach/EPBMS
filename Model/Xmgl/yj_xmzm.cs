using System;

namespace Model.Yjgl
{
    /// <summary>
    /// 业绩项目证明Yj_xmzm
    /// </summary>
    public class Yj_xmzm
    {
        #region 构造函数

        public Yj_xmzm()
        {
            _Aid = 0;
            _Pkid = "";
            _Xmbh = "";
            _Clbt = "";
            _Cflj = "";
            _Bz = "";
        }

        #endregion

        #region 属性

        private int _Aid;
        /// <summary>
        /// 自增序号
        /// </summary>
        public int Aid
        {
            get { return _Aid; }
        }

        private string _Pkid;
        /// <summary>
        /// 序号
        /// </summary>
        public string Pkid
        {
            get { return _Pkid; }
            set { _Pkid = value; }
        }

        private string _Xmbh;
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Xmbh
        {
            get { return _Xmbh; }
            set { _Xmbh = value; }
        }

        private string _Clbt;
        /// <summary>
        /// 材料标题
        /// </summary>
        public string Clbt
        {
            get { return _Clbt; }
            set { _Clbt = value; }
        }

        private string _Cflj;
        /// <summary>
        /// 存放路径
        /// </summary>
        public string Cflj
        {
            get { return _Cflj; }
            set { _Cflj = value; }
        }
                
        /// <summary>
        /// 原存放路径
        /// </summary>
        public string OldCflj;

        private string _Bz;
        /// <summary>
        /// 备注
        /// </summary>
        public string Bz
        {
            get { return _Bz; }
            set { _Bz = value; }
        }

        #endregion
    }
}
