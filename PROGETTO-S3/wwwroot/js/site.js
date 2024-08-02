let firstPath = '/Order/GetProcessedOrdersCount';

let secondPath = '/Order/GetTotalIncome';
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
        }
    });
}

function getTotalIncome() {
    $.ajax({
        url: secondPath,
        method: 'GET',
        success: (data) => {
            const countElement = $('#totalIncome');
            countElement.addClass('border border-dark px-2 fs-4');
            countElement.text(data + "€");
        },
        error: (err) => {
            console.error('Error fetching total income:', err);
        }
    });
}


$('#btnGetProcessedCount').on('click', () => {
    getProcessedOrdersCount();
});

$('#btnGetTotalIncome').on('click', () => {
    getTotalIncome();
});