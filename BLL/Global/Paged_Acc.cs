using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using FineUI;
using TU = TStar.Utility;

namespace TStar
{
    /// <summary>
    ///Paged 的摘要说明
    /// </summary>
    public partial class Globals
    {
        public static DataTable BindAccountGrid(Grid grid, int pageIndex, string strBll, string strWhere, string strSort)
        {
            int count = 1;
            DataTable table = null;

            switch (strBll.ToLower())
            {
                //case "userteacher":
                //    count = TU.SQLHelper.GetRecordCount<Model.V_account_userteacher>(strWhere);
                //    table = TU.SQLHelper.QueryByPage<Model.V_account_userteacher>(PageSize, pageIndex, strWhere, strSort);
                //    break;
                //case "usercollege":
                //    count = TU.SQLHelper.GetRecordCount<Model.V_account_college>(strWhere);
                //    table = TU.SQLHelper.QueryByPage<Model.V_account_college>(PageSize, pageIndex, strWhere, strSort);
                //    break;
                case "role":
                    //count = SCEMP.BLL.OS.T_OS_Role.GetRecordCount(strWhere);
                    //table = SCEMP.BLL.OS.T_OS_Role.GetListByPage(TStar.Globals.PageSize, pageIndex, strWhere, strSort).Tables[0];
                    break;
                case "right":
                    //grid.PageSize = count = SCEMP.BLL.OS.T_OS_Right.GetRecordCount(strWhere);
                    //table = SCEMP.BLL.OS.T_OS_Right.GetListByPage(grid.PageSize, pageIndex, strWhere, strSort).Tables[0];
                    break;
            }

            if (grid != null)
            {
                grid.RecordCount = count;
                grid.DataSource = table.DefaultView;
                if (count > 0) grid.DataBind();
                else grid.PageSize = 1;
            }

            if (count == 0)
            {
                grid.PageSize = 1;
                grid.RecordCount = 0;
                grid.DataSource = GetNullTable(grid);
                grid.DataBind();
                Alert.Show("暂无数据 ！", "查询提示", MessageBoxIcon.Warning);
                return null;
            }

            return table;
        }
    }
}