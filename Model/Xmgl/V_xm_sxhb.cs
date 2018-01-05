using System;

namespace Model.Xmgl
{
    /// <summary>
    /// 思想汇报Xm_sxhb
    /// </summary>
    public class V_xm_sxhb
    {
        #region 构造函数

        public V_xm_sxhb()
        {
            _Pkid = "";
            _Bmbh = "";
            _Dzbbh = "";
            _Dzbmc = "";
            _Fzrbh = "";
            _Xh = "";
            _Xm = "";
            _Sjhm = "";
            _Fzztdm = "";
            _Fzzt = "";
            _Lxrbh = "";
            _Lxrxm = "";
            _Lxrsjhm = "";
            _Tjxh = 1;
            _Yf = "";
            _Tbsj = "";
            _Tjsj = "";
            _Pysj = "";
            _Tjjzsj = "";
            _Tjtxsj = "";
            _Pyjzsj = "";
            _Pytxsj = "";
            _Shrbh = "";
            _Shrxm = "";
            _Ztdm = 10;
            _Ztmc = "草稿";
            _Ztxsmc = "草稿";
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

        private string _Dzbmc;
        /// <summary>
        /// 党支部名称
        /// </summary>
        public string Dzbmc
        {
            get { return _Dzbmc; }
            set { _Dzbmc = value; }
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

        private string _Lxrsjhm;
        /// <summary>
        /// 联系人手机号码
        /// </summary>
        public string Lxrsjhm
        {
            get { return _Lxrsjhm; }
            set { _Lxrsjhm = value; }
        }

        private int _Tjxh;
        /// <summary>
        /// 提交序号
        /// </summary>
        public int Tjxh
        {
            get { return _Tjxh; }
            set { _Tjxh = value; }
        }

        private string _Yf;
        /// <summary>
        /// 月份（准时为提交年月，延时为应交年月）
        /// </summary>
        public string Yf
        {
            get { return _Yf; }
            set { _Yf = value; }
        }

        private string _Tbsj;
        /// <summary>
        /// 填报时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Tbsj
        {
            get { return _Tbsj; }
            set { _Tbsj = value; }
        }

        private string _Tjsj;
        /// <summary>
        /// 提交时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Tjsj
        {
            get { return _Tjsj; }
            set { _Tjsj = value; }
        }

        private string _Pysj;
        /// <summary>
        /// 评阅时间（格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string Pysj
        {
            get { return _Pysj; }
            set { _Pysj = value; }
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

        private string _Ztxsmc;
        /// <summary>
        /// 状态显示名称
        /// </summary>
        public string Ztxsmc
        {
            get { return _Ztxsmc; }
            set { _Ztxsmc = value; }
        }

        #endregion
    }
}
