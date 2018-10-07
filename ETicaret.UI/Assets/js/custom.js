getCartDropdown();

//QUICKVIEW MODAL
$('#quickViewModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('whatever');

    $.ajax({
        type: "POST",
        url: "/Home/QuickView",
        data: '{id:"' + recipient + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $('#quickViewModal').html(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
});

$('.body').on('click', '.btn-remove', function () {
    removeCart(this.id);
});


//CART
function removeCart(productId) {
    $.ajax({
        type: "POST",
        url: "/Cart/CartRemove",
        data: { id: productId },
        dataType: "json",
        success: function (response) {
            getCartDropdown();
        },
        failure: function (response) {
            alert("Eklenemedi");
        },
        error: function (response) {
            alert("Hata Oluþtu");
        }
    });
}

$('.body').on('click', '.addtocart', function () {
    var parent = $(this).parent();
    var input = parent.find(".vertical-spinner").val();
    addCart(this.id, input);
});

function addCart(productId, orderCount) {
    $.ajax({
        type: "POST",
        url: "/Cart/AddCart/",
        data: { id: productId, count: orderCount },
        dataType: "json",
        success: function (response) {
            getCartDropdown();
        },
        failure: function (response) {
            alert("Eklenemedi");
        },
        error: function (response) {
            alert("Hata Oluþtu");
        }
    });
}

function getCartDropdown() {
    $.ajax({
        type: "POST",
        url: "/Cart/CartDropDown",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            var dropdown = $('.cart-dropdown');
            dropdown.empty();
            dropdown.html(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function getCartIndex() {
    $.ajax({
        type: "POST",
        url: "/Cart/CartIndex",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            var main = $('#cartDetailIndex');
            main.empty();
            main.html(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function cardDelete(id) {
    var x = confirm("Are you sure you want to delete?");
    if (x)
        return true;
    else
        return false;
}


//ADDRESS
$('#editAddressModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('whatever');

    $.ajax({
        type: "GET",
        url: "/UserDetail/addressModal/"+recipient,
        dataType: "html",
        success: function (response) {
            $('#editAddressModal').html(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
});

$('#CityID').change(function(){
    var city = { cityID: $('#CityID').val() };
    $.ajax({
        url: '/Order/getDistrict',
        data: city,
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            $('#DistrictID').empty();

            $.each(result, function (index, district) {
                $('#DistrictID').append("<option value='" + district.DistrictID + "' >" + district.DistrictName + "</option>");
            });
        }
    });
});