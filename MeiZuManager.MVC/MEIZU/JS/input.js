import { $one } from "./common.js";
import anime from "../node_modules/animejs/lib/anime.es.js";
let search_btn = $one(".search_btn>img");
let input = $one("input");
$one("input").css({
    transiton: "all 1000ms",
    width: "0px",
    background: "#fff",
    padding: "0 30px",
    color: "#000",
    fontSize: "16px",
    float: "right"
});
search_btn.css({
    zIndex: "999"
});
search_btn.addEventListener("click", () => {
    $one("input").css({
        transiton: "all 1000ms",
    });
    $one(".search").css({
        display: "block"
    });
    $one(".search_colse").css({
        display: "block"
    });
    $one("input").css({
        display: "block"
    });
    anime({
        targets: search_btn,
        translateX: -530,
    });
    anime({
        targets: input,
        width: 850
    });
});
$one(".search_colse").addEventListener("click", () => {
    $one("input").css({
        display: "none"
    });
    $one(".search_colse").css({
        display: "none"
    });
    anime({
        targets: search_btn,
        translateX: 0,
        duration: 0
    });
});
//# sourceMappingURL=input.js.map