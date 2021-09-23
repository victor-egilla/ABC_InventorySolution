function getRootUrl() {
    return window.location.origin
        ? window.location.origin + '/'
        : window.location.protocol + '/' + window.location.host + '/';
}


var clientBaseUrl = getRootUrl();
var apiBaseUrl = clientBaseUrl;

function ajaxAsync(actionUrl, Data = {}, type = "", callback = function () { }, callbackparams = {}, errorCallback = function () { }, cleanupCallback = function () { }) {

    $.ajax({
        url: apiBaseUrl + actionUrl,
        contentType: 'application/json; charset=UTF-8',
        type: type,
        crossDomain: true,
        headers: {
            //'Authorization': `Bearer ${token}`,
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, OPTIONS'
        },
        data: JSON.stringify(Data),
        beforeSend: function () {
        },
        success: function (data) {
            callback(data, callbackparams);
        },
        error: function (data, status, errorMsg) {
            if (data.status != 0) {
                if (data.responseText.length > 0) {
                    alert(data.responseText);
                } else {
                    errorCallback();
                    if (data.status == 500 || data.status == 501) {
                        alert("an error occured");
                    }
                }
            } else {
                alert("an error occured");
            }
            cleanupCallback();
        },
        failure: function (msg) {
            // alert("Error occured connecting to service");
            alert(msg);
        },
        complete: function () {
        }
    });
}



const validateForm = {
    required: (id) => {
        if ($(id).is("select")) {
            if ($(id).prop('multiple')) {
                if ($(id).val().length < 1) return false;

            } else if ($(`${id} option:selected`).val() <= 0 || $(`${id} option:selected`).val() == null) {
                return false;
            }
            return true;
        }
        return $(id).val() == "" ? false : $(id).val() == null ? false : $(id).val() <= 0 ? false : true;
    },
    isEmail: (id) => {
        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        return mailformat.test($(id).val()) == false ? false : true;
    }
};


function openModal(selector, callback = () => { }) {
    $(selector).modal({
        keyboard: false,
        backdrop: 'static'
    });
    callback();
}



async function getAllCountries() {
    var queryCode = '0';
    await ajaxAsync(`Sales/GetDropDownData/${TableTypeEnum.Country}/${queryCode}`, {}, "GET", (data) => {
        if (data.responseCode == "00") {
            let mockup = ` <option selected disabled value="0">Select Country</option>`;
            let resData = data.responseData;
            for (let i = 0; i < resData.length; i++) {
                let currentData = resData[i];
                let tag = `<option class="dropdown-item" value=${currentData.CountryCode}  style="color: black !important;">${currentData.CountryName}</option>`;
                mockup += tag;
            }
            $("#country, #searchCountry").empty().append(mockup);
        }
        else {
            alert("Couldn't fetch Countries");
        }
    }, {}, () => showToast("Please try again", "error"));
}

//GET ALL CITIES
async function getAllCities(queryId) {
    var regionCode = queryId == null || queryId == '' ? '0' : queryId;
    await ajaxAsync(`Sales/GetDropDownData/${TableTypeEnum.City}/${regionCode}`, {}, "GET", (data) => {
        if (data.responseCode == "00") {
            let mockup = ` <option selected disabled value="0">Select City</option>`;
            let resData = data.responseData;
            for (let i = 0; i < resData.length; i++) {
                let currentData = resData[i];
                let tag = `<option class="dropdown-item" value=${currentData.CityCode}  style="color: black !important;">${currentData.CityName}</option>`;
                mockup += tag;
            }
            $("#city, #searchCity").empty().append(mockup);
        }
        else {
            alert("Couldn't fetch city");
        }
    }, {}, () => showToast("Please try again", "error"));
}


//GET ALL STATES
async function getAllStates(queryId) {
    var countryCode = queryId == null || queryId == '' ? '0' : queryId;
    await ajaxAsync(`Sales/GetDropDownData/${TableTypeEnum.Region}/${countryCode}`, {}, "GET", (data) => {
        if (data.responseCode == "00") {
            let mockup = ` <option selected disabled value="0">Select State</option>`;
            let resData = data.responseData;
            for (let i = 0; i < resData.length; i++) {
                let currentData = resData[i];
                let tag = `<option class="dropdown-item" value=${currentData.RegionCode}  style="color: black !important;">${currentData.RegionName}</option>`;
                mockup += tag;
            }
            $("#state, #searchState").empty().append(mockup);
        }
        else {
            alert("Couldn't fetch states");
        }
    }, {}, () => showToast("Please try again", "error"));
}



//GET ALL PRODUCTS
async function getAllProducts() {
    var queryCode = '0';
    await ajaxAsync(`Sales/GetDropDownData/${TableTypeEnum.Product}/${queryCode}`, {}, "GET", (data) => {
        if (data.responseCode == "00") {
            let mockup = ` <option selected disabled value="0">Select Product</option>`;
            let resData = data.responseData;
            for (let i = 0; i < resData.length; i++) {
                let currentData = resData[i];
                let tag = `<option class="dropdown-item" value=${currentData.ProductID}  style="color: black !important;">${currentData.ProductName}</option>`;
                mockup += tag;
            }
            $("#product").empty().append(mockup);
        }
        else {
            alert("Couldn't fetch Products");
        }
    }, {}, () => showToast("Please try again", "error"));
}


var TableTypeEnum = {
    Country: 1,
    City: 2,
    Product: 3,
    Region: 4
};