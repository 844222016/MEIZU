import { $one, createEl } from "../common.js";
let Address = $one(".order-address-list");
if (localStorage.getItem("Address")) {
    let a = JSON.parse(localStorage.getItem("Address"));
    if (a.length > 0) {
        for (let i = 0; i < a.length; i++) {
            Address.appendChild(createEl({
                tagName: "li",
                dataId: `${i}`,
                class: "order-address-checkbox checked",
                events: [
                    {
                        eventName: "mouseover",
                        eventFunc: function () {
                            $(".order-address-checkbox-ctrl")[i].css({
                                opacity: "1"
                            });
                        }
                    }, {
                        eventName: "mouseout",
                        eventFunc: function () {
                            $(".order-address-checkbox-ctrl")[i].css({
                                opacity: "0"
                            });
                        }
                    }
                ],
                children: [{
                        tagName: "div",
                        class: "order-address-checkbox-top",
                        css: {
                            fontSize: "12px",
                            paddingTop: "16px",
                            color: "#262626",
                            height: "45px",
                            borderBottom: "1px solid #efefef",
                            fontWeight: "bolder"
                        },
                        children: [{
                                tagName: "div",
                                class: "order-address-checkbox-name",
                                css: {
                                    float: "left",
                                    widht: "125px",
                                    overflow: "hidden",
                                    whiteSpace: "nowrap",
                                    wordBreak: "keap-all",
                                    textOverflow: "ellipsis"
                                },
                                title: a[i].name,
                                text: a[i].name
                            }, {
                                tagName: "div",
                                class: "order-address-checkbox-phone",
                                css: {
                                    float: "right"
                                },
                                text: a[i].phone
                            }]
                    }, {
                        tagName: "div",
                        class: "order-address-checkbox-content",
                        css: {
                            paddingTop: "10px",
                            wordBreak: "break-all",
                            fontSize: "12px",
                            maxHeight: "70px",
                            overflow: "hidden",
                            color: "#595959"
                        },
                        text: a[i].ecityName + a[i].eprovinceName + a[i].edistrictName + a[i].detail
                    }, {
                        tagName: "div",
                        class: "order-address-checkbox-checked",
                        css: {
                            position: "absolute",
                            overflow: "hidden",
                            right: "0",
                            bottom: "0",
                            width: "20px",
                            height: "20px"
                        },
                        children: [{
                                tagName: "div",
                                class: "order-address-checkbox-tick"
                            }]
                    }, {
                        tagName: "div",
                        class: 'order-address-checkbox-ctrl',
                        css: {
                            position: "absolute",
                            opacity: "0",
                            bottom: "-20px",
                            left: "0",
                            width: "100%",
                            height: "20px",
                            fontSize: "14px",
                            paddingTop: "5px",
                            color: "#00c3f5"
                        },
                        children: [{
                                tagName: "div",
                                class: "order-address-checkbox-delete",
                                events: [
                                    {
                                        eventName: "click",
                                        eventFunc: function () {
                                            let b = $(".order-address-checkbox");
                                            for (let j = 0; j < b.length; j++) {
                                                if (b[j].className == "order-address-checkbox add") {
                                                    continue;
                                                }
                                                else {
                                                    if (b[j].getAttribute(`dataId`) == i.toString()) {
                                                        $one(`.order-address-checkbox[dataid="${i}"]`).remove();
                                                        delete a[i];
                                                        localStorage.setItem("Address", JSON.stringify(a));
                                                    }
                                                    else {
                                                        console.log(false);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                ],
                                css: {
                                    marginRight: "10px",
                                    float: "right"
                                }
                            }, {
                                tagName: "div",
                                class: "order-address-checkbox-edit",
                                css: {
                                    marginRight: "10px",
                                    float: "right"
                                }
                            }]
                    }]
            }));
        }
    }
}
else {
}
//# sourceMappingURL=Address.js.map