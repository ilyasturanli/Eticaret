document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('signupForm');
    const submitBtn = document.getElementById('submitBtn');
    const validationSummary = document.querySelector('.alert-danger');

    const firstNameInput = document.getElementById('firstNameInput');
    const lastNameInput = document.getElementById('lastNameInput');
    const emailInput = document.getElementById('floatingEmail');
    const passwordInput = document.getElementById('floatingPassword');
    const confirmPasswordInput = document.getElementById('floatingConfirmPassword');
    const togglePassword = document.getElementById('togglePassword');
    const toggleConfirmPassword = document.getElementById('toggleConfirmPassword');

    // Password visibility toggle
    if (togglePassword) {
        togglePassword.addEventListener('click', function () {
            const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
            passwordInput.setAttribute('type', type);

            const icon = this.querySelector('i');
            icon.classList.toggle('fa-eye');
            icon.classList.toggle('fa-eye-slash');
        });
    }

    // Confirm password visibility toggle
    if (toggleConfirmPassword) {
        toggleConfirmPassword.addEventListener('click', function () {
            const type = confirmPasswordInput.getAttribute('type') === 'password' ? 'text' : 'password';
            confirmPasswordInput.setAttribute('type', type);

            const icon = this.querySelector('i');
            icon.classList.toggle('fa-eye');
            icon.classList.toggle('fa-eye-slash');
        });
    }

    // Input validation
    [firstNameInput, lastNameInput, emailInput, passwordInput, confirmPasswordInput].forEach(input => {
        if (input) {
            input.addEventListener('blur', validateField);
            input.addEventListener('input', clearValidation);
        }
    });

    function validateField() {
        if (this === emailInput) validateEmail();
        else if (this === passwordInput || this === confirmPasswordInput) validatePassword();
        else validateText(this);
        checkValidationSummary();
    }

    function validateText(input) {
        if (!input.value.trim()) {
            setInvalid(input, 'Bu alan gereklidir.');
            return false;
        } else {
            setValid(input, 'Geçerli.');
            return true;
        }
    }

    function validateEmail() {
        const email = emailInput.value;
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!email) {
            setInvalid(emailInput, 'Email gereklidir.');
            return false;
        } else if (!emailRegex.test(email)) {
            setInvalid(emailInput, 'Geçerli bir email girin.');
            return false;
        } else {
            setValid(emailInput, 'Geçerli email.');
            return true;
        }
    }

    function validatePassword() {
        const password = passwordInput.value;
        const confirm = confirmPasswordInput.value;
        let isValid = true;

        if (!password) {
            setInvalid(passwordInput, 'Şifre gereklidir.');
            isValid = false;
        } else if (password.length < 6) {
            setInvalid(passwordInput, 'Şifre en az 6 karakter olmalıdır.');
            isValid = false;
        } else if (!/[A-Z]/.test(password)) {
            setInvalid(passwordInput, 'Şifre en az bir büyük harf içermelidir.');
            isValid = false;
        } else if (!/[a-z]/.test(password)) {
            setInvalid(passwordInput, 'Şifre en az bir küçük harf içermelidir.');
            isValid = false;
        } else if (!/[0-9]/.test(password)) {
            setInvalid(passwordInput, 'Şifre en az bir rakam içermelidir.');
            isValid = false;
        } else {
            setValid(passwordInput, 'Güvenli şifre ✓');
        }

        if (!confirm) {
            setInvalid(confirmPasswordInput, 'Şifre tekrar gereklidir.');
            isValid = false;
        } else if (confirm !== password) {
            setInvalid(confirmPasswordInput, 'Şifreler eşleşmiyor.');
            isValid = false;
        } else {
            setValid(confirmPasswordInput, 'Şifreler eşleşiyor ✓');
        }

        return isValid;
    }

    function setInvalid(input, message) {
        input.classList.remove('is-valid');
        input.classList.add('is-invalid');
        let feedback = input.parentNode.querySelector('.invalid-feedback');
        if (!feedback) {
            feedback = document.createElement('div');
            feedback.className = 'invalid-feedback';
            input.parentNode.appendChild(feedback);
        }
        feedback.textContent = message;

        const validFeedback = input.parentNode.querySelector('.valid-feedback');
        if (validFeedback) validFeedback.remove();
    }

    function setValid(input, message) {
        input.classList.remove('is-invalid');
        input.classList.add('is-valid');
        let feedback = input.parentNode.querySelector('.valid-feedback');
        if (!feedback) {
            feedback = document.createElement('div');
            feedback.className = 'valid-feedback';
            input.parentNode.appendChild(feedback);
        }
        feedback.textContent = message;

        const invalidFeedback = input.parentNode.querySelector('.invalid-feedback');
        if (invalidFeedback) invalidFeedback.remove();
    }

    function clearValidation() {
        [firstNameInput, lastNameInput, emailInput, passwordInput, confirmPasswordInput].forEach(input => {
            if (input) {
                input.classList.remove('is-valid', 'is-invalid');
                const feedback = input.parentNode.querySelector('.invalid-feedback, .valid-feedback');
                if (feedback) feedback.remove();
            }
        });
        checkValidationSummary();
    }

    function checkValidationSummary() {
        // Validation summary artık her zaman gösteriliyor, bu fonksiyonu kaldırıyoruz
        // veya sadece hata durumunda gösteriyoruz
    }

    // Form submission
    form.addEventListener('submit', function (e) {
        // Validation yap
        const firstNameValid = validateText(firstNameInput);
        const lastNameValid = validateText(lastNameInput);
        const emailValid = validateEmail();
        const passwordValid = validatePassword();

        const hasInvalidFields = !firstNameValid || !lastNameValid || !emailValid || !passwordValid;
        const termsAccepted = document.getElementById('termsCheck').checked;

        if (hasInvalidFields || !termsAccepted) {
            e.preventDefault();
            if (!termsAccepted) {
                alert('Lütfen kullanım şartlarını kabul edin!');
            }
            checkValidationSummary();
            return false;
        }

        // Form geçerliyse submit et
        submitBtn.disabled = true;
        submitBtn.querySelector('.btn-text').textContent = 'Kaydediliyor...';
    });

    // Sayfa yüklendiğinde validation summary'yi kontrol et
    checkValidationSummary();
});
