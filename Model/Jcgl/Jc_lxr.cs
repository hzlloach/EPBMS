using System;

namespace Model.Jcgl
{
    /// <summary>
    /// 联系人Jc_lxr
    /// </summary>
    public class Jc_lxr
    {
        #region 构造函数

        public Jc_lxr()
        {
            _Pkid = "";
            _Bmbh = "";
            _Dzbbh = "";
            _Lbdm = "1";
            //_Issj = false;
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

        /// <summary>
        /// 工号
        /// </summary>
        public string Gh;
        
        /// <summary>
        /// 姓名
        /// </summary>
        public string Xm;
        
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Sjhm;

        private string _Lbdm;
        /// <summary>
        /// 角色类别代码（0:党支部,1:教师,2:学生）
        /// </summary>
        public string Lbdm
        {
            get { return _Lbdm; }
            set { _Lbdm = value; }
        }

        //private bool _Issj;
        /// <summary>
        /// 是否支部书记
        /// </summary>
        public bool Issj;
        //{
        //    get { return _Issj; }
        //    set { _Issj = value; }
        //}

        #endregion
    }
}
