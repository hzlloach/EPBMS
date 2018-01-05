using System;

namespace Model
{
    public class Account_user
    {
        #region 构造函数

        public Account_user()
        {
            _Pkid = "";
            _DeptID = "";
            _UserID = "";
            _UserName = "";
            _Password = "";
            _Mobile = "";
            _Email = "";
            _Level = "00";
            _Status = "01";
            _Style = "1";
            _Creator = "System";
            _Message = "";
            _LoginIP = "127.0.0.1";
            _RegTime = _LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _LoginCount = 0;
            _VerifyCode = "000000";
            _Timestamp = 0;
            _PhotoUrl = "";
        }

        #endregion

        #region 属性

        /// <summary>
        /// 党支部编号
        /// </summary>
        public string Dzbbh;
        /// <summary>
        /// 党支部名称
        /// </summary>
        public string Dzbmc;
        /// <summary>
        /// 发展状态代码
        /// </summary>
        public string Fzztdm;
        /// <summary>
        /// 发展状态名称
        /// </summary>
        public string Fzzt;

        private string _Pkid;
        /// <summary>
        /// 序号
        /// </summary>
        public string Pkid
        {
            get { return _Pkid; }
            set { _Pkid = value; }
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

        public string UserIDName
        {
            get { return (String.IsNullOrEmpty(UserName) ? "用户" : UserName) + "【" + UserID + "】"; }
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

        private string _Status;
        /// <summary>
        /// 帐户状态（00:未激活,01:正常,02:禁用,03:删除）
        /// </summary>
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
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

        private string _Message;
        /// <summary>
        /// 预留信息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        private string _RegTime;
        /// <summary>
        /// 注册时间（日期格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string RegTime
        {
            get { return _RegTime; }
            set { _RegTime = value; }
        }

        private string _LoginIP;
        /// <summary>
        /// 最近登录IP
        /// </summary>
        public string LoginIP
        {
            get { return _LoginIP; }
            set { _LoginIP = value; }
        }

        private string _LoginTime;
        /// <summary>
        /// 最近登录时间（日期格式：yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string LoginTime
        {
            get { return _LoginTime; }
            set { _LoginTime = value; }
        }

        private int _LoginCount;
        /// <summary>
        /// 已登录次数
        /// </summary>
        public int LoginCount
        {
            get { return _LoginCount; }
            set { _LoginCount = value; }
        }

        private string _VerifyCode;
        /// <summary>
        /// 手机验证码
        /// </summary>
        public string VerifyCode
        {
            get { return _VerifyCode; }
            set { _VerifyCode = value; }
        }

        private long _Timestamp;
        /// <summary>
        /// 验证码过期时间戳
        /// </summary>
        public long Timestamp
        {
            get { return _Timestamp; }
            set { _Timestamp = value; }
        }

        private string _PhotoUrl;
        /// <summary>
        /// 照片路径
        /// </summary>
        public string PhotoUrl
        {
            get { return string.IsNullOrEmpty(_PhotoUrl) ? "/Uploads/Photo/default.jpg" : _PhotoUrl; }
            set { _PhotoUrl = value; }
        }

        #endregion
    }
}
