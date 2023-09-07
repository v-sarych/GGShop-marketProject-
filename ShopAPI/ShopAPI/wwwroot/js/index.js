let host = 'http://192.168.0.15'

checkAuthorization ()
function checkAuthorization () {
    if(sessionStorage.getItem("user") === "null" || !sessionStorage.getItem("user")) {
        if (localStorage.getItem("token") === "null" || !localStorage.getItem("token")) {
            console.log("There is no user, and no token")
            if (document.querySelector("#top_header__unauthorized")) {
                return
            } else {
                sessionStorage.setItem("user", null)
                const topHeaderButtons = document.querySelector(".top_header__action_buttons");
                let userButtonHTML = 
                `
                    <div class="top_header__user_button hexagon_button--1" id="top_header__unauthorized" onclick="openSignInPanel()">
                        <div class="top_header__user_button__contents hexagon_button__content--1">
                            <img src="icons/enter.svg" alt="" class="top_header__user_button__icon hexagon_button__icon--1" >
                        </div>
                    </div>
                `;
    
                let template = document.createElement('template');
                template.innerHTML = userButtonHTML.trim();
                let domNode = template.content.firstChild;
    
                topHeaderButtons.appendChild(domNode);
    
                if(document.querySelector("#top_header__authorized")) {
                    document.querySelector("#top_header__authorized").remove();
                    openProfileWidget()
                } else {
                    return
                }
            }
        } else {
            if (document.querySelector("#top_header__unauthorized")) {
                return
            } else {
                sessionStorage.setItem("user", null)
                const topHeaderButtons = document.querySelector(".top_header__action_buttons");
                let userButtonHTML = 
                `
                    <div class="top_header__user_button hexagon_button--1" id="top_header__unauthorized" onclick="openSignInPanel()">
                        <div class="top_header__user_button__contents hexagon_button__content--1">
                            <img src="icons/enter.svg" alt="" class="top_header__user_button__icon hexagon_button__icon--1" >
                        </div>
                    </div>
                `;
    
                let template = document.createElement('template');
                template.innerHTML = userButtonHTML.trim();
                let domNode = template.content.firstChild;
    
                topHeaderButtons.appendChild(domNode);
    
                if(document.querySelector("#top_header__authorized")) {
                    document.querySelector("#top_header__authorized").remove();
                } else {
                    return
                }
            }
        }
    } else {
        if (localStorage.getItem("token") === "null" || !localStorage.getItem("token")) {
            openSignInPanel()
        } else {
            let response = sessionStorage.getItem('user')
            let user = JSON.parse(response)

            if (user != null && user.phoneNumber === "admin") {
                const buttonLeftSection = document.querySelector(".top_header__action_buttons--left_section");
            
                const adminButtonHTML = `
                    <button class="top_header__admin_button hexagon_button--1">
                        <div class="top_header__admin_button__content hexagon_button__content--1">
                            <img src="icons/lock.png" alt="" class="top_header__admin_button__icon hexagon_button__icon--1">
                        </div>
                    </button>
                `;
            
                const tempContainer = document.createElement("div");
                tempContainer.innerHTML = adminButtonHTML;
            
                const adminButtonNode = tempContainer.firstElementChild;
                buttonLeftSection.insertBefore(adminButtonNode, buttonLeftSection.firstChild);

                adminButtonNode.addEventListener("click", () => {
                    console.log(1)
                    window.location.href = `adminIndex.html`;
                })
            }

            if (document.querySelector("#top_header__authorized")) {
                return
            } else {
                const topHeaderButtons = document.querySelector(".top_header__action_buttons");
                
                let userButtonHTML = 
                `
                    <div class="top_header__user_button hexagon_button--1" id="top_header__authorized" onclick="openProfileWidget()">
                        <div class="top_header__user_button__contents hexagon_button__content--1">
                            <img src="icons/user_logged_in.svg" alt="" class="top_header__user_button__icon hexagon_button__icon--1" >
                        </div>
                    </div>
                `;
    
                let buttonTemplate = document.createElement('template');
                buttonTemplate.innerHTML = userButtonHTML.trim();
                let domNode1 = buttonTemplate.content.firstChild;
    
                topHeaderButtons.appendChild(domNode1);
    
                domNode1.addEventListener("click", () => {
    
                    if (document.querySelector(".profile_widget")) {
                        return
                    } else {
                        let profileWidgetHTML = 
                        `
                        <div class="profile_widget">
                            <div class="profile_widget__top_section">
                                <img class="profile_widget__logo" src="icons/user_logged_in.svg"></img>
                                <div class="profile_widget__user_info">
                                    <p class="profile_widget__username" onclick="loadProfilePage()">${user.name}</p>
                                    <p class="profile_widget__user_phone">${user.phoneNumber}</p>
                                </div>
                            </div>
                            <div class="profile_widget__mid_section">
                                <p class="profile_widget__link" id="profile_widget__orders_link" onclick="loadOrdersPage()">Заказы</p>
                                <p class="profile_widget__link" id="profile_widget__money_back_link">Возврат</p>
                                <p class="profile_widget__link" id="profile_widget__personal_data_link" onclick="loadProfilePage(), openProfileWidget()">Мои Данные</p>
                            </div>
                            <div class="profile_widget__bottom_section">
                                <p class="profile_widget__link" id="profile_widget__log_out_link" onclick="logOut()">Выйти</p>
                            </div>
                        </div>
                        `;
    
                    let widgetTemplate = document.createElement('template');
                    widgetTemplate.innerHTML = profileWidgetHTML.trim();
                    let domNode2 = widgetTemplate.content.firstChild;
    
                    topHeaderButtons.appendChild(domNode2);
                    }
                })
    
                if(document.querySelector("#top_header__unauthorized")) {
                    document.querySelector("#top_header__unauthorized").remove();
                } else {
                    return
                }
            }
        }
    }
}

