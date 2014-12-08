
$(document).ready(function () {
    $("#btn-logon").click(function () {
        ValidateLogOn(txtUserName, pwdPassword, radRememberMe, txtRandomCode);
    });
    //单击验证码事件
    $("#random-code").bind("click", function () {
        this.src = "/Account/GetRandomCode?time=" + (new Date()).getTime();
    });
    function GetValidateCode() {
        $("#random-code").attr("src", "/Account/GetRandomCode?time=" + (new Date()).getTime());
    }
})

function ValidateLogOn(userNameEle, passwordEle, rememberMeEle, randomCodeEle) {
    var userName = userNameEle.value;
    var password = passwordEle.value;
    var rememberMe = rememberMeEle.checked;
    debugger;
    var randomCode = randomCodeEle.value;
    if (userName == "") {
        alert("用户名不能为空！");
        userNameEle.focus();
    } else if (password == "") {
        alert("密码不能为空！");
    } else if (randomCode == "") {
        alert("验证码不能为空！");
    } else {
        LogOn(userName, password, rememberMe, randomCode);
    }
}

function LogOn(userName, password, rememberMe, randomCode) {
    debugger;
    $.ajax({
        type: "POST",
        url: '/Account/LogOn',
        data: { userName: userName, password: password, rememberMe: rememberMe, randomCode: randomCode },
        success: function (data) {
            if (data == 1) {
                alert("登录成功！");
                location.reload();
            }
            else if (data==-2) {
                alert("验证码错误！");
            }
            else {
                alert("登录失败！");
            }
        },
        error: function () {
            alert("登录失败！");
        }
    });
}
