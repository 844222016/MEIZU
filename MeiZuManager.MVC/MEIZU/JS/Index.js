import { $, $one } from "./common.js";
let showLi = $one(".lilast");
let lilastul = $one(".lilast>ul");
showLi.addEventListener("mouseover", () => {
    lilastul.css({
        display: "block"
    });
});
showLi.addEventListener("mouseout", () => {
    lilastul.css({
        display: "none"
    });
});
lilastul.addEventListener("mouseover", () => {
    lilastul.css({
        display: "block"
    });
});
let a1 = $(".ii-subscribes img")[0];
let a2 = $(".ii-subscribes img")[1];
a1.addEventListener("mouseover", () => {
    $one(".weiImg img:nth-child(1)").css({
        display: "block"
    });
});
a1.addEventListener("mouseout", () => {
    $one(".weiImg img:nth-child(1)").css({
        display: "none"
    });
});
a2.addEventListener("mouseover", () => {
    $one(".weiImg img:nth-child(2)").css({
        display: "block"
    });
});
a2.addEventListener("mouseout", () => {
    $one(".weiImg img:nth-child(2)").css({
        display: "none"
    });
});
//动画回滚顶部
let timer = 0;
let top = $one(".onlive-btn");
top.onclick = function () {
    timer = setInterval(function () {
        let scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
        let ispeed = Math.floor(-scrollTop / 6);
        if (scrollTop == 0) {
            clearInterval(timer);
        }
        document.documentElement.scrollTop = document.body.scrollTop = scrollTop + ispeed;
    }, 10);
};
$one(".shoopingcar").onmouseover = () => {
    $one(".store-cart").css({
        display: "block"
    });
};
$one(".shoopingcar").onmouseout = () => {
    $one(".store-cart").css({
        display: "none"
    });
};
$one(".store-cart").addEventListener("mouseover", () => {
    $one(".store-cart").css({
        display: "block"
    });
});
window.addEventListener("load", () => {
    if (localStorage.getItem("LoginState") == "true") {
        if (localStorage.getItem("PhoneDesc")) {
            let a = JSON.parse(localStorage.getItem("PhoneDesc"));
            $one(".shoopingcar em").innerHTML = a.length.toString();
            $one(".store-cart-tips-text").innerText = `您已选购了${a.length.toString()}件商品`;
        }
        else {
            return;
        }
    }
    else {
        return;
    }
});
//# sourceMappingURL=Index.js.map