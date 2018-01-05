using System;

namespace Model
{
    public class V_account_userteacher
    {
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

        private string _DeptID;
        /// <summary>
        /// 部门代码
        /// </summary>
        public string DeptID
        {
            get { return _DeptID; }
            set { _DeptID = value; }
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

        private string _Jysbh;
        /// <summary>
        /// 教研室编号
        /// </summary>
        public string Jysbh
        {
            get { return _Jysbh; }
            set { _Jysbh = value; }
        }

        private string _Jysmc;
        /// <summary>
        /// 教研室名称
        /// </summary>
        public string Jysmc
        {
            get { return _Jysmc; }
            set { _Jysmc = value; }
        }

        private string _UserID;
        /// <summary>
        /// 用户帐号
        /// </summary>
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _UserName;
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _Password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _Mobile;
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            get { return String.IsNullOrEmpty(_Mobile) ? _Pkid : _Mobile; }
            set { _Mobile = value; }
        }

        private string _Email;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return String.IsNullOrEmpty(_Email) ? _Pkid : _Email; }
            set { _Email = value; }
        }

        private string _Level;
        /// <summary>
        /// 帐户级别
        /// </summary>
        public string Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        private string _UserLevel;
        /// <summary>
        /// 帐户级别名称
        /// </summary>
        public string UserLevel
        {
            get { return _UserLevel; }
            set { _UserLevel = value; }
        }

        private string _Status;
        /// <summary>
        /// 帐户状态（00:未激活,01:正常,02:禁用,03:删除）
        /// </summary>
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _StatusName;
        /// <summary>
        /// 帐户状态名称（00:未激活,01:正常,02:禁用,03:删除）
        /// </summary>
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }

        private string _Style;
        /// <summary>
        /// 帐户风格
        /// </summary>
        public string Style
        {
            get { return _Style; }
            set { _Style = value; }
        }

        private string _Creator;
        /// <summary>
        /// 创建者帐号
        /// </summary>
        public string Creator
        {
            get { return _Creator; }
            set { _Creator = value; }
        }

        //private string _Message;
        ///// <summary>
        ///// 预留信息
        ///// </summary>
        //public string Message
        //{
        //    get { return _Message; }
        //    set { _Message = value; }
        //}

        //private string _RegTime;
        ///// <summary>
        ///// 注册时间（日期格式：yyyy-MM-dd HH:mm:ss）
        ///// </summary>
        //public string RegTime
        //{
        //    get { return _RegTime; }
        //    set { _RegTime = value; }
        //}

        //private string _LoginTime;
        ///// <summary>
        ///// 最近登录时间（日期格式：yyyy-MM-dd HH:mm:ss）
        ///// </summary>
        //public string LoginTime
        //{
        //    get { return _LoginTime; }
        //    set { _LoginTime = value; }
        //}

        //private int _LoginCount;
        ///// <summary>
        ///// 已登录次数
        ///// </summary>
        //public int LoginCount
        //{
        //    get { return _LoginCount; }
        //    set { _LoginCount = value; }
        //}

        //private string _VerifyCode;
        ///// <summary>
        ///// 手机验证码
        ///// </summary>
        //public string VerifyCode
        //{
        //    get { return _VerifyCode; }
        //    set { _VerifyCode = value; }
        //}

        //private int _Timestamp;
        ///// <summary>
        ///// 验证码过期时间戳
        ///// </summary>
        //public int Timestamp
        //{
        //    get { return _Timestamp; }
        //    set { _Timestamp = value; }
        //}

        #endregion
    }
}
