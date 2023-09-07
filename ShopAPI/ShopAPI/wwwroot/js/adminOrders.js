function changeOrderSearchButton () {
    const searchButton = document.querySelector(".search_section__button--orders");
    const searchPanel = document.querySelector(".search_section__search_panel--orders")
    const statusesPanel = document.querySelector(".statuses_panel")

    if (searchButton.classList.contains("search_section__button--active")) {
        searchButton.classList.remove("search_section__button--active");
        searchPanel.style.display = "none";
        statusesPanel.style.display = "none"
    } else {
        searchButton.classList.add("search_section__button--active");
        searchPanel.style.display = "flex";
        statusesPanel.style.display = "flex"
        getAllStatuses()
    }
}

function getAllOrders () {
    const ordersDTO = {
        
    }

    const request = new XMLHttpRequest();
    request.open('POST', `${host}/api/Order/SearchOrders`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = JSON.parse(request.responseText)
            displayAllOrders(response)
        } else if (request.status === 401) {
            updateToken()
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(ordersDTO))
}

function displayAllOrders (orders) {
    const tableBody = document.querySelector(".table__body_data--orders");
    let dataHTML = '';
    for (let order of orders) {
        dataHTML += `<tr class="table__body_row" onclick="openOrderPanel('${order.id}')">
                        <td class="order_table__cell">${order.id}</td>
                        <td class="order_table__cell">${order.isPaidFor}</td>
                        <td class="order_table__cell" id="status_cell__${order.id}">${order.status}</td>
                    </tr>`;
        const orderPanel = createOrderPanel(order.id, order.deliveryAddress, order.status, order.isPaidFor, order.cost, order.orderItems);
        document.querySelector('.order_panels').appendChild(orderPanel);
    }
    tableBody.innerHTML = dataHTML;
}

window.addEventListener('ordersSectionLoaded', () => {
    getAllOrders()
});


function createOrderPanel(orderID, deliveryAddress, orderStatus, isPaidFor, orderCost, orderItems) {
    let paymentText;
    let paymentColor;
    if (isPaidFor === true) {
        paymentText = "Оплачено";
        paymentColor = "#468c3f";
    } else {
        paymentText = "Не оплачено";
        paymentColor = "var(--a-700)";
    }
    console.log(orderItems)

    const productsHTML = orderItems ? orderItems.map(orderItem => `
        <tr class="products__table__body_row">
            <td class="products__table__cell products__table__cell--name">${orderItem.product.name}</td>
            <td class="products__table__cell products__table__cell--size">${orderItem.size}</td>
            <td class="products__table__cell products__table__cell--price">${orderItem.cost} ₽  x  ${orderItem.count}</td>
        </tr>
    `).join('') : '';

    const newOrderPanel = document.createElement('div');
    newOrderPanel.setAttribute('id', `order_panel__${orderID}`);
    newOrderPanel.classList.add('order_panel');
    newOrderPanel.style.display = 'none';
    newOrderPanel.innerHTML = `
        <div class="order_panel__header_section">
            <h3 class="order_panel__header">Заказ:</h3>
            <span class="order_panel__id" id="order_panel__id__${orderID}">${orderID}</span>
        </div>
        <div class="order_panel__delivery_address_box">
            <p class="order_panel__delivery_address_label">Адрес Доставки:</p>
            <p class="order_panel__delivery_address" id="order_panel__delivery_address__${orderID}">${deliveryAddress}</p>
        </div>
        <div class="order_panel__status_box">
            <p class="order_panel__status_label">Статус Заказа</p>
            <div class="order_panel__select_box">
                <select name="orderStatuses" class="order_panel__select_menu" id="order_panel__select_menu__${orderID}" disabled>
                    <option value="Created" class="order_panel__option" id="order_panel__created_option__${orderID}">Created</option>
                    <option value="Cancelled" class="order_panel__option" id="order_panel__cancelled_option__${orderID}">Cancelled</option>
                    <option value="Accepted" class="order_panel__option" id="order_panel__accepted_option__${orderID}">Accepted</option>
                    <option value="InDelivery" class="order_panel__option" id="order_panel__indelivery_option__${orderID}">InDelivery</option>
                </select>
            </div>
            <i class="fa-solid fa-pen status__edit_button" id="status__edit_button__${orderID}" onclick="editOrderStatus('${orderID}')"></i>
        </divfdsfsd>
        <div class="order_panel__payment_box">
            <p class="order_panel__payment_label">Оплата Заказа</p>
            <div class="order_panel__payment" id="order_panel__payment__${orderID}" style="background: ${paymentColor};">${paymentText}</div>
        </div>
        <div class="order_panel__table">
            <table class="products_table" cellpadding="0" cellspacing="0">
                <tbody class="products__table__body_data">
                    ${productsHTML}
                </tbody>
            </table>
            <div class="products__table__footer">
                <p class="footer__cell">Итого:</p>
                <div class="footer__cell footer__cell--total"><span class="footer__cell__total" id="footer__cell__total__${orderID}">${orderCost}</span> ₽</div>
            </div>
        </div>
    `;

    return newOrderPanel;
}

