// Ждём полной загрузки документа
document.addEventListener('DOMContentLoaded', function () {
    const header = document.querySelector('.site-header');

    // Изменение цвета шапки при прокрутке
    window.addEventListener('scroll', function () {
        if (window.scrollY > 50) { // когда прокрутка > 50px
            header.style.backgroundColor = '#ff9900'; // оранжевый цвет
        } else {
            header.style.backgroundColor = 'transparent'; // прозрачный
        }
    });

    // ===== ШАГ 5: Логика для гамбургера и бокового меню =====
    const sideMenu = document.getElementById('side-menu');
    const toggles = document.querySelectorAll('#side-menu-button-click-to-hide');

    // Переключаем класс active (появление/скрытие меню)
    toggles.forEach(btn => {
        btn.addEventListener('click', function () {
            sideMenu.classList.toggle('active');
        });
    });
    document.addEventListener('DOMContentLoaded', () => {
        const burger = document.getElementById('side-menu-button-click-to-hide');
        const sideMenu = document.getElementById('side-menu');
        const closeBtn = document.getElementById('close-side-menu');

        const loginBtn = document.getElementById('burger-login');
        const registerBtn = document.getElementById('burger-register');
        const overlay = document.getElementById('overlay');
        const container = document.getElementById('container');

        if (burger) {
            burger.addEventListener('click', () => {
                burger.classList.toggle('active');
                sideMenu.classList.toggle('open');
            });
        }

        if (closeBtn) {
            closeBtn.addEventListener('click', () => {
                sideMenu.classList.remove('open');
                burger.classList.remove('active');
            });
        }

        // Открытие формы "Войти"
        if (loginBtn) {
            loginBtn.addEventListener('click', () => {
                sideMenu.classList.remove('open');
                container.style.display = 'block';
                overlay.style.display = 'block';
                document.body.style.overflow = 'hidden';
                container.classList.remove('right-panel-active');
            });
        }

        // Открытие формы "Регистрация"
        if (registerBtn) {
            registerBtn.addEventListener('click', () => {
                sideMenu.classList.remove('open');
                container.style.display = 'block';
                overlay.style.display = 'block';
                document.body.style.overflow = 'hidden';
                container.classList.add('right-panel-active');
            });
        }
    });

    /*  вфвфывф*/
    // Функция для открытия/закрытия выдвижного меню
    function toggleSideMenu() {
        const sideMenu = document.getElementById('side-menu');
        const overlay = document.getElementById('side-menu-overlay');
        const hamburger = document.getElementById('side-menu-button-click-to-hide');

        if (sideMenu && overlay && hamburger) {
            sideMenu.classList.toggle('active');
            overlay.classList.toggle('active');
            hamburger.classList.toggle('active');
            document.body.style.overflow = sideMenu.classList.contains('active') ? 'hidden' : 'auto';
        }
    }

    // Ждём полной загрузки документа
    document.addEventListener('DOMContentLoaded', function () {
        const header = document.querySelector('.site-header');

        // Обработчик для кнопки меню
        const menuButton = document.getElementById('side-menu-button-click-to-hide');
        if (menuButton) {
            menuButton.addEventListener('click', toggleSideMenu);
        }

        // Изменение цвета хедера при скролле
        window.addEventListener('scroll', function () {
            if (window.scrollY > 50) {
                header.style.backgroundColor = '#ff9900';
            } else {
                header.style.backgroundColor = 'transparent';
            }
        });

        // Закрытие меню при клике на оверлей
        const overlay = document.getElementById('side-menu-overlay');
        const sideMenu = document.getElementById('side-menu');

        if (overlay && sideMenu) {
            overlay.addEventListener('click', function () {
                sideMenu.classList.remove('active');
                overlay.classList.remove('active');

                const hamburger = document.getElementById('side-menu-button-click-to-hide');
                if (hamburger) {
                    hamburger.classList.remove('active');
                }

                document.body.style.overflow = 'auto';
            });
        }

        // Закрытие меню по ESC
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape') {
                if (sideMenu && overlay) {
                    sideMenu.classList.remove('active');
                    overlay.classList.remove('active');

                    const hamburger = document.getElementById('side-menu-button-click-to-hide');
                    if (hamburger) {
                        hamburger.classList.remove('active');
                    }

                    document.body.style.overflow = 'auto';
                }
            }
        });
    });
});

