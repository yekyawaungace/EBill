$(document).ready(function () {
    var table = $("#transactionLogsTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/api/TransactionLogsApi/GetTransactionLogs",
            "type": "POST",
            "datatype": "json"
        },
      /*  "columnDefs": [{
            "targets": [0],
            "visible": true,
            "searchable": false
        }],*/
        "columns": [
            { "data": "username", "name": "username", "autoWidth": true },
            { "data": "tDateTime", "name": "tDateTime", "autoWidth": true },
            { "data": "events", "name": "events", "autoWidth": true },
            { "data": "formName", "name": "formName", "autoWidth": true },
            
          
            
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

