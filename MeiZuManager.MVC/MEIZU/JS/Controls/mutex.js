import { $ } from "../common.js";
/**
 * 类型,样式类名
 */
class TabClick {
    constructor(Obj, ClassList) {
        this.Obj = Obj;
        this.ClassList = ClassList;
        this.obj = Obj;
        this.classList = ClassList;
        this.init();
    }
    init() {
        this.el = $(this.obj);
        for (let i = 0; i < this.el.length; i++) {
            this.el[i].addEventListener("click", () => {
                for (let j = 0; j < this.el.length; j++) {
                    this.el[j].classList.remove(this.classList);
                    this.el[j].classList.remove("selectedFFF");
                }
                this.el[i].classList.add(this.classList);
            });
        }
    }
}
window.addEventListener("load", () => {
    new TabClick(".fixOne", "selected");
    new TabClick(".fixTwo", "selected");
    new TabClick(".fixThree", "selected");
    new TabClick(".fixFour", "selected");
    new TabClick(".cycode-selectbox", "selected");
    new TabClick(".order-total-pay-huabei-choose", "active");
    new TabClick(".order-total-pay-label", "active");
});
//# sourceMappingURL=mutex.js.map