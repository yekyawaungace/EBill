$(document).ready(function () {
    var table = $("#nRCTypesTable").DataTable({
        "processing": true,
        "serverSide": true,
        "paging": false,
        "searching": false,
        "ordering": false,
        "filter": false,
        "bInfo": false,
        "ajax": {
            "url": "/api/NRCTypesApi/GetNRCTypes",
            "type": "POST",
            "datatype": "json"
        },
   
        "columns": [
          
      
            { "data": "typeEN", "name": "typeEN", "autoWidth": true },
            { "data": "typeMM", "name": "typeMM", "autoWidth": true },
            {
                data: "id",
                render: function (data) {

                    return "<a class='delete' data-customer-id=" + data + ">  <i class='fas fa-trash fa-sm'></i>Delete</a>";
                   
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

