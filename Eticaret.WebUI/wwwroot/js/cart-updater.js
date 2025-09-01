// Global sepet sayısı güncelleyici
function updateCartCount() {
    fetch('/Cart/GetCartCount')
        .then(response => response.json())
        .then(data => {
            const cartBadge = document.querySelector('.cart-count');
            if (cartBadge) {
                cartBadge.textContent = data.count;
            } else {
                // Badge yoksa oluştur
                const cartLink = document.querySelector('a[href="/Cart"]');
                if (cartLink) {
                    const newBadge = document.createElement('span');
                    newBadge.className = 'position-absolute top-0 start-100 translate-middle badge rounded-pill bg-primary cart-count';
                    newBadge.textContent = data.count;
                    cartLink.appendChild(newBadge);
                }
            }
        })
        .catch(error => console.error('Sepet sayısı güncellenirken hata:', error));
}

// Sayfa yüklendiğinde sepet sayısını güncelle
document.addEventListener('DOMContentLoaded', function() {
    updateCartCount();
});

// Global olarak erişilebilir yap
window.updateCartCount = updateCartCount;
