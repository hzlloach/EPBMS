using System;

namespace Model.Lcgl
{
    /// <summary>
    /// 拟发展名单V_lc_nfzmd
    /// </summary>
    public class V_lc_nfzmd
    {
        #region 构造函数

        public V_lc_nfzmd()
        {
            _Aid = 0;
            _Pkid = "";
            _Xq = "";
            _Bmbh = "";
            _Bmmc = "";
            _Dzbbh = "";
            _Dzbmc = "";
            _Xsbh = "";
            _Xh = "";
            _Xm = "";
            _Fzdxrq = "";
            _Zsjgdm = "";
            _Zsjg = "";
            _Dbjgdm = "";
            _Dbjg = "";
            _Dbrq = "";
            _Dbdd = "";
            _Dbzcy = "";
            _Dbpjyj = "";
            _Zswtgyy = "";
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

        private string _Xsbh;
        /// <summary>
        /// 学生编号
        /// </summary>
        public string Xsbh
        {
            get { return _Xsbh; }
            set { _Xsbh = value; }
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

        private string _Fzdxrq;
        /// <summary>
        /// 发展对象确定日期（格式：yyyyMMdd）
        /// </summary>
        public string Fzdxrq
        {
            get { return _Fzdxrq; }
            set { _Fzdxrq = value; }
        }

        private string _Zsjgdm;
        /// <summary>
        /// 政审结果代码
        /// </summary>
        public string Zsjgdm
        {
            get { return _Zsjgdm; }
            set { _Zsjgdm = value; }
        }

        private string _Zsjg;
        /// <summary>
        /// 政审结果
        /// </summary>
        public string Zsjg
        {
            get { return _Zsjg; }
            set { _Zsjg = value; }
        }

        private string _Dbjgdm;
        /// <summary>
        /// 答辩结果代码
        /// </summary>
        public string Dbjgdm
        {
            get { return _Dbjgdm; }
            set { _Dbjgdm = value; }
        }

        private string _Dbjg;
        /// <summary>
        /// 答辩结果
        /// </summary>
        public string Dbjg
        {
            get { return _Dbjg; }
            set { _Dbjg = value; }
        }

        private string _Dbrq;
        /// <summary>
        /// 答辩日期（格式：yyyyMMdd）
        /// </summary>
        public string Dbrq
        {
            get { return _Dbrq; }
            set { _Dbrq = value; }
        }

        private string _Dbdd;
        /// <summary>
        /// 答辩地点
        /// </summary>
        public string Dbdd
        {
            get { return _Dbdd; }
            set { _Dbdd = value; }
        }

        private string _Dbzcy;
        /// <summary>
        /// 答辩组成员（以、分隔）
        /// </summary>
        public string Dbzcy
        {
            get { return _Dbzcy; }
            set { _Dbzcy = value; }
        }

        private string _Dbpjyj;
        /// <summary>
        /// 答辩评价意见
        /// </summary>
        public string Dbpjyj
        {
            get { return _Dbpjyj; }
            set { _Dbpjyj = value; }
        }

        private string _Zswtgyy;
        /// <summary>
        /// 政审未通过原因
        /// </summary>
        public string Zswtgyy
        {
            get { return _Zswtgyy; }
            set { _Zswtgyy = value; }
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
