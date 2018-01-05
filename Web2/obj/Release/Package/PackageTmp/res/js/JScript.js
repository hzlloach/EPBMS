function showCtrl(ctrl_id) {
    $("[id$='" + ctrl_id + "']").show();
}
function hideCtrl(ctrl_id) {
    $("[id$='" + ctrl_id + "']").hide();
}
function clickCtrl(ctrl_id) {
    $("[id$='" + ctrl_id + "']").click();
}

function downloadFile(str) {
    window.open(str);
}

//var mainTabStrip = F("mainTabStrip");
function showAlert(msg) {
    F.alert({ message: msg, messageIcon: Ext.MessageBox.WARNING });
}

function showWindow(wndID, url, title) {
    F(wndID).f_show(url, title);
    var handler = setInterval(function () {
        F(wndID).f_maximize();
        clearInterval(handler); //清除倒计时
    }, 5);
}

function closeWindow() {
    window.opener = null;
    window.open('', '_self');
    window.close();
}

function jumpTo(url) {
    window.location.href = url;
}

function disable(btnid) {
    var btn = $("#" + btnid);
    btn.attr("disabled", true);
}

function enable(btnid) {
    var btn = $("#" + btnid);
    btn.removeAttr("disabled");
}

function isNull(str) {
    /// <summary>判断字符串str是否为空</summary>    
    return (str == "" || str == undefined || str == null) ? true : false;
}

function isInteger(str) {
    /// <summary>判断字符串str是否为整数</summary>    
    var ex = /^(-)?\d+$/;
    return ex.test(str);
}

function isDate(str) {
    /// <summary>判断字符串str是否为有效日期</summary>    
    if (!/^(\d{4})\/(\d{1,2})\/(\d{1,2})$/.test(str)) return false;
    var year = RegExp.$1 - 0;
    var month = RegExp.$2 - 1;
    var date = RegExp.$3 - 0;
    var obj = new Date(year, month, date);
    return !!(obj.getFullYear() == year && obj.getMonth() == month && obj.getDate() == date);
}

function replaceAll(str, s1, s2) {
    /// <summary>将字符串str中的子串s1全部替换为s2</summary>   
    var reg = new RegExp(s1, "g");
    return str.replace(reg, s2);
}

function getLength(str) {
    /// <summary>获取字符串str的长度（汉字算2个字符）</summary>    
    return str.replace(/[\u0391-\uFFE5]/g, "aa").length;  //先把中文替换成两个字节的英文，再计算长度
};


function clearTimer(buttonid, handler) {
    var btnSend = F(buttonid);
    var btn = $("#" + buttonid + "-btnInnerEl");
    clearInterval(handler); //清除倒计时
    btn.html("获取验证码");
    btnSend.enable();
}
function timer(buttonid, time) {
    var btnSend = F(buttonid);
    var btn = $("#" + buttonid + "-btnInnerEl");
    //btn.attr("disabled", true);  //按钮禁止点击
    btnSend.disable();
    btn.html(time <= 0 ? "获取验证码" : ("" + (time) + "秒后重发"));
    var handler = setInterval(function () {
        if (time <= 0) {
            //clearInterval(handler); //清除倒计时
            //btn.html("获取验证码");
            ////btn.removeAttr("disabled");
            //btnSend.enable();
            //return false;
            clearTimer(buttonid, handler)
        }
        else {
            --time;
            btn.html("" + (time) + "秒后重发");
        }
    }, 1000);
    return handler;
}

function ajaxGetVerifyCode(tbxID, tbxPhone, btnSend, hidCode, type) {
    /// <summary>获取手机验证码</summary>     
    /// <param name="type" type="int">操作类型（1-重置密码，0-注册，2-修改信息）</param>          
    var uid = $("#" + tbxID).val();
    var phone = $("#" + tbxPhone).val();
    if (type == 1 && uid == "") {
        showAlert("请输入学号/工号 ！");
        return;
    }
    if (phone == "") {
        showAlert("请输入手机号码 ！");
        return;
    }

    var handler = timer(btnSend, 60);

    var url = "../Handler/VerifyCode.ashx";
    $.post(url, { 'uid': uid, 'phone': phone, 'type': type }, function (data) {
        if (data.substr(0, 5) == "[Err]") {
            showAlert(data.substr(5));
            clearTimer(btnSend, handler);
        }
        else {
            var code = data.substr(0, 6);
            $("#" + hidCode).val(code + phone);
            showAlert(data.substr(6));
        }
    });
}

function ajaxGetPhone(tbxID, tbxPhone) {
    var tbx = $("#" + tbxPhone);
    var uid = $("#" + tbxID).val();
    tbx.val("");
    if (uid != "") {
        var url = "../Handler/VerifyPhone.ashx";
        $.post(url, { 'uid': uid }, function (data) {
            if (data.substr(0, 5) == "[Err]") showAlert(data.substr(5));
            else tbx.val(data);
        });
    }
}

function ajaxGetUser(ctrlid) {
    var tbxId = ctrlid.id;
    var lblId = tbxId.replace("tbxID", "lblName");
    var hidId = tbxId.replace("tbxID", "hidValue");
    var uid = $("#" + tbxId).val();
    if (uid == "") {
        $("#" + hidId).val("");
        $("#" + lblId).html("<span class='gray'>未分配</span>");
    }
    else {
        var url = "../Handler/VerifyUser.ashx";
        $.post(url, { 'uid': uid }, function (data) {
            if (data.substr(0, 5) == "[Err]") {
                $("#" + hidId).val("||查无此人");
                $("#" + lblId).html("<span class='TitleRed'>查无此人</span>");
                //$.alert(data.substr(5));
            }
            else {
                var sz = data.split("|");
                $("#" + hidId).val(data);
                $("#" + lblId).html("<span class='blue'>" + sz[2] + "</span>");
            }
        });
    }
}

function setPwd(tbxid) {
    var tbx = $("#" + tbxid);
    $("#hid" + tbxid).val(tbx.val());
}

function getPwd(tbxid) {
    var tbx = $("#" + tbxid);
    tbx.val($("#hid" + tbxid).val());
}
