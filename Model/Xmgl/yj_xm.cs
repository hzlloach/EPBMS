using System;

namespace Model.Xmgl
{
    /// <summary>
    /// 业绩项目Yj_xm
    /// </summary>
    public class Yj_xm
    {
        #region 构造函数

        public Yj_xm()
        {
            _Pkid = "";
            _Fzztdm = "2";
            _Bmbh = "";
            _Zbbh = "";
            _Djbh = null;
            _Ztbh = null;
            _Fzrbh = "";
            Fzrxm = "";
            _Xmmc = "";
            _Xmrq = "";
            _Jzrq = "";
            _Jlsl = 1;
            _Yjzf = 0;
            _Kfpyjf = 0;
            _Yfpyjf = 0;
            _Syyjf = 0;
            _Bz = "";
            _Bcsj = "";
            _Ztdm = 10;
            _Shrbh = null;
            _Shsj = "";
            _Shyj = "";
        }

        #endregion

        #region 属性

        /// <summary>
        /// 项目名称是否被修改
        /// </summary>
        public bool IsXmmcModified
        {
            get { return this.OldXmmc != this.Xmmc; }
        }
        /// <summary>
        /// 项目指标是否被修改
        /// </summary>
        public bool IsXmzbModified
        {
            get { return this.OldZbbh != this.Zbbh || this.OldDjbh != this.Djbh || this.OldZtbh != this.Ztbh; }
        }
        /// <summary>
        /// 计量数量是否被修改
        /// </summary>
        public bool IsJlslModified
        {
            get { return this.OldJlsl != this.Jlsl; }
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

        private string _Fzztdm;
        /// <summary>
        /// 发展状态代码
        /// </summary>
        public string Fzztdm
        {
            get { return _Fzztdm; }
            set { _Fzztdm = value; }
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
            set { _OldZbbh = _Zbbh; _Zbbh = value; }
        }
        private string _OldZbbh;
        /// <summary>
        /// 原始指标编号
        /// </summary>
        public string OldZbbh
        {
            get { return _OldZbbh; }
        }

        private string _Djbh;
        /// <summary>
        /// 等级编号
        /// </summary>
        public string Djbh
        {
            get { return _Djbh; }
            set { _OldDjbh = _Djbh; _Djbh = value; }
        }
        private string _OldDjbh;
        /// <summary>
        /// 原始状态编号
        /// </summary>
        public string OldDjbh
        {
            get { return _OldDjbh; }
        }

        private string _Ztbh;
        /// <summary>
        /// 状态编号
        /// </summary>
        public string Ztbh
        {
            get { return _Ztbh; }
            set { _OldZtbh = _Ztbh; _Ztbh = value; }
        }
        private string _OldZtbh;
        /// <summary>
        /// 原始状态编号
        /// </summary>
        public string OldZtbh
        {
            get { return _OldZtbh; }
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

        //private string _Fzrxm;
        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string Fzrxm;
        //{
        //    get { return _Fzrxm; }
        //    set { _Fzrxm = value; }
        //}

        private string _Xmmc;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Xmmc
        {
            get { return _Xmmc; }
            set { _OldXmmc = _Xmmc; _Xmmc = value; }
        }
        private string _OldXmmc;
        /// <summary>
        /// 原始的项目名称
        /// </summary>
        public string OldXmmc
        {
            get { return _OldXmmc; }
        }

        private string _Xmrq;
        /// <summary>
        /// 项目日期（默认为当前学年的起始日期）
        /// </summary>
        public string Xmrq
        {
            get { return _Xmrq; }
            set { _Xmrq = value; }
        }

        private string _Jzrq;
        /// <summary>
        /// 使用截止日期（默认为当前学年的截止日期）
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
            set { _OldJlsl = _Jlsl; _Jlsl = value; }
        }
        private int _OldJlsl;
        /// <summary>
        /// 原始的计量数量
        /// </summary>
        public int OldJlsl
        {
            get { return _OldJlsl; }
        }

        private int _Yjzf;
        /// <summary>
        /// 业绩总分（保存后自动计算）
        /// </summary>
        public int Yjzf
        {
            get { return _Yjzf; }
            set { _Yjzf = value; }
        }

        private int _Kfpyjf;
        /// <summary>
        /// 可分配业绩分（保存后自动计算）
        /// </summary>
        public int Kfpyjf
        {
            get { return _Kfpyjf; }
            set { _OldKfpyjf = _Kfpyjf; _Kfpyjf = value; }
        }
        private int _OldKfpyjf;
        /// <summary>
        /// 原始可分配业绩分
        /// </summary>
        public int OldKfpyjf
        {
            get { return _OldKfpyjf; }
        }

        private int _Yfpyjf;
        /// <summary>
        /// 已分配业绩分
        /// </summary>Y
        public int Yfpyjf
        {
            get { return _Yfpyjf; }
            set { _OldYfpyjf = _Yfpyjf; _Yfpyjf = value; }
        }
        private int _OldYfpyjf;
        /// <summary>
        /// 原始已分配业绩分
        /// </summary>
        public int OldYfpyjf
        {
            get { return _OldYfpyjf; }
        }

        private int _Syyjf;
        /// <summary>
        /// 已分配业绩分
        /// </summary>Y
        public int Syyjf
        {
            get { return _Syyjf; }
            set { _Syyjf = value; }
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

        private string _Shrbh;
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string Shrbh
        {
            get { return _Shrbh; }
            set { _Shrbh = value; }
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
