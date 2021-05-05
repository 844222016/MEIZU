import { $one, $ } from "./common.js";
let Order = $(".order-total-pay-label");
for (let i = 0; i < Order.length; i++) {
    if (i == 0) {
        Order[i].addEventListener("click", () => {
            $one(".order-total-pay-huabei").css({
                display: "block"
            });
        });
    }
    else {
        Order[i].addEventListener("click", () => {
            $one(".order-total-pay-huabei").css({
                display: "none"
            });
        });
    }
}
$one(".add").onclick = () => {
    $one(".mz-mask").css({
        display: "block"
    });
};
$one(".mz-dialog-close").onclick = () => {
    $one(".mz-mask").css({
        display: "none"
    });
};
$one("#submitForm").onclick = () => {
    alert("购买成功");
    localStorage.removeItem("PhoneDesc");
};
$one(".submit").onclick = () => {
    let Address;
    if (localStorage.getItem("Address")) {
        let a = JSON.parse(localStorage.getItem("Address"));
        if (a[0] == null) {
            localStorage.removeItem("Address");
            console.log(a.length);
        }
        else {
            let a = JSON.parse(localStorage.getItem("Address"));
            a.push({
                name: $one(".address-adder-name").value,
                phone: $one(".address-adder-phone").value,
                eprovinceName: $one("#eprovinceName").value,
                ecityName: $one("#ecityName").value,
                edistrictName: $one("#edistrictName").value,
                detail: $one(".address-adder-detail").value
            });
            localStorage.setItem("Address", JSON.stringify(a));
        }
    }
    else {
        Address = [({
                name: $one(".address-adder-name").value,
                phone: $one(".address-adder-phone").value,
                eprovinceName: $one("#eprovinceName").value,
                ecityName: $one("#ecityName").value,
                edistrictName: $one("#edistrictName").value,
                detail: $one(".address-adder-detail").value
            })];
        localStorage.setItem("Address", JSON.stringify(Address));
    }
    $one(".mz-mask").css({
        display: "none"
    });
};
//# sourceMappingURL=OrderDetails.js.map