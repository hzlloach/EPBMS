using System;

namespace Model.Lcgl
{
    /// <summary>
    /// 现有党员名单Lc_dymd
    /// </summary>
    public class Lc_dymd
    {
        #region 构造函数

        public Lc_dymd()
        {
            _Aid = 0;
            _Pkid = "";
            _Xq = "";
            _Bmbh = "";
            _Dzbbh = "";
            _Zybh = "";
            _Bjbh = "";
            _Xh = "";
            _Xm = "";
            _Xbdm = "";
            _Sfzh = "";
            _Jg = "";
            _Mz = "";
            _Lxdh = "";
            _QQ = "";
            _Zw = "";
            _Jtdz = "";
            _Sqrdrq = "";
            _Jjfzrq = "";
            _Dxkhztdm = null;
            _Dxjyrq = "";
            _Fzdxrq = "";
            _Rdrq = "";
            _Zzrq = "";
            _Zysbh = "";
            _Lxrbh = "";
            _Drsj = "";
            _Ztdm = 10;
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

        private string _Xbdm;
        /// <summary>
        /// 性别代码
        /// </summary>
        public string Xbdm
        {
            get { return _Xbdm; }
            set { _Xbdm = value; }
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

        private string _Lxdh;
        /// <summary>
        /// 手机
        /// </summary>
        public string Lxdh
        {
            get { return _Lxdh; }
            set { _Lxdh = value; }
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
            get { return _Dxkhztdm; }
            set { _Dxkhztdm = value; }
        }

        private string _Dxjyrq;
        /// <summary>
        /// 党校结业日期（格式：yyyyMMdd）
        /// </summary>
        public string Dxjyrq
        {
            get { return _Dxjyrq; }
            set { _Dxjyrq = value; }
        }

        private string _Fzdxrq;
        /// <summary>
        /// 发展对象确定日期（格式：yyyyMMdd）
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

        private string _Lxrbh;
        /// <summary>
        /// 联系人编号
        /// </summary>
        public string Lxrbh
        {
            get { return _Lxrbh; }
            set { _Lxrbh = value; }
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

        #endregion
    }
}
