using System;

namespace Model.Dmgl
{
    /// <summary>
    /// 考核指标Jd_khzb
    /// </summary>
    public class Jd_khzb
    {
        #region 构造函数

        public Jd_khzb()
        {
            _Pkid = "";
            _Bmbh = "";
            _Zbdm = "";
            _Zbmc = "";
            _Zbqx = "G";
            _Fzgs = "1";
            _Zdfz = 0;
            _Pxxh = 0;
            _Txsm = "";
            _Yxcy = false;
            _Yxsc = false;
            _Yxdn = false;
            _Sfgd = false;
            _Sfqy = false;
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

        private string _Zbdm;
        /// <summary>
        /// 指标代码
        /// </summary>
        public string Zbdm
        {
            get { return _Zbdm; }
            set { _Zbdm = value; }
        }

        private string _Zbmc;
        /// <summary>
        /// 指标名称
        /// </summary>
        public string Zbmc
        {
            get { return _Zbmc; }
            set { _Zbmc = value; }
        }

        private string _Zbqx;
        /// <summary>
        /// 指标权限（G:个人申报,B:部门申报,P:分类）
        /// </summary>
        public string Zbqx
        {
            get { return _Zbqx; }
            set { _Zbqx = value; }
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

        private int _Pxxh;
        /// <summary>
        /// 排序序号（0:默认，按Aid排序）
        /// </summary>
        public int Pxxh
        {
            get { return _Pxxh; }
            set { _Pxxh = value; }
        }

        private string _Txsm;
        /// <summary>
        /// 填写说明
        /// </summary>
        public string Txsm
        {
            get { return _Txsm; }
            set { _Txsm = value; }
        }

        private bool _Yxcy;
        /// <summary>
        /// 是否允许成员（0:不允许，1:允许）
        /// </summary>
        public bool Yxcy
        {
            get { return _Yxcy; }
            set { _Yxcy = value; }
        }

        private bool _Yxsc;
        /// <summary>
        /// 是否允许上传（0:不允许，1:允许）
        /// </summary>
        public bool Yxsc
        {
            get { return _Yxsc; }
            set { _Yxsc = value; }
        }

        private bool _Yxdn;
        /// <summary>
        /// 是否允许连续使用（0:不允许，1:允许）
        /// </summary>
        public bool Yxdn
        {
            get { return _Yxdn; }
            set { _Yxdn = value; }
        }

        private bool _Sfgd;
        /// <summary>
        /// 是否固定（0:不允许，1:允许）
        /// </summary>
        public bool Sfgd
        {
            get { return _Sfgd; }
            set { _Sfgd = value; }
        }

        private bool _Sfqy;
        /// <summary>
        /// 是否启用（0:不允许，1:允许）
        /// </summary>
        public bool Sfqy
        {
            get { return _Sfqy; }
            set { _Sfqy = value; }
        }

        #endregion
    }
}

