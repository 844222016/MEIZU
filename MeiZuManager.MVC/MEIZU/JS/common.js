/**
 * 通过查询字符串获取dom集合
 * @param query 查询字符串
 */
export function $(query) {
    if (this instanceof HTMLElement)
        return this.querySelectorAll(query);
    return document.querySelectorAll(query);
}
export function $one(query) {
    if (this instanceof HTMLElement)
        return this.querySelector(query);
    return document.querySelector(query);
}
/**
 * 获取id属性的dom元素
 * @param id html元素的id属性
 */
export function $id(id) {
    return document.getElementById(id);
}
/**
 * 创建元素
 * @param json 创建元素option
 */
export function createEl(json) {
    let dom = document.createElement(json.tagName);
    for (const key in json) {
        if (key === 'tagName')
            continue;
        if (key === "text") {
            dom.innerText = json['text'];
            continue;
        }
        if (key === "css") {
            for (const css in json['css']) {
                dom.style[css] = json["css"][css];
            }
        }
        if (key === "events") {
            for (const event of json['events']) {
                if (!event.thisArg)
                    dom.addEventListener(event.eventName, event.eventFunc);
                else
                    dom.addEventListener(event.eventName, event.eventFunc.bind(event.thisArg));
            }
            continue;
        }
        if (key === "children") {
            for (const child of json['children']) {
                dom.appendChild(createEl(child));
            }
            continue;
        }
        dom.setAttribute(key, json[key]);
    }
    return dom;
}
export var DateType;
(function (DateType) {
    DateType[DateType["year"] = 0] = "year";
    DateType[DateType["month"] = 1] = "month";
    DateType[DateType["day"] = 2] = "day";
    DateType[DateType["hour"] = 3] = "hour";
    DateType[DateType["minute"] = 4] = "minute";
    DateType[DateType["second"] = 5] = "second";
})(DateType || (DateType = {}));
Element.prototype.$ = $;
Element.prototype.$one = $one;
Element.prototype.css = function (json) {
    Object.assign(this.style, json);
    return this;
};
Element.prototype.getWidth = function () {
    return this.clientWidth;
};
Element.prototype.getHeight = function () {
    return this.clientHeight;
};
Element.prototype.getOutWidth = function () {
    return this.offsetWidth;
};
Element.prototype.getOutHeight = function () {
    return this.offsetHeight;
};
Element.prototype.getComputedStyle = function (styleName) {
    return getComputedStyle(this, null)[styleName];
};
Element.prototype.animate = function (json, fn) {
    clearInterval(this.timeId); //清理定时器
    //定时器,返回的是定时器的id
    this.timeId = setInterval(() => {
        var flag = true; //默认,假设,全部到达目标
        //遍历json对象中的每个属性还有属性对应的目标值
        for (var attr in json) {
            //判断这个属性attr中是不是opacity
            if (attr == "opacity") {
                //获取元素的当前的透明度,当前的透明度放大100倍
                var current = parseFloat(this.getComputedStyle(attr)) * 100;
                //目标的透明度放大100倍
                var target = json[attr] * 100;
                var step = (target - current) / 10;
                step = step > 0 ? Math.ceil(step) : Math.floor(step);
                current += step; //移动后的值
                this.style[attr] = (current / 100).toString();
            }
            else if (attr == "zIndex") { //判断这个属性attr中是不是zIndex
                //层级改变就是直接改变这个属性的值
                this.style[attr] = json[attr];
            }
            else {
                //普通的属性
                //获取元素这个属性的当前的值
                var current = parseInt(this.getComputedStyle(attr));
                //当前的属性对应的目标值
                var target = json[attr];
                //移动的步数
                var step = (target - current) / 10;
                step = step > 0 ? Math.ceil(step) : Math.floor(step);
                current += step; //移动后的值
                this.style[attr] = current + "px";
            }
            //是否到达目标
            if (current != target) {
                flag = false;
            }
        }
        if (flag) {
            //清理定时器
            clearInterval(this.timeId);
            //所有的属性到达目标才能使用这个函数,前提是用户传入了这个函数
            if (fn) {
                fn();
            }
        }
        //测试代码
        //console.log("目标:" + target + ",当前:" + current + ",每次的移动步数:" + step);
    }, 20);
};
String.prototype.isInt = function () {
    return /^-?\d+$/.test(this);
};
/**
 * 判断是否是数字
 */
String.prototype.isNum = function () {
    return /^-?\d*\.?\d+$/.test(this);
};
String.prototype.isEmail = function () {
    return /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/.test(this);
};
String.prototype.isPhone = function () {
    return /^1[34578]\d{9}$/.test(this);
};
String.prototype.isIdCard = function () {
    return /^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$/.test(this);
};
String.prototype.isUrl = function () {
    return /^((https?|ftp|file):\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/.test(this);
};
String.prototype.isColorString = function () {
    return /^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$/.test(this);
};
String.prototype.isDate = function () {
    return /^\d{4}(\-)\d{1,2}\1\d{1,2}$/.test(this);
};
String.prototype.toDate = function () {
    return new Date(this);
};
Date.prototype.toDateFormatString = function (formatStr) {
    let year = this.getFullYear().toString();
    let month = (this.getMonth() + 1).toString();
    let day = this.getDate().toString();
    let week = this.getDay(); //0是周日
    let hour = this.getHours().toString();
    let minute = this.getMinutes().toString();
    let second = this.getSeconds().toString();
    let weekArr = ['周日', '周一', '周二', '周三', '周四', '周五', '周六'];
    let weekDay = weekArr[week];
    return formatStr.replace("{y}", year)
        .replace("{M}", month)
        .replace("{d}", day)
        .replace("{h}", hour)
        .replace("{m}", minute)
        .replace("{s}", second)
        .replace("{w}", weekDay);
};
Date.prototype.addTime = function (dateType, num) {
    switch (dateType) {
        case DateType.year:
            this.setFullYear(this.getFullYear() + num);
            break;
        case DateType.month:
            this.setMonth(this.getMonth() + num);
            break;
        case DateType.day:
            this.setDate(this.getDate() + num);
            break;
        case DateType.hour:
            this.setHours(this.getHours() + num);
            break;
        case DateType.minute:
            this.setMinutes(this.getMinutes() + num);
            break;
        case DateType.second:
            this.setSeconds(this.getSeconds() + num);
            break;
        default:
            throw "错误数据";
    }
};
Date.prototype.dateDiff = function (dateType, objDate2) {
    var d = this, i = {}, t = d.getTime(), t2 = objDate2.getTime();
    i['y'] = objDate2.getFullYear() - d.getFullYear();
    i['q'] = i['y'] * 4 + Math.floor(objDate2.getMonth() / 4) - Math.floor(d.getMonth() / 4);
    i['m'] = i['y'] * 12 + objDate2.getMonth() - d.getMonth();
    i['ms'] = objDate2.getTime() - d.getTime();
    i['w'] = Math.floor((t2 + 345600000) / (604800000)) - Math.floor((t + 345600000) / (604800000));
    i['d'] = Math.floor(t2 / 86400000) - Math.floor(t / 86400000);
    i['h'] = Math.floor(t2 / 3600000) - Math.floor(t / 3600000);
    i['n'] = Math.floor(t2 / 60000) - Math.floor(t / 60000);
    i['s'] = Math.floor(t2 / 1000) - Math.floor(t / 1000);
    return i[dateType];
};
//# sourceMappingURL=common.js.map