function updateProfileWidget() {
    if(document.querySelector('.profile_widget')) {
        let response = sessionStorage.getItem('user')
        let user = JSON.parse(response)

        document.querySelector(".profile_widget__username").innerHTML = user.name
        document.querySelector(".profile_widget__user_phone").innerHTML = user.phoneNumber
    } else {
        return
    }
}

function getUser () {
    console.log("Getting user")
    const request = new XMLHttpRequest();

    request.open('GET', `${host}/api/User/Get`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json')
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = request.responseText
            console.log("Response from function getUser: ", response)
            sessionStorage.setItem("user", response)
            updateCartWidget()
            checkAuthorization()
        } else if (request.status === 401) {
            updateToken()
        } else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send()
}

function updateToken (callback) {
    console.log(localStorage.getItem('token'))
    if (localStorage.getItem('token') === 'null') {
        openSignInPanel()
        return
    } else {
        const request = new XMLHttpRequest();

        request.open('POST', `${host}/api/Identity/UpdateToken`);
        request.setRequestHeader('Content-Type', 'application/json')
        request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
        request.addEventListener('load', () => {
            if(request.status === 200 && request.readyState === 4) {
                const response = request.responseText
                localStorage.setItem('token', response)
                checkAuthorization ()
            } else if (request.status === 500) {
                console.log("Error 500")
                localStorage.setItem("token", null)
            } else {
                throw new Error("Failed to get response")
            }
        })
        
        request.send()
    }
    
    callback()
}

function openSignInPanel () {
    const signInPanel = document.querySelector(".account_panel__sign_in_panel")
    const signUpPanel = document.querySelector(".account_panel__sign_up_panel")
    const contentHolder = document.querySelector(".content_holder");

    document.querySelector("#sign_up_panel__password_input").value = '';
    document.querySelector("#sign_up_panel__repeat_password_input").value = '';
    document.querySelector("#sign_up_panel__phone_input").value = '';
    document.querySelector("#sign_up_panel__name_input").value = '';

    document.querySelector(".sign_up_panel__error_text").style.display = "none"

    if (signInPanel.style.display === 'none' || signInPanel.style.display != 'flex') {
        console.log("opened")
        signInPanel.style.display = "flex"
        signUpPanel.style.display = "none"
        signInPanel.style.pointerEvents = "all"
        contentHolder.classList.add("blur");
    } else {
        console.log("closed")
        signInPanel.style.display = "none"
        signInPanel.style.pointerEvents = "none"
        contentHolder.classList.remove("blur");
    }
}

function closeSignInPanel() {
    const signInPanel = document.querySelector(".account_panel__sign_in_panel")
    const contentHolder = document.querySelector(".content_holder");

    signInPanel.style.display = "none"
    signInPanel.style.pointerEvents = "all"
    contentHolder.classList.remove("blur");
}

function closeSignUpPanel() {
    const signUpPanel = document.querySelector(".account_panel__sign_up_panel")
    const contentHolder = document.querySelector(".content_holder");

    signUpPanel.style.display = "none"
    signUpPanel.style.pointerEvents = "all"
    contentHolder.classList.remove("blur");
}

