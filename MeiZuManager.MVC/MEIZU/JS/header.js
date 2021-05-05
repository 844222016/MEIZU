import { $, $id, $one } from "./common.js";
let numberY = 0;
let dom = $one(".showitem");
let headerBg = $one(".header-container");
let bg = $one(".header-BackGround");
let show = $one(".header-BackGround-ShowList");
let listOne = $id("ListOneCopy");
let listTwo = $id("ListTwoCopy");
let listThree = $id("ListThreeCopy");
let listFour = $id("ListFourCopy");
let domLi = $(".showitem>li");
domLi[0].style.display;
for (const item of domLi) {
    item.css({
        display: "block"
    });
    console.log(item.style[0]);
}
let Runitem = $one(".Runitem");
let RunitemLi = $one(".LastItem");
let RunitemA = $one(".LastItem a");
let don = $one(".meizu-header-sub");
let item = [listOne, listTwo, listThree, listFour];
dom.addEventListener("mouseover", () => {
    headerBg.css({
        backgroundColor: "white"
    });
    bg.css({
        transition: "all 200ms",
        height: "182px"
    });
});
function mouseMove(ev) {
    let mousePos = ev.clientY + document.body.scrollTop - document.body.clientTop;
    if (mousePos >= numberY) {
        headerBg.css({
            backgroundColor: ""
        });
        bg.css({
            transition: "all 0ms",
            height: ""
        });
        show.innerHTML = "";
    }
}
function mouseMoveTwo(ev) {
    let mousePos = ev.clientY + document.body.scrollTop - document.body.clientTop;
    if (mousePos >= numberY) {
        headerBg.css({
            backgroundColor: ""
        });
        don.css({
            transition: "all 0ms",
            height: ""
        });
        bg.css({
            transition: "all 0ms",
            height: ""
        });
        show.innerHTML = "";
    }
}
for (let i = 0; i < 4; i++) {
    domLi[i].addEventListener("mouseover", () => {
        numberY = 263;
        if (numberY == 263) {
            document.onmousemove = mouseMove;
        }
        show.innerHTML = item[i].innerHTML;
        $one(".header-BackGround-ShowList div").css({
            display: "block"
        });
        don.css({
            transition: "all 200ms",
            height: ""
        });
    });
}
bg.addEventListener("mouseover", () => {
    bg.css({
        transition: "all 200ms",
        height: "182px"
    });
});
RunitemA.addEventListener("mouseover", () => {
    numberY = 400;
    document.onmousemove = mouseMoveTwo;
});
Runitem.addEventListener("mouseover", () => {
    don.css({
        transition: "all 0ms",
        height: ""
    });
    bg.css({
        transition: "all 0ms",
        height: ""
    });
    setTimeout(() => {
        headerBg.css({
            backgroundColor: ""
        });
    }, 10);
    show.innerHTML = "";
});
$one(".header-search").addEventListener("mouseover", () => {
    don.css({
        transition: "all 0ms",
        height: ""
    });
    bg.css({
        transition: "all 0ms",
        height: ""
    });
    setTimeout(() => {
        headerBg.css({
            backgroundColor: ""
        });
    }, 10);
    show.innerHTML = "";
});
$one(".header-user-img").addEventListener("mouseover", () => {
    don.css({
        transition: "all 0ms",
        height: ""
    });
    bg.css({
        transition: "all 0ms",
        height: ""
    });
    setTimeout(() => {
        headerBg.css({
            backgroundColor: ""
        });
    }, 10);
    show.innerHTML = "";
});
RunitemLi.addEventListener("mouseover", () => {
    don.css({
        transition: "all 200ms",
        height: "320px"
    });
    headerBg.css({
        backgroundColor: "white"
    });
});
//# sourceMappingURL=header.js.map