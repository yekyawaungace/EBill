$(document).ready(function () {
    var table = $("#townShipCodesTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/api/TownShipCodesApi/GetTownShipCodes",
            "type": "POST",
            "datatype": "json"
        },
      /*  "columnDefs": [{
            "targets": [0],
            "visible": true,
            "searchable": false
        }],*/
        "columns": [
            /*  { "data": "id", "name": "Id", "autoWidth": true },*/
            {
                data: "codeEN",
                "name":"codeEN",
                render: function (data, type, Customers) {
                    return "<a style='color:blue', href='/TownShipCodes/edit/" + Customers.id + "'>" + Customers.codeEN + "</a>";
                }
            },
          /*  { "data": "codeEN", "name": "CodeEN", "autoWidth": true },*/
            { "data": "codeMM", "name": "CodeMM", "autoWidth": true },
            {
                data: "id",
                render: function (data) {
/*                    return "<button class='btn-link js-delete' data-customer-id=" + data + ">Delete</button>";*/
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa - trash fa - sm'></i>Delete</a>";
                   
                }
            }
         
          
          
            
        ]
    });

    $("#townShipCodesTable").on("click", ".delete", function () {
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

