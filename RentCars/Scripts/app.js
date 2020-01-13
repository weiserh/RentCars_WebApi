var selectedWorker;
var selectedOrder;
var selectedCar;
var selectedPriceList;
var signInUser;
var orderDetails;
var loggedUsername;
var recentlyViewed = {};


function getUser() {
    var result = goToServer("POST", "http://localhost:52042/Guest/isSignin");
}

function Car(carPriceListId, manufacturer, model, price, year, Image) {
    this.CarPriceListId = carPriceListId
    this.Manufacturer = manufacturer
    this.Model = model
    this.Price = price
    this.Year = year
    // this.Image = image
};

function OrderDetails(carPriceListId, startDate, returnDate) {
    this.CarPriceListId = carPriceListId
    this.StartDate = startDate
    this.ReturnDate = returnDate
};

$(document).ready(function () {
    
    handleRecentlyViewed();
});
var focusDate =  function(d) {
    $('#searchStartDate').attr('type', 'date'); 
    $('#searchStartDate').val(d);
    
    $('#returnDate').valueAsDate = new Date();
}
var handleRecentlyViewed = function () {
    recentlyViewed = JSON.parse(localStorage.getItem("recentlyViewed")) || {};
    if (recentlyViewed && Object.keys(recentlyViewed).length > 0) {
        renderRecentlyViewed();
    }
};

var renderRecentlyViewed = function () {
    var recentlyViewedWrap = $("#recentlyViewedWrap ul");
    var list = "";
    recentlyViewedWrap.html("");
    for (key in recentlyViewed) {
        list += "<li><img class=mediumImg src='" + recentlyViewed[key].image + "'/></li>";
    }
    $("#recentlyViewedWrap").show();
    recentlyViewedWrap.append(list);
};

var deleteWorker = function (workerId) {
    window.location = 'http://localhost:52042/Admin/DeleteWorker?workerId=' + workerId;
};

var deleteOrder = function (orderId) {
    window.location = 'http://localhost:52042/Admin/DeleteOrder?orderId=' + orderId;
};

var deleteCar = function (carNumber) {
    window.location = 'http://localhost:52042/Admin/DeleteCar?carNumber=' + carNumber;
};

var deletePriceList = function (carPriceListId) {
    window.location = 'http://localhost:52042/Admin/DeletePriceList?carPriceListId=' + carPriceListId;
};



var editWorker = function (workerId, name, username, password, role) {
    //update worker values
    selectedWorker.Name = $("#workerName").val();
    selectedWorker.Username = $("#username").val();
    selectedWorker.Password = $("#password").val();
    selectedWorker.Role = $("#role:checked").val();
    if (!selectedWorker) {
        alert('No worker was selected!');
        return false;
    }
    window.location = 'http://localhost:52042/Admin/EditWorker?workerId=' + selectedWorker.WorkerId + '&name=' + selectedWorker.Name + '&username=' + selectedWorker.Username + '&password=' + selectedWorker.Password + '&role=' + selectedWorker.Role;
    //goToServer("POST", '/api/AdminApiController/EditWorker', selectedWorker);
};

var editOrder = function (orderId, startDate, returnDate, actualReturnDate, tz, carNumber, isActive) {
    //update order values
    selectedOrder.StartDate = $("#startDate").val();
    selectedOrder.ReturnDate = $("#returnDate").val();
    selectedOrder.ActualReturnDate = $("#actualReturnDate").val();
    selectedOrder.TZ = $("#tz").val();
    selectedOrder.CarNumber = $("#carNumber").val();
    selectedOrder.IsActive = $("#isActive:checked").val();
    if (!selectedOrder) {
        alert('No order was selected!');
        return false;
    }
    window.location = 'http://localhost:52042/Admin/EditOrder?orderId=' + selectedOrder.OrderId + '&startDate=' + selectedOrder.StartDate + '&returnDate=' + selectedOrder.CarNumber + '&tz=' + selectedOrder.TZ + '&carNumber=' + selectedOrder.Password + '&isActive=' + selectedOrder.IsActive;
    //goToServer("POST", '/api/AdminApiController/EditWorker', selectedWorker);
};

var editCar = function (carNumber, carPriceListId, mileage, isValid, isAvailable) {
    //update car
    selectedCar.CarNumber = $("#carNumber").val();
    selectedCar.CarPriceListId = $("#carPriceListId").val();
    selectedCar.Mileage = $("#mileage").val();
    selectedCar.IsValid = $("#isValid:checked").val();
    selectedCar.IsAvailable = $("#isAvailable:checked").val();
    if (!selectedCar) {
        alert('No car was selected!');
        return false;
    }
    window.location = 'http://localhost:52042/Admin/EditCar?carNumber=' + selectedCar.CarNumber + '&carPriceListId=' + selectedCar.CarPriceListId + '&mileage=' + selectedCar.Mileage + '&isValid=' + selectedCar.IsValid + '&isAvailable=' + selectedCar.IsAvailable;
    //goToServer("POST", '/api/AdminApiController/EditWorker', selectedWorker);
};

