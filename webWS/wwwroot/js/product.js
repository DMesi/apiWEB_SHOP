var dataTable;

$(document).ready(function () {
  
    loadDataTable();
    loadCategory();
});

function loadDataTable() {
    $('#tblData').DataTable().clear();
    $('#tblData').DataTable().destroy();
    dataTable = $('#tblData').DataTable({
        "ajax": {
           "url": "/Products/GetAllProducts",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "price", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {

                    return `<div class="text-center">
                                <a href="/Products/Upsert/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("/Products/Delete/${data}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                                </div>
                            `;
                }, "width": "30%"
            }
        ]
    });
}

function loadDataTableCategory(id) {
    $('#tblData').DataTable().clear();
    $('#tblData').DataTable().destroy();

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Products/GetAllProductslByCategory/?category="+id,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "price", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {

                    return `<div class="text-center">
                                <a href="/Products/Upsert/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("/Products/Delete/${data}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                                </div>
                            `;
                }, "width": "30%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}


// "url": "/Products/CategoryMeni",

function loadCategory() {


    $.ajax({
        type: 'GET',
        url: 'https://localhost:44365/api/Products/categoryMeni',
        dataType: "json",
        success: function (result) {
            if (result.length > 0) {

                var count = 0;

                $(result).each(function (i, val) {
       
                    count += val.count;

                });

                $("#category").append("<a href='javascript:loadDataTable()'/>SVI PROIZVODI (" + count + ") </a><br/><HR>");


                $(result).each(function (i, val) {
                
                    $("#category").append("<a href='javascript:loadDataTableCategory(" + val.id + ")'/>" + val.name + "(" + val.count + ") </a><br/>");
                   
                });
            }
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });






}