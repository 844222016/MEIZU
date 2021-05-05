import { $one, } from "./common.js";
let input = $one("input");
input.addEventListener("keyup", () => {
    let filter = input.value.toUpperCase();
    let ul = $one("#myUL");
    let li = ul.$("li");
    let a;
    for (let i = 0; i < li.length; i++) {
        a = li[i].$("a")[0];
        if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            li[i].css({
                display: "block"
            });
        }
        else {
            li[i].css({
                display: "none"
            });
        }
    }
});
//# sourceMappingURL=Demo.js.map