function openSignUpPanel () {
    const errorMessage = document.querySelector(".sign_in_panel__error_text")
    errorMessage.style.display = "none"

    document.querySelector("#sign_in_panel__phone_input").classList.remove("sign_in_panel__input--error")
    document.querySelector("#sign_in_panel__password_input").classList.remove("sign_in_panel__input--error")
    document.querySelector("#sign_in_panel__phone_input").value = '';
    document.querySelector("#sign_in_panel__password_input").value = ''


    const signInPanel = document.querySelector(".account_panel__sign_in_panel")
    const signUpPanel = document.querySelector(".account_panel__sign_up_panel")
    const contentHolder = document.querySelector(".content_holder");

    if (signUpPanel.style.display === "none") {
        signInPanel.style.display = "none"
        signUpPanel.style.display = "flex"
        signUpPanel.style.pointerEvents = "all"
        contentHolder.classList.add("blur");
    } else {
        signUpPanel.style.display = "none"
        signUpPanel.style.pointerEvents = "none"
        contentHolder.classList.remove("blur");
    }
}

function toggleMenu() {
    const menu = document.querySelector(".menu");
    if(menu.style.display === "none") {
        menu.style.display = "flex";
    } else {
        menu.style.display = "none";
    }
}

const categoryHolders = document.querySelectorAll(".menu__category_main")

categoryHolders.forEach((categoryHolder) => {
    categoryHolder.addEventListener('click', function() {
        let subCategories = this.nextElementSibling;
        if (subCategories && subCategories.classList.contains('menu__sub_categories')) {
            if (subCategories.style.display === 'none' || subCategories.style.display === '') {
                subCategories.style.display = 'flex';
            } else {
                subCategories.style.display = 'none';
            }
        }
    });
});

const userButton = document.querySelector(".top_header__user_button")

function openProfileWidget () {
    setTimeout(() => {
        const profileWidget = document.querySelector(".profile_widget")
        if (profileWidget.style.opacity === "0" || profileWidget.style.opacity === '') {
            profileWidget.style.opacity = "1";
            profileWidget.style.display = "flex";
            profileWidget.style.pointerEvents = "all";
        } else {
            profileWidget.style.opacity = "0";
            profileWidget.style.display = "none";
            profileWidget.style.pointerEvents = "none";
        }
    }, 200)
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
        icon.style.padding = "0.25rem 0.6rem"
        icon.classList.remove("fa-exclamation");
        icon.classList.remove("fa-info")
        icon.classList.add("fa-check");
    } else if (event === "warning") {
        toast.style.background = "#ebab3f";
        icon.style.background = "#e3930b";
        icon.style.padding = "0.25rem 0.6rem"
        icon.classList.remove("fa-check")
        icon.classList.remove("fa-info")
        icon.classList.add("fa-exclamation");
    } else if (event === "error") {
        toast.style.background = "#cc3d3d";
        icon.style.background = "#b80d0d";
        icon.style.padding = "0.25rem 0.6rem"
        icon.classList.remove("fa-check")
        icon.classList.remove("fa-info")
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

function changeGenderCategory (element) {
    let activeElement = document.querySelector('.menu__gender_category--active');
    activeElement.classList.remove('menu__gender_category--active');

    element.classList.add('menu__gender_category--active');

    let genderTagElements = document.querySelectorAll('[gender-tag-id]');

    let genderTagId = (element.id === 'men__gender_category') ? 'Мужское' : 'Женское';
    genderTagElements.forEach(function(element) {
        element.setAttribute('gender-tag-id', genderTagId);
    });
}


function togglePassword () {
    const passwordIcon = document.querySelector("#sign_up_panel__password_icon")
    const passwordInput = document.querySelector("#sign_up_panel__password_input")

    if(passwordInput.type === "password") {
        passwordIcon.classList.replace("fa-eye", "fa-eye-slash")
        passwordInput.type = "text"
    } else {
        passwordIcon.classList.replace("fa-eye-slash", "fa-eye")
        passwordInput.type = "password"
    }
}

function toggleRepeatPassword () {
    const passwordIcon = document.querySelector("#sign_up_panel__repeat_password_icon")
    const passwordInput = document.querySelector("#sign_up_panel__repeat_password_input")

    if(passwordInput.type === "password") {
        passwordIcon.classList.replace("fa-eye", "fa-eye-slash")
        passwordInput.type = "text"
    } else {
        passwordIcon.classList.replace("fa-eye-slash", "fa-eye")
        passwordInput.type = "password"
    }
}

function checkPasswordValidity(input) {
    input.value = input.value.replace(/\s/g, '');
}

function checkPasswordsMatching () {
    const password = document.querySelector("#sign_up_panel__password_input").value;
    const newPassword = document.querySelector("#sign_up_panel__repeat_password_input").value

    if(password.trim() === newPassword.trim()) {
        registerUser()
    } else {
        alert("Пароли не совпадают!")
    }
}

function registerUser () {
    const signUpPanel = document.querySelector(".account_panel__sign_up_panel")
    const contentHolder = document.querySelector(".content_holder");

    const password = document.querySelector("#sign_up_panel__password_input").value;
    const phone = document.querySelector("#sign_up_panel__phone_input").value;
    const name = document.querySelector("#sign_up_panel__name_input").value

    const newUser = {
        phoneNumber: phone,
        password: password
    }

    const request = new XMLHttpRequest();

    request.open('POST', `${host}/api/Identity/Register`);
    request.setRequestHeader('Content-Type', 'application/json')
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = request.responseText
            localStorage.setItem('token', response)
            updateUser(name);
            getUser();
            checkAuthorization ()

            signUpPanel.style.display = "none"
            signUpPanel.style.pointerEvents = "none"
            contentHolder.classList.remove("blur");

            showPopUp ("success", "Вы успешно зарегистрировались!")
        } else if(request.status === 500) {
            document.querySelector(".sign_up_panel__error_text").style.display = "flex"
        }
        else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(newUser))
}  

