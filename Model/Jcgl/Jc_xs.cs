using System;

namespace Model.Jcgl
{
    /// <summary>
    /// 学生Jc_xs
    /// </summary>
    public class Jc_xs
    {
        #region 构造函数

        public Jc_xs()
        {
            _Pkid = "";
            _Bmbh = "";
            _Dzbbh = "";
            _Zybh = "";
            _Bjbh = "";
            _Xh = "";
            _Xm = "";
            _Sfzh = "";
            _Xbdm = "1";
            _Fzztdm = "2";
            _Csrq = "";
            _Jg = "";
            _Mz = "";
            _Sjhm = "";
            _QQ = "";
            _Zw = "";
            _Jtdz = "";
            _Sqrdrq = "";
            _Jjfzrq = "";
            _Dxkhztdm = null;
            _Dxjyrq = "";
            _Xxcjpm = "";
            _Zhkppm = "";
            _Bjgms = "";
            _Fzdxrq = "";
            _Rdrq = "";
            _Zzrq = "";
            _Zysbh = "";
            _Rdlxrbh1 = "";
            _Rdlxrbh2 = null;
            _Bz = "";
            //_PhotoUrl = "";
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

        private string _Dzbbh;
        /// <summary>
        /// 党支部编号
        /// </summary>
        public string Dzbbh
        {
            get { return _Dzbbh; }
            set { _Dzbbh = value; }
        }

        private string _Zybh;
        /// <summary>
        /// 专业编号
        /// </summary>
        public string Zybh
        {
            get { return _Zybh; }
            set { _Zybh = value; }
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

        private string _Sfzh;
        /// <summary>
        /// 身份证号
        /// </summary>
        public string Sfzh
        {
            get { return _Sfzh; }
            set { _Sfzh = value; }
        }

        private string _Xbdm;
        /// <summary>
        /// 性别代码
        /// </summary>
        public string Xbdm
        {
            get { return _Xbdm; }
            set { _Xbdm = value; }
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

        private string _Csrq;
        /// <summary>
        /// 出生日期（格式：yyyyMMdd）
        /// </summary>
        public string Csrq
        {
            get { return _Csrq; }
            set { _Csrq = value; }
        }

        private string _Jg;
        /// <summary>
        /// 籍贯
        /// </summary>
        public string Jg
        {
            get { return _Jg; }
            set { _Jg = value; }
        }

        private string _Mz;
        /// <summary>
        /// 民族
        /// </summary>
        public string Mz
        {
            get { return _Mz; }
            set { _Mz = value; }
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

        private string _QQ;
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            get { return _QQ; }
            set { _QQ = value; }
        }

        private string _Zw;
        /// <summary>
        /// 职务
        /// </summary>
        public string Zw
        {
            get { return _Zw; }
            set { _Zw = value; }
        }

        private string _Jtdz;
        /// <summary>
        /// 家庭详细住址
        /// </summary>
        public string Jtdz
        {
            get { return _Jtdz; }
            set { _Jtdz = value; }
        }

        private string _Sqrdrq;
        /// <summary>
        /// 申请入党日期（格式：yyyyMMdd）
        /// </summary>
        public string Sqrdrq
        {
            get { return _Sqrdrq; }
            set { _Sqrdrq = value; }
        }

        private string _Jjfzrq;
        /// <summary>
        /// 确定积极分子日期（格式：yyyy.MM.dd）
        /// </summary>
        public string Jjfzrq
        {
            get { return _Jjfzrq; }
            set { _Jjfzrq = value; }
        }

        private string _Dxkhztdm;
        /// <summary>
        /// 党校考核状态代码
        /// </summary>
        public string Dxkhztdm
        {
            get { return _Dxkhztdm == "__" ? null : _Dxkhztdm; }
            set { _Dxkhztdm = value; }
        }

        private string _Dxjyrq;
        /// <summary>
        /// 党校结业日期（格式：yyyy.MM.dd）
        /// </summary>
        public string Dxjyrq
        {
            get { return _Dxjyrq.Replace("—", ""); }
            set { _Dxjyrq = value; }
        }

        private string _Xxcjpm;
        /// <summary>
        /// 学习成绩排名（格式：1/2）
        /// </summary>
        public string Xxcjpm
        {
            get { return _Xxcjpm.Replace("—", ""); }
            set { _Xxcjpm = value; }
        }

        private string _Zhkppm;
        /// <summary>
        /// 综合考评排名（格式：1/2）
        /// </summary>
        public string Zhkppm
        {
            get { return _Zhkppm.Replace("—", ""); }
            set { _Zhkppm = value; }
        }

        private string _Bjgms;
        /// <summary>
        /// 不及格门数
        /// </summary>
        public string Bjgms
        {
            get { return _Bjgms.Replace("—", ""); }
            set { _Bjgms = value; }
        }

        private string _Fzdxrq;
        /// <summary>
        /// 确定发展对象日期（格式：yyyyMMdd）
        /// </summary>
        public string Fzdxrq
        {
            get { return _Fzdxrq; }
            set { _Fzdxrq = value; }
        }

        private string _Rdrq;
        /// <summary>
        /// 入党日期（格式：yyyyMMdd）
        /// </summary>
        public string Rdrq
        {
            get { return _Rdrq; }
            set { _Rdrq = value; }
        }

        private string _Zzrq;
        /// <summary>
        /// 转正日期（格式：yyyyMMdd）
        /// </summary>
        public string Zzrq
        {
            get { return _Zzrq; }
            set { _Zzrq = value; }
        }

        private string _Zysbh;
        /// <summary>
        /// 入党志愿书编号
        /// </summary>
        public string Zysbh
        {
            get { return _Zysbh; }
            set { _Zysbh = value; }
        }

        private string _Rdlxrbh1;
        /// <summary>
        /// 入党联系人编号1
        /// </summary>
        public string Rdlxrbh1
        {
            get { return _Rdlxrbh1; }
            set { _Rdlxrbh1 = value; }
        }

        private string _Rdlxrbh2;
        /// <summary>
        /// 入党联系人编号2
        /// </summary>
        public string Rdlxrbh2
        {
            get { return _Rdlxrbh2; }
            set { _Rdlxrbh2 = value; }
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

        //private string _PhotoUrl;
        ///// <summary>
        ///// 照片路径
        ///// </summary>
        //public string PhotoUrl
        //{
        //    get { return _PhotoUrl; }
        //    set { _PhotoUrl = value; }
        //}

        #endregion
    }
}
