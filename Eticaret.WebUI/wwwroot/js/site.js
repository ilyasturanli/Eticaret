// Favori sistemi
document.addEventListener('DOMContentLoaded', function() {
    console.log('Favori sistemi yüklendi');
    
    // Favori butonlarına tıklama olayı
    document.addEventListener('click', function(e) {
        console.log('Click event tetiklendi:', e.target);
        
        if (e.target.closest('.favorite-btn') || e.target.closest('.favorite-badge')) {
            e.preventDefault();
            e.stopPropagation();
            
            const button = e.target.closest('.favorite-btn') || e.target.closest('.favorite-badge');
            const productId = button.getAttribute('data-product-id');
            const isFavorite = button.getAttribute('data-is-favorite') === 'true';
            
            console.log('Favori butonuna tıklandı:', { 
                productId, 
                isFavorite, 
                button: button,
                buttonClass: button.className 
            });
            
            toggleFavorite(productId, button);
        }
    });
    
    // Sepet ekleme formları için event listener
    document.addEventListener('submit', function(e) {
        if (e.target.classList.contains('cart-form')) {
            e.preventDefault();
            
            const form = e.target;
            const productId = form.getAttribute('data-product-id');
            const formData = new FormData(form);
            
            console.log('Sepete ekleme formu gönderildi:', { productId, formData });
            
            // Form verilerini gönder
            fetch('/Cart/Add', {
                method: 'POST',
                body: formData
            })
            .then(response => {
                if (response.ok) {
                    // Sepet sayısını güncelle
                    updateCartCount();
                    
                    // Başarı mesajı göster
                    showNotification('Ürün sepete eklendi!', 'success');
                    
                    // Buton metnini güncelle
                    const button = form.querySelector('button');
                    const originalText = button.innerHTML;
                    button.innerHTML = '<i class="fas fa-check me-2"></i>Eklendi!';
                    button.classList.remove('btn-primary');
                    button.classList.add('btn-success');
                    
                    // 2 saniye sonra eski haline döndür
                    setTimeout(() => {
                        button.innerHTML = originalText;
                        button.classList.remove('btn-success');
                        button.classList.add('btn-primary');
                    }, 2000);
                } else {
                    showNotification('Ürün sepete eklenirken hata oluştu', 'error');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                showNotification('Bir hata oluştu', 'error');
            });
        }
    });
    
    function toggleFavorite(productId, button) {
        console.log('toggleFavorite fonksiyonu çağrıldı:', { productId, button });
        
        // Butonu devre dışı bırak
        button.style.pointerEvents = 'none';
        button.style.opacity = '0.6';
        
        console.log('ToggleFavorite çağrılıyor, ProductId:', productId);
        
        // CSRF token'ı al
        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        console.log('CSRF Token:', token);
        
        fetch('/Favorites/ToggleFavorite', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': token || ''
            },
            body: JSON.stringify({ productId: parseInt(productId) })
        })
        .then(response => {
            console.log('Response status:', response.status);
            console.log('Response headers:', response.headers);
            return response.json();
        })
        .then(data => {
            console.log('Response data:', data);
            if (data.success) {
                // Kalp ikonunu güncelle
                const heartIcon = button.querySelector('i');
                if (data.isFavorite) {
                    heartIcon.classList.remove('text-muted');
                    heartIcon.classList.add('text-white');
                    button.setAttribute('data-is-favorite', 'true');
                    // Favori eklendiğinde mavi arka plan
                    button.style.background = '#007bff';
                    button.style.boxShadow = '0 4px 15px rgba(0, 123, 255, 0.4)';
                } else {
                    heartIcon.classList.remove('text-white');
                    heartIcon.classList.add('text-white');
                    button.setAttribute('data-is-favorite', 'false');
                    // Favori kaldırıldığında kırmızı arka plan
                    button.style.background = '#dc3545';
                    button.style.boxShadow = '0 4px 15px rgba(220, 53, 69, 0.3)';
                }
                
                // Header'daki favori sayısını güncelle
                updateFavoriteCount(data.count);
                
                // Sepet sayısını da güncelle (eğer favori ürün sepette varsa)
                updateCartCount();
                
                // Başarı mesajı göster
                showNotification(data.message, 'success');
                
                // Click animasyonu
                button.classList.add('clicked');
                setTimeout(() => {
                    button.classList.remove('clicked');
                }, 600);
                
                // Hover animasyonu
                button.style.transform = 'scale(1.2)';
                setTimeout(() => {
                    button.style.transform = 'scale(1)';
                }, 200);
            } else {
                showNotification(data.message, 'error');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showNotification('Bir hata oluştu', 'error');
        })
        .finally(() => {
            // Butonu tekrar aktif et
            button.style.pointerEvents = 'auto';
            button.style.opacity = '1';
        });
    }
    
    function updateFavoriteCount(count) {
        const favoriteCountElement = document.querySelector('.favorite-count');
        if (favoriteCountElement) {
            favoriteCountElement.textContent = count;
            
            // Animasyon efekti
            favoriteCountElement.style.transform = 'scale(1.2)';
            setTimeout(() => {
                favoriteCountElement.style.transform = 'scale(1)';
            }, 200);
        }
    }
    
    function updateCartCount() {
        fetch('/Cart/GetCartCount')
            .then(response => response.json())
            .then(data => {
                const cartBadge = document.querySelector('.cart-count');
                if (cartBadge) {
                    cartBadge.textContent = data.count;
                    
                    // Animasyon efekti
                    cartBadge.style.transform = 'scale(1.2)';
                    setTimeout(() => {
                        cartBadge.style.transform = 'scale(1)';
                    }, 200);
                }
            })
            .catch(error => console.error('Sepet sayısı güncellenirken hata:', error));
    }
    
    function showNotification(message, type) {
        // Basit bir bildirim sistemi
        const notification = document.createElement('div');
        notification.className = `alert alert-${type === 'success' ? 'success' : 'danger'} position-fixed`;
        notification.style.cssText = `
            top: 20px;
            right: 20px;
            z-index: 9999;
            min-width: 300px;
            animation: slideIn 0.3s ease;
        `;
        notification.textContent = message;
        
        document.body.appendChild(notification);
        
        // 3 saniye sonra kaldır
        setTimeout(() => {
            notification.style.animation = 'slideOut 0.3s ease';
            setTimeout(() => {
                if (notification.parentNode) {
                    notification.parentNode.removeChild(notification);
                }
            }, 300);
        }, 1000);// favorilere eklendi yada sepete eklendi yazısının saniyesini düsürme...
    }
});

// CSS animasyonları için style ekle
const style = document.createElement('style');
style.textContent = `
    @keyframes slideIn {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    @keyframes slideOut {
        from {
            transform: translateX(0);
            opacity: 1;
        }
        to {
            transform: translateX(100%);
            opacity: 0;
        }
    }
    
    .favorite-btn:disabled,
    .favorite-badge:disabled {
        opacity: 0.6;
        cursor: not-allowed;
    }
    
    .favorite-count {
        transition: transform 0.2s ease;
    }
    
    .favorite-badge.clicked {
        animation: heartBeat 0.6s ease;
    }
    
    @keyframes heartBeat {
        0% { transform: scale(1); }
        14% { transform: scale(1.3); }
        28% { transform: scale(1); }
        42% { transform: scale(1.3); }
        70% { transform: scale(1); }
    }
`;
document.head.appendChild(style);