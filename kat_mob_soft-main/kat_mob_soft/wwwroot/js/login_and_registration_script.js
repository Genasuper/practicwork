// wwwroot/js/login_and_registration_script.js
// Объявляем функции как глобальные для доступа из других скриптов
window.openLoginForm = openLoginForm;
window.openRegisterForm = openRegisterForm;
window.closeAllForms = closeAllForms;

document.addEventListener('DOMContentLoaded', function () {
    // Элементы
    const overlay = document.getElementById('overlay');
    const loginForm = document.getElementById('login-form');
    const registerForm = document.getElementById('register-form');
    const clickToHide = document.getElementById('click-to-hide');
    const btnRegisterTop = document.getElementById('btn-register-top');

    // Кнопки закрытия
    const btnCloseLogin = document.getElementById('btn-close-login');
    const btnCloseRegister = document.getElementById('btn-close-register');

    // Кнопки отправки
    const btnLoginSubmit = document.getElementById('btn-login-submit');
    const btnRegisterSubmit = document.getElementById('btn-register-submit');

    // Ссылки переключения
    const switchToRegister = document.querySelector('.switch-to-register');
    const switchToLogin = document.querySelector('.switch-to-login');

    // Функция закрытия всех форм
    function closeAllForms() {
        if (overlay) overlay.style.display = 'none';
        if (loginForm) loginForm.style.display = 'none';
        if (registerForm) registerForm.style.display = 'none';
        document.body.style.overflow = '';
    }

    // Функция открытия формы входа
    function openLoginForm() {
        if (overlay) overlay.style.display = 'block';
        if (loginForm) loginForm.style.display = 'block';
        if (registerForm) registerForm.style.display = 'none';
        document.body.style.overflow = 'hidden';
    }

    // Функция открытия формы регистрации
    function openRegisterForm() {
        if (overlay) overlay.style.display = 'block';
        if (registerForm) registerForm.style.display = 'block';
        if (loginForm) loginForm.style.display = 'none';
        document.body.style.overflow = 'hidden';
    }

    // Обработчики открытия форм
    if (clickToHide) {
        clickToHide.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            openLoginForm();
        });
    }

    if (btnRegisterTop) {
        btnRegisterTop.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            openRegisterForm();
        });
    }

    // Обработчики закрытия
    if (overlay) {
        overlay.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            closeAllForms();
        });
    }

    if (btnCloseLogin) {
        btnCloseLogin.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            closeAllForms();
        });
    }

    if (btnCloseRegister) {
        btnCloseRegister.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            closeAllForms();
        });
    }

    // Обработчики переключения
    if (switchToRegister) {
        switchToRegister.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            openRegisterForm();
        });
    }

    if (switchToLogin) {
        switchToLogin.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            openLoginForm();
        });
    }

    // Обработчики кнопок входа и регистрации - ПРОСТО ЗАКРЫВАЕМ ФОРМЫ
    if (btnLoginSubmit) {
        btnLoginSubmit.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            closeAllForms();
        });
    }

    if (btnRegisterSubmit) {
        btnRegisterSubmit.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            closeAllForms();
        });
    }

    // Закрытие по Escape
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') {
            closeAllForms();
        }
    });

    // Предотвращаем всплытие событий от самих форм
    if (loginForm) {
        loginForm.addEventListener('click', function (e) {
            e.stopPropagation();
        });
    }

    if (registerForm) {
        registerForm.addEventListener('click', function (e) {
            e.stopPropagation();
        });
    }

    // Анимация label для полей ввода
    const inputs = document.querySelectorAll('.input-group input');
    inputs.forEach(input => {
        if (!input.getAttribute('placeholder')) {
            input.setAttribute('placeholder', ' ');
        }

        input.addEventListener('focus', function () {
            this.parentElement.classList.add('focused');
        });

        input.addEventListener('blur', function () {
            if (!this.value) {
                this.parentElement.classList.remove('focused');
            }
        });
    });
});