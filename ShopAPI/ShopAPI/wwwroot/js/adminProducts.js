let host = 'http://192.168.0.15';
let deletedTagIds = [];

function changeProductSearchButton () {
    const searchButton = document.querySelector(".search_section__button--products");
    const searchPanel = document.querySelector(".search_section__search_panel--products")

    if (searchButton.classList.contains("search_section__button--active")) {
        searchButton.classList.remove("search_section__button--active");
        searchPanel.style.display = "none";
    } else {
        searchButton.classList.add("search_section__button--active");
        searchPanel.style.display = "flex";
    }
}


function updateToken(callback) {
    const request = new XMLHttpRequest();

    request.open('POST', `${host}/api/Identity/UpdateToken`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = request.responseText
            let token = localStorage.setItem('token', response)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send()
    callback()
}

function createProduct () {
    const request = new XMLHttpRequest();

    request.open('POST', `${host}/api/Product/Controll/Create`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = JSON.parse(request.responseText)
            addProductToTable(response)
            getAllProducts()
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(createProduct)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send()
}

function addProductToTable (id) {
    const request = new XMLHttpRequest();
    request.open('GET', `${host}/api/Product/GetExtendedProductInfo?id=${id}`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    request.addEventListener('load', () => {
        if (request.status === 200 && request.readyState === 4) {
            const product = JSON.parse(request.responseText);
            showPopUp("success", "Вы добавили новый продукт!")
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(addProductToTable)
        }
        else {
            console.error("Failed to fetch product from server");
        }
    });

    request.send(id)
}

function getAllProducts () {
    const productSearchDTO = {
        
    }

    const request = new XMLHttpRequest();
    request.open('POST', `${host}/api/Product/Search/WithoutAccounting`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = JSON.parse(request.responseText)
            displayAllProducts(response)
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(getAllProducts)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(productSearchDTO))
}

function searchProducts () {
    const nameInput = document.querySelector("#search_input__product_name")
    const tagIdsInput = document.querySelector("#search_input__product_tagsIds")

    const productSearchDTO = {
        name: nameInput.value ? nameInput.value : null,
        tagIds: tagIdsInput.value ? tagIdsInput.value : null,
    }

    const request = new XMLHttpRequest();
    request.open('POST', `${host}/api/Product/Search/WithoutAccounting`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = JSON.parse(request.responseText)
            displayAllProducts(response)
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(searchProducts)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(productSearchDTO))
}

function displayAllProducts (products) {
    const tableBody = document.querySelector(".table__body_data--products");
    let dataHTML = '';
    for (let product of products) {
        dataHTML += `<tr class="table__body_row" onclick="openProductsPanel(${product.id})">
                        <td class="table__cell table__cell_id" id="cell_id__${product.id}">${product.id}</td>
                        <td class="table__cell table__cell_name" id="cell_name__${product.id}">${product.name}</td>
                    </tr>`;
        const productPanel = createProductPanel(product.id, product.name, product.description, product.canBeFound, product.tags, product.availabilitisOfProduct);
        document.querySelector('.content_container').appendChild(productPanel);
    }
    tableBody.innerHTML = dataHTML;
    sortTable()
}

function sortTable() {
    console.log("WOrking")
    var tbody = document.querySelector('.table__body_data--products');

    var rows = [].slice.call(tbody.querySelectorAll('tr'));

    rows.sort(function(a, b) {
        var idA = parseInt(a.querySelector('.table__cell_id').textContent);
        var idB = parseInt(b.querySelector('.table__cell_id').textContent);
        return idA - idB;
    });

    rows.forEach(function(row) {
        tbody.appendChild(row);
    });
}

function createProductPanel(newProductID, productName, productDescription, productCanBeFound, productTags, productAccounting) {
    let canBeFoundButtonText;
    let canBeFoundButtonColor;
    if (productCanBeFound === "Да") {
        canBeFoundButtonText = "Да";
        canBeFoundButtonColor = "#468c3f";
    } else {
        canBeFoundButtonText = "Нет";
        canBeFoundButtonColor = "var(--a-700)";
    }


    const tagsHTML = productTags ? productTags.map(tag => `<div class="tags__tag" tag-id="${tag.id}">${tag.name}</div>`).join('') : '';

    const accountingHTML = productAccounting ? productAccounting.map(accounting => `
        <tr class="accounting__table__body_row">
            <td class="accounting__table__cell">${accounting.id}</td>
            <td class="accounting__table__cell">${accounting.size}</td>
            <td class="accounting__table__cell" id="accounting__table__cost_cell__${accounting.id}">${accounting.cost}</td>
            <td class="accounting__table__cell" id="accounting__table__count_cell__${accounting.id}">${accounting.id}</td>
            <td><i class="accounting__edit_row fa-solid fa-pen" id="accounting__edit_row__${accounting.id}" onclick="editAccounting(${accounting.id})"></i></td>
        </tr>
    `).join('') : '';

    const newProductPanel = document.createElement('div');
    newProductPanel.setAttribute('id', `product_panel__${newProductID}`);
    newProductPanel.classList.add('products__product_panel');
    newProductPanel.style.display = 'none';
    newProductPanel.innerHTML = `
        <div class="product_panel__header_section">
            <h3 class="product_panel__header">Продукт:</h3>
            <span class="product_panel__id" id="product_panel__id__${newProductID}">${newProductID}</span>
        </div>
        <div class="product_panel__images">
            <p class="images__label">Фотографии</p>
            <div class="images__action_section">
                <div class="product_panel__add_images">
                    <input type="file" class="add_images__files" id="add_images__files__${newProductID}">
                    <div class="add_images__files_content">
                        <i class="add_images__files_icon fa-solid fa-images"></i>
                        <div class="add_images__files_text">Загрузить Фото</div>
                    </div>
                    <input type="text" class="add_images__files__number_input" id="add_images__files__number_input__${newProductID}" placeholder="Введите номер...">
                    <div class="add_images__add_images_button" id="add_images__add_images_button__${newProductID}" onclick="addProductImages(${newProductID})">
                        <i class="add_images__add_images_icon fa-solid fa-plus"></i>
                    </div>
                </div>
                <div class="product_panel__added_images">
                    <div class="added__images_images" id="added__images_images__${newProductID}">
                    </div>
                </div>
            </div>
        </div>
        <div class="product_panel__inputs_row">
            <div class="product_panel__input">
                <div class="input__label">Название</div>
                <input type="text" class="input__input_box input__name_input_box" id="input__name_input_box__${newProductID}" placeholder="Введите Название..." data-product-id="${newProductID}" value="${productName}" readonly>
                <i class="fa-solid fa-pen input__edit_button input__name_edit_button" id="input__name_edit_button__${newProductID}" onclick="editNameInput(${newProductID})"></i>
            </div>
            <div class="product_panel__input">
                <div class="input__label">Описание</div>
                <input type="text" class="input__input_box input__description_input_box" id="input__description_input_box__${newProductID}" placeholder="Введите Название..." value="${productDescription}" readonly>
                <i class="fa-solid fa-pen input__edit_button input__description_edit_button" id="input__description_edit_button__${newProductID}" onclick="editDescriptionInput(${newProductID})"></i>
            </div>
            <div class="product_panel__button">
                <div class="input__label">canBeFound</div>
                <div class="input__button" id="input__button__${newProductID}" onclick="canBeFoundClick(${newProductID})" style="background: ${canBeFoundButtonColor};">${canBeFoundButtonText}</div>
            </div>
        </div>
        <div class="product_panel__tags">
            <p class="tags__label">Теги</p>
            <div class="tags__section">
                <div class="tags__left_section">
                    <div class="tags__add_tag">
                        <input type="text" class="tags__input_box" id="tags__input_box__${newProductID}" placeholder="Новый тег...">
                    </div>
                    <div class="tags__tags_section">
                        <div class="tags__tags_box" id="tags__tags_box__${newProductID}">
                            ${tagsHTML}
                        </div>
                    </div>
                </div>
                <div class="tags__right_section">
                    <div class="tags__suggestions_panel" id="tags__suggestions_panel__${newProductID}">
                        <div class="suggestions_panel__top_section">
                            <p class="suggestions_panel__label">Теги</p>
                            <i class="fa-solid fa-close suggestions_panel__close_button" onclick="closeTagSuggestions(${newProductID})"></i>
                        </div>
                        <div class="suggestions_panel__suggestions">
                            <div class="suggestions__suggestion">48 M</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="product_panel__save_button--border" id="product_panel__save_button--border__${newProductID}" onclick="saveUpdatedProductInfo(${newProductID})">
            <div class="product_panel__save_button">
                Сохранить
            </div>
        </div>
        <div class="product_panel__accounting">
            <h2 class="accounting__label">Учёт</h2>
            <div class="accounting__add_box">
                <div class="accounting__inputs">
                    <input class="accounting__input" id="accounting__size_input__${newProductID}" placeholder="Размер...">
                    <input class="accounting__input" id="accounting__cost_input__${newProductID}" placeholder="Цена...">
                    <input class="accounting__input" id="accounting__count_input__${newProductID}" placeholder="Количество...">
                </div>
                <i class="accounting__add_icon fa-solid fa-plus" onclick="createAccounting(${newProductID})"></i>
            </div>
            <table class="accounting__table" id="accounting__table__${newProductID}">
                <thead class="accounting__table__head">
                    <tr class="accounting__table__headers">
                        <th class="accounting__table__header">ID</th>
                        <th class="accounting__table__header">Размер</th>
                        <th class="accounting__table__header">Цена</th>
                        <th class="accounting__table__header">Доступно</th>
                    </tr>
                </thead>
                <tbody class="accounting__table__body_data" id="accounting__table__body_data__${newProductID}">
                    ${accountingHTML}
                </tbody>
            </table>
        </div>
    `;
    // <div class="add_images__add_button" id="add_images__add_button__${newProductID}">
    //     <i class="add_images__add_icon fa-solid fa-plus"></i>
    // </div>
    return newProductPanel;
}

function openProductsPanel(id) {
    const allPanels = document.querySelectorAll('.products__product_panel');
    const specificPanel = document.getElementById(`product_panel__${id}`);
    if (specificPanel.style.display === 'flex') {
        specificPanel.style.display = 'none';
        return;
    }

    allPanels.forEach(panel => {
        panel.style.display = 'none';
    });

    specificPanel.style.display = 'flex';

    const request = new XMLHttpRequest();

    request.open('GET', `${host}/api/Product/GetAllProductInfo?id=${id}`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    request.addEventListener('load', () => {
        if (request.status === 200) {
            const product = JSON.parse(request.responseText);
            console.log(product)
            let name = document.querySelector(`#input__name_input_box__${id}`).value
            name = product.name
            let description = document.querySelector(`#input__description_input_box__${id}`).value
            description = product.description
            const canBeFound = document.querySelector(`#input__button__${id}`)
            if(product.canBeFound === false) {
                canBeFound.innerHTML = "Нет"
                canBeFound.style.background = "var(--a-700)"
            } else {
                canBeFound.innerHTML = "Да"
                canBeFound.style.background = "#468c3f"
            }

            const container = document.getElementById(`added__images_images__${id}`);
    
            // Check if there are any elements with class "added__images__image_holder" and attribute "imageNumber" from 1 to 4
            const elements = container.querySelectorAll('.added__images__image_holder[imageNumber="1"], .added__images__image_holder[imageNumber="2"], .added__images__image_holder[imageNumber="3"], .added__images__image_holder[imageNumber="4"]');
            
            // Check if there are no elements with attribute "imageNumber" from 1 to 4
            if (elements.length === 0) {
                // Call the function if no such elements are found
                GetAllProductImages(`${id}`, function(files){
                    CreateAndPasteImages(files, container, id);
                });
            }

            let tagsHTML = product.tags ? product.tags.map(tag => `
                <div class="tags__tag" tag-id="${tag.id}">
                    ${tag.name}
                    <div class="tags__tag_delete_widget" tag-delete-id="${tag.id}">
                        <i class="tags__tag_delete_icon fa-solid fa-xmark"></i>
                    </div>
                </div>
            `).join('') : '';

            document.querySelector(`#tags__tags_box__${id}`).innerHTML = tagsHTML;

            // Attach event listeners for tag hover and delete widget clicks
            const tags = document.querySelectorAll(`[tag-id]`);
            tags.forEach(tag => {
                const deleteWidget = tag.querySelector('.tags__tag_delete_widget');

                tag.addEventListener('mouseenter', () => {
                    deleteWidget.style.display = 'flex';
                });

                tag.addEventListener('mouseleave', () => {
                    deleteWidget.style.display = 'none';
                });

                deleteWidget.addEventListener('click', handleDeleteTagClick);
            });

            const accountingHTML = product.availabilitisOfProduct ? product.availabilitisOfProduct.map(accounting => `
                    <tr class="accounting__table__body_row" id="accounting__table__body_row__${accounting.id}">
                        <td class="accounting__table__cell">${accounting.id}</td>
                        <td class="accounting__table__cell">${accounting.size}</td>
                        <td class="accounting__table__cell" id="accounting__table__cost_cell__${accounting.id}">${accounting.cost}</td>
                        <td class="accounting__table__cell" id="accounting__table__count_cell__${accounting.id}">${accounting.count}</td>
                        <td><i class="accounting__edit_row fa-solid fa-pen" id="accounting__edit_row__${accounting.id}" onclick="editAccounting(${accounting.id})"></i></td>
                    </tr>
                `).join('')
                : '';

            const tbody = document.getElementById(`accounting__table__body_data__${product.id}`);
            tbody.innerHTML = accountingHTML;    
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(openProductsPanel)
        }
        else {
            console.error("Failed to fetch product from server");
        }
    });

    request.send(id)
}




function showPopUp (event, textMessage) {
    const toast = document.querySelector(".notification_pop_up");
    (closeIcon = document.querySelector(".notification_pop_up__close")),
    (progress = document.querySelector(".notification_pop_up__progress")),
    (icon = document.querySelector(".notification_pop_up__icon")),
    (text = document.querySelector(".notification_pop_up__text"));

    text.innerHTML = textMessage;

    if (event === "success") {
        toast.style.background = "#4b9f5d";
        icon.style.background = "#2c8b40";
        icon.classList.remove("fa-exclamation");
        icon.classList.add("fa-check");
    } else if (event === "warning") {
        toast.style.background = "#ff9900";
        icon.style.background = "#d15706";
        icon.style.padding = "0.25rem 0.6rem"
        text.style.color = "var(--p-900)"
        icon.style.color = "var(--p-900)"
        progress.style.background = "#d15706"
        closeIcon.style.color = "#d15706"
        icon.classList.remove("fa-check")
        icon.classList.add("fa-exclamation");
    } else if (event === "error") {
        toast.style.background = "#cc3d3d";
        icon.style.background = "#b80d0d";
        icon.style.padding = "0.25rem 0.6rem"
        icon.classList.remove("fa-check")
        icon.classList.add("fa-exclamation");
    } else if (event === "info") {
        toast.style.background = "#4480eb"
        icon.style.background = "#0756e0";
        icon.style.padding = "0.25rem 0.6rem"
        icon.classList.remove("fa-check")
        icon.classList.remove("fa-exclamation")
        icon.classList.add("fa-info");
    }

    let timer1, timer2;

    toast.classList.add("notification_pop_up--active");
    progress.classList.add("notification_pop_up__progress--active");

    timer1 = setTimeout(() => {
        toast.classList.remove("notification_pop_up--active");
    }, 3000);

    timer2 = setTimeout(() => {
        progress.classList.remove("notification_pop_up__progress--active");
    }, 3300);

    closeIcon.addEventListener("click", () => {
        toast.classList.remove("notification_pop_up--active");

        setTimeout(() => {
            progress.classList.remove("notification_pop_up__progress--active");
        }, 300);

        clearTimeout(timer1);
        clearTimeout(timer2);
    });

}





function editNameInput(id) {
    const editButton = document.querySelector(`#input__name_edit_button__${id}`)
    const nameInput = document.querySelector(`#input__name_input_box__${id}`)

    if(editButton.classList.contains("fa-pen")) {
        editButton.classList.replace("fa-pen", "fa-check")
        nameInput.readOnly = false;
    } else {
        editButton.classList.replace("fa-check", "fa-pen")
        nameInput.readOnly = true;
    }
}

function editDescriptionInput(id) {
    const editButton = document.querySelector(`#input__description_edit_button__${id}`)
    const descriptionInput = document.querySelector(`#input__description_input_box__${id}`)

    if(editButton.classList.contains("fa-pen")) {
        editButton.classList.replace("fa-pen", "fa-check")
        descriptionInput.readOnly = false;
    } else {
        editButton.classList.replace("fa-check", "fa-pen")
        descriptionInput.readOnly = true;
    }
}











function canBeFoundClick(id) {
    const canBeFoundButton = document.querySelector(`#input__button__${id}`)

    if(canBeFoundButton.innerHTML === "Да") {
        canBeFoundButton.innerHTML = "Нет"
        canBeFoundButton.style.background = "var(--a-700)"
    } else {
        canBeFoundButton.innerHTML = "Да"
        canBeFoundButton.style.background = "#468c3f"
    }
}

function findClosest(el, selector) {
    while (el && el !== document) {
        if (el.matches(selector)) {
            return el;
        }
        el = el.parentElement;
    }
    return null;
}

function showTagSuggestions(id) {
    const input = document.querySelector(`#tags__input_box__${id}`);
    const panel = document.querySelector(`#tags__suggestions_panel__${id}`);
    const suggestionsContainer = panel.querySelector('.suggestions_panel__suggestions');

    suggestionsContainer.innerHTML = '';

    const inputValue = input.value.toLowerCase().trim();

    if (inputValue === '') {
        panel.style.display = 'none';
        return;
    }

    const request = new XMLHttpRequest();

    request.open('GET', `${host}/api/Product/Tag/GetAll`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json')
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const tags = JSON.parse(request.responseText);
            
            const matchingTags = tags.filter(tag => tag.name.toLowerCase().includes(inputValue));

            for (let tag of matchingTags) {
                const suggestion = document.createElement('div');
                suggestion.classList.add('suggestions__suggestion');
                suggestion.textContent = tag.name;
                suggestion.addEventListener('click', () => {
                    addSuggestedTagToProduct(id, tag, tag.id);
                });
                suggestionsContainer.appendChild(suggestion);
            }

            panel.style.display = matchingTags.length > 0 ? 'flex' : 'none';
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(showTagSuggestions)
        } else {
            throw new Error("Failed to get response")
        }
    })
    request.send()
}

function closeTagSuggestions(id) {
    const panel = document.querySelector(`#tags__suggestions_panel__${id}`);
    const input = document.querySelector(`#tags__input_box__${id}`);

    panel.style.display = 'none';
    input.value = '';
}

function addSuggestedTagToProduct(productID, suggestedTag, tagID) {
    closeTagSuggestions(productID);

    const newTagElem = document.createElement('div');
    newTagElem.classList.add('tags__tag');
    newTagElem.setAttribute("tag-id", `${tagID}`);
    newTagElem.textContent = suggestedTag.name;

    const deleteWidget = document.createElement('div');
    deleteWidget.classList.add('tags__tag_delete_widget');
    deleteWidget.setAttribute('tag-delete-id', `${tagID}`);
    
    const deleteIcon = document.createElement('i');
    deleteIcon.classList.add('tags__tag_delete_icon', 'fa-solid', 'fa-xmark');
    deleteWidget.appendChild(deleteIcon);

    newTagElem.appendChild(deleteWidget);

    const tagsBox = document.querySelector(`#tags__tags_box__${productID}`);
    tagsBox.appendChild(newTagElem);

    newTagElem.addEventListener('mouseenter', handleTagMouseEnter);
    newTagElem.addEventListener('mouseleave', handleTagMouseLeave);
    deleteWidget.addEventListener('click', handleDeleteTagClick);
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
    deletedTagIds.push(tagID);
    tagElem.remove();
}

document.addEventListener('input', (event) => {
    if (event.target.matches('.tags__input_box')) {
        const id = event.target.getAttribute('id').replace('tags__input_box__', '');
        showTagSuggestions(id);
    }
});















function createAccounting(id) {
    const size = document.querySelector(`#accounting__size_input__${id}`).value;
    const cost = document.querySelector(`#accounting__cost_input__${id}`).value;
    const count = document.querySelector(`#accounting__count_input__${id}`).value;


    const newAccountingDTO = {
        count: Number(count),
        size: size,
        cost: Number(cost),
        productId: id
    }

    const request = new XMLHttpRequest();

    request.open('POST', `${host}/api/Product/Accounting/Create`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const accountingID = JSON.parse(request.responseText)

            const newRow = document.createElement('tr');
            newRow.classList.add('accounting__table__body_row');

            newRow.innerHTML = `
                <td class="accounting__table__cell">${accountingID}</td>
                <td class="accounting__table__cell">${size}</td>
                <td class="accounting__table__cell" contenteditable="false" id="accounting__table__cell__cost__${accountingID}" data-accounting-id="accountingID">${cost}</td>
                <td class="accounting__table__cell" contenteditable="false" id="accounting__table__cell__count__${accountingID}">${count}</td>
                <i class="accounting__edit_row fa-solid fa-pen" id="accounting__edit_row__${accountingID}" onclick="editAccounting(${accountingID})"></i>
            `;
            document.querySelector(`#accounting__table__body_data__${id}`).appendChild(newRow);
            showPopUp("info", "Вы добавили учёт к продукту")
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(createAccounting)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(newAccountingDTO))

}

function editAccounting(id) {
    const editButton = document.querySelector(`#accounting__edit_row__${id}`)
    const newCost = document.querySelector(`#accounting__table__cost_cell__${id}`);
    const newCount = document.querySelector(`#accounting__table__count_cell__${id}`);

    editButton.addEventListener("click", () => {
        if(editButton.classList.contains("fa-pen")) {
            editButton.classList.remove("fa-pen")
            editButton.classList.add("fa-check")
            newCost.contentEditable = "true"
            newCount.contentEditable = "true"
        } else {
            editButton.classList.add("fa-pen")
            editButton.classList.remove("fa-check")
            newCost.contentEditable = "false"
            newCount.contentEditable = "false"
            updateAccounting(id, newCount.innerHTML, newCost.innerHTML)
        }
    })
}

function saveUpdatedProductInfo (id) {
    const panel = document.querySelector(`#product_panel__${id}`);
    
    productName = panel.querySelector(`#input__name_input_box__${id}`).value;
    description = panel.querySelector(`#input__description_input_box__${id}`).value;
    canBeFound = panel.querySelector(`#input__button__${id}`).textContent;

    let canBeFoundUpdated = false;

    if(canBeFound === "Да") {
        canBeFoundUpdated = true;
    } else {
        canBeFoundUpdated = false;
    }

    let newName = document.querySelector(`#cell_name__${id}`).innerHTML
    newName = productName;

    productTags = Array.from(panel.querySelectorAll('.tags__tag')).map((tagElem) => ({
        id: Number(tagElem.getAttribute("tag-id")),
        name: tagElem.textContent
    })).filter(tag => !deletedTagIds.includes(tag.id));
    
    const tagsIds = productTags.map(tag => tag.id);

    updateProductInfo(id, newName, description, canBeFoundUpdated, tagsIds)
}

function updateProductInfo(id, name, description, canBeFound, tagsIDs) {
    const updatedProduct = {
        id: id,
        name: name,
        description: description,
        canBeFound: canBeFound,
        tagsIds: tagsIDs
    }

    const request = new XMLHttpRequest();

    request.open('PATCH', `${host}/api/Product/Controll/Update`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const inputElement = document.querySelector(`#input__name_input_box__${id}`);
            const newName = inputElement.value;

            const productId = inputElement.getAttribute('data-product-id');

            const cellName = document.getElementById(`cell_name__${productId}`);

            if (cellName) {
                cellName.innerHTML = newName;
            }
            showPopUp("info", "Вы обновили информацию о продукте")
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(updateProductInfo)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(updatedProduct))
}

function updateAccounting (id, count, cost) {
    const updatedAccounting = {
        id: id,
        count: Number(count),
        cost: Number(cost)
    }

    const request = new XMLHttpRequest();

    request.open('PATCH', `${host}/api/Product/Accounting/Update`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            showPopUp("info", "Вы обновили учёт продукта")
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(updateAccounting)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(updatedAccounting))
}

window.addEventListener('productsSectionLoaded', () => {
    getAllProducts();
});


function addProductImages (id) {

    let formData = new FormData();

    formData.append('ProductID', id);

    let fileInput = document.querySelector(`#add_images__files__${id}`);
    let imageNumbersInput = document.querySelector(`#add_images__files__number_input__${id}`);
    
    if(imageNumbersInput.value.trim === '' || Number(imageNumbersInput.value) <= 0) {
        showPopUp('warning', "Неправильная нумерация фотографии!")
    } else {
        formData.append('files', fileInput.files[0]);
        formData.append('ImageNumbers', imageNumbersInput.value.trim());

        imageNumbersInput.value = ''

        const request = new XMLHttpRequest();

        request.open('POST', `${host}/api/File/Product/AddImages`);
        request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
        request.addEventListener('load', () => {
            if(request.status === 200 && request.readyState === 4) {
                GetAllProductImages(`${id}`, function(files){
                    CreateImages(files, document.getElementById(`added__images_images__${id}`), id);
                });
            } else if (request.status === 401) {
                showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
                updateToken(addProductImages)
            } else {
                throw new Error("Failed to get response")
            }
        })
        
        request.send(formData)
    }
}

async function GetProductsMainImage(productIds, callback){
    var request = new XMLHttpRequest();

    await request.open("POST", `${host}/api/File/Product/GetProductMainImagesZip`);
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    request.responseType = 'arraybuffer';

    request.onload = async function(){
        if(request.status != 200){
            console.log(`Ошибка ${request.status}: ${request.statusText}`);
            return;
        } 

        callback(await GetFilesFromZip(request.response));
    }

    request.onerror = function(){
        console.log(`Ошибка при выполнении запроса`);
    }
    
    await request.send(JSON.stringify(productIds));
}

//запрос 2
async function GetAllProductImages(productId, callback){
    var request = new XMLHttpRequest();

    let url = new URL(`${host}/api/File/Product/GetAllProductImagesZip`);
    url.searchParams.set('productId', productId);

    await request.open("GET", url);

    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

    request.responseType = 'arraybuffer';

    request.onload = async function(){
        if(request.status != 200){
            console.error(`Ошибка ${request.status}: ${request.statusText}`);
            return;
        }

        callback(await GetFilesFromZip(request.response));
    }

    request.onerror = function(){
        console.log(`Ошибка при выполнении запроса`);
    }
    
    await request.send();
}

// распаковка архива, возвращает его файлы 
async function GetFilesFromZip(zipFile){
    var images = [];
    var i = 0;

    await JSZip.loadAsync(zipFile).then(function (files){
        files.forEach(function (relativePath, zipEntry){
            images[i] = zipEntry;
            i++;
        });
    }).catch(function(error){
        console.error("Ошибка при разархивации:", error);
    });

    return images;
}

// создает img теги в таргете на каждый файл
async function CreateAndPasteImages(images, targetElement, productID){
    for(let i = 0; i < images.length; i++){
        const image = images[i];
        await CreateAndPasteImage(image, targetElement, productID);
    }
}

// создает img тег в таргете и ставит туда значение файла
async function CreateAndPasteImage(image, targetElement, productID){
    var imageName = image.name;
    var imageNumber = imageName.match(/\d+/)[0];

    var divElement = document.createElement('div');
    divElement.className = 'added__images__image_holder';
    divElement.setAttribute("id", `added__images__image_holder__${productID}__${imageNumber}`)
    divElement.setAttribute("imageNumber", `${imageNumber}`)

    var imgElement = document.createElement('img');
    imgElement.className = 'image_holder__image';
    await PastDataToImg(image, imgElement);
    divElement.appendChild(imgElement);

    var deleteButtonDiv = document.createElement('div');
    deleteButtonDiv.className = 'image_holder__delete_button';
    divElement.appendChild(deleteButtonDiv);

    deleteButtonDiv.addEventListener("click", () => {
        deleteProductImage(productID, imageNumber)
        targetElement.removeChild(divElement);
    })

    var iElement = document.createElement('i');
    iElement.className = 'fa-solid fa-trash-can image_holder__delete_button_icon';
    deleteButtonDiv.appendChild(iElement);

    targetElement.appendChild(divElement);

    await PastDataToImg(image, imgElement);
}

async function CreateImages(images, targetElement, productID){
    for(let i = 0; i < images.length; i++){
        const image = images[i];
        await CreateImage(image, targetElement, productID);
    }
}

// создает img тег в таргете и ставит туда значение файла
async function CreateImage(image, targetElement, productID){
    var imageName = image.name;
    var imageNumber = imageName.match(/\d+/)[0];
    
    console.log(imageNumber)
    const elements = document.querySelector(`#added__images__image_holder__${productID}__${imageNumber}`);
    console.log(elements)   
    if (elements === null) {
        var divElement = document.createElement('div');
        divElement.className = 'added__images__image_holder';
        divElement.setAttribute("id", `added__images__image_holder__${productID}__${imageNumber}`)
        divElement.setAttribute("imageNumber", `${imageNumber}`)

        var imgElement = document.createElement('img');
        imgElement.className = 'image_holder__image';
        await PastDataToImg(image, imgElement);
        divElement.appendChild(imgElement);

        var deleteButtonDiv = document.createElement('div');
        deleteButtonDiv.className = 'image_holder__delete_button';
        divElement.appendChild(deleteButtonDiv);

        deleteButtonDiv.addEventListener("click", () => {
            deleteProductImage(productID, imageNumber)
            targetElement.removeChild(divElement);
        })

        var iElement = document.createElement('i');
        iElement.className = 'fa-solid fa-trash-can image_holder__delete_button_icon';
        deleteButtonDiv.appendChild(iElement);

        targetElement.appendChild(divElement);

        await PastDataToImg(image, imgElement);
    }

    const container = document.getElementById(`added__images_images__${productID}`);
    if (!container) {
        console.error("Container not found");
        return;
    }

    const imageHolders = Array.from(container.getElementsByClassName("added__images__image_holder"));

    imageHolders.sort((a, b) => {
        const aNumber = parseInt(a.getAttribute("imagenumber"));
        const bNumber = parseInt(b.getAttribute("imagenumber"));
        return aNumber - bNumber;
    });

    imageHolders.forEach(holder => container.appendChild(holder));

    showPopUp("info", "Вы добавили фотографию к продукту")
}

// ставит в img тег файл
async function PastDataToImg(file, imgElement){
    const imageData = await file.async('base64');

    imgElement.src = `data:image/png;base64,${imageData}`;
}

function deleteProductImage (productID, imageNumber) {
    const request = new XMLHttpRequest();
    request.open('DELETE', `${host}/api/File/Product/DeleteImage?productId=${productID}&imageNumber=${imageNumber}`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            showPopUp ("error", "Вы удалили фотографию продукта")
        } else if (request.status === 401) {
            showPopUp ("warning", "Вы не авторизированны. Дождитесь обновления токена")
            updateToken(deleteProductImage)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send()
}