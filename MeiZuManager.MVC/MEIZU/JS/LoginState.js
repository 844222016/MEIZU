import { $, $one } from "./common.js";
let LoginLastString = ["个人中心", "我的订单", "M码通道", "推出登录"];
let state = "true";
state.isEmail();
window.addEventListener("load", () => {
    let imgPath = "../Images/Index/userImg.png";
    if (localStorage.getItem("LoginState") == state) {
        $one(".header-user-img img").attributes[0].value = imgPath;
        $one(".header-user-img img").css({
            display: "block",
            width: "29px",
            height: "29px",
            borderRadius: "50%"
        });
        $one(".header-user-img").css({
            background: "url()"
        });
        let ulLi = $(".meizu-header-user-dropdown li a");
        for (let i = 0; i < ulLi.length; i++) {
            console.log(ulLi[i].innerHTML = LoginLastString[i]);
        }
        ulLi[0].href = "javascript:;";
        ulLi[1].href = "javascript:;";
        ulLi[2].href = "javascript:;";
        ulLi[3].addEventListener("click", () => {
            localStorage.setItem("LoginState", "false");
        });
    }
    else {
        console.log(false);
    }
});
//# sourceMappingURL=LoginState.js.map