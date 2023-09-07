window.onload = function() {
    const contentContainer = document.querySelector(".content_container");
    const productsLink = document.querySelector("#tab_products");
    const ordersLink = document.querySelector("#tab_orders");
    const tagsLink = document.querySelector("#tab_tags");

    let loadedEvent = null;
    
    checkSessionStorage()
    function checkSessionStorage () {
        if(!sessionStorage.getItem("adminPagePath") || sessionStorage.getItem("adminPagePath") === "404") {
            sessionStorage.setItem("adminPagePath", "adminProducts");
            path = sessionStorage.getItem("adminPagePath"); 
            checkPath(path) 
        } else {
            path = sessionStorage.getItem("adminPagePath");
            checkPath(path)
        }
    }

    
    function checkPath(path) {
        switch(path) {
            case "adminProducts": {
                loadPage("adminProducts");
                setTimeout(() => {
                    loadedEvent = new Event('productsSectionLoaded');
                    window.dispatchEvent(loadedEvent);
                }, 100)
                break;
            }
            case "adminTags": {
                loadPage("adminTags");
                setTimeout(() => {
                    loadedEvent = new Event('tagsSectionLoaded');
                    window.dispatchEvent(loadedEvent);
                }, 100)
                break;
            }
            case "adminOrders": {
                loadPage("adminOrders");
                setTimeout(() => {
                    loadedEvent = new Event('ordersSectionLoaded');
                    window.dispatchEvent(loadedEvent);
                }, 100)
                break;
            }
            default: {
                loadPage("404");
                break;
            }
        }
    }

    function loadPage($path) {
        if($path == "") return;
        const request = new XMLHttpRequest();
        request.open("GET", $path + ".html");
        request.send();
        request.onload = function() {
            if(request.status == 200) {
                contentContainer.innerHTML = request.responseText;
                document.title = `${$path} | ` + "GG Brand";
                sessionStorage.setItem("adminPagePath", $path);
            }
        }
    }

    function loadProductsPage () {
        path = "adminProducts";
        checkPath(path);
    }

    function loadTagsPage () {
        path = "adminTags";
        checkPath(path);
    }

    function loadOrdersPage () {
        path = "adminOrders";
        checkPath(path);
    }

    document.querySelector(".logo_section__logo").addEventListener("click", () => {
        window.location.href = 'index.html';
    })

    productsLink.addEventListener("click", () => {
        loadProductsPage();
    })

    tagsLink.addEventListener("click", () => {
        loadTagsPage();
    })

    ordersLink.addEventListener("click", () => {
        loadOrdersPage();
    })
}