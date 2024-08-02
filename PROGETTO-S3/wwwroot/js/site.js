let firstPath = '/Cart/GetProcessedOrdersCount';
let secondPath = '/Cart/GetTotalIncome';

function getProcessedOrdersCount() {
    $.ajax({
        url: firstPath,
        method: 'GET',
        success: (data) => {
            const countElement = $('#processedOrdersCount');
            countElement.addClass('border border-dark px-2 fs-4');
            countElement.text(data);
        },
        error: (err) => {
            console.error('Error fetching processed orders count:', err);
            alert('Si è verificato un errore durante il recupero del conteggio degli ordini processati.');
        }
    });
}

function getTotalIncome() {
    let date = $('#dateInput').val();
    if (!date) {
        alert('Seleziona una data');
        return;
    }

    $.ajax({
        url: `${secondPath}?date=${encodeURIComponent(date)}`,
        method: 'GET',
        success: (data) => {
            const countElement = $('#totalIncome');
            countElement.addClass('border border-dark px-2 fs-4');
            countElement.text(`Incassi per il ${date}: €${data}`);
        },
        error: (err) => {
            console.error('Error fetching total income:', err);
            alert('Si è verificato un errore durante il recupero del totale incassi.');
        }
    });
}

$('#btnGetTotalIncome').on('click', () => {
    getTotalIncome();
});

$('#btnGetProcessedCount').on('click', () => {
    getProcessedOrdersCount();
});
