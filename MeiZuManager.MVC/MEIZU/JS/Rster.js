import { $one, $ } from "./common.js";
let focus = $(".inp-focus");
let cycode = $(".cycode-selectbox");
for (let i = 0; i < focus.length; i++) {
    focus[i].addEventListener("blur", () => {
        cycode[i].classList.add("selectedFFF");
        cycode[i].classList.remove("selected");
    });
    focus[i].addEventListener("focus", () => {
        if ($one(".checkboxPic").classList[3] == "check_unchkselected") {
            $one(".tip-box").css({
                display: 'none'
            });
        }
        else {
            $one(".tip-font").innerHTML = "请先同意用户条款";
            $one(".tip-box").css({
                display: 'table'
            });
        }
    });
}
$one(".checkboxPic").onclick = () => {
    if ($one(".checkboxPic").classList[3] == "check_unchkselected") {
        $one(".checkboxPic").classList.remove("check_unchkselected");
    }
    else {
        $one(".checkboxPic").classList.add("check_unchkselected");
        $one(".tip-box").css({
            display: 'none'
        });
    }
};
$one(".close-ico").onclick = () => {
    $one(".tip-box").css({
        display: 'none'
    });
};
//let PhonePwd;
//$one(".register-btn").addEventListener("click",()=>{
//    let filterPhone= (<HTMLInputElement>$one("#phone")).value;
//    let filter = (<HTMLInputElement>$one("#Pwd")).value;
//    if ($one(".checkboxPic").classList[3] =="check_unchkselected") {
//        if (!filterPhone || !filter) {
//            $one(".tip-font").innerHTML = "请输入完整的注册信息";
//            $one(".tip-box").css({
//                display:'table'
//            })
//        }else{
//            if (localStorage.getItem("PhonePwd")) {
//                let a = JSON.parse(localStorage.getItem("PhonePwd"));
//                if (a.Phone == filterPhone) {
//                    $one(".tip-font").innerHTML = "该账号已存在-跳转登录页面";
//                    $one(".tip-box").css({
//                        display:'table'
//                    })
//                    setTimeout(()=>{
//                        window.location.href="RLogin.html"
//                    },2000)
//                }else{
//                    $one(".tip-font").innerHTML = "账号注册成功";
//                    $one(".tip-box").css({
//                        display:'table',
//                        background: "greenyellow"
//                    })
//                    PhonePwd= {"Phone": filterPhone,"Pwd": filter};
//                    PhonePwd = JSON.stringify(PhonePwd);
//                    localStorage.setItem("PhonePwd", PhonePwd);
//                }
//            }else{
//                PhonePwd= {"Phone": "","Pwd": ""};
//                PhonePwd = JSON.stringify(PhonePwd);
//                localStorage.setItem("PhonePwd", PhonePwd);
//            }
//        }
//    }else{
//        $one(".tip-font").innerHTML = "请先同意用户条款";
//        $one(".tip-box").css({
//            display:'table'
//        })
//    }
//})
//# sourceMappingURL=Rster.js.map