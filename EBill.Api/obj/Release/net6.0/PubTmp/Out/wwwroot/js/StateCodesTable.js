﻿$(document).ready(function () {
    var table = $("#stateCodesTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/api/StateCodesApi/GetStateCodes",
            "type": "POST",
            "datatype": "json"
        },
      /*  "columnDefs": [{
            "targets": [0],
            "visible": true,
            "searchable": false
        }],*/
        "columns": [
          
            {
                data: "stateCodeEN",
                "name":"stateCodeEN",
                render: function (data, type, Customers) {
                    return "<a style='color:blue', href='/StateCodes/edit/" + Customers.id + "'>" + Customers.stateCodeEN + "</a>";
                }
            },
          /*  { "data": "codeEN", "name": "CodeEN", "autoWidth": true },*/
            { "data": "stateCodeMM", "name": "stateCodeMM", "autoWidth": true },
            {
                data: "id",
                render: function (data) {
/*                    return "<button class='btn-link js-delete' data-customer-id=" + data + ">Delete</button>";*/
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa - trash fa - sm'></i>Delete</a>";
                   
                }
            }
         
          
          
            
        ]
    });

    $("#stateCodesTable").on("click", ".delete", function () {
        var a = $(this);

        bootbox.confirm("Are you sure you want to delete this StateCodes?", function (result) {
            if (result) {

                var inputData =  a.attr("data-customer-id");
                //alert(inputData);
                var requestData = JSON.stringify({ id: inputData });
                //alert(requestData);

                $.ajax({
                    url: "/api/StateCodesApi/deleteStateCodes",
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

