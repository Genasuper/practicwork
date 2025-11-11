// Скрипт для прокрутки карточек услуг
document.addEventListener('DOMContentLoaded', function () {
    initializeServicesCarousel();
    initializeAnimations();
    initializeContactInteractions();
});

// Инициализация карусели услуг
function initializeServicesCarousel() {
    const track = document.querySelector('.services-track');
    const cards = document.querySelectorAll('.service-card');
    const leftArrow = document.querySelector('.left-arrow');
    const rightArrow = document.querySelector('.right-arrow');

    if (!track || !cards.length) return;

    let currentPosition = 0;
    const cardWidth = cards[0].offsetWidth + 32; // width + gap
    const visibleCards = 3;
    const totalCards = cards.length;

    function updateArrows() {
        if (leftArrow && rightArrow) {
            leftArrow.style.visibility = currentPosition === 0 ? 'hidden' : 'visible';
            rightArrow.style.visibility = currentPosition <= -(totalCards - visibleCards) * cardWidth ? 'hidden' : 'visible';
        }
    }

    function scrollToPosition(position) {
        currentPosition = position;
        track.style.transform = `translateX(${currentPosition}px)`;
        updateArrows();
    }

    if (rightArrow) {
        rightArrow.addEventListener('click', function () {
            if (currentPosition > -(totalCards - visibleCards) * cardWidth) {
                scrollToPosition(currentPosition - cardWidth);
            }
        });
    }

    if (leftArrow) {
        leftArrow.addEventListener('click', function () {
            if (currentPosition < 0) {
                scrollToPosition(currentPosition + cardWidth);
            }
        });
    }

    // Touch events для мобильных устройств
    let startX = 0;
    let currentX = 0;

    track.addEventListener('touchstart', function (e) {
        startX = e.touches[0].clientX;
    });

    track.addEventListener('touchmove', function (e) {
        currentX = e.touches[0].clientX;
    });

    track.addEventListener('touchend', function () {
        const diff = startX - currentX;
        const swipeThreshold = 50;

        if (Math.abs(diff) > swipeThreshold) {
            if (diff > 0 && currentPosition > -(totalCards - visibleCards) * cardWidth) {
                // Swipe left
                scrollToPosition(currentPosition - cardWidth);
            } else if (diff < 0 && currentPosition < 0) {
                // Swipe right
                scrollToPosition(currentPosition + cardWidth);
            }
        }
    });

    // Адаптация к изменению размера окна
    window.addEventListener('resize', function () {
        const newCardWidth = cards[0].offsetWidth + 32;
        currentPosition = Math.round(currentPosition / cardWidth) * newCardWidth;
        track.style.transform = `translateX(${currentPosition}px)`;
        updateArrows();
    });

    // Инициализация
    updateArrows();
}

// Инициализация анимаций
function initializeAnimations() {
    // Анимация появления карточек при скролле
    const animateOnScroll = function () {
        const elements = document.querySelectorAll('.service-card, .contact-item');

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
    const animatedElements = document.querySelectorAll('.service-card, .contact-item');
    animatedElements.forEach(element => {
        element.style.opacity = '0';
        element.style.transform = 'translateY(30px)';
        element.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
    });

    window.addEventListener('scroll', animateOnScroll);
    animateOnScroll(); // Initial check
}

// Инициализация интерактивных элементов контактов
function initializeContactInteractions() {
    const contactItems = document.querySelectorAll('.contact-item');

    contactItems.forEach(item => {
        // Добавляем обработчик клика для контактных элементов
        item.addEventListener('click', function () {
            const text = this.querySelector('p').textContent;

            // Копирование текста в буфер обмена
            if (navigator.clipboard) {
                navigator.clipboard.writeText(text).then(() => {
                    showNotification('Текст скопирован в буфер обмена');
                });
            }
        });

        // Подсказка при наведении
        item.title = 'Нажмите, чтобы скопировать';
    });
}

// Функция показа уведомлений
function showNotification(message) {
    const notification = document.createElement('div');
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        background: #ff6b35;
        color: white;
        padding: 1rem 2rem;
        border-radius: 5px;
        z-index: 10000;
        box-shadow: 0 5px 15px rgba(0,0,0,0.2);
        transition: opacity 0.3s ease;
    `;
    notification.textContent = message;

    document.body.appendChild(notification);

    setTimeout(() => {
        notification.style.opacity = '0';
        setTimeout(() => notification.remove(), 300);
    }, 3000);
}

// Функции для управления каруселью (можно вызывать извне)
window.servicesCarousel = {
    next: function () {
        const track = document.querySelector('.services-track');
        const cards = document.querySelectorAll('.service-card');
        if (!track || !cards.length) return;

        const cardWidth = cards[0].offsetWidth + 32;
        const totalCards = cards.length;
        const visibleCards = 3;
        let currentPosition = parseInt(track.style.transform.replace('translateX(', '').replace('px)', '')) || 0;

        if (currentPosition > -(totalCards - visibleCards) * cardWidth) {
            currentPosition -= cardWidth;
            track.style.transform = `translateX(${currentPosition}px)`;
            updateArrows();
        }
    },

    prev: function () {
        const track = document.querySelector('.services-track');
        const cards = document.querySelectorAll('.service-card');
        if (!track || !cards.length) return;

        const cardWidth = cards[0].offsetWidth + 32;
        let currentPosition = parseInt(track.style.transform.replace('translateX(', '').replace('px)', '')) || 0;

        if (currentPosition < 0) {
            currentPosition += cardWidth;
            track.style.transform = `translateX(${currentPosition}px)`;
            updateArrows();
        }
    }
};

// Вспомогательная функция для обновления стрелок
function updateArrows() {
    const track = document.querySelector('.services-track');
    const cards = document.querySelectorAll('.service-card');
    const leftArrow = document.querySelector('.left-arrow');
    const rightArrow = document.querySelector('.right-arrow');

    if (!track || !cards.length) return;

    const cardWidth = cards[0].offsetWidth + 32;
    const totalCards = cards.length;
    const visibleCards = 3;
    let currentPosition = parseInt(track.style.transform.replace('translateX(', '').replace('px)', '')) || 0;

    if (leftArrow && rightArrow) {
        leftArrow.style.visibility = currentPosition === 0 ? 'hidden' : 'visible';
        rightArrow.style.visibility = currentPosition <= -(totalCards - visibleCards) * cardWidth ? 'hidden' : 'visible';
    }
}

// Обработка клавиатуры для доступности
document.addEventListener('keydown', function (e) {
    if (e.key === 'ArrowLeft') {
        window.servicesCarousel.prev();
    } else if (e.key === 'ArrowRight') {
        window.servicesCarousel.next();
    }
});

console.log('Home scripts loaded successfully');