﻿using System;

namespace Model.Lcgl
{
    /// <summary>
    /// 发展名单Lc_fzmd
    /// </summary>
    public class Lc_fzmd
    {
        #region 构造函数

        public Lc_fzmd()
        {
            _Aid = 0;
            _Pkid = "";
            _Xq = "";
            _Bmbh = "";
            _Dzbbh = "";
            _Xsbh = "";
            _Zysbh = "";
            _Bjjgdm = "";
            _Zbdhrq = "";
            _Dzzsyqk = "";
            _Dwspyj = "";
            _Bz = "";
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

        private string _Xsbh;
        /// <summary>
        /// 学生编号
        /// </summary>
        public string Xsbh
        {
            get { return _Xsbh; }
            set { _Xsbh = value; }
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

        private string _Bjjgdm;
        /// <summary>
        /// 表决结果代码
        /// </summary>
        public string Bjjgdm
        {
            get { return _Bjjgdm; }
            set { _Bjjgdm = value; }
        }

        private string _Zbdhrq;
        /// <summary>
        /// 支部大会日期（格式：yyyyMMdd）
        /// </summary>
        public string Zbdhrq
        {
            get { return _Zbdhrq; }
            set { _Zbdhrq = value; }
        }

        private string _Dzzsyqk;
        /// <summary>
        /// 党总支审议情况
        /// </summary>
        public string Dzzsyqk
        {
            get { return _Dzzsyqk; }
            set { _Dzzsyqk = value; }
        }

        private string _Dwspyj;
        /// <summary>
        /// 党委审批意见
        /// </summary>
        public string Dwspyj
        {
            get { return _Dwspyj; }
            set { _Dwspyj = value; }
        }

        private string _Bz;
        /// <summary>
        /// 备注
        /// </summary>
        public string Bz
        {
            get { return _Bz; }
            set { _Bz = value; }
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