var editPriceList = function (carPriceListId, manufacturer, model, price, delayPrice, year, gear, carGroup, image) {
    //update car
    selectedPriceList.CarPriceListId = $("#carPriceListId").val();
    selectedPriceList.Manufacturer = $("#manufacturer").val();
    selectedPriceList.Model = $("#model").val();
    selectedPriceList.Price = $("#price").val();
    selectedPriceList.DelayPrice = $("#delayPrice").val();
    selectedPriceList.Year = $("#year").val();
    selectedPriceList.Gear = $("#gear_s:checked").val();
    selectedPriceList.CarGroup = $("#carGroup").val();
    selectedPriceList.Image = $("#image").val();
    if (!selectedPriceList) {
        alert('No price-list was selected!');
        return false;
    }
    window.location = 'http://localhost:52042/Admin/EditPriceList?carPriceListId=' + selectedPriceList.CarPriceListId + '&manufacturer=' + selectedPriceList.Manufacturer + '&model=' + selectedPriceList.Model + '&price=' + selectedPriceList.Price + '&delayPrice=' + selectedPriceList.DelayPrice + '&year=' + selectedPriceList.Year + '&gear_s=' + selectedPriceList.Gear + '&carGroup=' + selectedPriceList.CarGroup + '&image=' + selectedPriceList.Image;
    //goToServer("POST", '/api/AdminApiController/EditCar', selectedCar);
};

var SignIn = function (username, password) {

    var str = '{username: "' + username + '",password: "' + password + '"}'
    var result = goToServer("POST", "http://localhost:52042/Guest/SigninW", str);
    return result.message;

}

var order = function (username, orderDetails) {

    var str = '?username=' + username + '&carPriceListId=' + orderDetails.CarPriceListId + '&startDate=' + orderDetails.StartDate + '&returnDate=' + orderDetails.ReturnDate

    window.location = 'http://localhost:52042/User/OrderCar' + str
    //window.location = ('http://localhost:52042/User/MyOrders', username); //not true

}

var openOrderWindow = function (username, carPriceListId, year, gear, manufacturer, model, price, delayPrice, imageUrl) {
    loggedUsername = username;
    $('#carPriceListId').val(carPriceListId);
    $('#signInModal').modal('hide');
    $('#orderModal').modal('show');
    $("#selectedCarImage").attr('src', imageUrl);
    var startDate = $("#searchStartDate").val();
    var returnDate = $("#searchReturnDate").val();
    $("#startDate").val(startDate);
    $("#returnDate").val(returnDate);
    $("#year").text(year);
    if (gear!=='') {
        $("#gear").text("Automatic gear");
    }
    else {
        $("#gear").text("Manual gear");
    }
    $("#manufacturer").text(manufacturer);
    $("#model").text(model);
    $("#price").text(price);
    $("#delayPrice").text(delayPrice);
    var car = {
        "manufacturer": manufacturer,
        "model": model,
        "carPriceListId": carPriceListId,
        image: imageUrl
    };
    //localStorage.setItem("car", "manufacturer:" + manufacturer + "  model:" + model);
    if (!recentlyViewed[carPriceListId]) {
        recentlyViewed[carPriceListId] = car;
        localStorage.setItem("recentlyViewed", JSON.stringify(recentlyViewed));
        handleRecentlyViewed();
    }
    calcTotal();

};

function calcTotal() {

    var startDate = new Date($('#startDate').val());
    var returnDate = new Date($('#returnDate').val());
    var price = document.getElementById("price").innerHTML

    daysCount = 1 + Math.round((returnDate - startDate) / (1000 * 60 * 60 * 24));
    total = daysCount * price;

    document.getElementById("calcTotal").innerHTML = total;

    orderDetails = new OrderDetails($('#carPriceListId').val(), startDate.toISOString(), returnDate.toISOString());

    $('#orderBtn').click(function () {
        order(loggedUsername, orderDetails);
    });
}

function calcFinalRent() {

    var startDate = new Date($('#startDate').val());
    var actualReturnDate = new Date($('#actualReturnDate').val());
    var price = document.getElementById("price").innerHTML

    daysCount = 1 + Math.round((actualReturnDate - startDate) / (1000 * 60 * 60 * 24));
    total = daysCount * price;

    document.getElementById("calcTotal").innerHTML = total;
}

