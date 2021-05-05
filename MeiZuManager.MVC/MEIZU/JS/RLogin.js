import { $one, $ } from "./common.js";
let focus = $(".inp-focus");
let cycode = $(".cycode-selectbox");
for (let i = 0; i < focus.length; i++) {
    focus[i].addEventListener("focus", () => {
        $one(".tip-box").css({
            display: 'none'
        });
    });
}
$one(".register-btn").onclick = () => {
    let phone = $one("#phone").value;
    let Pwd = $one("#Pwd").value;
    let a = JSON.parse(localStorage.getItem("PhonePwd"));
    if (phone == "" || Pwd == "") {
        $one(".tip-font").innerHTML = "账号或密码不可为空";
        $one(".tip-box").css({
            display: 'table'
        });
    }
    else {
        if (a.Phone == phone) {
            if (a.Pwd == Pwd) {
                $one(".tip-font").innerHTML = "登录成功";
                $one(".tip-box").css({
                    display: 'table'
                });
                localStorage.setItem("LoginState", "true");
                setTimeout(() => {
                    window.location.href = "Index.html";
                }, 1000);
            }
            else {
                $one(".tip-font").innerHTML = "密码错误,请检查密码";
                $one(".tip-box").css({
                    display: 'table'
                });
            }
        }
        else {
            $one(".tip-font").innerHTML = "该账号不存在";
            $one(".tip-box").css({
                display: 'table'
            });
        }
    }
};
//# sourceMappingURL=RLogin.js.map