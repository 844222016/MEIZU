import { $one } from "./common.js";
let button = $one("button");
let input2 = $one("#phone2");
button.addEventListener("click", () => {
    let input1 = $one("#phone1");
    let a = input1.value;
    input2.setAttribute("value", (a));
});
//# sourceMappingURL=Demo2.js.map