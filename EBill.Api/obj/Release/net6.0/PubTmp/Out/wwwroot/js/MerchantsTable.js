$(document).ready(function () {
    var table = $("#merchantsTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/api/MerchantsApi/GetMerchants",
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
                data: "name",
                "name":"name",
                render: function (data, type, Customers) {
                    return "<a style='color:blue', href='/Merchants/Edit/" + Customers.id + "'>" + Customers.name + "</a>";
                }
            },
       
          
            { "data": "phoneNo", "name": "phoneNo", "autoWidth": true },
            { "data": "address", "name": "address", "autoWidth": true },
            {
                data: "id",
                render: function (data, type, Customers) {
                    return "<a style='color:blue', href='/Merchants/Details/" + Customers.id + "'> <i class='fas fa - trash fa - sm'></i>View</a>";
                }
            },
            {
                data: "id",
                render: function (data) {
/*                    return "<button class='btn-link js-delete' data-customer-id=" + data + ">Delete</button>";*/
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa - trash fa - sm'></i>Delete</a>";
                   
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