function loginUser () {
    const signInPanel = document.querySelector(".account_panel__sign_in_panel")
    const contentHolder = document.querySelector(".content_holder");

    let phone = document.querySelector("#sign_in_panel__phone_input").value;
    let password = document.querySelector("#sign_in_panel__password_input").value;

    const logUser = {
        phoneNumber: phone,
        password: password
    }

    const request = new XMLHttpRequest();
    
    request.open('POST', `${host}/api/Identity/Login`);
    request.setRequestHeader('Content-Type', 'application/json')
    request.addEventListener('load', () => {
        if(request.status === 200 && request.readyState === 4) {
            const response = request.responseText
            let token = localStorage.setItem('token', response)

            getUser()
            checkAuthorization ();
            updateCartWidget()

            signInPanel.style.display = "none"
            signInPanel.style.pointerEvents = "none"
            contentHolder.classList.remove("blur");
            
            phone = '';
            password = '';

            showPopUp ("sucess", "Вы успешно вошли в аккаунт")
        } else if (request.status === 404) {
            const errorMessage = document.querySelector(".sign_in_panel__error_text")
            errorMessage.style.display = "flex"

            document.querySelector("#sign_in_panel__phone_input").classList.add("sign_in_panel__input--error")
            document.querySelector("#sign_in_panel__password_input").classList.add("sign_in_panel__input--error")

            document.querySelector("#sign_in_panel__phone_input").addEventListener("focus", () => {
                document.querySelector("#sign_in_panel__phone_input").classList.remove("sign_in_panel__input--error")
            })

            document.querySelector("#sign_in_panel__password_input").addEventListener("focus", () => {
                document.querySelector("#sign_in_panel__password_input").classList.remove("sign_in_panel__input--error")
            })
        }else {
            throw new Error("Failed to get response")
        }
    })
    
    request.send(JSON.stringify(logUser))
}

function logOut () {
    localStorage.setItem("token", null)
    sessionStorage.setItem("user", null)
    checkAuthorization ()
    updateCartWidget()
    loadMainPage()
    showPopUp ("sucess", "Вы успешно вышли из аккаунта")
}


function defineTags(element) {
    var tagValues = [];

    if (element.hasAttribute("gender-tag-id")) {
        tagValues.push(element.getAttribute("gender-tag-id").toLowerCase());
    }
    if (element.hasAttribute("category-id")) {
        tagValues.push(element.getAttribute("category-id").toLowerCase());
    }
    if (element.hasAttribute("tag-id")) {
        tagValues.push(element.getAttribute("tag-id").toLowerCase());
    }

    const request = new XMLHttpRequest();

    request.open('GET', `${host}/api/Product/Tag/GetAll`);
    request.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('token'))
    request.setRequestHeader('Content-Type', 'application/json')
    request.addEventListener('load', () => {
        if (request.status === 200 && request.readyState === 4) {
            const tags = JSON.parse(request.responseText);
            localStorage.setItem("pagePath", "search")
            checkPath(localStorage.getItem("pagePath"));
            defineSearch(tagValues, tags);
        } else if (request.status === 401) {
            updateToken();
        } else {
            throw new Error("Failed to get response");
        }
    });

    request.send();
}


