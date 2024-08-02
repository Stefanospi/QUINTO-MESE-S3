
let firstPath = '/Cart/GetProcessedOrdersCount';
let secondPath = '/Cart/GetTotalIncome';

function GetProcessedOrders() {
    $.ajax({
        url: firstPath,
        method: 'GET',
        success: (data) => {
            const countElem = $('#processedOrdersCount');
            countElem.addClass('border border-dark px-2 fs-4');
            countElem.text(data);
        },
        error: (err) => {
            console.error('Error fetching processed orders count', err);
        }
    });
}

$('#btnGetProcessedCount').on('click', () => {
    GetProcessedOrders();
});


function GetTotalIncome(date) {
    $.ajax({
        url: secondPath,
        method: 'GET',
        data: { date: date.toISOString() },
        success: (data) => {
            const incomeElem = $('#totalIncome');
            incomeElem.addClass('border border-dark px-2 fs-4');
            incomeElem.text(`Total Income: ${data}`);
        },
        error: (err) => {
            console.error('Error fetching total income', err);
        }
    });
}

$('#btnGetTotalIncome').on('click', () => {
    const dateValue = $('#dateInput').val();
    if (dateValue) {
        const date = new Date(dateValue);
        GetTotalIncome(date);
    } else {
        alert('Please select a date');
    }
});


