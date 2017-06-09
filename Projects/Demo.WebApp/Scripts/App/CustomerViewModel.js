function Customer(id, name, surname, phoneNumber, address, version) {
    var self = this;
    self.id = id;
    self.name = name;
    self.surname = surname;
    self.phoneNumber = phoneNumber;
    self.address = address;
    self.version = version;
}

function ObservableCustomer(id, name, surname, phoneNumber, address, version) {
    var self = this;
    self.id = window.ko.observable(id);
    self.name = window.ko.observable(name);
    self.surname = window.ko.observable(surname);
    self.phoneNumber = window.ko.observable(phoneNumber);
    self.address = window.ko.observable(address);
    self.version = window.ko.observable(version);
    self.nameValidationText = window.ko.observable("");
    self.surnameValidationText = window.ko.observable("");
    self.phoneNumberValidationText = window.ko.observable("");
    self.addressValidationText = window.ko.observable("");
    self.title = version === "" ? "Add new customer" : "Edit customer";
}


function CustomerViewModel(baseUrl) {
    var self = this;

    self.baseUrl = baseUrl;

    self.editVisible = window.ko.observable(false);

    self.customers = window.ko.observableArray([]);

    self.customer = window.ko.observable(null);

    self.modalText = window.ko.observable("");

    self.initCustomers = function () {
        ajax("GET", "customers/?top=1000&skip=0", null,
            function (data) {
                var itemList = data.items;
                var items = [];
                $.each(itemList, function (i, item) {
                    items.push(new Customer(item.id, item.name, item.surname, item.phoneNumber, item.address, item.version));
                });
                self.customers(items);
            }),
            function (data) {
                handleError(data);
            }
    }

    self.deleteCustomer = function (customer) {
        ajax("DELETE", "customers/" + customer.id + "?version=" + customer.version, null,
            function () {
                self.customers.remove(customer);
            },
            function (data) {
                handleError(data);
            });
    }

    self.backToList = function () {
        hideEdit();
    }

    self.addCustomer = function () {
        var observableCustomer = new ObservableCustomer(0, "", "", "", "", "");
        showEdit(observableCustomer);
    }

    self.editCustomer = function (customer) {
        ajax("GET", "customers/" + customer.id, null,
            function (data) {
                var observableCustomer = new ObservableCustomer(data.id, data.name, data.surname, data.phoneNumber, data.address, data.version);
                showEdit(observableCustomer);
            },
            function (data) {
                handleError(data);
            });
    }

    self.insertOrUpdateCustomer = function () {
        var version = self.customer().version();
        var postOrPutData;
        var method;
        var url;
        var customerIsValid = validateCustomer();
        if (customerIsValid) {
            if (version === "") {
                method = "POST";
                postOrPutData = '{ "name": "' + self.customer().name() + '", "surname": "' + self.customer().surname() + '", "phoneNumber": "' + self.customer().phoneNumber() + '", "address": "' + self.customer().address() + '" }';
                url = "customers/";
            } else {
                method = "PUT";
                postOrPutData = '{ "name": "' + self.customer().name() + '", "surname": "' + self.customer().surname() + '", "phoneNumber": "' + self.customer().phoneNumber() + '", "address": "' + self.customer().address() + '", "version": "' + self.customer().version() + '" }';
                url = "customers/" + self.customer().id();
            }
            ajax(method,
                url,
                postOrPutData,
                function () {
                    hideEdit();
                    self.initCustomers();
                },
                function (data) {
                    handleError(data);
                });

        }
    }

    function showEdit(customer) {
        self.customer(customer);
        self.editVisible(true);
    }

    function hideEdit() {
        self.editVisible(false);
        self.customer(null);
    }


    function handleError(data) {
        self.modalText(data.responseText);
        $("#dialog").modal({
            backdrop: "static"
        });
    }

    function validateCustomer() {
        var customer = self.customer();
        var valid =
            validateField("Name", 50, false, customer.name, customer.nameValidationText) &
            validateField("Surname", 50, false, customer.surname, customer.surnameValidationText) &
            validateField("Phone number", 50, false, customer.phoneNumber, customer.phoneNumberValidationText) &
            validateField("Address", 100, true, customer.address, customer.addressValidationText);
        return valid;
    }

    function validateField(fieldName, maxLength, optional, field, validationField) {
        var valid = true;
        if (field() !== "" || optional) {
            if (field().length <= maxLength) {
                validationField("");
            } else {
                validationField(fieldName + " cannot be longer than " + maxLength + " chars");
                valid = false;
            }
        } else {
            validationField(fieldName + " is required");
            valid = false;
        }
        return valid;
    }

    function ajax(method, endpoint, data, doneFn, failFn) {
        $.ajax({
            method: method,
            cache: false,
            url: self.baseUrl + endpoint,
            data: data,
            contentType: "application/json; charset=UTF-8"
        }).done(function (result) {
            doneFn(result);
        }).fail(function (result) {
            failFn(result);
        });
    }
}

$(function () {
    var vm = new CustomerViewModel("http://localhost:2776/");
    vm.initCustomers();
    window.ko.applyBindings(vm);
})