function defineSearch(tagNames, tags) {
    var tagIds = [];
    let matchingTagsNames = [];

    for (const tagName of tagNames) {
        if (!isNaN(tagName)) {
            const matchingTag = tags.find(tag => tag.id === parseInt(tagName));
            if (matchingTag) {
                tagIds.push(matchingTag.id);
                matchingTagsNames.push(matchingTag.name);
            }
        } else {
            const matchingTag = tags.find(tag => tag.name.toLowerCase() === tagName.toLowerCase());
            if (matchingTag) {
                tagIds.push(matchingTag.id);
                matchingTagsNames.push(matchingTag.name);
            }
        }
    }

    if (document.querySelector(".filter_panel__tags_section")) {
        const additionalTags = document.querySelectorAll(".tag_content")

        additionalTags.forEach(function(additionalTag) {
            const matchingTag = tags.find(tag => tag.name.toLowerCase() === additionalTag.innerHTML.toLowerCase());
            if (matchingTag && matchingTagsNames.includes(matchingTag.name)) {
                return
            } else if (matchingTag){
                tagIds.push(matchingTag.id);
                matchingTagsNames.push(matchingTag.name);
            } 
        })

        if(additionalTags === '' || additionalTags === null || additionalTags === undefined) {
            if(document.querySelector(".filter_panel__checkboxes")) {
                document.querySelector(".filter_panel__checkboxes").remove()
            } else {
                return
            }
        }
    }

    if (tagIds.length === 0) {
        tagIds = [0];
    }

    if (tagNames.length === 0) {
        tagIds = [];
    }

    const searchDTO = {
        firstRangePoint: 0,
        endRangePoint: 20,
        name: null,
        tagIds: tagIds
    };

    console.log(matchingTagsNames)
    localStorage.setItem("endPoint", JSON.stringify(searchDTO));
    console.log(JSON.parse(localStorage.getItem("endPoint")));
    search(matchingTagsNames);
}




async function GetProductMainImage(productIds, callback){
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
async function CreateAndPasteSearchImages(images, targetElement, productID){
    for(let i = 0; i < images.length; i++){
        const image = images[i];
        await CreateAndPasteSearchImage(image, targetElement, productID);
    }
}

// создает img тег в таргете и ставит туда значение файла
async function CreateAndPasteSearchImage(image, targetElement, productID){
    var imgElement = document.createElement('img');
    imgElement.className = 'product__image';
    imgElement.setAttribute("id", `product__image__${productID}`)
    imgElement.setAttribute("onclick", `getClickedProductInfo(${productID})`)
    await PastDataToImg(image, imgElement);
    targetElement.appendChild(imgElement);

    await PastDataToImg(image, imgElement);
}

async function CreateAndPasteProductImages(images, targetElement, productID){
    for(let i = 0; i < images.length; i++){
        const image = images[i];
        await CreateAndPasteProductImage(image, targetElement, productID);
    }
}

async function CreateAndPasteProductImage(image, targetElement, productID){
    var imgElement = document.createElement('img');
    imgElement.className = 'product_image';
    imgElement.setAttribute("id", `product__image__${productID}`)
    await PastDataToImg(image, imgElement);
    targetElement.appendChild(imgElement);

    await PastDataToImg(image, imgElement);
}

async function CreateAndPasteMainProductsImages(images, targetElement, productID){
    for(let i = 0; i < images.length; i++){
        const image = images[i];
        await CreateAndPasteMainProductsImage(image, targetElement, productID);
    }
}

async function CreateAndPasteMainProductsImage(image, targetElement, productID){
    var imgElement = document.createElement('img');
    imgElement.className = 'product__image';
    imgElement.setAttribute("id", `product__image__${productID}`)
    imgElement.setAttribute("onclick", `getClickedProductInfo(${productID})`)
    await PastDataToImg(image, imgElement);
    targetElement.appendChild(imgElement);

    await PastDataToImg(image, imgElement);
}

async function CreateAndPasteCartImages(images, targetElement, productID){
    for(let i = 0; i < images.length; i++){
        const image = images[i];
        await CreateAndPasteCartImage(image, targetElement, productID);
    }
}

async function CreateAndPasteCartImage(image, targetElement, productID){
    var imgElement = document.createElement('img');
    imgElement.className = 'product__image';
    imgElement.setAttribute("id", `product__image__${productID}`)
    await PastDataToImg(image, imgElement);
    targetElement.appendChild(imgElement);

    await PastDataToImg(image, imgElement);
}

async function PastDataToImg(file, imgElement){
    const imageData = await file.async('base64');

    imgElement.src = `data:image/png;base64,${imageData}`;
}