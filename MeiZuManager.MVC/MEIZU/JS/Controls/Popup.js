import { $one, $, $id } from "../common.js";
let list = $one(".list");
let item = $(".item");
let Left = $id("Left");
let Right = $id("Right");
let dot = $(".dot");
let index = 0;
Right.addEventListener("click", () => {
    index++;
    for (let i = 0; i < item.length; i++) {
        item[i].classList.remove("active");
        dot[i].classList.remove("rander");
    }
    if (index < 4) {
        item[index].classList.add("active");
        dot[index].classList.add("rander");
    }
    else {
        index = 0;
        item[index].classList.add("active");
        dot[index].classList.add("rander");
    }
});
Left.addEventListener("click", () => {
    index--;
    for (let i = 0; i < item.length; i++) {
        item[i].classList.remove("active");
        dot[i].classList.remove("rander");
    }
    if (index < 0) {
        index = 3;
        item[index].classList.add("active");
        dot[index].classList.add("rander");
    }
    else {
        item[index].classList.add("active");
        dot[index].classList.add("rander");
    }
});
for (let i = 0; i < dot.length; i++) {
    dot[i].addEventListener("click", () => {
        index = i;
        for (let i = 0; i < item.length; i++) {
            item[i].classList.remove("active");
            dot[i].classList.remove("rander");
        }
        item[i].classList.add("active");
        dot[i].classList.add("rander");
    });
}
let timer = 0;
function Time() {
    timer = setInterval(() => {
        index++;
        for (let i = 0; i < item.length; i++) {
            item[i].classList.remove("active");
            dot[i].classList.remove("rander");
        }
        if (index < 4) {
            item[index].classList.add("active");
            dot[index].classList.add("rander");
        }
        else {
            index = 0;
            item[index].classList.add("active");
            dot[index].classList.add("rander");
        }
    }, 2000);
}
let warp = $one(".warp");
warp.addEventListener("mouseover", () => {
    clearInterval(timer);
});
warp.addEventListener("mouseout", () => {
    clearInterval(timer);
    Time();
});
window.addEventListener("load", () => {
    clearInterval(timer);
    Time();
});
//# sourceMappingURL=Popup.js.map