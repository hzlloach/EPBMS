using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using FineUI;
using TU = TStar.Utility;
using TUD = TStar.Utility.DataSource;

namespace BLL
{
    /// <summary>
    ///Paged 的摘要说明
    /// </summary>
    public partial class Globals
    {
        public static int PageSize
        {
            get
            {
                object size = TU.Globals.GetObject("$SystemCode$PageSize");
                if (size == null)
                {
                    int _size = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
                    TU.Globals.SetObject(_size, "$SystemCode$PageSize", false);
                    return _size;
                }
                return (int)size;
            }
            set
            {
                TU.Globals.SetObject(value, "$SystemCode$PageSize", false);
            }
        }

        public static DataView GetNullTable(Grid grid)
        {
            DataTable dt = new DataTable("NullData");
            for (int i = 0; i < grid.DataKeyNames.Length; i++)
            {
                dt.Columns.Add(grid.DataKeyNames[i], typeof(String));
            }
            return dt.DefaultView;
        }

        public static void BindGrid(Grid grid, DataView dv)
        {
            int count = dv.Count;
            if (count > 0)
            {
                grid.PageSize = grid.RecordCount = count;
                grid.DataSource = dv;
                grid.DataBind();
            }
        }

        public static DataTable BindGrid(Grid grid, int pageIndex, int pageSize, string strBll, string strWhere, string strSort)
        {
            int count = -1;
            DataTable table = null;
            switch (strBll.ToLower())
            {
                #region Jcgl
                case "bm":
                    count = TUD.SQLHelper.GetRecordCount<Model.Jcgl.Jd_bm>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Jcgl.Jd_bm>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "dzb":
                    count = TUD.SQLHelper.GetRecordCount<Model.Jcgl.V_jd_dzb>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Jcgl.V_jd_dzb>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "zy":
                    count = TUD.SQLHelper.GetRecordCount<Model.Jcgl.V_jd_zy>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Jcgl.V_jd_zy>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "bj":
                    count = TUD.SQLHelper.GetRecordCount<Model.Jcgl.V_jd_bj>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Jcgl.V_jd_bj>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "lxr":
                    count = TUD.SQLHelper.GetRecordCount<Model.Jcgl.V_jc_lxr>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Jcgl.V_jc_lxr>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "xs":
                case "hz_jjfz":
                    count = TUD.SQLHelper.GetRecordCount<Model.Jcgl.V_jc_xs_hz>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Jcgl.V_jc_xs_hz>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "xmzb":
                    count = TUD.SQLHelper.GetRecordCount<Model.Dmgl.Jd_khzb>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Dmgl.Jd_khzb>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "xmdj":
                    count = TUD.SQLHelper.GetRecordCount<Model.Dmgl.V_jd_xmdj>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Dmgl.V_jd_xmdj>(pageSize, pageIndex, strWhere, strSort);
                    break;
                #endregion

                #region Xmgl
                case "sxhbcx":
                    count = TUD.SQLHelper.GetRecordCount<Model.Xmgl.V_xm_sxhb_cur>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Xmgl.V_xm_sxhb_cur>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "xxcjcx":
                case "dxpxcx":
                case "yjxmcx":
                    count = TUD.SQLHelper.GetRecordCount<Model.Xmgl.V_yj_xm_cur>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Xmgl.V_yj_xm_cur>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "ysdbcx":
                    count = TUD.SQLHelper.GetRecordCount<Model.Xmgl.V_xm_ysdb>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Xmgl.V_xm_ysdb>(pageSize, pageIndex, strWhere, strSort);
                    break;
                #endregion

                #region Lcgl
                case "jjfzmdcx":
                    count = TUD.SQLHelper.GetRecordCount<Model.Lcgl.V_lc_jjfzmd>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Lcgl.V_lc_jjfzmd>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "nfzmdcx":
                    count = TUD.SQLHelper.GetRecordCount<Model.Lcgl.V_lc_nfzmd>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Lcgl.V_lc_nfzmd>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "fzmdcx":
                    count = TUD.SQLHelper.GetRecordCount<Model.Lcgl.V_lc_fzmd>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Lcgl.V_lc_fzmd>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "zzmdcx":
                    count = TUD.SQLHelper.GetRecordCount<Model.Lcgl.V_lc_zzmd>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Lcgl.V_lc_zzmd>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "dymdcx":
                    count = TUD.SQLHelper.GetRecordCount<Model.Lcgl.V_lc_dymd>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Lcgl.V_lc_dymd>(pageSize, pageIndex, strWhere, strSort);
                    break;
                #endregion

                case "sxhbcx_all":
                    count = TUD.SQLHelper.GetRecordCount<Model.Xmgl.V_xm_sxhb>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Xmgl.V_xm_sxhb>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "yjxmcx_all":
                    count = TUD.SQLHelper.GetRecordCount<Model.Xmgl.V_yj_xm>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Xmgl.V_yj_xm>(pageSize, pageIndex, strWhere, strSort);
                    break;

                case "hz_fzdx":
                    count = TUD.SQLHelper.GetRecordCount<Model.Tjbb.Tj_hz_fzdx>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Tjbb.Tj_hz_fzdx>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "hz_ybdy":
                    count = TUD.SQLHelper.GetRecordCount<Model.Tjbb.Tj_hz_ybdy>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Tjbb.Tj_hz_ybdy>(pageSize, pageIndex, strWhere, strSort);
                    break;
                case "hz_zsdy":
                    count = TUD.SQLHelper.GetRecordCount<Model.Tjbb.Tj_hz_zsdy>(strWhere);
                    table = TUD.SQLHelper.QueryByPage<Model.Tjbb.Tj_hz_zsdy>(pageSize, pageIndex, strWhere, strSort);
                    break;
            }

            //switch (strBll.ToLower())
            //{
                //case "energymonitor_comp_bd":
                //    InitalDynamicGrid(grid, table);
                //    grid.PageSize = count = table.Rows.Count;
                //    break;
            //}

            if (count == -1) count = table.Rows.Count;
            if (grid != null)
            {
                if (count > 0)
                {
                    grid.PageSize = PageSize;
                    grid.RecordCount = count;
                    grid.DataSource = table.DefaultView;
                    grid.DataBind();
                }
                else
                {
                    grid.RecordCount = 0;
                    grid.DataSource = null;// GetNullTable(grid);
                    grid.DataBind();
                    //Alert.Show("暂无数据 ！", "查询提示", MessageBoxIcon.Warning);
                    return null;
                }
            }

            return table;
        }

        public static DataTable GetData(string strBll, string strWhere, string strSort)
        {
            int MaxCount = 1000;
            DataTable table = null;

            switch (strBll.ToLower())
            {
                #region BD

                //case "build":
                //    table = SCEMP.BLL.BD.V_BD_BuildBaseInfo.GetListByPage(MaxCount, 1, strWhere, strSort).Tables[0];
                //    break;

                #endregion
            }

            return table;
        }
    }
}