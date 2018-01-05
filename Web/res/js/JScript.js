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

function timer(buttonid, time) {
    var btnSend = F(buttonid);
    var btn = $("#" + buttonid + "-btnInnerEl");
    //btn.attr("disabled", true);  //按钮禁止点击
    btnSend.disable();
    btn.html(time <= 0 ? "获取验证码" : ("" + (time) + "秒后重发"));
    var handler = setInterval(function () {
        if (time <= 0) {
            clearInterval(handler); //清除倒计时
            btn.html("获取验证码");
            //btn.removeAttr("disabled");
            btnSend.enable();
            return false;
        }
        else {
            --time;
            btn.html("" + (time) + "秒后重发");
        }
    }, 1000);
}

function ajaxGetVerifyCode(tbxID, tbxPhone, btnSend, hidCode, type) {
    /// <summary>获取手机验证码</summary>     
    /// <param name="type" type="int">操作类型（1-重置密码，0-注册）</param>          
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

    timer(btnSend, 60);

    var url = "./Handler/VerifyCode.ashx";
    $.post(url, { 'uid': uid, 'phone': phone, 'type': type }, function (data) {
        if (data.substr(0, 5) == "[Err]") showAlert(data.substr(5));
        else {
            //F("SimpleForm1_tbxPwd").enable();
            //F("SimpleForm1_tbxPwd2").enable();

            var code = data.substr(0, 6);
            $("#" + hidCode).val(code);
            showAlert(data.substr(6));
        }
    });
}

function ajaxGetPhone(tbxID, tbxPhone) {
    var tbx = $("#" + tbxPhone);
    var uid = $("#" + tbxID).val();
    tbx.val("");
    if (uid != "") {
        var url = "./Handler/VerifyPhone.ashx";
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
        var url = "./Handler/VerifyUser.ashx";
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
