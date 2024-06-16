$(document).ready(function () {

    var table = $('#townShipCodesTable').DataTable({
        "paging": false,
        "searching": false,
        "ordering": false,
        "filter": false,
        ajax: {
            url: '/api/TownShipCodesApi/GetTownShipCodes',
            data: function (d) {
                d.departmentId = $('#stateCodesDropdown').val();
            },
            dataSrc: 'data'
        },
        columns: [
            { data: 'srno' },
            { data: 'codeEN' },
            { data: 'codeMM' },
            { data: 'statecode' },
            {
                data: "id",
                render: function (data, type, Customers) {
                    return "<a class='edit', href='/TownShipCodes/edit/" + Customers.id + "'>  <i class='fas fa-edit fa-sm'></i>Edit</a>";

                }
            },
            {
                data: "id",
                render: function (data, type, Customers) {
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa-trash fa-sm'></i>Delete</a>";

                }
            }
            
        ]
    });

  /*  var table = $("#townShipCodesTable").DataTable({
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
            "url": "/api/TownShipCodesApi/GetTownShipCodes",
            "type": "POST",
            "datatype": "json"
        },
     
        "columns": [
            { "data": "srno", "name": "srno", "autoWidth": true },
            { "data": "codeEN", "name": "CodeEN", "autoWidth": true },
            { "data": "codeMM", "name": "CodeMM", "autoWidth": true },
            { "data": "statecode", "name": "statecode", "autoWidth": true },
            {
                data: "id",
                render: function (data, type, Customers) {
                    return "<a class='edit', href='/TownShipCodes/edit/" + Customers.id + "'>  <i class='fas fa-edit fa-sm'></i>Edit</a>";
                   
                }
            },
            {
                data: "id",
                render: function (data) {
                  
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa-trash fa-sm'></i>Delete</a>";

                }
            }
        ]
    });*/

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

    $('#stateCodesDropdown').on('change', function () {
        // Reload DataTable based on the selected department
        table.ajax.reload();
    });
});  

