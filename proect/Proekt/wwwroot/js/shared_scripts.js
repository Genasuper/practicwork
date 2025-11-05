// Ждём полной загрузки документа
document.addEventListener('DOMContentLoaded', function () {
    const header = document.querySelector('.site-header');

    window.addEventListener('scroll', function () {
        if (window.scrollY > 50) { // когда прокрутка > 50px
            header.style.backgroundColor = '#ff9900'; // оранжевый цвет
        } else {
            header.style.backgroundColor = 'transparent'; // прозрачный
        }
    });
});
