// Основные скрипты для сайта
console.log('PhotoRental platform initialized');

// Обработчики для кнопок входа и регистрации
document.addEventListener('DOMContentLoaded', function () {
    // Кнопки авторизации
    const loginBtn = document.querySelector('.btn-login');
    const registerBtn = document.querySelector('.btn-register');

    if (loginBtn) {
        loginBtn.addEventListener('click', function () {
            alert('Функция входа будет реализована в ближайшее время');
        });
    }

    if (registerBtn) {
        registerBtn.addEventListener('click', function () {
            alert('Функция регистрации будет доступна скоро');
        });
    }

    // Плавная прокрутка для внутренних ссылок
    const smoothScrollLinks = document.querySelectorAll('a[href^="#"]');
    smoothScrollLinks.forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            const targetId = this.getAttribute('href');
            if (targetId === '#') return;

            const targetElement = document.querySelector(targetId);
            if (targetElement) {
                targetElement.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // Обработка форм с валидацией
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', function (e) {
            const requiredFields = form.querySelectorAll('[required]');
            let isValid = true;

            requiredFields.forEach(field => {
                if (!field.value.trim()) {
                    isValid = false;
                    field.style.borderColor = 'red';
                } else {
                    field.style.borderColor = '';
                }
            });

            if (!isValid) {
                e.preventDefault();
                alert('Пожалуйста, заполните все обязательные поля');
            }
        });
    });

    // Анимации при скролле
    const animateOnScroll = function () {
        const elements = document.querySelectorAll('.service-card, .equipment-card');

        elements.forEach(element => {
            const elementTop = element.getBoundingClientRect().top;
            const windowHeight = window.innerHeight;

            if (elementTop < windowHeight - 100) {
                element.style.opacity = '1';
                element.style.transform = 'translateY(0)';
            }
        });
    };

    // Инициализация анимаций
    const animatedElements = document.querySelectorAll('.service-card, .equipment-card');
    animatedElements.forEach(element => {
        element.style.opacity = '0';
        element.style.transform = 'translateY(30px)';
        element.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
    });

    window.addEventListener('scroll', animateOnScroll);
    animateOnScroll(); // Initial check

    // Обработка сообщений об успехе
    const successMessages = document.querySelectorAll('.alert-success');
    successMessages.forEach(message => {
        setTimeout(() => {
            message.style.opacity = '0';
            message.style.transition = 'opacity 0.5s ease';
            setTimeout(() => message.remove(), 500);
        }, 5000);
    });
});

// Глобальные функции
function formatPrice(price) {
    return new Intl.NumberFormat('ru-RU', {
        style: 'currency',
        currency: 'RUB',
        minimumFractionDigits: 0
    }).format(price);
}

function calculateRentalPrice(pricePerDay, startDate, endDate) {
    const days = Math.ceil((new Date(endDate) - new Date(startDate)) / (1000 * 60 * 60 * 24));
    return days > 0 ? days * pricePerDay : 0;
}

// Экспорт функций для использования в других скриптах
if (typeof module !== 'undefined' && module.exports) {
    module.exports = { formatPrice, calculateRentalPrice };
}