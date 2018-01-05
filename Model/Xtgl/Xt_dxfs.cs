using System;

namespace Model.Xtgl
{
    /// <summary>
    /// 短信发件箱 Xt_dxfs
    /// </summary>
    public class Xt_dxfs
    {
        #region 构造函数

        public Xt_dxfs()
        {
            _Pkid = "";
            _Dxlb = "";
            _Jsr = "";
            _Sjhm = "";
            _Nr = "";
            _Fssj = "0001-01-01 00:00:00";
            _Bcsj = "0001-01-01 00:00:00";
            _Ztdm = "0";
            _Sbyy = "";
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

        private string _Dxlb;
        /// <summary>
        /// 短信类别
        /// </summary>
        public string Dxlb
        {
            get { return _Dxlb; }
            set { _Dxlb = value; }
        }

        private string _Jsr;
        /// <summary>
        /// 接收人姓名（多个用'|'分隔）
        /// </summary>
        public string Jsr
        {
            get { return _Jsr; }
            set { _Jsr = value; }
        }

        private string _Sjhm;
        /// <summary>
        /// 接收人手机（多个用'|'分隔）
        /// </summary>
        public string Sjhm
        {
            get { return _Sjhm; }
            set { _Sjhm = value; }
        }

        private string _Nr;
        /// <summary>
        /// 短信内容
        /// </summary>
        public string Nr
        {
            get { return _Nr; }
            set { _Nr = value; }
        }

        private string _Fssj;
        /// <summary>
        /// 发送时间（日期格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Fssj
        {
            get { return _Fssj; }
            set { _Fssj = value; }
        }

        private string _Bcsj;
        /// <summary>
        /// 保存时间（日期格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Bcsj
        {
            get { return _Bcsj; }
            set { _Bcsj = value; }
        }

        private string _Ztdm;
        /// <summary>
        /// 发送状态（0:未发送,1:发送失败,2:发送成功）
        /// </summary>
        public string Ztdm
        {
            get { return _Ztdm; }
            set { _Ztdm = value; }
        }

        private string _Sbyy;
        /// <summary>
        /// 失败原因
        /// </summary>
        public string Sbyy
        {
            get { return _Sbyy; }
            set { _Sbyy = value; }
        }

        #endregion
    }
}
