import { $, $one } from "./common.js";
let listA = $(".mod-control-input a");
let mopInput = $one(".mod-control-input input");
listA[0].addEventListener("click", () => {
    console.log(mopInput);
});
//# sourceMappingURL=PopupDesc.js.map