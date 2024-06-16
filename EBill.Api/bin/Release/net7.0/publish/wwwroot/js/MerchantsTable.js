$(document).ready(function () {
    var table = $("#merchantsTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ordering": false,
        //"bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "bAutoWidth": false ,
        "ajax": {
            "url": "/api/MerchantsApi/GetMerchants",
            "type": "POST",
            "datatype": "json"
        },
     
        "columns": [
            { "data": "srno", "name": "srno", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "phoneNo", "name": "phoneNo", "autoWidth": true },
            { "data": "address", "name": "address", "autoWidth": true },

            {
                data: "id",
                render: function (data, type, Customers) {
                    return "<a class='password', href='/Merchants/Details/" + Customers.id + "'>  <i class='fas fa-eye fa-sm'></i>View</a>";
                }
            },
            {
                data: "id",
                render: function (data, type, Customers) {
                    return "<a class='edit', href='/Merchants/Edit/" + Customers.id + "'>  <i class='fas fa-edit fa-sm'></i>Edit</a>";
                }
            },
            {
                data: "id",
                render: function (data) {
                  
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa-trash fa-sm'></i>Delete</a>";
                   
                }
            }
         
        ]
    });

    $("#merchantsTable").on("click", ".delete", function () {
        var a = $(this);

        bootbox.confirm("Are you sure you want to delete this Merchants ?", function (result) {
            if (result) {

                var inputData =  a.attr("data-customer-id");
                //alert(inputData);
                var requestData = JSON.stringify({ id: inputData });
                //alert(requestData);

                $.ajax({
                    url: "/api/MerchantsApi/deleteMerchants",
                    type: "POST",
                    contentType: "application/json",
                    data: requestData,
                    success: function (data) {
                        //alert(data);
                        alert("Delete Successful");
                        table.row(a.parents("tr")).remove().draw();
                    },
                    error: function () {
                        alert("Delete Fail");
                        //$("#result").text("An error occurred.");
                    }
                });
               
            }
        });
    });

});  

