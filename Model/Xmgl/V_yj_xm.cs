using System;
namespace Model.Xmgl
{
    /// <summary>
    /// V_yj_xm:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class V_yj_xm
    {
        #region 构造函数

        public V_yj_xm()
        {
            _Pkid = "";
            _Fzztdm = "";
            _Fzzt = "";
            _Bmbh = "";
            _Bmdm = "";
            _Bmmc = "";
            _Bjbh = "";
            _Bjmc = "";
            _Dzbbh = "";
            _Dzbmc = "";
            _Zbbh = "";
            _Zbdm = "";
            _Zbmc = "";
            _Zbqx = "";
            _Yxsc = false;
            _Sfgd = false;
            _Fjsl = 0;
            _Djbh = null;
            _Djmc = "";
            _Fzrbh = "";
            _Xh = "";
            _Xm = "";
            _Sjhm = "";
            _Lxrbh = "";
            _Lxrxm = "";
            _Xmmc = "";
            _Xmrq = "";
            _Jzrq = "";
            _Jlsl = 1;
            _Yjzf = 0;
            _Kfpyjf = 0;
            _Yfpyjf = 0;
            _Bz = "";
            _Bcsj = "";
            _Ztdm = 10;
            _Ztmc = "";
            _Ztxsmc = "";
            _Shrbh = null;
            _Shrxm = null;
            _Shsj = "";
            _Shyj = "";
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

        private string _Fzztdm;
        /// <summary>
        /// 发展状态代码
        /// </summary>
        public string Fzztdm
        {
            get { return _Fzztdm; }
            set { _Fzztdm = value; }
        }

        private string _Fzzt;
        /// <summary>
        /// 发展状态
        /// </summary>
        public string Fzzt
        {
            get { return _Fzzt; }
            set { _Fzzt = value; }
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

        private string _Bmdm;
        /// <summary>
        /// 部门代码
        /// </summary>
        public string Bmdm
        {
            get { return _Bmdm; }
            set { _Bmdm = value; }
        }

        private string _Bmmc;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Bmmc
        {
            get { return _Bmmc; }
            set { _Bmmc = value; }
        }

        private string _Bjbh;
        /// <summary>
        /// 班级编号
        /// </summary>
        public string Bjbh
        {
            get { return _Bjbh; }
            set { _Bjbh = value; }
        }

        private string _Bjmc;
        /// <summary>
        /// 班级名称
        /// </summary>
        public string Bjmc
        {
            get { return _Bjmc; }
            set { _Bjmc = value; }
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
        /// 党支部名称
        /// </summary>
        public string Dzbmc
        {
            get { return _Dzbmc; }
            set { _Dzbmc = value; }
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
        /// 指标权限
        /// </summary>
        public string Zbqx
        {
            get { return _Zbqx; }
            set { _Zbqx = value; }
        }

        private bool _Yxsc;
        /// <summary>
        /// 允许上传
        /// </summary>
        public bool Yxsc
        {
            get { return _Yxsc; }
            set { _Yxsc = value; }
        }

        private bool _Sfgd;
        /// <summary>
        /// 是否固定
        /// </summary>
        public bool Sfgd
        {
            get { return _Sfgd; }
            set { _Sfgd = value; }
        }

        private int _Fjsl;
        /// <summary>
        /// 附件数量
        /// </summary>
        public int Fjsl
        {
            get { return _Fjsl; }
            set { _Fjsl = value; }
        }

        private string _Djbh;
        /// <summary>
        /// 等级编号
        /// </summary>
        public string Djbh
        {
            get { return _Djbh; }
            set { _Djbh = value; }
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

        private string _Fzrbh;
        /// <summary>
        /// 负责人编号
        /// </summary>
        public string Fzrbh
        {
            get { return _Fzrbh; }
            set { _Fzrbh = value; }
        }

        private string _Xh;
        /// <summary>
        /// 学号
        /// </summary>
        public string Xh
        {
            get { return _Xh; }
            set { _Xh = value; }
        }

        private string _Xm;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Xm
        {
            get { return _Xm; }
            set { _Xm = value; }
        }

        private string _Sjhm;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Sjhm
        {
            get { return _Sjhm; }
            set { _Sjhm = value; }
        }

        private string _Lxrbh;
        /// <summary>
        /// 联系人编号
        /// </summary>
        public string Lxrbh
        {
            get { return _Lxrbh; }
            set { _Lxrbh = value; }
        }

        private string _Lxrxm;
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string Lxrxm
        {
            get { return _Lxrxm; }
            set { _Lxrxm = value; }
        }

        private string _Xmmc;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Xmmc
        {
            get { return _Xmmc; }
            set { _Xmmc = value; }
        }

        private string _Xmrq;
        /// <summary>
        /// 项目日期
        /// </summary>
        public string Xmrq
        {
            get { return _Xmrq; }
            set { _Xmrq = value; }
        }

        private string _Jzrq;
        /// <summary>
        /// 截止日期
        /// </summary>
        public string Jzrq
        {
            get { return _Jzrq; }
            set { _Jzrq = value; }
        }

        private int _Jlsl;
        /// <summary>
        /// 计量数量
        /// </summary>
        public int Jlsl
        {
            get { return _Jlsl; }
            set { _Jlsl = value; }
        }

        private int _Yjzf;
        /// <summary>
        /// 业绩总分
        /// </summary>
        public int Yjzf
        {
            get { return _Yjzf; }
            set { _Yjzf = value; }
        }

        private int _Kfpyjf;
        /// <summary>
        /// 可分配业绩分
        /// </summary>
        public int Kfpyjf
        {
            get { return _Kfpyjf; }
            set { _Kfpyjf = value; }
        }

        private int _Yfpyjf;
        /// <summary>
        /// 已分配业绩分
        /// </summary>Y
        public int Yfpyjf
        {
            get { return _Yfpyjf; }
            set { _Yfpyjf = value; }
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

        private string _Bcsj;
        /// <summary>
        /// 保存时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Bcsj
        {
            get { return _Bcsj; }
            set { _Bcsj = value; }
        }

        private int _Ztdm;
        /// <summary>
        /// 项目状态代码
        /// </summary>
        public int Ztdm
        {
            get { return _Ztdm; }
            set { _Ztdm = value; }
        }

        private string _Ztmc;
        /// <summary>
        /// 项目状态
        /// </summary>
        public string Ztmc
        {
            get { return _Ztmc; }
            set { _Ztmc = value; }
        }

        private string _Ztxsmc;
        /// <summary>
        /// 项目状态显示名称
        /// </summary>
        public string Ztxsmc
        {
            get { return _Ztxsmc; }
            set { _Ztxsmc = value; }
        }

        private string _Shrbh;
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string Shrbh
        {
            get { return _Shrbh; }
            set { _Shrbh = value; }
        }

        private string _Shrxm;
        /// <summary>
        /// 审核人姓名
        /// </summary>
        public string Shrxm
        {
            get { return _Shrxm; }
            set { _Shrxm = value; }
        }

        private string _Shsj;
        /// <summary>
        /// 审核时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Shsj
        {
            get { return _Shsj; }
            set { _Shsj = value; }
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

        #endregion

    }
}

