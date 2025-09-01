<script>
        // Smooth scroll ve animasyonlar için basit JavaScript
    document.addEventListener('DOMContentLoaded', function() {
            // Container'a fade-in animasyonu ekle
            const container = document.querySelector('.container');
    container.style.opacity = '0';
    container.style.transform = 'translateY(30px)';
            
            setTimeout(() => {
        container.style.transition = 'all 0.6s ease';
    container.style.opacity = '1';
    container.style.transform = 'translateY(0)';
            }, 100);
        });

    // Konsola güvenlik mesajı
    console.log('%c🔒 Güvenlik Uyarısı', 'color: #ff6b6b; font-size: 20px; font-weight: bold;');
    console.log('%cBu sayfa yetkisiz erişime karşı korunmaktadır.', 'color: #666; font-size: 14px;');
</script>