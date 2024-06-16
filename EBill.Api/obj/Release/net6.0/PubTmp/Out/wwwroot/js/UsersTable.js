$(document).ready(function () {
    var table = $("#usersTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/api/UsersApi/GetUsers",
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
                data: "userName",
                "name":"userName",
                render: function (data, type, Customers) {
                    return "<a style='color:blue', href='/Users/edit/" + Customers.id + "'>" + Customers.userName + "</a>";
                }
            },
        
            { "data": "email", "name": "email", "autoWidth": true },
            {
                data: "id",
                render: function (data) {
              
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa - trash fa - sm'></i>Delete</a>";
                   
                }
            }
         
          
          
            
        ]
    });

    $("#usersTable").on("click", ".delete", function () {
        var a = $(this);

        bootbox.confirm("Are you sure you want to delete this Users?", function (result) {
            if (result) {

                var inputData =  a.attr("data-customer-id");
                //alert(inputData);
                var requestData = JSON.stringify({ id: inputData });
                //alert(requestData);

                $.ajax({
                    url: "/api/UsersApi/deleteUsers",
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

