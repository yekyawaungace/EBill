$(document).ready(function () {
    var table = $("#transactionLogsTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ordering": false,
        //"bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "bAutoWidth": false,
        "ajax": {
            "url": "/api/TransactionLogsApi/GetTransactionLogs",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            {
                "targets": 6,    // column index, 0 is the first column
                "type": "date",
                "render": function (data) {
                    // US English uses month-day-year order
                    var date = new Date(data);
                    return date.toLocaleDateString('en-US'); // 4/25/2018
                }
            }

        ],
        "columns": [
            { "data": "invoiceNo", "name": "invoiceNo", "autoWidth": true },
            { "data": "cardNo", "name": "cardNo", "autoWidth": true },
            { "data": "amount", "name": "amount", "autoWidth": true },
            { "data": "currencyCode", "name": "currencyCode", "autoWidth": true },
            { "data": "tranRef", "name": "tranRef", "autoWidth": true },
            { "data": "referenceNo", "name": "referenceNo", "autoWidth": true },
            { "data": "transactionDateTime", "name": "transactionDateTime", "autoWidth": true },
            { "data": "respCode", "name": "respCode", "autoWidth": true },
            { "data": "respDesc", "name": "respDesc", "autoWidth": true },
            { "data": "paymentID", "name": "paymentID", "autoWidth": true },
        ]
    });

    $("#transactionLogsTable").on("click", ".delete", function () {
        var a = $(this);

        bootbox.confirm("Are you sure you want to delete this TownShipCode?", function (result) {
            if (result) {

                var inputData =  a.attr("data-customer-id");
                //alert(inputData);
                var requestData = JSON.stringify({ id: inputData });
                //alert(requestData);

                $.ajax({
                    url: "/api/TownShipCodesApi/deleteTownShipCodes",
                    type: "POST",
                    contentType: "application/json",
                    data: requestData,
                    success: function (data) {
                       // alert(data);
                        table.row(a.parents("tr")).remove().draw();
                    },
                    error: function () {
                        //$("#result").text("An error occurred.");
                    }
                });
               
            }
        });
    });

});  

