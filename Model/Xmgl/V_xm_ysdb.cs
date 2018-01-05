using System;

namespace Model.Xmgl
{
    /// <summary>
    /// 预审答辩Xm_ysdb
    /// </summary>
    public class V_xm_ysdb
    {
        #region 构造函数

        public V_xm_ysdb()
        {
            _Aid = 0;
            _Pkid = "";
            _Bmbh = "";
            _Dzbbh = "";
            _Fzrbh = "";
            _Xh = "";
            _Xm = "";
            _Fzdxrq = "";
            _Zsjgdm = "";
            _Dbjgdm = "";
            _Dbrq = "";
            _Dbdd = "";
            _Dbzcy = "";
            _Dbpjyj = "";
            _Zswtgyy = "";
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
            set { _Xh = value; }
            get { return _Xh; }
        }

        private string _Xm;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Xm
        {
            set { _Xm = value; }
            get { return _Xm; }
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

        private string _Dbjgdm;
        /// <summary>
        /// 答辩结果代码
        /// </summary>
        public string Dbjgdm
        {
            get { return _Dbjgdm; }
            set { _Dbjgdm = value; }
        }

        private string _Dbrq;
        /// <summary>
        /// 答辩日期（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Dbrq
        {
            get { return _Dbrq; }
            set { _Dbrq = value; }
        }

        private string _Dbdd;
        /// <summary>
        /// 答辩地点（格式：yyyy-MM-dd HH:mm:ss）
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

        #endregion
    }
}
