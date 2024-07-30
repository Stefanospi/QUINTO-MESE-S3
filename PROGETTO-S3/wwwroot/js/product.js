$(document).ready(function () {
    function loadProducts() {
        $.ajax({
            url: '/Products/ProductList',
            method: 'GET',
            beforeSend: function () {
                $('#loadingIndicator').show(); // Mostra l'indicatore di caricamento
            },
            success: function (data) {
                $('#productList').html(data); // Aggiorna il contenuto della lista dei prodotti
            },
            error: function (error) {
                alert('Error loading products: ' + error.responseText);
            },
            complete: function () {
                $('#loadingIndicator').hide(); // Nasconde l'indicatore di caricamento
            }
        });
    }

    // Carica la lista dei prodotti all'avvio
    loadProducts();
});