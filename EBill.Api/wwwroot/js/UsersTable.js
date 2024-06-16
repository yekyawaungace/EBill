﻿$(document).ready(function () {
    var table = $("#usersTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "processing": true,
        "ordering": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "bAutoWidth": false,
        "ajax": {
            "url": "/api/UsersApi/GetUsers",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "srno", "name": "srno", "autoWidth": true },
            { "data": "userName", "name": "userName", "autoWidth": true },
            { "data": "email", "name": "email", "autoWidth": true },
            { "data": "role", "name": "role", "autoWidth": true },
            {
                data: "id",
                render: function (data, type, Customers) {
              
                    return "<a class='edit', href='/Users/edit/" + Customers.id + "'>  <i class='fas fa-edit fa-sm'></i>Edit</a>";
                   
                }
            },
            {
                data: "id",
                render: function (data) {

                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa-trash fa-sm'></i>Delete</a>";

                }
            },
            {
                data: "id",
                render: function (data, type, Customers) {

                    return "<a class='password', href='/Users/changePassword/" + Customers.id + "'>  <i class='fas fa-key fa-sm'></i> Change Password</a>";

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

