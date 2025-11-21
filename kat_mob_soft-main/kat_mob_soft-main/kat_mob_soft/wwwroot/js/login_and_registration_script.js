// Обработка формы входа
document.addEventListener('DOMContentLoaded', function () {
    const loginForm = document.getElementById('form_signin');
    const registerForm = document.getElementById('form_signup');

    if (loginForm) {
        loginForm.addEventListener('submit', handleLoginSubmit);
    }

    if (registerForm) {
        registerForm.addEventListener('submit', handleRegisterSubmit);
    }
});

// Обработка отправки формы входа
async function handleLoginSubmit(event) {
    event.preventDefault();

    const formData = new FormData(event.target);
    const data = {
        Email: formData.get('Email'),
        Password: formData.get('Password'),
        RememberMe: formData.get('RememberMe') === 'on'
    };

    await sendRequest('/Home/Login', data, 'form_signin');
}

// Обработка отправки формы регистрации
async function handleRegisterSubmit(event) {
    event.preventDefault();

    const formData = new FormData(event.target);
    const data = {
        FirstName: formData.get('FirstName'),
        LastName: formData.get('LastName'),
        Email: formData.get('Email'),
        Password: formData.get('Password'),
        ConfirmPassword: formData.get('ConfirmPassword')
    };

    await sendRequest('/Home/Register', data, 'form_signup');
}

// Функция отправки fetch-запроса
async function sendRequest(url, data, formType) {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        if (result.success) {
            // Успешный запрос
            displaySuccess(result.message);
            // Перенаправление или другие действия
            if (result.redirectUrl) {
                window.location.href = result.redirectUrl;
            }
        } else {
            // Показ ошибок валидации
            displayErrors(result.errors, formType);
        }
    } catch (error) {
        console.error('Ошибка:', error);
        displayErrors(['Произошла ошибка при отправке запроса'], formType);
    }
}

// Функция отображения ошибок
function displayErrors(errors, formType) {
    const errorContainer = document.getElementById(`error-messages-${formType === 'form_signin' ? 'singin' : 'signup'}`);

    if (errorContainer) {
        errorContainer.innerHTML = '';
        errors.forEach(error => {
            const errorElement = document.createElement('div');
            errorElement.className = 'alert alert-danger';
            errorElement.textContent = error;
            errorContainer.appendChild(errorElement);
        });
    }
}

// Функция отображения успешного сообщения
function displaySuccess(message) {
    // Реализация показа успешного сообщения
    alert(message); // Можно заменить на красивый toast
}