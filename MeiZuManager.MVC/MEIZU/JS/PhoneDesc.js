import { $, $one } from "./common.js";
let dom = $(".fixThree");
let det = $one(".swiper-container");
let mod = $one(".mod-bd");
let detUl = $one(".swiper-container>ul");
let detImg = $one(".swiper-container>ul>li img");
dom[0].addEventListener("click", () => {
    mod.css({
        display: "none"
    });
    mod.removeChild(det);
});
dom[1].addEventListener("click", () => {
    detImg.attributes[0].value = "../Images/Index/swiper-container1.png";
    detUl.children[1].css({
        display: "block"
    });
    detUl.children[0].css({
        width: "284px"
    });
    mod.css({
        display: "block"
    });
    mod.appendChild(det);
});
dom[2].addEventListener("click", () => {
    detImg.attributes[0].value = "../Images/Index/swiper-container3.png";
    detUl.children[1].css({
        display: "none"
    });
    detUl.children[0].css({
        width: "100%"
    });
    mod.css({
        display: "block"
    });
    mod.appendChild(det);
});
dom[3].addEventListener("click", () => {
    detImg.attributes[0].value = "../Images/Index/swiper-container4.png";
    detUl.children[1].css({
        display: "none"
    });
    detUl.children[0].css({
        width: "100%"
    });
    mod.css({
        display: "block"
    });
    mod.appendChild(det);
});
let money = $one(".mod-buttom span em");
let moneyCopy = money.innerHTML;
let listA = $(".mod-control-input a");
let mopInput = $one(".mod-control-input input");
let InputValue = mopInput.getAttribute("value");
let moneys;
listA[0].addEventListener("click", () => {
    if (InputValue == "1") {
        return;
    }
    else {
        mopInput.attributes[3].value = (InputValue = (parseInt(InputValue) - 1).toString());
        money.innerHTML = (parseInt(mopInput.attributes[3].value) * 3699).toString();
    }
});
listA[1].addEventListener("click", () => {
    if (InputValue == "3") {
        return;
    }
    else {
        mopInput.attributes[3].value = (InputValue = (parseInt(InputValue) + 1).toString());
        money.innerHTML = (parseInt(mopInput.attributes[3].value) * (parseInt(moneyCopy))).toString();
    }
});
//手机详情页切换
let index = 0;
let PxNumber = 0;
let PopupPreview = $one(".preview-div");
let btnPrev = $one(".swiper-button-prev");
let btnNext = $one(".swiper-button-next");
btnPrev.addEventListener("click", () => {
    PopupPreview.css({
        transform: `translate3d(${PxNumber}px, 0px, 0px)`,
        transitionDuration: "300ms"
    });
    if (index == 0) {
        return;
    }
    else {
        for (let i = 0; i < perviewDot.length; i++) {
            perviewDot[i].classList.remove("dot-active");
        }
        index--;
        PxNumber = PxNumber + 580;
        PopupPreview.css({
            transform: `translate3d(${PxNumber}px, 0px, 0px)`,
            transitionDuration: "300ms"
        });
        perviewDot[index].classList.add("dot-active");
    }
    console.log(index);
});
btnNext.addEventListener("click", () => {
    if (index == 3) {
        return;
    }
    else {
        for (let i = 0; i < perviewDot.length; i++) {
            perviewDot[i].classList.remove("dot-active");
        }
        index++;
        PxNumber = -580 * index;
        PopupPreview.css({
            transform: `translate3d(${PxNumber}px, 0px, 0px)`,
            transitionDuration: "300ms"
        });
        perviewDot[index].classList.add("dot-active");
    }
    console.log(index);
});
//小点点渲染
let XY3d = [];
let perviewDot = $(".preview-dot span");
for (let i = 0; i < perviewDot.length; i++) {
    XY3d[i] = PxNumber = -580 * i;
    perviewDot[i].addEventListener("click", () => {
        index = i;
        for (let j = 0; j < perviewDot.length; j++) {
            perviewDot[j].classList.remove("dot-active");
        }
        perviewDot[i].classList.add("dot-active");
        PopupPreview.css({
            transform: `translate3d(${XY3d[i]}px, 0px, 0px)`,
            transitionDuration: "300ms"
        });
    });
}
//手机详情切换定位
let FlexdShow = $one(".FlexdShow");
let preview = $one(".preview");
function mouseMove() {
    let scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
    if (scrollTop <= 100) {
        preview.classList.remove("preview-Position");
        preview.classList.remove("preview-PositionAb");
    }
    else if (scrollTop >= 150) {
        preview.classList.add("preview-Position");
        if (scrollTop >= 1100) {
            preview.classList.remove("preview-Position");
            preview.classList.add("preview-PositionAb");
        }
        else {
            preview.classList.add("preview-Position");
            preview.classList.remove("preview-PositionAb");
        }
    }
    if (scrollTop >= 1792) {
        FlexdShow.css({
            height: "120px"
        });
    }
    else {
        FlexdShow.css({
            height: "0px",
        });
    }
}
window.addEventListener("scroll", mouseMove);
let flexNext = $one(".flex-next");
let flexPrev = $one(".flex-prev");
let flexViewport = $one(".flex-viewport>ul");
flexNext.addEventListener("click", () => {
    flexViewport.css({
        transitionDuration: "300ms",
        transform: "translate3d(-240px, 0px, 0px)"
    });
});
flexPrev.addEventListener("click", () => {
    flexViewport.css({
        transitionDuration: "300ms",
        transform: "translate3d(0px, 0px, 0px)"
    });
});
$one(".ShoppingCar").addEventListener("click", () => {
    if (localStorage.getItem("LoginState") == "true") {
        let selected = $(".selected");
        ;
        let PhoneDesc = [({
                PhoneName: "魅族17",
                PhoneImg: "../Images/ShoppingCart/Phone1.png",
                PhoneBB: selected[0].innerText,
                PhoneColor: selected[1].innerText,
                PhoneGF: selected[2].innerText,
                PhonePrice: $one(".mod-buttom span em").innerText,
                PhoneNumber: $one(".mod-control-input input").value
            })];
        localStorage.setItem("PhoneDesc", JSON.stringify(PhoneDesc));
        $one(".bi-alert").css({
            display: "table"
        });
        setTimeout(() => {
            $one(".bi-alert").css({
                display: "none"
            });
        }, 2000);
    }
    else {
        alert("您未登录，将跳转至登录页面");
        window.location.href = "http://localhost:2080/MEIZU/Html/RLogin.html";
    }
});
$one(".mod-link").onclick = () => {
    window.location.href = "http://localhost:2080/MEIZU/Html/ShoppingCart.html";
};
$one(".icon-chahao").onclick = () => {
    $one(".bi-alert").css({
        display: "none"
    });
};
let video = $one(".vjs-tech");
$one(".video").onclick = () => {
    $one(".video-win").css({
        display: "block"
    });
    if (video.paused) {
        video.play();
    }
    else {
        video.pause();
    }
};
$one(".flyme-win__close").onclick = () => {
    $one(".video-win").css({
        display: "none"
    });
    video.pause();
};
window.addEventListener("load", () => {
    let a = JSON.parse(localStorage.getItem("PhoneDesc"));
    $one(".shoopingcar em").innerHTML = a.length.toString();
});
//# sourceMappingURL=PhoneDesc.js.map