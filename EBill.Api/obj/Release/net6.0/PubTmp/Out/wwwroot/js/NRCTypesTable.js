$(document).ready(function () {
    var table = $("#nRCTypesTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/api/NRCTypesApi/GetNRCTypes",
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
                data: "typeEN",
                "name":"typeEN",
                render: function (data, type, Customers) {
                    return "<a style='color:blue', href='/NRCTypes/edit/" + Customers.id + "'>" + Customers.typeEN + "</a>";
                }
            },
            { "data": "typeMM", "name": "typeMM", "autoWidth": true },
            {
                data: "id",
                render: function (data) {
/*                    return "<button class='btn-link js-delete' data-customer-id=" + data + ">Delete</button>";*/
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa - trash fa - sm'></i>Delete</a>";
                   
                }
            }
         
          
          
            
        ]
    });

    $("#nRCTypesTable").on("click", ".delete", function () {
        var a = $(this);

        bootbox.confirm("Are you sure you want to delete this NRCTypes?", function (result) {
            if (result) {

                var inputData =  a.attr("data-customer-id");
                //alert(inputData);
                var requestData = JSON.stringify({ id: inputData });
                //alert(requestData);

                $.ajax({
                    url: "/api/NRCTypesApi/deleteNRCTypes",
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

