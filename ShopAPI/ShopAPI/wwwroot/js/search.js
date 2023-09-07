let tagIDs = [];
let productsByTags = [];

let minGap = 0;

function slideOne() {
    let displayValOne = document.getElementById("range1");
    let sliderOne = document.getElementById("slider-1");
    let displayValTwo = document.getElementById("range2");
    let sliderTwo = document.getElementById("slider-2");
    if(parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap){
        sliderOne.value = parseInt(sliderTwo.value) - minGap;
    }
    displayValOne.textContent = sliderOne.value;
    fillColor();
}

function slideTwo(){
    let displayValOne = document.getElementById("range1");
    let sliderOne = document.getElementById("slider-1");
    let displayValTwo = document.getElementById("range2");
    let sliderTwo = document.getElementById("slider-2");
    if(parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap){
        sliderTwo.value = parseInt(sliderOne.value) + minGap;
    }
    displayValTwo.textContent = sliderTwo.value;
    fillColor();
}
function fillColor(){
    let sliderOne = document.getElementById("slider-1");
    let sliderTwo = document.getElementById("slider-2");
    let sliderTrack = document.querySelector(".slider-track");
    let sliderMaxValue = document.getElementById("slider-1").max;
    percent1 = (sliderOne.value / sliderMaxValue) * 100;
    percent2 = (sliderTwo.value / sliderMaxValue) * 100;
    sliderTrack.style.background = `linear-gradient(to right, var(--a-900) ${percent1}% , var(--a-700) ${percent1}% , var(--a-700) ${percent2}%, var(--a-900) ${percent2}%)`;
}

window.searchBar = function () {
    const searchName = document.querySelector(".second_header__input").value;

    if (searchName === "null" || searchName === '') {
        searchName === null
    }

    let searchDTO = {
        firstRangePoint: 0,
        endRangePoint: 20,
        name: searchName
    }

    localStorage.setItem("pagePath", "search")
    checkPath(localStorage.getItem("pagePath"))
    localStorage.setItem("endPoint", JSON.stringify(searchDTO))
    search()
}

function handleCheckboxClick(event) {
    const checkboxes = document.querySelectorAll('.checkbox_section__checkbox');

    checkboxes.forEach(checkbox => {
        const tagID = checkbox.getAttribute('tag-id');
        
        if (checkbox.checked) {
            if (tagID && !tagIDs.includes(tagID)) {
                tagIDs.push(tagID);
            }
        } else {
            const index = tagIDs.indexOf(tagID);
            if (index !== -1) {
                tagIDs.splice(index, 1);
            }
        }
    });

    let searchDTO = {
        firstRangePoint: 0,
        endRangePoint: 20,
        tagIds: tagIDs
    }

    localStorage.setItem("endPoint", JSON.stringify(searchDTO))
}

