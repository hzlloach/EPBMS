using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FineUIMvc;

namespace TStar.Utility.FineUI
{
    public class MvcHelper
    {
        public static void BindDropDownList(DropDownListAjaxHelper ddl, string text = "－请选择－")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("dm", Type.GetType("System.String"));
            dt.Columns.Add("mc", Type.GetType("System.String"));
            dt.Rows.Add(new string[] { "__", text });

            ddl.DataSource(dt.DefaultView, "dm", "mc");
        }
        public static void BindDropDownList(DataTable dt, DropDownListAjaxHelper ddl, string textField, string valueField, string sort = null, string selectedValue = null)
        {
            string srt = dt.DefaultView.Sort;
            if (!String.IsNullOrEmpty(sort) && srt != sort) dt.DefaultView.Sort = sort;

            ddl.DataSource(dt.DefaultView, valueField, textField);
            if (selectedValue != null) ddl.SelectedValue(selectedValue);

            if(!String.IsNullOrEmpty(sort) && srt != sort) dt.DefaultView.Sort = srt;
        }
        public static void BindDropDownList(DataTable dt, DropDownListAjaxHelper ddl, string textField, string valueField, string sort, string filter, string selectedValue, string firstTitle = null)
        {
            DataView dv = dt.DefaultView;
            string srt = dv.Sort;
            string flt = dv.RowFilter;

            if (!String.IsNullOrEmpty(sort) && srt != sort) dv.Sort = sort;
            if (!String.IsNullOrEmpty(filter)) dv.RowFilter = filter;
            if (!String.IsNullOrEmpty(firstTitle)) dv[0][textField] = firstTitle;

            ddl.DataSource(dt.DefaultView, valueField, textField);
            if (selectedValue != null) ddl.SelectedValue(selectedValue);

            dt.RejectChanges();
            dv.Sort = srt;
            dv.RowFilter = flt;
        }

        public static void BindCheckBoxList(DataTable dt, CheckBoxListAjaxHelper cbl, string text, string value, string sort)
        {
            if (!String.IsNullOrEmpty(sort)) dt.DefaultView.Sort = sort;

            //cbl.DataSource = dt.DefaultView;
            //cbl.DataTextField = text;
            //cbl.DataValueField = value;
            //cbl.DataBind();
        }
        public static void BindRadioButtonList(DataTable dt, RadioButtonListAjaxHelper rbl, string text, string value, string sort)
        {
            if (!String.IsNullOrEmpty(sort)) dt.DefaultView.Sort = sort;

            //rbl.DataSource = dt.DefaultView;
            //rbl.DataTextField = text;
            //rbl.DataValueField = value;
            //rbl.DataBind();
        }
    }
}
