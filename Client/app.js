var app = angular.module("app", []);

app.controller('MainController', ['$scope', '$http', function ($scope, $http) {
    $scope.currentUser = JSON.parse(window.localStorage.getItem("currentUser")) || {};
    $scope.apiURL = window.location.protocol + '//' + window.location.hostname + ':9180';

    $scope.loginUserModel = {
        username: "",
        password: ""
    }

    $scope.registerUserModel = {
        username: "",
        password: "",
        confirmPassword: ""
    }

    $scope.state = {
        checkout: false,
        submitted: false
    }

    $scope.pizzas = [];

    function init() {
        $http.get($scope.apiURL + '/api/v1/Pizzas').then(function (response) {
            $scope.pizzas = response.data;

            console.log(response);
        });
        $scope.getOrderHistory();
    }
    window.onload = init;


    $scope.login = function () {
        $http({
            method: 'POST',
            url: $scope.apiURL + '/token',
            data: 'grant_type=password&username=' + $scope.loginUserModel.username + '&password=' + $scope.loginUserModel.password,
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
            },
        }).then(function (response) {
            $scope.currentUser = response.data;
            $scope.getOrderHistory();

            window.localStorage.setItem("currentUser", JSON.stringify($scope.currentUser));
        });
    };

    $scope.logout = function () {
        $http({
            method: 'POST',
            url: $scope.apiURL + '/api/Account/Logout',
            headers: {
                "Content-Type": "application/json",
                Authorization: $scope.currentUser.access_token ? $scope.currentUser.token_type + ' ' + $scope.currentUser.access_token : ""
            },
        }).then(function () {
            $scope.currentUser = {};
            window.localStorage.setItem("currentUser", JSON.stringify({}));
        });
    };

    $scope.register = function () {
        $http({
            method: 'POST',
            url: $scope.apiURL + '/api/Account/Register',
            data: 'grant_type=password&Email=' + $scope.registerUserModel.username + '&Password=' + $scope.registerUserModel.password + '&ConfirmPassword=' + $scope.registerUserModel.confirmPassword,
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
            },
        }).then(function (response) {
            $scope.loginUserModel.username = $scope.registerUserModel.username;
            $scope.loginUserModel.password = $scope.registerUserModel.password;

            $scope.login();
        });
    };

    $scope.getOrderHistory = function () {
        if ($scope.currentUser.access_token) {
            $http({
                method: 'GET',
                url: $scope.apiURL + '/api/v1/Orders',
                headers: {
                    "Content-Type": "application/json",
                    Authorization: $scope.currentUser.access_token ? $scope.currentUser.token_type + ' ' + $scope.currentUser.access_token : ""
                },
            }).then(function (response) {
                console.log({ response });

                $scope.currentUser.orders = response.data;
            });
        }
    }

    $scope.addToCart = function (pizza) {
        console.log({ pizza })

        var data = {
            pizzaId: pizza.PizzaId,
            extraToppings: null,
            order: $scope.cart
        };

        var parameter = JSON.stringify(data);

        console.log({ parameter })

        $http({
            method: 'POST',
            url: $scope.apiURL + '/api/v1/AddToCart',
            data: parameter,
            headers: {
                "Content-Type": "application/json",
                Authorization: $scope.currentUser.access_token ? $scope.currentUser.token_type + ' ' + $scope.currentUser.access_token : ""
            },
        }).then(function (response) {
            console.log({ response });

            $scope.cart = response.data;
        });

        console.log($scope.cart);
    };

    $scope.removeFromCart = function (item) {
        console.log({ item })

        var data = {
            pizzaId: item.Pizza.PizzaId,
            extraToppings: null,
            order: $scope.cart,
            orderItem: item
        };

        var parameter = JSON.stringify(data);

        $http({
            method: 'PUT',
            url: $scope.apiURL + '/api/v1/RemoveFromCart',
            data: parameter,
            headers: {
                "Content-Type": "application/json",
                Authorization: $scope.currentUser.access_token ? $scope.currentUser.token_type + ' ' + $scope.currentUser.access_token : ""
            },
        }).then(function (response) {
            console.log({ response });

            $scope.cart = response.data;
        });
    };

    $scope.submitOrder = function () {
        var data = {
            pizzaId: null,
            extraToppings: null,
            order: $scope.cart,
            orderItem: null
        };

        var parameter = JSON.stringify(data);

        $http({
            method: 'POST',
            url: $scope.apiURL + '/api/v1/SubmitOrder',
            data: parameter,
            headers: {
                "Content-Type": "application/json",
                Authorization: $scope.currentUser.access_token ? $scope.currentUser.token_type + ' ' + $scope.currentUser.access_token : ""
            },
        }).then(function success(response) {
            console.log({ success: response });

            $scope.state.completedOrder = response.data;

            $scope.checkout = false;
            $scope.state.submitted = true;
        }, function failure(response) {
            console.log({ error: response });

            $scope.errorMessage = response.data.Message;
        });
    };

}]);