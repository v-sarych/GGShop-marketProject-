let slideIndex = 1;
let progressButtonCount = 0;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    const slides = document.querySelectorAll(".panel_holder__panel");
    const dots = document.querySelectorAll(".panels__progress_button");
    let i;
    if (n > slides.length) {slideIndex = 1}
    if (n < 1) {slideIndex = slides.length}
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace("--active", "");
    }
    slides[slideIndex-1].style.display = "flex";
    dots[slideIndex-1].className += "--active";
}

function prevButton () {
    plusSlides(-1);
}

function nextButton () {
    plusSlides(+1);
}




function prevProducts () {
    const products = document.querySelector(".product_carousel__slider");
    progressButtonCount -= 1;
    checkProgress();
    products.scrollBy(-310, 0);
}

function nextProducts () {
    const products = document.querySelector(".product_carousel__slider");
    progressButtonCount += 1;
    checkProgress();
    products.scrollBy(310, 0);
}

function checkProgress () {
    const progressButton = document.querySelectorAll(".products__progress_button")
    if (progressButtonCount == 0) {
        progressButtonCount = 0;
        progressButton[0].className += "--active";
        progressButton[1].className = progressButton[1].className.replace("--active", "");
        progressButton[2].className = progressButton[2].className.replace("--active", "");
        progressButton[3].className = progressButton[3].className.replace("--active", "");
        progressButton[4].className = progressButton[4].className.replace("--active", "");
    } else if (progressButtonCount == 1) {
        progressButton[1].className += "--active";
        progressButton[0].className = progressButton[0].className.replace("--active", "");
        progressButton[2].className = progressButton[2].className.replace("--active", "");
        progressButton[3].className = progressButton[3].className.replace("--active", "");
        progressButton[4].className = progressButton[4].className.replace("--active", "");
    } else if (progressButtonCount == 2) {
        progressButton[2].className += "--active";
        progressButton[1].className = progressButton[1].className.replace("--active", "");
        progressButton[0].className = progressButton[0].className.replace("--active", "");
        progressButton[3].className = progressButton[3].className.replace("--active", "");
        progressButton[4].className = progressButton[4].className.replace("--active", "");
    } else if (progressButtonCount == 3) {;
        progressButton[3].className += "--active";
        progressButton[1].className = progressButton[1].className.replace("--active", "");
        progressButton[2].className = progressButton[2].className.replace("--active", "");
        progressButton[0].className = progressButton[0].className.replace("--active", "");
        progressButton[4].className = progressButton[4].className.replace("--active", "");
    } else if (progressButtonCount == 4) {;
        progressButton[4].className += "--active";
        progressButton[1].className = progressButton[1].className.replace("--active", "");
        progressButton[2].className = progressButton[2].className.replace("--active", "");
        progressButton[0].className = progressButton[0].className.replace("--active", "");
        progressButton[3].className = progressButton[3].className.replace("--active", "");
    } else if (progressButtonCount < 0) {
        progressButtonCount = 0;
        progressButton[1].className = progressButton[1].className.replace("--active", "");
        progressButton[2].className = progressButton[2].className.replace("--active", "");
        progressButton[3].className = progressButton[3].className.replace("--active", "");
        progressButton[4].className = progressButton[4].className.replace("--active", "");
    } else if (progressButtonCount > 4) {
        progressButtonCount = 4;
        progressButton[1].className = progressButton[1].className.replace("--active", "");
        progressButton[2].className = progressButton[2].className.replace("--active", "");
        progressButton[0].className = progressButton[0].className.replace("--active", "");
        progressButton[3].className = progressButton[3].className.replace("--active", "");
    }
}