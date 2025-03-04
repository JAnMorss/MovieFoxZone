document.addEventListener("DOMContentLoaded", function () {
    const fox = document.querySelector(".walking-fox");

    fox.addEventListener("animationiteration", function () {
        fox.style.left = "-150px";
    });
});