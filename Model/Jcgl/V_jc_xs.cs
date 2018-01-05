using System;
namespace Model.Jcgl
{
    /// <summary>
    /// V_jc_xs:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class V_jc_xs
    {
        public V_jc_xs()
        { }        
        
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

        private string _Bmmc;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Bmmc
        {
            get { return _Bmmc; }
            set { _Bmmc = value; }
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

        private string _Zybh;
        /// <summary>
        /// 专业编号
        /// </summary>
        public string Zybh
        {
            get { return _Zybh; }
            set { _Zybh = value; }
        }

        private string _Zymc;
        /// <summary>
        /// 专业名称
        /// </summary>
        public string Zymc
        {
            get { return _Zymc; }
            set { _Zymc = value; }
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

        private string _Xb;
        /// <summary>
        /// 性别
        /// </summary>
        public string Xb
        {
            get { return _Xb; }
            set { _Xb = value; }
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
            get { return string.IsNullOrEmpty(_Zw) ? "无" : _Zw; }
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
        /// 确定积极分子日期（格式：yyyyMMdd）
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
            get { return _Dxjyrq == "" ? "-1" : _Dxkhztdm; }
            set { _Dxkhztdm = value; }
        }

        private string _Dxkhzt;
        /// <summary>
        /// 党校考核状态
        /// </summary>
        public string Dxkhzt
        {
            get { return string.IsNullOrEmpty(_Dxjyrq) ? "—" : _Dxkhzt; }
            set { _Dxkhzt = value; }
        }

        private string _Dxjyrq;
        /// <summary>
        /// 党校结业日期（格式：yyyyMMdd）
        /// </summary>
        public string Dxjyrq
        {
            get { return string.IsNullOrEmpty(_Dxjyrq) ? "—" : _Dxjyrq; }
            set { _Dxjyrq = value; }
        }

        private string _Xxcjpm;
        /// <summary>
        /// 学习成绩排名（格式：1/2）
        /// </summary>
        public string Xxcjpm
        {
            get { return string.IsNullOrEmpty(_Xxcjpm) ? "—" : _Xxcjpm; }
            set { _Xxcjpm = value; }
        }

        private string _Zhkppm;
        /// <summary>
        /// 综合考评排名（格式：1/2）
        /// </summary>
        public string Zhkppm
        {
            get { return string.IsNullOrEmpty(_Zhkppm) ? "—" : _Zhkppm; }
            set { _Zhkppm = value; }
        }

        private string _Bjgms;
        /// <summary>
        /// 不及格门数
        /// </summary>
        public string Bjgms
        {
            get { return string.IsNullOrEmpty(_Bjgms) ? "—" : _Bjgms; }
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
        /// 联系人一编号
        /// </summary>
        public string Rdlxrbh1
        {
            get { return _Rdlxrbh1; }
            set { _Rdlxrbh1 = value; }
        }

        private string _Rdlxrid1;
        /// <summary>
        /// 联系人一工号
        /// </summary>
        public string RdlxrID1
        {
            get { return _Rdlxrid1; }
            set { _Rdlxrid1 = value; }
        }

        private string _Rdlxrxm1;
        /// <summary>
        /// 联系人一姓名
        /// </summary>
        public string Rdlxrxm1
        {
            set { _Rdlxrxm1 = value; }
            get { return _Rdlxrxm1; }
        }

        private string _Rdlxrsj1;
        /// <summary>
        /// 联系人一手机
        /// </summary>
        public string Rdlxrsj1
        {
            get { return _Rdlxrsj1; }
            set { _Rdlxrsj1 = value; }
        }

        private string _Rdlxrbh2;
        /// <summary>
        /// 联系人二编号
        /// </summary>
        public string Rdlxrbh2
        {
            get { return _Rdlxrbh2; }
            set { _Rdlxrbh2 = value; }
        }

        private string _Rdlxrid2;
        /// <summary>
        /// 联系人二工号
        /// </summary>
        public string RdlxrID2
        {
            get { return _Rdlxrid2; }
            set { _Rdlxrid2 = value; }
        }

        private string _Rdlxrxm2;
        /// <summary>
        /// 联系人二姓名
        /// </summary>
        public string Rdlxrxm2
        {
            get { return string.IsNullOrEmpty(_Rdlxrxm2) ? "无" : _Rdlxrxm2; }
            set { _Rdlxrxm2 = value; }
        }

        private string _Rdlxrsj2;
        /// <summary>
        /// 联系人二手机
        /// </summary>
        public string Rdlxrsj2
        {
            get { return _Rdlxrsj2; }
            set { _Rdlxrsj2 = value; }
        }

        private string _PhotoUrl;
        /// <summary>
        /// 照片路径
        /// </summary>
        public string PhotoUrl
        {
            get { return _PhotoUrl; }
            set { _PhotoUrl = value; }
        }

        #endregion
    }
}

