using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TStar.Web
{
    public partial class Globals
    {
        public class SystemSetting
        {
            /// <summary>
            /// 系统版本号
            /// </summary>
            public static string Version = ConfigurationManager.AppSettings["Version"];
            /// <summary>
            /// 系统更新日期
            /// </summary>
            public static string UpdateDate = ConfigurationManager.AppSettings["UpdateDate"];

            /// <summary>
            /// 用户级别
            /// </summary>
            public enum UserLevel
            {
                /// <summary>
                /// 学生
                /// </summary>
                Student = 0,
                /// <summary>
                /// 联系人
                /// </summary>                
                Contacts = 1,
                /// <summary>
                /// 党支部
                /// </summary>
                Branch = 2,
                /// <summary>
                /// 分党委
                /// </summary>
                Committee = 5,
                /// <summary>
                /// 党总支
                /// </summary>
                Party = 7,
                /// <summary>
                /// 系统
                /// </summary>
                System = 9
            }

            /// <summary>
            /// 发展状态
            /// </summary>
            public enum Fzzt
            {
                /// <summary>
                /// 积极分子
                /// </summary>
                Jjfz = 2,
                /// <summary>
                /// 拟发展对象
                /// </summary>                
                Nfzdx = 3,
                /// <summary>
                /// 发展对象
                /// </summary>
                Fzdx = 4,
                /// <summary>
                /// 预备党员
                /// </summary>
                Ybdy = 5,
                /// <summary>
                /// 正式党员
                /// </summary>
                Zsdy = 6
            }

            /// <summary>
            /// 数据状态
            /// </summary>
            public enum Status
            {
                /// <summary>
                /// 已删除
                /// </summary>
                Deleted = -10,
                /// <summary>
                /// 已重写
                /// </summary>
                ReWritten = -20,
                /// <summary>
                /// 已修改
                /// </summary>
                Modified = -21,
                /// <summary>
                /// 审核拒绝
                /// </summary>
                AuditRefused = -23,
                /// <summary>
                /// 已销毁
                /// </summary>
                Destroyed = -29,
                /// <summary>
                /// 草稿                 
                /// </summary>
                Draft = 10,
                /// <summary>
                /// 重写中
                /// </summary>
                InRewrite = 11,
                /// <summary>
                /// 修改中
                /// </summary>
                InModify = 12,
                /// <summary>
                /// 已撤回
                /// </summary>
                Revoked = 13,
                /// <summary>
                /// 已提交
                /// </summary>
                Submitted = 16,
                /// <summary>
                /// 退回重写
                /// </summary>
                ToBeRewritten = 20,
                /// <summary>
                /// 退回修改
                /// </summary>
                ToBeModified = 21,
                /// <summary>
                /// 评阅通过
                /// </summary>
                Audited = 22,
                /// <summary>
                /// 审核通过
                /// </summary>
                AuditAccepted = 23,
                /// <summary>
                /// 党支部导入
                /// </summary>
                BranchImported = 25,
                /// <summary>
                /// 分党委导入
                /// </summary>
                CommitteeImported = 26,
                /// <summary>
                /// 待归档
                /// </summary>
                ToBeArchived = 28,
                /// <summary>
                /// 已归档
                /// </summary>
                Archived = 29
            }

            /// <summary>
            /// 操作级别
            /// </summary>
            public enum Czjb
            {
                /// <summary>
                /// 学生
                /// </summary>
                Student = 0,
                /// <summary>
                /// 联系人
                /// </summary>                
                Contacts = 1,
                /// <summary>
                /// 党支部
                /// </summary>
                Branch = 2,
                /// <summary>
                /// 分党委
                /// </summary>
                Committee = 5,
                /// <summary>
                /// 党总支
                /// </summary>
                Party = 7,
                /// <summary>
                /// 系统
                /// </summary>
                System = 9
            }

            /// <summary>
            /// 操作类型
            /// </summary>
            public enum Czlx
            {
                /// <summary>
                ///申请清空
                /// </summary>
                ApplyClear = 10,
                /// <summary>
                ///申请导入
                /// </summary>
                ApplyImport = 11,
                /// <summary>
                ///申请新增
                /// </summary>
                ApplyInsert = 12,
                /// <summary>
                ///申请修改
                /// </summary>
                ApplyUpdate = 13,
                /// <summary>
                ///申请删除
                /// </summary>
                ApplyDelete = 14,
                /// <summary>
                ///申请重置
                /// </summary>
                ApplyReset = 15,
                /// <summary>
                ///操作清空
                /// </summary>
                OperClear = 20,
                /// <summary>
                ///操作导入
                /// </summary>
                OperImport = 21,
                /// <summary>
                ///操作新增
                /// </summary>
                OperInsert = 22,
                /// <summary>
                ///操作修改
                /// </summary>
                OperUpdate = 23,
                /// <summary>
                ///操作删除
                /// </summary>
                OperDelete = 24,
                /// <summary>
                ///申请重置
                /// </summary>
                OperReset = 25,
                /// <summary>
                ///操作上报
                /// </summary>
                OperSubmit = 26,
                /// <summary>
                ///操作打印
                /// </summary>
                OperPrint = 27,
                /// <summary>
                ///操作改派
                /// </summary>
                OperReassign = 28,
                /// <summary>
                ///操作撤销
                /// </summary>
                OperCancel = 29,
                /// <summary>
                ///操作自动派遣
                /// </summary>
                OperDispatch = 30,
                /// <summary>
                ///审核有误
                /// </summary>
                AudError = 31,
                /// <summary>
                ///审核退回修改
                /// </summary>
                AudReturn = 32,
                /// <summary>
                ///审核拒绝
                /// </summary>
                AudRefuse = 33,
                /// <summary>
                ///审核通过
                /// </summary>
                AudAccept = 34,
                /// <summary>
                ///代码导入
                /// </summary>
                CodeImport = 41,
                /// <summary>
                ///代码新增
                /// </summary>
                CodeInsert = 42,
                /// <summary>
                ///代码修改
                /// </summary>
                CodeUpdate = 43,
                /// <summary>
                ///代码删除
                /// </summary>
                CodeDelele = 44,
                /// <summary>
                ///代码清空
                /// </summary>
                CodeClear = 45,
                /// <summary>
                ///系统登录
                /// </summary>
                SysLogin = 91,
                /// <summary>
                ///系统修改密码
                /// </summary>
                SysModpwd = 92,
                /// <summary>
                ///系统修改信息
                /// </summary>
                SysModInfo = 93,
                /// <summary>
                ///系统注销
                /// </summary>
                SysLogout = 94,
                /// <summary>
                ///系统设置
                /// </summary>
                SysSet = 95,
                /// <summary>
                ///系统归档
                /// </summary>
                SysArchive = 99
            }
        }
    }
}
