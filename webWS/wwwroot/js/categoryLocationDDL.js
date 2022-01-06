$(document).ready(function () {

    loadCategoryDDL();
    loadLocationDDL();
});


function loadCategoryDDL() {
  
    var categoryID = $("#Id_categories").val();

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44365/api/Products/categoryMeni',
        dataType: "json",
        success: function (result) {

            if (result.length > 0) {

                $('#Category').append("<option value=''>---</option>");

                $(result).each(function (i, val) {

                   $('#Category').append("<option value='" + val.id + "'>" + val.name + "</option>");

                    $("#Category option[value=" + categoryID + "]").prop("selected", true);

                   
                });
            }
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });

  
 

}

function loadLocationDDL() {

    var locationID = $("#Id_location").val();

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44365/api/Locations',
        dataType: "json",
        success: function (result) {

            if (result.length > 0) {

                $('#Location').append("<option value=''>---</option>");


                $(result).each(function (i, val) {

                    $('#Location').append("<option value='" + val.id + "'>" + val.name_location + "</option>");

                    $("#Location option[value=" + locationID + "]").prop("selected", true);

                });
            }
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

function categoryID() {

    var id = $('#Category option:selected').val();

    $('#Id_categories').val(id);

}

function locationID() {

    var id = $('#Location option:selected').val();

    $('#Id_location').val(id);

}