function openOrderPanel(id) {
    const allPanels = document.querySelectorAll('.order_panel');
    const specificPanel = document.getElementById(`order_panel__${id}`);
    if (specificPanel.style.display === 'flex') {
        specificPanel.style.display = 'none';
        return;
    }

    allPanels.forEach(panel => {
        panel.style.display = 'none';
    });

    specificPanel.style.display = 'flex';

    let newStatus = document.querySelector(`#order_panel__select_menu__${id}`);
    const statusCell = document.querySelector(`#status_cell__${id}`);
    newStatus.value = statusCell.innerHTML
}

function editOrderStatus(id) {
    const editButton = document.querySelector(`#status__edit_button__${id}`)
    const statusInput = document.querySelector(`#order_panel__select_menu__${id}`)

    if(editButton.classList.contains("fa-pen")) {
        editButton.classList.replace("fa-pen", "fa-check")
        statusInput.disabled = false;
    } else {
        editButton.classList.replace("fa-check", "fa-pen")
        statusInput.disabled = true;
        let newStatus = document.querySelector(`#order_panel__select_menu__${id}`).value;
        const statusCell = document.querySelector(`#status_cell__${id}`);
        statusCell.innerHTML = newStatus;
        updateOrderStatus(id, statusInput.value)
    }
}

function updateOrderStatus (id, newStatus) {
    const updatedOrder = {
        id: id,
        newStatus: newStatus
    }

    const request = new XMLHttpRequest();

    request.open('PUT', `${host}/api/Order/UpdateStatus?id=${id}&newStatus=${newStatus}`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            console.log("Updated")
            showPopUp("success", "Вы обновили статус заказа")
        } else if (request.status === 401) {
            updateToken()
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(updatedOrder))
}

function searchOrders () {
    const orderStatus = document.querySelector("#search__order_status")
    const userID = document.querySelector("#search__user_id")
    const productID = document.querySelector("#search__product_id")


    const ordersDTO = {
        orderStatus: orderStatus.value ? orderStatus.value : null,
        userID: userID.value ? userID.value : null,
        productID: productID.value ? productID.value : null
    }

    const request = new XMLHttpRequest();
    request.open('POST', `${host}/api/Order/SearchOrders`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = JSON.parse(request.responseText)
            console.log(response)
            displayAllOrders(response)
        } else if (request.status === 401) {
            setTimeout(() => {
                updateToken()
            }, 20000)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(ordersDTO))
}

function getAllStatuses () {
    const request = new XMLHttpRequest();
    request.open('GET', `${host}/api/Order/GetAvailableStatuses`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = JSON.parse(request.responseText)

            const statusesPanel = document.querySelector('.statuses_panel__statuses');
            statusesPanel.innerHTML = ''; // clear any existing content
            
            for (const status in response) {
                const statusDiv = document.createElement('div');
                statusDiv.className = 'statuses_panel__status';
                statusDiv.textContent = response[status];
                statusesPanel.appendChild(statusDiv);
            }
        } else if (request.status === 401) {
            setTimeout(() => {
                updateToken()
            }, 20000)
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send()
}

$(document).ready(function () {
    $('.select-label').click(function () {
        $('.dropdown').toggleClass('active');
    });
    $('.dropdown-list li').click(function () {
        $('.select-label').text($(this).text());
        $('.dropdown').removeClass('active');
    });
});