function chooseSize (clickedButton) {
    const sizePrice = clickedButton.getAttribute('data-price');
    const productPriceElem = document.querySelector('.product_price');
    productPriceElem.textContent = sizePrice;

    const sizeButtons = document.querySelectorAll(".available_size");

    sizeButtons.forEach((sizeButton) => {
        sizeButton.classList.remove("available_size--selected");
    });

    clickedButton.classList.add("available_size--selected");
}

function toggleDescription () {
    const dropdownSection = document.querySelector(".dropdown_link__bottom_section");
    const dropdownLinkIcon = document.querySelector(".dropdown_link__icon")

    if (window.getComputedStyle(dropdownSection).display === "none") {
        dropdownSection.style.display = "flex";
        dropdownLinkIcon.classList.add("dropdown_link__icon--rotated")
    } else {
        dropdownSection.style.display = "none";
        dropdownLinkIcon.classList.remove("dropdown_link__icon--rotated")
    }
}

let progressButtonCount1 = 0;

function prevProducts1 () {
    const products1 = document.querySelector(".product_carousel__slider");
    progressButtonCount1 -= 1;
    console.log(progressButtonCount1)
    checkProgress1();
    products1.scrollBy(-310, 0);
}

function nextProducts1 () {
    const products1 = document.querySelector(".product_carousel__slider");
    progressButtonCount1 += 1;
    console.log(progressButtonCount1)
    checkProgress1();
    products1.scrollBy(310, 0);
}

function checkProgress1 () {
    const progressButton1 = document.querySelectorAll(".products__progress_button")
    if (progressButtonCount1 == 0) {
        progressButtonCount1 = 0;
        progressButton1[0].className += "--active";
        progressButton1[1].className = progressButton1[1].className.replace("--active", "");
        progressButton1[2].className = progressButton1[2].className.replace("--active", "");
        progressButton1[3].className = progressButton1[3].className.replace("--active", "");
        progressButton1[4].className = progressButton1[4].className.replace("--active", "");
    } else if (progressButtonCount1 == 1) {
        progressButton1[1].className += "--active";
        progressButton1[0].className = progressButton1[0].className.replace("--active", "");
        progressButton1[2].className = progressButton1[2].className.replace("--active", "");
        progressButton1[3].className = progressButton1[3].className.replace("--active", "");
        progressButton1[4].className = progressButton1[4].className.replace("--active", "");
    } else if (progressButtonCount1 == 2) {
        progressButton1[2].className += "--active";
        progressButton1[1].className = progressButton1[1].className.replace("--active", "");
        progressButton1[0].className = progressButton1[0].className.replace("--active", "");
        progressButton1[3].className = progressButton1[3].className.replace("--active", "");
        progressButton1[4].className = progressButton1[4].className.replace("--active", "");
    } else if (progressButtonCount1 == 3) {;
        progressButton1[3].className += "--active";
        progressButton1[1].className = progressButton1[1].className.replace("--active", "");
        progressButton1[2].className = progressButton1[2].className.replace("--active", "");
        progressButton1[0].className = progressButton1[0].className.replace("--active", "");
        progressButton1[4].className = progressButton1[4].className.replace("--active", "");
    } else if (progressButtonCount1 == 4) {;
        progressButton1[4].className += "--active";
        progressButton1[1].className = progressButton1[1].className.replace("--active", "");
        progressButton1[2].className = progressButton1[2].className.replace("--active", "");
        progressButton1[0].className = progressButton1[0].className.replace("--active", "");
        progressButton1[3].className = progressButton1[3].className.replace("--active", "");
    } else if (progressButtonCount1 < 0) {
        progressButtonCount1 = 0;
        progressButton1[1].className = progressButton1[1].className.replace("--active", "");
        progressButton1[2].className = progressButton1[2].className.replace("--active", "");
        progressButton1[3].className = progressButton1[3].className.replace("--active", "");
        progressButton1[4].className = progressButton1[4].className.replace("--active", "");
    } else if (progressButtonCount1 > 4) {
        progressButtonCoun1t = 4;
        progressButton1[1].className = progressButton1[1].className.replace("--active", "");
        progressButton1[2].className = progressButton1[2].className.replace("--active", "");
        progressButton1[0].className = progressButton1[0].className.replace("--active", "");
        progressButton1[3].className = progressButton1[3].className.replace("--active", "");
    }
}


