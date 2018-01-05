using System;

namespace Model.Dmgl
{
    /// <summary>
    /// 项目等级Jd_xmdj
    /// </summary>
    public class Jd_xmdj
    {
        #region 构造函数

        public Jd_xmdj()
        {
            _Pkid = "";
            _Bmbh = "";
            _Zbbh = "";
            _Djmc = "";
            _Fzgs = "0";
            _Zdfz = 0;
            _Synx = 1;
            _Pxxh = 0;
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

        private string _Zbbh;
        /// <summary>
        /// 指标编号
        /// </summary>
        public string Zbbh
        {
            get { return _Zbbh; }
            set { _Zbbh = value; }
        }

        private string _Djmc;
        /// <summary>
        /// 等级名称
        /// </summary>
        public string Djmc
        {
            get { return _Djmc; }
            set { _Djmc = value; }
        }

        private string _Fzgs;
        /// <summary>
        /// 分值公式
        /// </summary>
        public string Fzgs
        {
            get { return _Fzgs; }
            set { _Fzgs = value; }
        }

        private int _Zdfz;
        /// <summary>
        /// 最大分值（0:不限）
        /// </summary>
        public int Zdfz
        {
            get { return _Zdfz; }
            set { _Zdfz = value; }
        }

        private int _Synx;
        /// <summary>
        /// 使用年限（1:当年）
        /// </summary>
        public int Synx
        {
            get { return _Synx; }
            set { _Synx = value; }
        }

        private int _Pxxh;
        /// <summary>
        /// 排序序号（0:默认，按Aid排序）
        /// </summary>
        public int Pxxh
        {
            get { return _Pxxh; }
            set { _Pxxh = value; }
        }

        #endregion
    }
}
