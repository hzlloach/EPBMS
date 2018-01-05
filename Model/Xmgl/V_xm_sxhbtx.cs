using System;

namespace Model.Xmgl
{
    /// <summary>
    /// 思想汇报提醒Xm_sxhbtx
    /// </summary>
    public class V_xm_sxhbtx
    {
        #region 构造函数

        public V_xm_sxhbtx()
        {
            _Pkid = "";
            _Xm = "";
            _Sjhm = "";
            _Tjjzsj = "";
            _Tjtxsj = "";
            _Pyjzsj = "";
            _Pytxsj = "";
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

        private string _Tjjzsj;
        /// <summary>
        /// 提交截止时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Tjjzsj
        {
            get { return _Tjjzsj; }
            set { _Tjjzsj = value; }
        }

        private string _Tjtxsj;
        /// <summary>
        /// 提交提醒时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Tjtxsj
        {
            get { return _Tjtxsj; }
            set { _Tjtxsj = value; }
        }

        private string _Pyjzsj;
        /// <summary>
        /// 评阅截止时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Pyjzsj
        {
            get { return _Pyjzsj; }
            set { _Pyjzsj = value; }
        }

        private string _Pytxsj;
        /// <summary>
        /// 评阅提醒时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Pytxsj
        {
            get { return _Pytxsj; }
            set { _Pytxsj = value; }
        }

        #endregion
    }
}
