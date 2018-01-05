using System;

namespace Model.Lcgl
{
    /// <summary>
    /// 转正名单V_lc_zzmd
    /// </summary>
    public class V_lc_zzmd
    {
        #region 构造函数

        public V_lc_zzmd()
        {
            _Aid = 0;
            _Pkid = "";
            _Xq = "";
            _Bmbh = "";
            _Dzbbh = "";
            _Dzbmc = "";
            _Xsbh = "";
            _Bjjgdm = "";
            _Bjjg = "";
            _Yqzzrq = "";
            _Zbdhrq = "";
            _Fdwshrq = "";
            _Shyj = "";
            _Bz = "";
            _Drsj = "";
            _Ztdm = 10;
            _Ztmc = "";
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

        private string _Xq;
        /// <summary>
        /// 学期
        /// </summary>
        public string Xq
        {
            get { return _Xq; }
            set { _Xq = value; }
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

        private string _Dzbmc;
        /// <summary>
        /// 党支部编号
        /// </summary>
        public string Dzbmc
        {
            get { return _Dzbmc; }
            set { _Dzbmc = value; }
        }

        private string _Xsbh;
        /// <summary>
        /// 学生编号
        /// </summary>
        public string Xsbh
        {
            get { return _Xsbh; }
            set { _Xsbh = value; }
        }

        private string _Bjjgdm;
        /// <summary>
        /// 表决结果代码
        /// </summary>
        public string Bjjgdm
        {
            get { return _Bjjgdm; }
            set { _Bjjgdm = value; }
        }

        private string _Bjjg;
        /// <summary>
        /// 表决结果
        /// </summary>
        public string Bjjg
        {
            get { return _Bjjg; }
            set { _Bjjg = value; }
        }

        private string _Yqzzrq;
        /// <summary>
        /// 延期转正日期（格式：yyyyMMdd）
        /// </summary>
        public string Yqzzrq
        {
            get { return _Yqzzrq; }
            set { _Yqzzrq = value; }
        }

        private string _Zbdhrq;
        /// <summary>
        /// 支部大会日期（格式：yyyyMMdd）
        /// </summary>
        public string Zbdhrq
        {
            get { return _Zbdhrq; }
            set { _Zbdhrq = value; }
        }

        private string _Fdwshrq;
        /// <summary>
        /// 分党委审核日期（格式：yyyyMMdd）
        /// </summary>
        public string Fdwshrq
        {
            get { return _Fdwshrq; }
            set { _Fdwshrq = value; }
        }

        private string _Shyj;
        /// <summary>
        /// 审核意见
        /// </summary>
        public string Shyj
        {
            get { return _Shyj; }
            set { _Shyj = value; }
        }

        private string _Bz;
        /// <summary>
        /// 备注
        /// </summary>
        public string Bz
        {
            get { return _Bz; }
            set { _Bz = value; }
        }

        private string _Drsj;
        /// <summary>
        /// 导入时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Drsj
        {
            get { return _Drsj; }
            set { _Drsj = value; }
        }

        private int _Ztdm;
        /// <summary>
        /// 状态代码
        /// </summary>
        public int Ztdm
        {
            get { return _Ztdm; }
            set { _Ztdm = value; }
        }

        private string _Ztmc;
        /// <summary>
        /// 状态名称
        /// </summary>
        public string Ztmc
        {
            get { return _Ztmc; }
            set { _Ztmc = value; }
        }

        #endregion
    }
}
