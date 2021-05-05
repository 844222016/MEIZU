import { $, $one } from "./common.js";
$one(".signin").addEventListener("mouseover", () => {
    $one(".layout-member-downmenu").css({
        height: "176px "
    });
});
$one(".signin").addEventListener("mouseout", () => {
    $one(".layout-member-downmenu").css({
        height: "0px "
    });
});
$one(".exit").onclick = () => {
    localStorage.setItem("LoginState", "false");
};
$one(".cart-col-ctrl").onclick = () => {
    if ($one(".cart-product-remove").classList[1] == "let-show") {
        $one(".cart-product-remove").classList.remove("let-show");
        $one(".cart-col-ctrl").innerText = "编辑";
    }
    else {
        $one(".cart-product-remove").classList.add("let-show");
        $one(".cart-col-ctrl").innerText = "完成";
    }
};
$one(".to-order-btn").onclick = () => {
    window.location.href = "OrderDetails.html";
};
$one(".cart-product-remove").onclick = () => {
    localStorage.removeItem("PhoneDesc");
    $one(".cart-empty").css({
        display: "table"
    });
    $one(".cart-header").css({
        display: "none"
    });
    $one(".cart-merchant-list").css({
        display: "none"
    });
    $one(".cart-footer").css({
        display: "none"
    });
};
window.addEventListener("load", () => {
    if (localStorage.getItem("LoginState") == "true") {
        let a = JSON.parse(localStorage.getItem("PhonePwd"));
        $one(".layout-member-username").innerHTML = `用户`;
        $(".signout")[0].css({
            display: "none"
        });
        $(".signout")[1].css({
            display: "none"
        });
        $one(".signin").css({
            display: "block"
        });
        if (localStorage.getItem("PhoneDesc")) {
            $one(".cart-empty").css({
                display: "none"
            });
            $one(".cart-header").css({
                display: "table"
            });
            $one(".cart-merchant-list").css({
                display: "block"
            });
            $one(".cart-footer").css({
                display: "block"
            });
        }
        else {
            $one(".cart-empty").css({
                display: "table"
            });
            $one(".cart-header").css({
                display: "none"
            });
            $one(".cart-merchant-list").css({
                display: "none"
            });
            $one(".cart-footer").css({
                display: "none"
            });
        }
    }
    else {
        $(".signout")[0].css({
            display: "block"
        });
        $(".signout")[1].css({
            display: "block"
        });
        $one(".signin").css({
            display: "none"
        });
        $one(".cart-empty").css({
            display: "table"
        });
        $one(".cart-header").css({
            display: "none"
        });
        $one(".cart-merchant-list").css({
            display: "none"
        });
        $one(".cart-footer").css({
            display: "none"
        });
    }
});
let checkbox = $(".mz-checkbox");
for (let i = 0; i < checkbox.length; i++) {
    checkbox[i].addEventListener("click", () => {
        for (let j = 0; j < checkbox.length; j++) {
            if (checkbox[j].className == "mz-checkbox checked") {
                checkbox[j].classList.remove("checked");
            }
            else {
                checkbox[j].classList.add("checked");
            }
        }
    });
}
//# sourceMappingURL=ShoppingCart.js.map