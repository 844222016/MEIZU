window.addEventListener("load",
    function () {

        // 获取 父类
        let parent = document.querySelector(".type-tab-btn");
        // 获取 父类中的 a 标签
        let sub = parent.querySelectorAll("a");
        // 获取订单内容
        let content = document.querySelectorAll(".Short_Information");

        sub[0].className = 'show';
        sub[0].style.color = "#00c3f5";
        sub[0].style.borderBottom = "solid 2px #00c3f5";
        content[0].style.display = "block";
        for (let i = 0; i < sub.length; i++) {
            sub[i].setAttribute("index", i);
            sub[i].onclick = function (){
                for (let j = 0; j < sub.length; j++) {
                    sub[j].class = '';
                    sub[j].style.color = "";
                    sub[j].style.borderBottom = "";
                }
                this.className = 'show';
                this.style.color = "#00c3f5";
                this.style.borderBottom = "solid 2px #00c3f5";
                let index = this.getAttribute("index");
                console.log(index);
                for (let k = 0; k < content.length; k++) {
                    content[k].style.display = "none";
                }
                content[index].style.display = "block";
            }
        }
    });