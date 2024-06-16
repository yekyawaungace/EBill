$(document).ready(function () {
    var table = $("#applicationsTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ajax": {
            "url": "/api/ApplicationsApi/GetApplications",
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
                data: "orderNo",
                "name":"orderNo",
                render: function (data, type, Customers) {
                    return "<a style='color:blue', href='/Travellers/Index/" + Customers.id + "'>" + Customers.orderNo + "</a>";
                }
            },
            { "data": "noofPeople", "name": "noofPeople", "autoWidth": true },
            { "data": "contactPhone", "name": "contactPhone", "autoWidth": true },
            { "data": "address", "name": "address", "autoWidth": true },
            { "data": "startDate", "name": "startDate", "autoWidth": true },
            { "data": "endDate", "name": "endDate", "autoWidth": true },
            { "data": "secondContactPerson", "name": "secondContactPerson", "autoWidth": true },
            { "data": "secondContactPhone", "name": "secondContactPhone", "autoWidth": true },
            { "data": "paymentStatus", "name": "paymentStatus", "autoWidth": true },
            {
                data: "id",
                render: function (data) {
/*                    return "<button class='btn-link js-delete' data-customer-id=" + data + ">Delete</button>";*/
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa - trash fa - sm'></i>Delete</a>";
                   
                }
            }
         
        ]
    });

    $("#applicationsTable").on("click", ".delete", function () {
        var a = $(this);

        bootbox.confirm("Are you sure you want to delete this Application  ?", function (result) {
            if (result) {

                var inputData =  a.attr("data-customer-id");
                //alert(inputData);
                var requestData = JSON.stringify({ id: inputData });
                //alert(requestData);

                $.ajax({
                    url: "/api/ApplicationsApi/deleteApplications",
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

