let filterTags = [];
let mainPageProducts = [];

window.onload = function() {
    const contentContainer = document.querySelector(".content_container");
    const logo = document.querySelector(".top_header__logo");
    const cartButton = document.querySelector(".top_header__cart_button");
    let path = null;
    const searchIcon = document.querySelector(".second_header__input_box__icon");
    const searchInput = document.querySelector(".second_header__input");

    searchIcon.addEventListener("click", function() {
        if (searchInput.value.trim() !== "") {
            loadSearchPage();
        }
    });

    searchInput.addEventListener("keydown", function(event) {
        if (event.key === "Enter" && searchInput.value.trim() !== "") {
            loadSearchPage();
            searchBar();
        }
    });

    
    function checkSessionStorage () {
        if(!localStorage.getItem("pagePath")) {
            localStorage.setItem("pagePath", "mainpage");
            path = localStorage.getItem("pagePath"); 
            checkPath(path) 
        } else {
            path = localStorage.getItem("pagePath");
            checkPath(path)
        }
    }

    window.checkPath = function (path) {
        switch(path) {
            case "mainpage":
                loadPageWithPromise("mainpage")
                    .then(() => mainPageOnLoad())
                    .catch(err => console.error(err));
                break;
            case "product":
                loadPageWithPromise("product")
                    .then(() => {
                        mainPageOnLoad()
                        populateProductPage(JSON.parse(localStorage.getItem("endPoint")));
                    })
                    .catch(err => console.error(err));
                break;
            case "cart":
                loadPageWithPromise("cart")
                    .then(() => {
                        loadSideLinks();
                        buyPageButton();
                        setTimeout(() => {
                            checkUserCart()
                        }, 50)
                    })
                    .catch(err => console.error(err));
                break;
            case "orders":
                loadPageWithPromise("orders")
                    .then(() => {
                        loadSideLinks()
                        setTimeout(() => {
                            displayUserOrders()
                        }, 50)
                    })
                    .catch(err => console.error(err));
                break;
            case "profile":
                loadPageWithPromise("profile")
                    .then(() => {
                        loadSideLinks()
                        getUserInfo();
                    })
                    .catch(err => console.error(err));
                break;
            case "buy":
                loadPageWithPromise("buy")
                    .then(() => {
                        loadSideLinks()
                        setTimeout(() => {
                            checkCartTotal()
                        }, 50)
                    })
                    .catch(err => console.error(err));
                break;
            case "search":
                loadPageWithPromise("search")
                    .then(() => {
                        loadedEvent = new Event('searchPageLoaded');
                        slideOne()
                        slideTwo()

                        const slider1 = document.getElementById("slider-1");
                        const slider2 = document.getElementById("slider-2");

                        slider1.addEventListener("input", slideOne);
                        slider2.addEventListener("input", slideTwo);

                        document.querySelectorAll('.checkbox_section__checkbox').forEach(checkbox => {
                            checkbox.addEventListener('click', handleCheckboxClick);
                        });
                    })
                    .catch(err => console.error(err));
                break;
            default:
                loadPageWithPromise("404").catch(err => console.error(err));
                break;
        }
    }

    checkSessionStorage();

    function loadPageWithPromise($path) {
        return new Promise((resolve, reject) => {
            if(!$path) reject("Path is empty or invalid.");
    
            const request = new XMLHttpRequest();
            request.open("GET", $path + ".html");
            request.send();
    
            request.onload = function() {
                if(request.status == 200) {
                    contentContainer.innerHTML = request.responseText;
                    document.title = `${$path} | ` + "GG Brand";
                    localStorage.setItem("pagePath", $path);
                    resolve();
                } else {
                    reject("Failed to load the page.");
                }
            };
        });
    }

    window.loadProfilePage = function () {
        path = "profile";
        checkPath(path);
        if(path == "mainpage") {
            return;
        }
    }

    window.loadOrdersPage = function () {
        document.querySelector(".order_panel").style.display = "none"
        document.querySelector(".order_panel").style.pointerEvents = "none"
        document.querySelector(".content_holder").classList.remove("blur");
        document.querySelector(".footer").classList.remove("blur");
        path = "orders";
        checkPath(path);
        if(path == "mainpage") {
            return;
        }
    }

    function loadCartPage () {
        path = "cart";
        checkPath(path);
        if(path == "mainpage") {
            return;
        }
    }

    function loadBuyPage() {
        path = "buy";
        checkPath(path);
        if(path == "mainpage") {
            return;
        }
    }

    function loadSearchPage() {
        path = "search"
        checkPath(path)
        if(path == "mainpage") {
            return;
        }
    }

    window.loadMainPage = function () {
        document.querySelector(".order_panel").style.display = "none"
        document.querySelector(".order_panel").style.pointerEvents = "none"
        document.querySelector(".content_holder").classList.remove("blur");
        document.querySelector(".footer").classList.remove("blur");
        path = "mainpage"
        checkPath(path)
    }

    function loadSideLinks () {
        document.querySelector("#orders_link").addEventListener("click", () => {
            loadOrdersPage();
        })
        document.querySelector("#cart_link").addEventListener("click", () => {
            loadCartPage();
        })
        document.querySelector("#profile_link").addEventListener("click", () => {
            loadProfilePage();
        })
    }

    function buyPageButton () {
        document.querySelector(".main_content__buy_button--border").addEventListener("click", () => {
            loadBuyPage();
        })
    }

    logo.addEventListener("click", () => {
        path = logo.getAttribute("value");
        checkPath(path);
        if(path == "mainpage") {
            return;
        }
    })

    cartButton.addEventListener("click", () => {
        if (sessionStorage.getItem("user") === "null") {
            openSignInPanel()
        } else {
            loadCartPage();
        }
    })

    function mainPageOnLoad () {
        tagIDs.push(39)

        const searchDTO = {
            firstRangePoint: 0,
            endRangePoint: 10,
            name: null,
            tagIds: tagIDs
        }
    
        localStorage.setItem("bestSellers", JSON.stringify(searchDTO))
        
        const request = new XMLHttpRequest();
        request.open('POST', `${host}/api/Product/Search`);
        request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
        request.setRequestHeader('Content-Type', 'application/json');
        request.addEventListener('load', () => {
            if(request.status === 200 && request.readyState === 4) {
                const products = JSON.parse(request.responseText)
                mainPageProducts = products
                getProducts (products)
            } else {
                throw new Error("Failed to get response")
            }
        })

        request.send(localStorage.getItem("bestSellers"))
    }

    function getProducts(products) {
        const container = document.querySelector('.product_carousel__products');
        let productIds = []
        if (products && container) {
            products.forEach(product => {
                productIds.push(product.id)
            })

            GetProductMainImage(productIds, function(files){
                for (let i = 0; i < files.length; i++) {
                    CreateAndPasteMainProductsImage(files[i], document.getElementById(`product__image_container__${products[i].id}`), products[i]);
                }
            });

            products.forEach(product => {
                let productHTML = `
                    <div class="product_carousel__product product_carousel__product--${product.id}">
                        <div class="product__top_section" id="product__image_section__${product.id}" value="product">
                            <div class="product__image_container" id="product__image_container__${product.id}"></div>
                            <p class="product__price" id="product__price__${product.id}">${product.availabilitisOfProduct && product.availabilitisOfProduct.length > 0 ? product.availabilitisOfProduct[0].cost + ' ₽' : ''}</p>
                            <p class="product_name" id="product_name__${product.id}">${product.name}</p>
                        </div>
                        <div class="product__hover_section">
                            <div class="product__sizes" id="product__sizes__${product.id}">
                            `;
                if (product.availabilitisOfProduct) {
                    product.availabilitisOfProduct.forEach(size => {
                        productHTML += `<p class="product_size">${size.size}</p>`;
                    });
                }
                productHTML += `
                            </div>
                            <button class="product__cart_button" id="product__cart_button__${product.id}">
                                <img src="icons/cart.svg" alt="" class="product__cart_button__icon">
                                <p class="product__cart_button__text">В Корзину</p>
                            </button>
                        </div>
                    </div>
                `;
    
                // Create a DOM element from the productHTML string
                const productElement = document.createElement('div');
                productElement.innerHTML = productHTML.trim();
    
                // Add the event listener to the productElement
                setTimeout(() => {
                    const productImage = productElement.querySelector('.product__image');
                    productImage.addEventListener("click", function() {
                        const clickedProductData = mainPageProducts.find(p => p.id == product.id);
            
                        localStorage.setItem('endPoint', JSON.stringify(clickedProductData));
                        localStorage.setItem("pagePath", "product");
                        checkPath(localStorage.getItem("pagePath"));
                    });
                }, 3000)
                
                const cartButton = productElement.querySelector('.product__cart_button');

                // Add the event listener to the cartButton
                cartButton.addEventListener("click", function() {
                    addToCart(product.id, product.availabilitisOfProduct[0].size, product);
                });
                // Append the productElement to the container
                container.appendChild(productElement);
            });
        }
    }

    function populateProductPage(product) {
        const breadCrums = document.querySelector(".breadcrums__current_page")

        breadCrums.innerHTML = product.name
        
        let productIds = [product.id]

        GetAllProductImages(productIds, function(files){
            CreateAndPasteProductImages(files, document.querySelector(".product__images"), product.id);
        });

        const productNameElem = document.querySelector('.info_panel__product_name');
        const productPriceElem = document.querySelector('.product_price');
        const sizesContainer = document.querySelector('.available_sizes__options');
        const productDescriptionElem = document.querySelector('.drowdown_link__description');
        const addToCartButton = document.querySelector('.cta_section__cart_button--border')
        const cartButtonIcon = document.querySelector(".cart_button__image");
        const cartButtonText = document.querySelector(".cart_button__text");
        let user = JSON.parse(sessionStorage.getItem("user"))

        product.availabilitisOfProduct.sort((sizeA, sizeB) => {
            const sizeANumeric = parseInt(sizeA.size);
            const sizeBNumeric = parseInt(sizeB.size);
            return sizeANumeric - sizeBNumeric;
        });


        let chosenSize = product.availabilitisOfProduct[0].size;
        
        productNameElem.textContent = product.name;
        productPriceElem.textContent = product.availabilitisOfProduct && product.availabilitisOfProduct.length > 0 ? product.availabilitisOfProduct[0].cost : '';

        sizesContainer.innerHTML = '';
        product.availabilitisOfProduct.forEach((size, index) => {
            const sizeElem = document.createElement('p');
            sizeElem.className = 'available_size';

            if (index === 0) {
                sizeElem.classList.add('available_size--selected');
            }

            sizeElem.textContent = size.size;

            sizeElem.setAttribute('onclick', 'chooseSize(this)');
            sizeElem.setAttribute('data-size', size.size);
            sizeElem.setAttribute('data-price', size.cost);

            sizesContainer.appendChild(sizeElem);

            sizeElem.addEventListener("click", () => {
                chosenSize = sizeElem.innerHTML;
                user = JSON.parse(sessionStorage.getItem("user"))
                if(user === null) {
                    return
                } else {
                    if (user.userShoppingCartItems.some(item => item.size === size.size) && user.userShoppingCartItems.some(item => Number(item.product.id) === Number(product.id))) {
                        cartButtonIcon.classList.remove("fa-bag-shopping")
                        cartButtonIcon.classList.add("fa-check")
                        cartButtonText.innerHTML = "Добавлено"
                    } else {
                        cartButtonIcon.classList.remove("fa-check")
                        cartButtonIcon.classList.add("fa-bag-shopping")
                        cartButtonText.innerHTML = "В корзину";
                        addToCartButton.addEventListener("click", () => {
                            cartButtonIcon.classList.remove("fa-bag-shopping")
                            cartButtonIcon.classList.add("fa-check")
                            cartButtonText.innerHTML = "Добавлено"
                        })
                    }
                }
            })
        });

        productDescriptionElem.textContent = product.description || 'No description available';

        addToCartButton.addEventListener("click", () => {
            if (user === null) {
                openSignInPanel()
            } else {
                addToCart(product.id, chosenSize, product)
            }
        })

        if(user === null) {
            return
        } else {
            if (user.userShoppingCartItems.some(item => item.size === product.availabilitisOfProduct[0].size) && user.userShoppingCartItems.some(item => Number(item.product.id) === Number(product.id))) {
                cartButtonIcon.classList.remove("fa-bag-shopping")
                cartButtonIcon.classList.add("fa-check")
                cartButtonText.innerHTML = "Добавлено"
            } else {
                cartButtonIcon.classList.remove("fa-check")
                cartButtonIcon.classList.add("fa-bag-shopping")
                cartButtonText.innerHTML = "В корзину";
                addToCartButton.addEventListener("click", () => {
                    cartButtonIcon.classList.remove("fa-bag-shopping")
                    cartButtonIcon.classList.add("fa-check")
                    cartButtonText.innerHTML = "Добавлено"
                })
            }
        }
    }

    // window.getClickedProductInfo = function (id) {
    //     const request = new XMLHttpRequest();
    //     request.open('GET', `${host}/api/Product/GetExtendedProductInfo?id=${id}`);
    //     request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    //     request.addEventListener('load', () => {
    //         if (request.status === 200 && request.readyState === 4) {
    //             const product = JSON.parse(request.responseText);
    //             const mappedProduct = {
    //                 id: product.id.toString(),
    //                 name: product.name,
    //                 description: product.description,
    //                 canBeFound: product.canBeFound ? "Да" : "Нет",
    //                 tags: product.tags.map(tag => ({
    //                     id: tag.id.toString(),
    //                     name: tag.name
    //                 })),
    //                 availabilitisOfProduct: product.availabilitisOfProduct.map(avail => ({
    //                     size: avail.size,
    //                     cost: avail.cost.toString(),
    //                     count: avail.count.toString()
    //                 }))
    //             };
    //             sessionStorage.setItem("ClickedProduct", JSON.stringify(mappedProduct))
    //             sessionStorage.setItem("pagePath", "product")
    //             console.log("This shit is called for some reason")
    //             checkPath(sessionStorage.getItem("pagePath"));
    //         } else if (request.status === 401) {
    //             updateToken()
    //         }
    //         else {
    //             console.error("Failed to fetch product from server");
    //         }
    //     });

    //     request.send(id)
    // }

    function checkUserCart() {
        let user = JSON.parse(sessionStorage.getItem('user'));
        let cartItems = user.userShoppingCartItems;

        let totalCount = 0;
        cartItems.forEach(item => {
            totalCount += item.count;
        });

        if (totalCount === 0 || totalCount === undefined || totalCount === null || !totalCount) {
            const cartContent = document.querySelector(".content__right_section")
            const noCartItems = document.querySelector(".right_section__main_content--no_products")

            cartContent.style.display = "none"
            noCartItems.style.display = "flex"
        } else {
            const cartContent = document.querySelector(".content__right_section")
            const noCartItems = document.querySelector(".right_section__main_content--no_products")

            cartContent.style.display = "flex"
            noCartItems.style.display = "none"

            checkCartCount()
            checkCartTotal()
            displayCartItems();
        }
        
    }

    window.addToCart = function(id, size, product) {
        if (sessionStorage.getItem("user") === "null") {
            openSignInPanel()
        } else {
            const newCartItem = {
                productId: id,
                size: size.toString(),
                count: 1
            }
    
            const request = new XMLHttpRequest();
            
            request.open('POST', `${host}/api/User/ShoppingCart/AddItem`);
            request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
            request.setRequestHeader('Content-Type', 'application/json');
            request.addEventListener('load', () => {
                if(request.status === 200 && request.readyState === 4) {
                    const response = request.responseText;
                    addCartItemToUser(response, size, product)
                    updateCartWidget();
                    showPopUp ("info", "Новый продукт в корзине!");
                } else if (request.status === 401) {
                    showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
                    updateToken(addToCart);
                } else if (request.status === 500) {
                    showPopUp ("info", "Этот товар уже в корзине!")
                } else {
                    throw new Error("Failed to get response");
                }
            });
            
            request.send(JSON.stringify(newCartItem));
        }
    }

    function addCartItemToUser(id, size, product) {
        let user = JSON.parse(sessionStorage.getItem('user'));
        console.log(product);
    
        let cost;
        for (let i = 0; i < product.availabilitisOfProduct.length; i++) {
            if (product.availabilitisOfProduct[i].size === size) {
                cost = Number(product.availabilitisOfProduct[i].cost);
                break;
            }
        }
    
        const newCartItem = {
            id: id,
            size: size,
            count: 1,
            cost: cost,
            product: {
                id: product.id,
                name: product.name
            }   
        };
        user.userShoppingCartItems.push(newCartItem);
        
        sessionStorage.setItem('user', JSON.stringify(user));

        updateCartWidget()
    }

    window.displayCartItems = function() {
        let user = JSON.parse(sessionStorage.getItem('user'));
        const cartItemsContainer = document.querySelector(".main_content__products");

        user.userShoppingCartItems.forEach(cartItem => {
            let productIds = [cartItem.product.id]

            GetProductMainImage(productIds, function(files){
                CreateAndPasteCartImages(files, document.getElementById(`product__image_container__${cartItem.id}`), cartItem.product.id);
            });

            let cartItemHTML = `
                <div class="products__product" id="products__product__${cartItem.id}">
                    <div class="product__left_section">
                        <div class="product__image_container" id="product__image_container__${cartItem.id}"></div>
                        <div class="product__info_section">
                            <div class="product__text_section">
                                <p class="product__name" id="product__name__${cartItem.id}">${cartItem.product.name}</p>
                                <p class="product__size" id="product__size__${cartItem.id}">${cartItem.size}</p>
                            </div>
                            <div class="product__amount_box">
                                <i class="fa-solid fa-minus amount_box__minus" id="amount_box__minus__${cartItem.id}" onclick="changeCartItemCount(${cartItem.id}, -1)"></i>
                                <p class="amount_box__count" id="amount_box__count__${cartItem.id}">${cartItem.count}</p>
                                <i class="fa-solid fa-plus amount_box__plus" id="amount_box__plus__${cartItem.id}"></i>
                            </div>
                        </div>
                    </div>
                    <div class="product__right_section">
                        <div class="product__price_box">
                            <span class="product__price" id="product__price__${cartItem.id}">${cartItem.cost}</span>
                            <p class="product__price">₽</p>
                        </div>
                        <i class="product__delete_icon fa-solid fa-trash-can" id="product__delete_icon__${cartItem.id}" onclick="deleteCartItem(${cartItem.id})"></i>
                    </div>
                </div>
            `;

            let template = document.createElement('template');
            template.innerHTML = cartItemHTML;
            cartItemsContainer.appendChild(template.content.cloneNode(true));

            document.querySelector(`#amount_box__minus__${cartItem.id}`).addEventListener("click", () => {
                changeCartItemCount(cartItem.id, -1)
            })

            document.querySelector(`#amount_box__plus__${cartItem.id}`).addEventListener("click", () => {
                changeCartItemCount(cartItem.id, +1)
            })

            checkCartTotal()
        });
    }

    window.changeCartItemCount = function (cartItemID, value) {
        const cartItemCount = document.querySelector(`#amount_box__count__${cartItemID}`);
        const cartItemPrice = document.querySelector(`#product__price__${cartItemID}`);
        let user = JSON.parse(sessionStorage.getItem('user'));
        let cartItems = user.userShoppingCartItems;
    
        for (let i = 0; i < cartItems.length; i++) {
            if (cartItems[i].id === cartItemID) {
                const singleItemCost = Number(cartItems[i].cost) / Number(cartItems[i].count);
                cartItems[i].cost += Number(singleItemCost * value);
                cartItemPrice.innerHTML = cartItems[i].cost;
                cartItems[i].count += value;
                cartItemCount.innerHTML = cartItems[i].count;
                if (cartItems[i].count < 1) {
                    cartItems[i].count = 1;
                    cartItemCount.innerHTML = 1;
                    cartItems[i].cost = singleItemCost
                    cartItemPrice.innerHTML = cartItems[i].cost
                }
                updateCartItemCount(cartItemID, cartItems[i].count)
                break;
            }
        }

        sessionStorage.setItem('user', JSON.stringify(user));

        checkCartTotal()
    }

    window.checkCartTotal = function () {
        let user = JSON.parse(sessionStorage.getItem('user'));
        let cartItems = user.userShoppingCartItems;

        let totalCount = 0;
        let totalCost = 0;
        cartItems.forEach(item => {
            totalCount += item.count;
            totalCost += Number(item.cost);
        });

        const productsCountElement = document.querySelector(".buy_panel__products_count");
        const productsTextElement = document.querySelector(".buy_panel__products_text");
        const productPriceElement = document.querySelector(".buy_panel__product_price");
        const productsTotal = document.querySelector(".total_sum__price");

        productsCountElement.innerHTML = totalCount;
        productPriceElement.innerHTML = productsTotal.innerHTML = totalCost;

        if (totalCount === 1) {
            productsTextElement.innerHTML = "товар на сумму";

        } else if (totalCount >= 2 && totalCount <= 4) {
            productsTextElement.innerHTML = "товара на сумму";
        } else {
            productsTextElement.innerHTML = "товаров на сумму";
        }
    }

    window.checkCartCount = function () {
        let user = JSON.parse(sessionStorage.getItem('user'));
        let cartItems = user.userShoppingCartItems;

        const productsCountText = document.querySelector(".right_section__products")
        const productsCountLabel = document.querySelector(".cart_items_count")

        let totalCount = 0;
        cartItems.forEach(item => {
            totalCount += item.count;
        });

        productsCountLabel.innerHTML = totalCount;

        if (totalCount === 1) {
            productsCountText.innerHTML = "товар"

        } else if (totalCount >= 2 && totalCount <= 4) {
            productsCountText.innerHTML = "товара"
        } else {
            productsCountText.innerHTML = "товаров"
        }
    }

    window.updateCartItemCount = function (id, count) {

        const updatedCartItem = {
            id: id,
            count: count
        }

        const request = new XMLHttpRequest();

        request.open('PUT', `${host}/api/User/ShoppingCart/UpdateItemCount`);
        request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
        request.setRequestHeader('Content-Type', 'application/json')
        request.addEventListener('load', () => {
            if(request.status === 200 && request.readyState === 4) {
                updateCartWidget()
                checkCartCount()
            } else if (request.status === 401) {
                showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
                updateToken(updateCartItemCount)
            } else {
                throw new Error("Failed to get response")
            }
        })
        
        request.send(JSON.stringify(updatedCartItem))
    }

    window.deleteCartItem = function (id) {
        const request = new XMLHttpRequest();

        request.open('DELETE', `${host}/api/User/ShoppingCart/Remove?itemId=${id}`);
        request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
        request.setRequestHeader('Content-Type', 'application/json')
        request.addEventListener('load', () => {
            if(request.status === 200 && request.readyState === 4) {

                let user = JSON.parse(sessionStorage.getItem('user'));
                let itemIndex = user.userShoppingCartItems.findIndex(item => item.id === id);
                user.userShoppingCartItems.splice(itemIndex, 1);

                sessionStorage.setItem('user', JSON.stringify(user));
                checkCartTotal()
                updateCartWidget()
                checkCartCount()
                checkUserCart()
                if (document.querySelector(`#products__product__${id}`)) {
                    document.querySelector(`#products__product__${id}`).remove()
                } else {
                    return
                }
                
            } else if (request.status === 401) {
                showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
                updateToken(deleteCartItem)
            } else {
                throw new Error("Failed to get response")
            }
        })
        
        request.send()
    }

    window.updateCartWidget = function () {
        if(JSON.parse(sessionStorage.getItem('user')) != null) {
            const widgetText = document.querySelector(".cart_button__widget_text")
            let user = JSON.parse(sessionStorage.getItem('user'));
            let cartItems = user.userShoppingCartItems;

            let totalCount = 0;
            cartItems.forEach(item => {
                totalCount += item.count;
            });

            widgetText.innerHTML = totalCount
        } else {
            const widgetText = document.querySelector(".cart_button__widget_text")
            widgetText.innerHTML = 0
            return
        }
    }

    window.getUserInfo = function () {
        let user = JSON.parse(sessionStorage.getItem('user'))

                const userInfoHolder = document.querySelector(".main_content__inputs")

                userInfoHolder.innerHTML = 
                `
                    <input type="text" class="main_content__username" value="${user.name}">
                    <div class="main_content__input">
                        <p class="main_content__input_label">Phone</p>
                        <div class="main_content__input_hidden_content main_content__password_input">
                            <span class="main_content__phone main_content__input_content">${user.phoneNumber}</span>
                        </div>
                    </div>
                `
    }

    window.getNameInput = function () {
        const userInput = document.querySelector(".main_content__username").value;

        updateUser(userInput)
    }

    window.updateUser = function (name) {
        const userUpdateDTO  = {
            name: name
        }

        const request = new XMLHttpRequest();

        request.open('PATCH', `${host}/api/User/Update`);
        request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
        request.setRequestHeader('Content-Type', 'application/json');
        request.addEventListener('load', () => {
            if(request.status === 200 && request.readyState === 4) {
                let user = JSON.parse(sessionStorage.getItem("user"))
                let updatedUser = {
                    name: name,
                    orders: user.orders,
                    phoneNumber: user.phoneNumber,
                    userShoppingCartItems: user.userShoppingCartItems
                }
                sessionStorage.setItem("user", JSON.stringify(updatedUser))
                document.querySelector(".profile_widget__username").innerHTML = name
            } else if (request.status === 401) {
                showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
                updateToken(updateToken)
                checkAuthorization()
            } else {
                throw new Error("Failed to get response")
            }
        })
        
        request.send(JSON.stringify(userUpdateDTO))
    }

    updateCartWidget()
}