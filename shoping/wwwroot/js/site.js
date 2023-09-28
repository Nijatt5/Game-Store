 //@ts-nocheck
addToCartButton = document.querySelectorAll(".add-to-cart-button");

document.querySelectorAll('.add-to-cart-button').forEach(function (addToCartButton) {
    addToCartButton.addEventListener('click', function () {
        addToCartButton.classList.add('added');
        setTimeout(function () {
            addToCartButton.classList.remove('added');
        }, 2000);
    });
});

$(".category div").click(function () {
    $(".category div").each(function () {
        $(this).removeClass("active2")
    })
    $(this).toggleClass("active2")
})


document.querySelector(".search input").addEventListener("keyup", function () {
    document.querySelectorAll(".gamename .game-text").forEach(y => {
        y.parentElement.parentElement.style.display = "none"

        if (y.innerText.toLowerCase().includes(document.querySelector(".search input").value.toLowerCase())) {
            y.parentElement.parentElement.style.display = ""
        }
    })
})



document.querySelectorAll(".before select").forEach(x => {
    x.addEventListener("change", function () {
        document.querySelectorAll("tbody tr td p").forEach(y => {
            y.parentElement.parentElement.style.display = "none"
            if (y.innerText <= x.value) {
                y.parentElement.parentElement.style.display = ""
            }
        })
    })
})


var a = 0
document.querySelectorAll("table tbody td p").forEach(x => {
    a++;
    x.innerText = a
})


$(".filtermain .fa-bars").click(function () {
    $("nav").toggle()
})

$(".check").click(function () {
    $(".check").removeClass("reng")
    $(this).toggleClass("check2")
})



$(".login").each(function () {
    $(this).click(function () {
        $(this).removeClass("login2")
        $(this).toggleClass("login2")
    })
})

$(".games").on("click", ".item .heart i", function () {
    if ($(this).attr("class") == "fa-regular fa-heart") {
        $(this).attr("class", "fa-solid fa-heart")
    } else {
        $(this).attr("class","fa-regular fa-heart")
    }
})



$(".visa img").click(function () {
    if ($(".card-master").css("display") == "block") {
        $(".card-master").hide()
    }
    $(".card-visa").toggle()
})

$(".master img").click(function () {
    if ($(".card-visa").css("display") == "block") {
        $(".card-visa").hide()
    }
    $(".card-master").toggle()
})


$("nav .fa-bars").click(function () {
    $(".items").toggle()
})