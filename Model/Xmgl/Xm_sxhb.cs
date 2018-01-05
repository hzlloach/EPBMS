using System;

namespace Model.Xmgl
{
    /// <summary>
    /// 思想汇报Xm_sxhb
    /// </summary>
    public class Xm_sxhb
    {
        #region 构造函数

        public Xm_sxhb()
        {
            _Aid = 0;
            _Pkid = "";
            _Fzztdm = "";
            _Fzrbh = "";
            _Shrbh = null;
            _Tjxh = 1;
            _Yf = "";
            _Tbsj = "";
            _Tjsj = "";
            _Pysj = "";
            _Tjjzsj = "";
            _Tjtxsj = "";
            _Pyjzsj = "";
            _Pytxsj = "";
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

        private string _Fzztdm;
        /// <summary>
        /// 发展状态代码
        /// </summary>
        public string Fzztdm
        {
            get { return _Fzztdm; }
            set { _Fzztdm = value; }
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

        private string _Shrbh;
        /// <summary>
        /// 审核人编号
        /// </summary>
        public string Shrbh
        {
            get { return _Shrbh; }
            set { _Shrbh = value; }
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