var openEditWorkerWindow = function (workerId, name, username, password, role) {
    selectedWorker = { WorkerId: workerId, Name: name, Username: username, Password: password, Role: role };
    $("#workerId").val(selectedWorker.WorkerId);
    $("#workerName").val(selectedWorker.Name);
    $("#username").val(selectedWorker.Username);
    $("#password").val(selectedWorker.Password);
    $('input[name="role"][value="' + selectedWorker.Role + '"]').prop('checked', true);
    $('#modalBtn').trigger("click");

};

var openEditOrderWindow = function (orderId, startDate, returnDate, actualReturnDate, tz, carNumber, isActive) {
    $('#editOrderId').val(orderId);
    $('#editStartDate').val(startDate);
    $('#editReturnDate').val(startDate);
    $('#editActualReturnDate').val(actualReturnDate);
    $('#editUserId').val(tz);
    $('#editCarNumber').val(carNumber);
    $('input[name="editIsActive"][value="' + isActive + '"]').prop('checked', true);

    $('#modalBtn').trigger("click");
};

var openEditCarWindow = function (carNumber, carPriceListId, mileage, isValid, isAvailable) {
    selectedCar = { CarNumber: carNumber, CarPriceListId: carPriceListId, Mileage: mileage, IsValid: isValid, IsAvailable: isAvailable };
    $("#carNumber").val(selectedCar.CarNumber);
    $("#carPriceListId").val(selectedCar.CarPriceListId);
    $("#mileage").val(selectedCar.Mileage);
    if (selectedCar.IsValid !== '') {
        $('input[name="isValid"][value="Valid"]').prop('checked', true);
    }
    else {
        $('input[name="isValid"][value="Invalid"]').prop('checked', true);
    }

    if (selectedCar.isAvailable !== '') {
        $('input[name="isAvailable"][value="Available"]').prop('checked', true);
    }
    else { $('input[name="isAvailable"][value="Not Available"]').prop('checked', true); }

    $('#modalBtn').trigger("click");
    //goToServer("POST", '/api/AdminApiController/EditWorker', worker);
};

var openEditPriceListWindow = function (carPriceListId, manufacturer, model, price, delayPrice, year, gear, carGroup, image) {
    selectedPriceList = { CarPriceListId: carPriceListId, Manufacturer: manufacturer, Model: model, Price: price, DelayPrice: delayPrice, Year: year, Gear: gear, CarGroup: carGroup, Image: image };
    $("#carPriceListId").val(selectedPriceList.CarPriceListId);
    $("#manufacturer").val(selectedPriceList.Manufacturer);
    $("#model").val(selectedPriceList.Model);
    $("#price").val(selectedPriceList.Price);
    $("#delayPrice").val(selectedPriceList.DelayPrice);
    $("#year").val(selectedPriceList.Year);
    if (selectedPriceList.Gear !== '') { $('input[name="gear"][value="Automatic"]').prop('checked', true); }
    else { $('input[name="gear"][value="Manual"]').prop('checked', true); }
    $("#carGroup").val(selectedPriceList.CarGroup);
    $("#image").val(selectedPriceList.Image);
    $('#modalBtn').trigger("click");

    //goToServer("POST", '/api/AdminApiController/EditWorker', worker);
};

var openSignInWindow = function () {
    $("#errorMessage").html('');
    $("#errorMessage").hide();
    $('#signInModal').modal('show');
    $('#modalBtn').trigger("click");
    $('#signInBtn').click(function () {
        var result = SignIn($("#username").val(), $("#password").val());

        if (result !== "Login success") {
            $("#errorMessage").html(result);
            $("#errorMessage").show();
        }
        else {
            $("#errorMessage").html('');
            $('#signInModal').modal('hide');
        }
    });
};

/*
$("#searhCarForm").submit(function (e) {

    var result = goToServer("POST", "http://localhost:52042/Guest/SearchCars", $("#searhCarForm").serialize());
    if (result.message != "Order success") {
        alert(result.message);

    } else {
        openOrder(username, orderDetails); //not true

    }


    e.preventDefault(); // avoid to execute the actual submit of the form.
});
*/
var goToServer = function (method, url, data) {
    var msg;
    $.ajax({
        url: url,
        dataType: "json", //to work with json format
        type: method, //to do a post request 
        contentType: 'application/json; charset=utf-8', //define a contentType of your request
        cache: false, //avoid caching results
        async: false,
        data: data, // here you can pass arguments to your request if you need
        success: function (data) {
            // data is your result from controller

            //alert(data.message);
            msg = data;

        },
        error: function (xhr) {
            //alert('error');
        }
    });
    return msg;
};