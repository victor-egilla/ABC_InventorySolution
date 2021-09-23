$(document).ready(function () {
    getAllCountries();
    getAllProducts();

    $("#country").on("change", getRegion);
    $("#state").on("change", getCity);
    $("#addProductBtn").on("click", submitSalesInfo);
});

function getRegion() {
    var countryCode = $("#country").val();
    getAllStates(countryCode);
}

function getCity() {
    var regionCode = $("#state").val();
    getAllCities(regionCode);
}

//ADD SALES INFORMATION
async function submitSalesInfo() {
    if (!validateForm.required("#fullname")) return (alert("Please add Full Name, it is required", "error"));
    if (!validateForm.required("#country")) return (alert("Please select a country, it is required", "error"));
    if (!validateForm.required("#state")) return (alert("Please select a state, it is required", "error"));
    if (!validateForm.required("#city")) return (alert("Please select a city, it is required", "error"));
    if (!validateForm.required("#dateofSale")) return (alert("Please select a date, it is required", "error"));
    if (!validateForm.required("#quantity")) return (alert("Please add quantity, it is required", "error"));
    if (!validateForm.required("#product")) return (alert("Please select product, it is required", "error"));

    $("#addProductBtn").text("Please wait...");
    $("#addProductBtn").prop("readOnly", true);
    $("#addProductBtn").attr("disabled", true);

    let data = getSalesDetails();
    await ajaxAsync(`Sales/AddSales`, data, "POST", (retdata) => {
        
        if (retdata.responseCode == "00") {
            alert(retdata.responseMessage);
            reset();
        }
        else {
            $("#addProductBtn").text("ADD INFORMATION");
            $("#addProductBtn").prop("readOnly", false);
            $("#addProductBtn").attr("disabled", false);
            alert(retdata.responseMessage);
        }
    }, {}, () => {
        alert("an error occured trying to add sales record");
    });
}

//GET SALES DETAILS
function getSalesDetails() {
    return {
        CustomerName: $("#fullname").val(),
        CityCode: parseInt($("#city").val()),
        CountryCode: $("#country").val(),
        RegionCode: $("#state").val(),
        ProductID: parseInt($("#product").val()),
        DateofSale: $("#dateofSale").val(),
        ProductQuantity: parseInt($("#quantity").val())
    };
}


function reset() {
    $('input[type=text]').val('');
    $('input[type=number]').val('');
    $('input[type=date]').val('');
    $('select').val('0');
    $("#addProductBtn").text("ADD INFORMATION");
    $("#addProductBtn").prop("readOnly", false);
    $("#addProductBtn").attr("disabled", false);
}