// const reviewsCountElement = document.querySelectorAll(".review_section__review");
// const reviewsCount = reviewsCountElement.length;
// const reviewsCountDomElementOne = document.querySelector(".product_rating__count");
// const reviewsCountDomElementTwo = document.querySelector(".reviews__review_count")
// const reviewsCountDomElementThree = document.querySelector(".rating_widget__review_count")

// function setReviewsCount (reviewsCount) {
//     reviewsCountDomElementOne.innerHTML = `(${reviewsCount})`;
//     reviewsCountDomElementTwo.innerHTML = reviewsCount;
//     reviewsCountDomElementThree.innerHTML = reviewsCount;
// }

// setReviewsCount(reviewsCount);

function addReviewButton () {
    const writeReviewPopUp = document.querySelector(".reviews__write");
    writeReviewPopUp.style.display = "flex";
}

function cancelReviewButton () {
    const writeReviewPopUp = document.querySelector(".reviews__write");
    writeReviewPopUp.style.display = "none";
}

const writeReviewStars = document.querySelectorAll(".write__star");

writeReviewStars.forEach((star, index) => {
    star.addEventListener("click", () => {
        writeReviewStars.forEach((star, index1) => {
            index >= index1 ? star.classList.add("write__star--full") : star.classList.remove("write__star--full");
            console.log(index)
            console.log(index1)
        });
    });
});


let starCount = 1;

function checkStars () {
    let starCountElement = document.querySelectorAll(".write__star--full");
    let starCount = starCountElement.length
}

function getSubmitDate () {
    let writeReviewDate = document.querySelector(".review__date").innerHTML;
    let in_date = new Date();
    let str = in_date.getDate() +'.'+(in_date.getMonth()+1)+'.'+in_date.getFullYear()
    writeReviewDate = str;
}

function getReviewInfo () {
    let writeReviewUsername = document.querySelector(".write__user_input").value;
    let writeReviewText = document.querySelector(".write__comment_input").value;
    console.log(writeReviewUsername)
    console.log(writeReviewText)
}

function submitReview () {
    const reviewsContainer = document.querySelector(".review_section__reviews")
    let writeReviewUsername = document.querySelector(".write__user_input").value;
    let writeReviewText = document.querySelector(".write__comment_input").value;
    let writeReviewDate = document.querySelector(".review__date").innerHTML;
    checkStars();
    getSubmitDate();
    getReviewInfo();
    const review = `
        <div class="review_section__review">
            <div class="review__top_section">
                <div class="review__info">
                    <span class="review__username">${writeReviewUsername}</span>
                    <span class="review__date">${writeReviewDate}</span>
                </div>
                <div class="review__star_rating">
                    <div class="review__star_holder">
                        <i class="fa-solid fa-star review__star"></i>
                    </div>
                    <div class="review__star_holder">
                        <i class="fa-solid fa-star review__star"></i>
                    </div>
                    <div class="review__star_holder">
                        <i class="fa-solid fa-star review__star"></i>
                    </div>
                    <div class="review__star_holder">
                        <i class="fa-solid fa-star review__star"></i>
                    </div>
                    <div class="review__star_holder">
                        <i class="fa-solid fa-star review__star"></i>
                    </div>
            </div>
            </div>
            <p class="review__text">${writeReviewText}</p>
        </div>
    `
    reviewsContainer.insertAdjacentHTML('beforeend', review);
    writeReviewPopUp.style.display = "none";
}