window.searchByTags = function () {
    const request = new XMLHttpRequest();

    request.open('GET', `${host}/api/Product/Tag/GetAll`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json')
    request.addEventListener('load', () => {
        if (request.status === 200 && request.readyState === 4) {
            const tags = JSON.parse(request.responseText);

            const endPointTags = JSON.parse(localStorage.getItem("endPoint"))

            if(endPointTags.tagIds === undefined || endPointTags.tagIds === null) {
                console.log("endPointTags were undefined: ", endPointTags.tagIds)
                defineSearch(tagIDs, tags);
            } else {
                console.log("endPointTags were defined: ", endPointTags.tagIds)
                defineSearch(endPointTags.tagIds, tags);
            }
            
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken();
        } else {
            throw new Error("Failed to get response");
        }
    });

    request.send()
}

window.search = function(tagNames) {
    const request = new XMLHttpRequest();
    request.open('POST', `${host}/api/Product/Search`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const products = JSON.parse(request.responseText)

            let productsByPrice = []
            let checkboxes = [];
            let checkboxesChecked = []

            let checkboxElements = document.querySelectorAll('.checkbox_section__checkbox');
            checkboxElements = Array.from(checkboxElements);

            let displayValOne = Number(document.getElementById("range1").textContent);
            let displayValTwo = Number(document.getElementById("range2").textContent);

            checkboxElements.forEach(function(element) {
                var tagId = element.getAttribute('tag-id');
                checkboxes.push(tagId);
            });

            products.forEach(product => {
                const productCost = product.availabilitisOfProduct[0].cost;

                if (displayValOne < productCost && productCost < displayValTwo) {
                    productsByPrice.push(product);
                }
            });

            if (tagNames === null || tagNames === undefined) {
                displayProductsByTags(productsByPrice);
                tagIDs = []
            } else {
                tagNames.forEach(function(tagName) {
                    const matchFound = checkboxElements.some(function(checkboxElement) {
                        if (checkboxElement.getAttribute('tag-id') === tagName) {
                            checkboxElement.checked = true;
                            return true;
                        }
                        return false;
                    });
                
                    if (matchFound) {
                        return;
                    } else if (checkboxesChecked.includes(tagName)) {
                        return
                    } else {
                        checkboxesChecked.push(tagName);
                    } 
                })

                if (checkboxesChecked.length > 0) {
                    const filterPanel = document.querySelector(".filter_panel__checkboxes")

                    if(!document.querySelector(".filter_panel__tags_section")) {
                        let tagsHTML = 
                        `
                        <div class="filter_panel__tags_section">
                            <h3 class="checkbox_section__header">Доп. Теги</h3>
                            <div class="tags_section__tags">
                            </div>
                        </div>
                        `

                        const tempContainer = document.createElement('div');
                        tempContainer.innerHTML = tagsHTML.trim();

                        filterPanel.appendChild(tempContainer.firstChild);

                        checkboxesChecked.forEach(function (checkboxChecked) {
                            const tag = document.createElement('div')
                            tag.classList.add("tags_section__tag")
                            tag.setAttribute("tag-id", checkboxChecked.toLowerCase())

                            const tagContent = document.createElement('div')
                            tagContent.classList.add("tag_content")
                            tag.appendChild(tagContent)
                            tagContent.innerHTML = checkboxChecked


                            const tags = document.querySelector(".tags_section__tags")
                            tags.appendChild(tag)

                            const deleteWidget = document.createElement('div');
                            deleteWidget.classList.add('tags__tag_delete_widget');
                            deleteWidget.setAttribute('tag-delete-id', `${checkboxChecked.toLowerCase()}`);
                            
                            const deleteIcon = document.createElement('i');
                            deleteIcon.classList.add('tags__tag_delete_icon', 'fa-solid', 'fa-xmark');
                            deleteWidget.appendChild(deleteIcon);

                            tag.addEventListener('mouseenter', handleTagMouseEnter);
                            tag.addEventListener('mouseleave', handleTagMouseLeave);
                            deleteWidget.addEventListener('click', handleDeleteTagClick);

                            tag.appendChild(deleteWidget);
                        })
                    } else {
                        return
                    }
                }

                displayProductsByTags(productsByPrice);
                tagIDs = []
            }
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken()
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(localStorage.getItem("endPoint"))
}

function handleTagMouseEnter(event) {
    const tagID = event.currentTarget.getAttribute('tag-id');
    const deleteWidget = document.querySelector(`[tag-delete-id="${tagID}"]`);
    deleteWidget.style.display = 'flex';
}

function handleTagMouseLeave(event) {
    const tagID = event.currentTarget.getAttribute('tag-id');
    const deleteWidget = document.querySelector(`[tag-delete-id="${tagID}"]`);
    deleteWidget.style.display = 'none';
}

function handleDeleteTagClick(event) {
    const tagID = event.currentTarget.getAttribute('tag-delete-id');
    const tagElem = document.querySelector(`[tag-id="${tagID}"]`);
    tagElem.remove();
    search()

    const searchDTO = {
        firstRangePoint: 0,
        endRangePoint: 20,
        name: null,
    };

    localStorage.setItem("endPoint", JSON.stringify(searchDTO))
}


function displayProductsByTags(products) {
    let productsContainer = document.querySelector('.products');
    productsContainer.innerHTML = '';

    if (products.length === 0) {
        let productHTML = `
            <div class="products__none">
                <img src="css/images/No Products.png" alt="" class="none__image">
                <p class="none__header">Мы ничего не нашли по вашему запросу</p>
                <p class="none__text">Попробуйте изменить ваш поисковой запрос</p>
            </div>
        `;
        productsContainer.innerHTML = productHTML;
        return;
    }

    products.forEach(product => {
        let productIds = [];
        productIds.push(product.id)

        GetProductMainImage(productIds, function(files){
            CreateAndPasteSearchImages(files, document.getElementById(`product__image_section__${product.id}`), product.id);
        });

        let firstAvailability = product.availabilitisOfProduct[0];
        let chosenSize = product.availabilitisOfProduct[0].size;

        let productHTML = `
            <div class="products__product" id="product__${product.id}">
                <div class="product__image_section" id="product__image_section__${product.id}">
                    <div class="product__cart_button">
                        <img src="icons/bag_solid.svg" alt="" class="product__cart_button_icon">
                    </div>
                </div>
                <div class="product__text_section">
                    <p class="product__price" id="product__price__${product.id}">${firstAvailability ? firstAvailability.cost + ' ₽' : 'Price not available'}</p>
                    <p class="product__name">${product.name}</p>
                </div>
                <div class="product__sizes" id="product__sizes__${product.id}">
        `;

        product.availabilitisOfProduct.forEach((size, index) => {
            productHTML += `<div class="product__size" data-cost="${size.cost}" id="product__size__${product.id}__${index}">${size.size}</div>`;
        });

        productHTML += `</div></div>`;

        const productElement = document.createElement('div');
        productElement.innerHTML = productHTML.trim();


        const cartButton = productElement.querySelector('.product__cart_button');

        // Add the event listener to the cartButton
        cartButton.addEventListener("click", function() {
            addToCart(product.id, product.availabilitisOfProduct[0].size, product);
        });

        productsContainer.appendChild(productElement);

        product.availabilitisOfProduct.forEach((size, index) => {
            document.getElementById(`product__size__${product.id}__${index}`).addEventListener('click', function() {
                chosenSize = size.size
                document.getElementById(`product__price__${product.id}`).innerText = this.getAttribute('data-cost') + ' ₽';
            });
        });
    });

    setTimeout(() => {
        products.forEach(product => {
            const productElement = document.getElementById(`product__image__${product.id}`);
            productElement.addEventListener("click", function() {
                const clickedProductData = products.find(p => p.id === product.id);

                localStorage.setItem('endPoint', JSON.stringify(clickedProductData));
                console.log(JSON.parse(localStorage.getItem('endPoint')))

                localStorage.setItem("pagePath", "product")
                checkPath(localStorage.getItem("pagePath"));
            });
        });
    }, 3000);
}

if (localStorage.getItem("pagePath") === "search") {
    setTimeout(() => {
        searchBar()
    }, 500)
}