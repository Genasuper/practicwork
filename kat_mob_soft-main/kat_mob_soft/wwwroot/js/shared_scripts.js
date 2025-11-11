// Скрипт для изменения позиции нижней шапки при прокрутке
window.addEventListener('scroll', function () {
    const headerTop = document.querySelector('.header-top');
    const headerBottom = document.querySelector('.header-bottom');
    const scrollPosition = window.scrollY;

    if (scrollPosition > 100) {
        headerTop.classList.add('scrolled');
        headerBottom.classList.add('scrolled');
    } else {
        headerTop.classList.remove('scrolled');
        headerBottom.classList.remove('scrolled');
    }
});

// Базовая инициализация при загрузке страницы
document.addEventListener('DOMContentLoaded', function () {
    console.log('PhotoRental shared scripts loaded');

    // Проверяем, что шапки на месте
    const headerBottom = document.querySelector('.header-bottom');
    if (headerBottom) {
        console.log('Bottom header found and positioned');
    }
});