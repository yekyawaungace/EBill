$(document).ready(function () {
    var table = $("#premiumFeesTable").DataTable({
        "processing": true,
        "serverSide": true,
        "paging": false,
        "searching": false,
        "ordering": false,
        "filter": false,
        "bInfo": false,
        "ajax": {
            "url": "/api/PremiumFeesApi/GetPremiumFees",
            "type": "POST",
            "datatype": "json"
        },
    
        "columns": [
          
            { "data": "duration", "name": "duration", "autoWidth": true },
            { "data": "fee", "name": "fee", "autoWidth": true },
            {
                data: "id",
                render: function (data) {
                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa-trash fa-sm'></i>Delete</a>";

                }
            }
         
          
          
            
        ]
    });

    $("#premiumFeesTable").on("click", ".delete", function () {
        var a = $(this);

        bootbox.confirm("Are you sure you want to delete this Premium Fees?", function (result) {
            if (result) {

                var inputData =  a.attr("data-customer-id");
                //alert(inputData);
                var requestData = JSON.stringify({ id: inputData });
                //alert(requestData);

                $.ajax({
                    url: "/api/PremiumFeesApi/deletePremiumFees",
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

