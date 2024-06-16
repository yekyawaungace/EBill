$(document).ready(function () {
    var receivedDate = new Date(0); // You can set the desired date here
    var minimumDate = "0001-01-01T00:00:00.000Z";
    // Convert the JavaScript date to a string in the "yyyy-MM-dd" format
    var formattedDate = receivedDate.toISOString().split('T')[0];
    var table = $('#applicationsTable').DataTable({
       /* "paging": false,*/
        "searching": false,
        "ordering": false,
        "filter": false,
        ajax: {
            url: '/api/ApplicationsApi/GetApplications',
            data: function (d) {
               
                d.applicatonId = $('#applicationIdInput').val() == '' ? '-' : $('#applicationIdInput').val(),
                    d.merchantName = $('#merchantNameInput').val() == '' ? '-' : $('#merchantNameInput').val(),
                    d.name = $('#nameInput').val() == '' ? '-' : $('#nameInput').val(),
                    d.receivedDate = $("#receivedDateInput").val() == '' ? minimumDate : $("#receivedDateInput").val(),
                    d.startDate = $("#startDateInput").val() == '' ? minimumDate : $("#startDateInput").val(),
                    d.endDate = $("#endDateInput").val() == '' ? minimumDate : $("#endDateInput").val(),
                /*d.endosement = ($('#Endosementyes').is(':checked')) ? 1 : 0,*/
                    d.endosement = ($('#Endosementyes').is(':checked') == false && $('#Endosementno').is(':checked') == false) ? '-' : ($('#Endosementyes').is(':checked')) ? 1 : 0,
                    d.paymentStatus = ($('#paid').is(':checked') == false && $('#unpaid').is(':checked') == false) ? '-' : 
                    $('#paid').is(':checked') == true ? 1 : 0;
              

            },
            dataSrc: 'data'
        },
        "columnDefs": [
            {
                "targets": 5,    // column index, 0 is the first column
                "type": "date",
                "render": function (data) {
                    // US English uses month-day-year order
                    var date = new Date(data);
                    return date.toLocaleDateString('en-US'); // 4/25/2018
                }
            },
            {
                "targets": 6,    // column index, 0 is the first column
                "type": "date",
                "render": function (data) {
                    // US English uses month-day-year order
                    var date = new Date(data);
                    return date.toLocaleDateString('en-US'); // 4/25/2018
                }
            }

        ],
        columns: [
            { data: 'srno' },
            { data: 'orderNo' },
            { data: 'noofPeople' },
            { data: 'contactPhone' },
            { data: 'address' },
            { data: 'startDate' },
            { data: 'endDate' },
            { data: 'secondContactPerson' },
            { data: 'secondContactPhone' },
            { data: 'paymentStatus' },
            { data: 'merchantname' },
            

            {
                data: "id",
                render: function (data, type, Customers) {
                    return "<a class='password', href='/Travellers/Index/" + Customers.id + "'><i class='fas fa-user fa-sm'></i>People</a>";

                }
            },
            {
                data: "id",
                render: function (data, type, Customers) {
                    return "<a class='edit', href='/Applications/ApplicationViewDetail/" + Customers.id + "'><i class='fas fa-eye fa-sm'></i>View</a>";


                }
            }

        ]
    });


    $("#applicationsTable").on("click", ".delete", function () {
        var a = $(this);

        bootbox.confirm("Are you sure you want to delete this TownShipCode?", function (result) {
            if (result) {

                var inputData = a.attr("data-customer-id");
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

    $('#filterButton').on('click', function () {
        //alert("click");
        table.ajax.reload();
    });
    
});

