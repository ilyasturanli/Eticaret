
    document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('form[asp-action="SignUp"]');
    const submitBtn = form.querySelector('button[type="submit"]');

    const emailInput = document.getElementById('floatingEmail');
    const passwordInput = document.getElementById('floatingPassword');
    const confirmPasswordInput = document.getElementById('floatingConfirmPassword');

    const togglePassword = document.getElementById('togglePassword');
    const toggleConfirmPassword = document.getElementById('toggleConfirmPassword');

    // Password visibility toggle
    togglePassword.addEventListener('click', function () {
        const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
    passwordInput.setAttribute('type', type);

    const icon = this.querySelector('i');
    icon.classList.toggle('fa-eye');
    icon.classList.toggle('fa-eye-slash');
    });

    toggleConfirmPassword.addEventListener('click', function () {
        const type = confirmPasswordInput.getAttribute('type') === 'password' ? 'text' : 'password';
    confirmPasswordInput.setAttribute('type', type);

    const icon = this.querySelector('i');
    icon.classList.toggle('fa-eye');
    icon.classList.toggle('fa-eye-slash');
    });

    // Input validation
    emailInput.addEventListener('blur', validateEmail);
    passwordInput.addEventListener('blur', validatePassword);
    confirmPasswordInput.addEventListener('blur', validateConfirmPassword);

    emailInput.addEventListener('input', clearValidation);
    passwordInput.addEventListener('input', clearValidation);
    confirmPasswordInput.addEventListener('input', clearValidation);

    function validateEmail() {
        const email = emailInput.value;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!email) {
        setInvalid(emailInput, 'Email adresi gereklidir.');
        } else if (!emailRegex.test(email)) {
        setInvalid(emailInput, 'Geçerli bir email adresi girin.');
        } else {
        setValid(emailInput, 'Email adresi geçerli.');
        }
    }

    function validatePassword() {
        const password = passwordInput.value;

    if (!password) {
        setInvalid(passwordInput, 'Şifre gereklidir.');
        } else if (password.length < 6) {
        setInvalid(passwordInput, 'Şifre en az 6 karakter olmalıdır.');
        } else {
        setValid(passwordInput, 'Şifre geçerli.');
        }
    }

    function validateConfirmPassword() {
        const password = passwordInput.value;
    const confirm = confirmPasswordInput.value;

    if (!confirm) {
        setInvalid(confirmPasswordInput, 'Şifre tekrar gereklidir.');
        } else if (password !== confirm) {
        setInvalid(confirmPasswordInput, 'Şifreler eşleşmiyor.');
        } else {
        setValid(confirmPasswordInput, 'Şifreler eşleşiyor.');
        }
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
        [emailInput, passwordInput, confirmPasswordInput].forEach(input => {
            input.classList.remove('is-valid', 'is-invalid');
            const feedback = input.parentNode.querySelector('.invalid-feedback, .valid-feedback');
            if (feedback) feedback.remove();
        });
    }

    // Form submission
    form.addEventListener('submit', function (e) {
        e.preventDefault();

    validateEmail();
    validatePassword();
    validateConfirmPassword();

    const isValid =
    !emailInput.classList.contains('is-invalid') &&
    !passwordInput.classList.contains('is-invalid') &&
    !confirmPasswordInput.classList.contains('is-invalid') &&
    emailInput.value &&
    passwordInput.value &&
    confirmPasswordInput.value;

    if (isValid) {
        submitBtn.classList.add('btn-loading');
    submitBtn.disabled = true;

            // Simulate signup process
            setTimeout(() => {
        submitBtn.classList.remove('btn-loading');
    submitBtn.classList.add('btn-success-anim');
    submitBtn.querySelector('.btn-text').textContent = 'Başarılı!';

                setTimeout(() => {
        alert('Kayıt başarılı! (Demo)');
    form.reset();
    clearValidation();
    submitBtn.classList.remove('btn-success-anim');
    submitBtn.querySelector('.btn-text').textContent = 'Kayıt Ol';
    submitBtn.disabled = false;
                }, 1500);
            }, 2000);
        }
    });
});

