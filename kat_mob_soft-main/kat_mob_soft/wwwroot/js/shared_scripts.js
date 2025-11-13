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
// Функция для открытия/закрытия бокового меню
function toggleSideMenu() {
    const sideMenu = document.getElementById('side-menu');
    const overlay = document.getElementById('side-menu-overlay');
    const hamburger = document.getElementById('hamburger-menu');

    sideMenu.classList.toggle('active');
    overlay.classList.toggle('active');
    hamburger.classList.toggle('active');
}

// Инициализация гамбургер-меню
document.addEventListener('DOMContentLoaded', function () {
    const hamburgerMenu = document.getElementById('hamburger-menu');
    const sideMenuClose = document.getElementById('side-menu-close');
    const sideMenuOverlay = document.getElementById('side-menu-overlay');
    const sideMenuLogin = document.getElementById('side-menu-login');
    const sideMenuRegister = document.getElementById('side-menu-register');

    // Открытие меню
    if (hamburgerMenu) {
        hamburgerMenu.addEventListener('click', toggleSideMenu);
    }

    // Закрытие меню
    if (sideMenuClose) {
        sideMenuClose.addEventListener('click', toggleSideMenu);
    }

    if (sideMenuOverlay) {
        sideMenuOverlay.addEventListener('click', toggleSideMenu);
    }

    // Кнопки в боковом меню
    if (sideMenuLogin) {
        sideMenuLogin.addEventListener('click', function () {
            toggleSideMenu();
            openLoginForm();
        });
    }

    if (sideMenuRegister) {
        sideMenuRegister.addEventListener('click', function () {
            toggleSideMenu();
            openRegisterForm();
        });
    }
});