using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using TU = TStar.Utility;

namespace Web.Xtgl
{
    public partial class Export : TStar.Web.BasePage
    {
        int MaxPageSize = 10000;
        public string Param
        {
            get { return TU.Globals.GetParaValue("param", "").Replace("@", "+"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(Param)) return;

                string param = TU.Globals.TripleDESDecrypt(Param);
                string[] p = param.Split('&');
                string bll = p[0].ToLower();
                string where = p.Length > 1 ? p[1] : "";
                string sort = p.Length > 2 ? p[2] : "";

                ExportData(bll, where, sort);
            }
            catch (Exception err)
            {
                Alert.ShowInTop(err.Message, "导出失败", MessageBoxIcon.Error);
            }
        }

        public void ExportData(string strBll, string strWhere, string strSort)
        {
            DataTable dt = null;
            DataView dv = null;
            int blankRows = 1;
            bool isTemplate = true;
            string filename = "", template = "", title = "", subtitle = "";
            string[] s = null, columns = null, captions = null;
            
            switch (strBll)
            {
                #region 直接导出

                case "xs":
                    filename = "基本信息汇总表";
                    dt = BLL.Dmgl.GetList<Model.Jcgl.V_jc_xs_hz>(strWhere, strSort);                    
                    string fzztdm = TStar.Web.Globals.GetQueryFieldValue(strWhere, "Fzztdm");
                    if (fzztdm == "2" || fzztdm == "3") goto case "hz_jjfz";// 积极分子
                    else if (fzztdm == "4") goto case "hz_fzdx";// 发展对象
                    else if (fzztdm == "5") goto case "hz_ybdy";// 预备党员
                    else if (fzztdm == "6") goto case "hz_zsdy";// 正式党员
                    else // 未指定
                    {
                        columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Jg", "Mz", "Csrq", "Zw", "Rdlxrxm1", "Fzzt", "Sxhb", "Zyfw", "Slx", "Jshj", "Qtxm" };
                        captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "籍贯", "民族", "出生年月", "职务", "联系人", "发展阶段", "思想汇报", "志愿服务", "党员三联系", "竞赛获奖", "其他项目" };
                        dt = MapColumns(dt, columns, captions);
                    }
                    break;
                case "hz_jjfz":
                    filename = "积极分子汇总表";
                    dt = BLL.Dmgl.GetList<Model.Jcgl.V_jc_xs_hz>(strWhere, strSort);
                    columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Jg", "Mz", "Csrq", "Zw", "Rdlxrxm1", "Sqrdrq", "Jjfzrq", "Dxjyrq", "Dxkhzt", "Xxcjpm", "Zhszpm", "Bjgms", "Sxhb", "Zyfw", "Jshj", "Qtxm" };
                    captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "籍贯", "民族", "出生年月", "职务", "联系人", "申请入党时间", "确定积极分子时间", "党校结业时间", "党校考核结果", "学习成绩排名", "综合素质排名", "不及格课程数", "思想汇报", "志愿服务", "竞赛获奖", "其他项目" };
                    dt = MapColumns(dt, columns, captions);
                    break;
                case "hz_fzdx":
                    filename = "发展对象汇总表";
                    dt = BLL.Dmgl.GetList<Model.Tjbb.Tj_hz_fzdx>(strWhere, strSort);
                    columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Jg", "Mz", "Csrq", "Zw", "Lxrxm", "Sqrdrq", "Jjfzrq", "Fzdxrq", "Zsjg", "Dbjg", "Zbdhrq", "Dzzsyqk", "Dwspyj" };
                    captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "籍贯", "民族", "出生年月", "职务", "联系人", "申请入党时间", "确定积极分子时间", "确定发展对象时间", "政审情况", "党委预审情况", "支部大会通过时间", "党总支审议情况", "党委审批意见" };
                    dt = MapColumns(dt, columns, captions);
                    break;
                case "hz_ybdy":
                    filename = "预备党员汇总表";
                    dt = BLL.Dmgl.GetList<Model.Tjbb.Tj_hz_ybdy>(strWhere, strSort);
                    columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Jg", "Mz", "Csrq", "Zw", "Lxrxm", "Rdrq", "Zyfw", "Lxbj", "Lxqs", "Lxxs", "Hjqk" };
                    captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "籍贯", "民族", "出生年月", "职务", "联系人", "入党时间", "志愿服务时数", "联系班级次数", "联系寝室次数", "联系同学次数", "获奖情况" };
                    dt = MapColumns(dt, columns, captions);
                    break;
                case "hz_zsdy":
                    filename = "正式党员汇总表";
                    dt = BLL.Dmgl.GetList<Model.Tjbb.Tj_hz_zsdy>(strWhere, strSort);
                    columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Jg", "Mz", "Csrq", "Zw", "Lxrxm", "Rdrq", "Zzrq", "Zyfw", "Lxbj", "Lxqs", "Lxxs", "Hjqk" };
                    captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "籍贯", "民族", "出生年月", "职务", "联系人", "入党时间", "转正时间", "志愿服务时数", "联系班级次数", "联系寝室次数", "联系同学次数", "获奖情况" };
                    dt = MapColumns(dt, columns, captions);
                    break;
                case "yjxmcx":
                    dt = BLL.Dmgl.GetList<Model.Xmgl.V_yj_xm_cur>(strWhere, strSort);
                    string zbbh = TStar.Web.Globals.GetQueryFieldValue(strWhere, "Zbbh");
                    string zbmc = filename = TU.Globals.BindSystemCode(BLL.Globals.SystemCode.DtJd_khzbLocal, "Pkid", "Zbmc", zbbh, "其他项目");
                    switch (zbmc)
                    {
                        case "志愿服务":
                            columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Lxrxm", "Xmrq", "Jlsl", "Xmmc", "Bz" };
                            captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "联系人", "服务日期", "服务时数", "服务地点", "服务内容" };
                            foreach (DataRow dr in dt.Rows) dr["Xmmc"] = dr["Xmmc"].ToString().Substring(11).Replace("志愿服务", "");
                            break;
                        case "竞赛获奖":
                            columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Lxrxm", "Xmrq", "Djmc", "Xmmc" };
                            captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "联系人", "获奖日期", "奖项等级", "奖项名称" };
                            break;
                        case "党员三联系":
                            columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Lxrxm", "Djmc", "Xmmc", "Xmrq", "Bz" };
                            captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "联系人", "联系类别", "联系对象", "联系日期", "联系内容" };
                            foreach (DataRow dr in dt.Rows) dr["Xmmc"] = dr["Xmmc"].ToString().Substring(13);
                            break;
                        default:
                            columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Lxrxm", "Zbmc", "Djmc", "Xmrq", "Xmmc", "Bz" };
                            captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "联系人", "项目类别", "项目等级", "项目日期", "项目名称", "备注" };
                            break;
                    }
                    dt = MapColumns(dt, columns, captions);
                    break;
                case "sxhbcx":
                    filename = "思想汇报";
                    dt = BLL.Dmgl.GetList<Model.Xmgl.V_xm_sxhb_cur>(strWhere, strSort);
                    columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Fzzt", "Lxrxm", "Yf", "Tjsj", "Ztxsmc", "Shrxm", "Pysj" };
                    captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "培养阶段", "联系人", "月份", "提交时间", "评阅状态", "评阅人", "评阅时间" };
                    dt = MapColumns(dt, columns, captions);
                    break;
                case "nfzmdcx":
                    filename = "预审答辩";
                    dt = BLL.Dmgl.GetList<Model.Lcgl.V_lc_nfzmd>(strWhere, strSort);
                    columns = new string[] { "Bmmc", "Dzbmc", "Bjmc", "Xh", "Xm", "Xb", "Lxrxm", "Fzdxrq", "Zsjg", "Dbjg", "Dbrq", "Dbdd", "Dbzcy", "Dbpjyj", "Zswtgyy" };
                    captions = new string[] { "分党委", "党支部", "班级", "学号", "姓名", "性别", "联系人", "发展对象确定日期", "政审结果", "答辩结果", "答辩日期", "答辩地点", "答辩组成员", "答辩评价意见", "政审未通过原因" };
                    dt = MapColumns(dt, columns, captions);
                    break;

                #endregion

                #region 模版

                case "tj_zb": 
                    blankRows = 2;
                    template = "~/Template/ExcelOut/总表.xls";
                    dt = BLL.Tjbb.Zk.TjZbByXy(strWhere);
                    columns = new string[] { "Bmmc", "Zbs", "JjfzRs", "JjfzFws", "Dbs", "YbdyRs", "ZsdyRs", "DyFws", "BjLxs", "QsLxs", "XsLxs", "DyYjHjs", "DyXjHjs", "DyYsHjs" };
                    dt = MapColumns(dt, columns, null);
                    break;
                case "tj_zbfdw":
                    blankRows = 2;
                    template = "~/Template/ExcelOut/总表分党委.xls";
                    s = strWhere.Split('|');
                    dt = BLL.Tjbb.Zk.TjZbByXyDzb(s[0], s[1]);
                    columns = new string[] { "Dzbmc", "JjfzRs", "JjfzFws", "Dbs", "YbdyRs", "ZsdyRs", "DyFws", "BjLxs", "QsLxs", "XsLxs", "DyYjHjs", "DyXjHjs", "DyYsHjs" };
                    dt = MapColumns(dt, columns, null);
                    break;
                case "tj_zblxr":
                    blankRows = 2;
                    template = "~/Template/ExcelOut/总表联系人.xls";
                    dt = BLL.Tjbb.Zk.TjZbByXyLxr(strWhere);
                    columns = new string[] { "Xm", "JjfzRs", "JjfzFws", "Dbs", "YbdyRs", "ZsdyRs", "DyFws", "BjLxs", "QsLxs", "XsLxs", "DyYjHjs", "DyXjHjs", "DyYsHjs" };
                    dt = MapColumns(dt, columns, null);
                    break;

                #endregion
            }

            dv = dt.DefaultView;
            if (columns[0] == "XhID")
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    dv[i][0] = (i + 1);
                }
            }

            if (dt == null || dv.Count == 0)
            {
                Alert.ShowInTop("没有需要导出的数据 ！", "导出提示", MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(template))
            {
                string[] titles = string.IsNullOrEmpty(title) ? null : new string[] { title, subtitle };
                template = Server.MapPath(template);
                TStar.Utility.Excel.WebHelper.ExcelOutput(this.Context, dv, template, blankRows, titles, null);
            }
            else
                TStar.Utility.Excel.WebHelper.ExcelOutput(this.Context, dv, filename + ".xls", true, null);
        }

        /// <summary>
        /// 对数据表的列名进行处理并映射新列名
        /// </summary>
        /// <param name="columns">原始列</param>
        /// <param name="captions">新列名（null时不映射）</param>
        private static DataTable MapColumns(DataTable dt, string[] columns, string[] captions)
        {
            bool rename = captions != null;
            List<string> cols = columns.ToList<string>();
            for (int i = 0; i < columns.Length; i++)
            {
                string col = columns[i];
                if (dt.Columns.IndexOf(col) == -1)
                {
                    dt.Columns.Add(col, typeof(System.String));
                }
                dt.Columns[col].SetOrdinal(i);
                if (rename) dt.Columns[col].ColumnName = captions[i];
            }
            for (int j = dt.Columns.Count - 1; j >= columns.Length; j--)
            {
                dt.Columns.RemoveAt(j);
            }
            return dt;
        }
    }

}