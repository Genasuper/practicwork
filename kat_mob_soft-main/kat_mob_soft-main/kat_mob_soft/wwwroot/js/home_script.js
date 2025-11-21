// home_script.js
document.addEventListener('DOMContentLoaded', function () {
    // Прокрутка услуг
    const track = document.getElementById('services-track');
    const leftButton = document.getElementById('services-left');
    const rightButton = document.getElementById('services-right');
    const cards = document.querySelectorAll('.service-card');

    if (!track || !leftButton || !rightButton) return;

    const cardWidth = cards[0].offsetWidth + 30; // ширина карточки + gap
    const visibleCards = 3;
    let currentPosition = 0;
    const maxPosition = (cards.length - visibleCards) * cardWidth;

    function updateButtons() {
        leftButton.disabled = currentPosition === 0;
        rightButton.disabled = currentPosition >= maxPosition;
    }

    leftButton.addEventListener('click', function () {
        if (currentPosition > 0) {
            currentPosition -= cardWidth;
            if (currentPosition < 0) currentPosition = 0;
            track.style.transform = `translateX(-${currentPosition}px)`;
            updateButtons();
        }
    });

    rightButton.addEventListener('click', function () {
        if (currentPosition < maxPosition) {
            currentPosition += cardWidth;
            if (currentPosition > maxPosition) currentPosition = maxPosition;
            track.style.transform = `translateX(-${currentPosition}px)`;
            updateButtons();
        }
    });

    // Инициализация кнопок
    updateButtons();

    // Автоматическая прокрутка (опционально)
    let autoScroll = setInterval(function () {
        if (currentPosition >= maxPosition) {
            currentPosition = 0;
        } else {
            currentPosition += cardWidth;
        }
        track.style.transform = `translateX(-${currentPosition}px)`;
        updateButtons();
    }, 5000);

    // Остановка автоскролла при наведении
    track.addEventListener('mouseenter', function () {
        clearInterval(autoScroll);
    });

    track.addEventListener('mouseleave', function () {
        autoScroll = setInterval(function () {
            if (currentPosition >= maxPosition) {
                currentPosition = 0;
            } else {
                currentPosition += cardWidth;
            }
            track.style.transform = `translateX(-${currentPosition}px)`;
            updateButtons();
        }, 5000);
    });

    // Адаптация к изменению размера окна
    window.addEventListener('resize', function () {
        const newCardWidth = cards[0].offsetWidth + 30;
        if (newCardWidth !== cardWidth) {
            currentPosition = Math.round(currentPosition / cardWidth) * newCardWidth;
            track.style.transform = `translateX(-${currentPosition}px)`;
        }
    });
});