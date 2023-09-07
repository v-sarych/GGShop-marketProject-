function deliveryOptions(event) {
    const pickupOption = document.querySelector("#delivery_option__pickup");
    const pickupOptionCircle = document.querySelector("#delivery_option__pickup_circle");
    const carrierOption = document.querySelector("#delivery_option__carrier");
    const carrierOptionCircle = document.querySelector("#delivery_option__carrier_circle");

    const clickedOption = event.target.closest('.info__option');

    if (clickedOption.id === 'delivery_option__pickup') {
        pickupOption.classList.toggle("info__option--active");
        pickupOptionCircle.classList.toggle("info__option_circle--active");
        carrierOption.classList.remove("info__option--active");
        carrierOptionCircle.classList.remove("info__option_circle--active");
    } else if (clickedOption.id === 'delivery_option__carrier') {
        carrierOption.classList.toggle("info__option--active");
        carrierOptionCircle.classList.toggle("info__option_circle--active");
        pickupOption.classList.remove("info__option--active");
        pickupOptionCircle.classList.remove("info__option_circle--active");
    }
}


function paymentOptions(event) {
    const offlineOption = document.querySelector("#payment_option__offline");
    const offlineOptionCircle = document.querySelector("#payment_option__offline_circle");
    const onlineOption = document.querySelector("#payment_option__online");
    const onlineOptionCircle = document.querySelector("#payment_option__online_circle");

    const clickedOption = event.target.closest('.info__option');

    if (clickedOption.id === 'payment_option__offline') {
        offlineOption.classList.toggle("info__option--active");
        offlineOptionCircle.classList.toggle("info__option_circle--active");
        onlineOption.classList.remove("info__option--active");
        onlineOptionCircle.classList.remove("info__option_circle--active");
    } else if (clickedOption.id === 'payment_option__online') {
        onlineOption.classList.toggle("info__option--active");
        onlineOptionCircle.classList.toggle("info__option_circle--active");
        offlineOption.classList.remove("info__option--active");
        offlineOptionCircle.classList.remove("info__option_circle--active");
    }
}


function createOrder() {
    const deliveryAddress = document.querySelector("#delivery_address__input").value

    let user = JSON.parse(sessionStorage.getItem("user"));
    let orderItems = [];
    for (let item of user.userShoppingCartItems) {
        orderItems.push({
            size: item.size,
            count: item.count,
            productId: item.product.id
        });
    }

    const newOrder = {
        deliveryAddress: deliveryAddress,
        orderItems: orderItems
    }

    const request = new XMLHttpRequest();
    request.open('POST', `${host}/api/Order/Create`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'));
    request.setRequestHeader('Content-Type', 'application/json');
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = JSON.parse(request.responseText);
            
            const newOrder = response.orderItems.map(orderItem => ({
                cost: response.cost,
                deliveryAddress: response.deliveryAddress,
                id: response.id,
                isPaidFor: response.isPaidFor,
                orderItems: [orderItem],
                status: response.status
            }));

            user.orders.push(...newOrder);
            sessionStorage.setItem("user", JSON.stringify(user));
            console.log(JSON.parse(sessionStorage.getItem("user")))
            
            document.querySelector(".order_panel").style.display = "flex";
            document.querySelector(".content_holder").classList.add("blur");
            document.querySelector(".footer").classList.add("blur");

            showPopUp ("success", "Вы успешно оформили заказ!");
            for (let item of user.userShoppingCartItems) {
                deleteCartItem(item.id);
            }
        } else if (request.status === 401) {
            updateToken(createOrder);
        } else if (request.status === 500) {
            showPopUp("error", "Заказ не был оформлен. Товара нет в наличии!")
        }else {
            throw new Error("Failed to get response");
        }
    });
    
    request.send(JSON.stringify(newOrder));
}