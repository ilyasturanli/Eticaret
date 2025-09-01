using BCrypt.Net;

namespace Eticaret.Core.Helpers
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Şifreyi hash'ler
        /// </summary>
        /// <param name="password">Hash'lenecek şifre</param>
        /// <returns>Hash'lenmiş şifre</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Şifre boş olamaz", nameof(password));

            // BCrypt ile şifreyi hash'le (workFactor: 12 - güvenlik ve performans dengesi)
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        /// <summary>
        /// Şifre doğrulaması yapar
        /// </summary>
        /// <param name="password">Kontrol edilecek şifre</param>
        /// <param name="hashedPassword">Hash'lenmiş şifre</param>
        /// <returns>Şifre doğru mu?</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // Hash formatı geçersiz
                return false;
            }
        }

        /// <summary>
        /// Şifre güvenlik kontrolü yapar
        /// </summary>
        /// <param name="password">Kontrol edilecek şifre</param>
        /// <returns>Şifre güvenli mi?</returns>
        public static bool IsPasswordSecure(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            // En az 6 karakter
            if (password.Length < 6)
                return false;

            // En az bir büyük harf
            if (!password.Any(char.IsUpper))
                return false;

            // En az bir küçük harf
            if (!password.Any(char.IsLower))
                return false;

            // En az bir rakam
            if (!password.Any(char.IsDigit))
                return false;

            return true;
        }

        /// <summary>
        /// Şifre güvenlik mesajı döner
        /// </summary>
        /// <param name="password">Kontrol edilecek şifre</param>
        /// <returns>Güvenlik mesajı</returns>
        public static string GetPasswordSecurityMessage(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "Şifre boş olamaz";

            var messages = new List<string>();

            if (password.Length < 6)
                messages.Add("En az 6 karakter olmalı");

            if (!password.Any(char.IsUpper))
                messages.Add("En az bir büyük harf içermeli");

            if (!password.Any(char.IsLower))
                messages.Add("En az bir küçük harf içermeli");

            if (!password.Any(char.IsDigit))
                messages.Add("En az bir rakam içermeli");

            if (messages.Count == 0)
                return "Şifre güvenli ✓";

            return string.Join(", ", messages);
        }
    }
}
