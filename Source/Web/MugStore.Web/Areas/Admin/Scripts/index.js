$(function () {
    var ctx = $('#priceChart');
    var labes = ctx.data('labels').split(',');
    var priceCustomer = ctx.data('price-customer').split(',');
    var priceSupplier = ctx.data('price-supplier').split(',');

    var priceChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labes,
            datasets: [{
                label: 'Price Customer',
                fill: false,
                lineTension: 0.1,
                backgroundColor: 'rgba(75,192,192,0.4)',
                borderColor: 'rgba(75,192,192,1)',
                borderCapStyle: 'butt',
                borderDash: [],
                borderDashOffset: 0.0,
                borderJoinStyle: 'miter',
                pointBorderColor: 'rgba(75,192,192,1)',
                pointBackgroundColor: '#fff',
                pointBorderWidth: 1,
                pointHoverRadius: 5,
                pointHoverBackgroundColor: 'rgba(75,192,192,1)',
                pointHoverBorderColor: 'rgba(220,220,220,1)',
                pointHoverBorderWidth: 2,
                pointRadius: 1,
                pointHitRadius: 10,
                data: priceCustomer,
                spanGaps: false,
            },
            {
                label: 'Price Supplier',
                fill: false,
                lineTension: 0.1,
                backgroundColor: 'rgba(167,127,249,0.4)',
                borderColor: 'rgba(167,127,249,1)',
                borderCapStyle: 'butt',
                borderDash: [],
                borderDashOffset: 0.0,
                borderJoinStyle: 'miter',
                pointBorderColor: 'rgba(167,127,249,1)',
                pointBackgroundColor: '#fff',
                pointBorderWidth: 1,
                pointHoverRadius: 5,
                pointHoverBackgroundColor: 'rgba(167,127,249,1)',
                pointHoverBorderColor: 'rgba(220,220,220,1)',
                pointHoverBorderWidth: 2,
                pointRadius: 1,
                pointHitRadius: 10,
                data: priceSupplier,
                spanGaps: false,
            }]
        },
        options: {
            yAxes: [{
                stacked: true
            }]
        }
    });

    $('.feedback-status').on('click', feedbackStatusClick);
    function feedbackStatusClick(e) {
        e.preventDefault();
        var id = $(this).data('id');
        var url = $('#feedback-section').data('url-status');

        $.ajax({ method: 'POST', url: url, data: { id: id }})
            .done(function (msg) {
                location.reload();
            });
    }

    $('.feedback-delete').on('click', feedbackDeleteClick);
    function feedbackDeleteClick(e) {
        e.preventDefault();
        var id = $(this).data('id');
        var url = $('#feedback-section').data('url-delete');

        var confirmDelete = confirm("Delete feedback?");
        if (confirmDelete !== true) {
            return;
        }

        $.ajax({ method: 'POST', url: url, data: { id: id } })
            .done(function (msg) {
                location.reload();
            });
    }

});