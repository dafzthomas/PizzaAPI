var app = angular.module("app", []);

app.controller('MainController', ['$scope', '$http', function ($scope, $http) {
    $scope.currentUser = JSON.parse(window.localStorage.getItem("currentUser")) || {};
    $scope.cart = JSON.parse(window.localStorage.getItem("pizzaCart")) || {};
    $scope.apiURL = window.location.protocol + '//' + window.location.hostname + ':9180';

    $scope.loginUserModel = {
        username: "",
        password: ""
    };

    $scope.registerUserModel = {
        username: "",
        password: "",
        confirmPassword: ""
    };

    $scope.state = {
        checkout: false,
        submitted: false
    };

    $scope.pizzas = [];
    $scope.toppings = [];

    function init() {
        $http.get($scope.apiURL + '/api/v1/Pizzas').then(function (response) {
            const data = response.data;
            let pizzaNames = [];

            data.map(function (item) {
                if (!pizzaNames.includes(item.Name)) {
                    pizzaNames.push(item.Name);
                    $scope.pizzas.push(item);
                }
            });

            $scope.allPizzas = data;
            console.log("$scope.pizzas", $scope.pizzas);
            console.log("$scope.allPizzas", $scope.allPizzas);
        });

        $http.get($scope.apiURL + '/api/v1/Toppings').then(function (response) {
            $scope.toppings = response.data;

            console.log("$scope.toppings", $scope.toppings);
        });

        $scope.getOrderHistory();
    }
    window.onload = init;

    $scope.applyTheCart = function (cart) {
        $scope.cart = cart;
        window.localStorage.setItem("pizzaCart", JSON.stringify(cart));
    }

    $scope.applyVoucher = function () {
        console.log('applyVoucher');
        var data = {
            order: $scope.cart,
            voucherCode: $scope.cart.CurrentVoucher
        };

        var parameter = JSON.stringify(data);

        $http({
            method: 'PUT',
            url: $scope.apiURL + '/api/v1/AddCoupon',
            data: parameter,
            headers: {
                "Content-Type": "application/json",
                Authorization: $scope.currentUser.access_token ? $scope.currentUser.token_type + ' ' + $scope.currentUser.access_token : ""
            },
        }).then(function (response) {
            console.log({ response });

            $scope.applyTheCart(response.data);
        });
    };

    $scope.forThisPizza = function (topping, pizza) {
        console.log({ topping });
        console.log({ pizza });


        if (topping.Size == pizza.Size) {
            return true;
        }

        return false;
    };

    $scope.toppingChanged = function (value, pizza, toppingId) {
        console.log({
            value: value,
            pizza: pizza,
            toppingId: toppingId
        });

        if (pizza.ExtraToppings == undefined) {
            pizza.ExtraToppings = [];
        }

        if (value) {
            pizza.ExtraToppings.push(toppingId);
        } else if (!value) {
            let index = pizza.ExtraToppings.indexOf(toppingId);

            if (index > -1) {
                pizza.ExtraToppings.splice(index, 1);
            }

        }
    };

    $scope.reorder = function (order) {
        order.CurrentVoucher = "";
        order.Discount = 0;
        $scope.applyTheCart(order);
    };

    $scope.selectedPizzaSize = function (pizzaSize, normalPizza) {
        normalPizza.CurrentSize = pizzaSize.Size;
        normalPizza.PizzaId = pizzaSize.PizzaId;
        normalPizza.ExtraToppings = [];
    };

    $scope.applyDelivery = function (value) {
        var data = {
            order: $scope.cart
        };

        var parameter = JSON.stringify(data);

        $http({
            method: 'PUT',
            url: $scope.apiURL + '/api/v1/ApplyDelivery',
            data: parameter,
            headers: {
                "Content-Type": "application/json",
                Authorization: $scope.currentUser.access_token ? $scope.currentUser.token_type + ' ' + $scope.currentUser.access_token : ""
            },
        }).then(function (response) {
            console.log({ response });

            $scope.applyTheCart(response.data);
        });
    }

    $scope.resetCart = function () {
        $http({
            method: 'DELETE',
            url: $scope.apiURL + '/api/v1/ResetCart',
            headers: {
                "Content-Type": "application/json",
                Authorization: $scope.currentUser.access_token ? $scope.currentUser.token_type + ' ' + $scope.currentUser.access_token : ""
            },
        }).then(function (response) {
            console.log({ response });

            $scope.applyTheCart(response.data);
        });
    };

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
    };

    $scope.addToCart = function (pizza) {
        console.log({ pizza })

        var data = {
            pizzaId: pizza.PizzaId,
            extraToppings: pizza.ExtraToppings,
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

            $scope.applyTheCart(response.data);
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
            method: 'DELETE',
            url: $scope.apiURL + '/api/v1/RemoveFromCart',
            data: parameter,
            headers: {
                "Content-Type": "application/json",
                Authorization: $scope.currentUser.access_token ? $scope.currentUser.token_type + ' ' + $scope.currentUser.access_token : ""
            },
        }).then(function (response) {
            console.log({ response });

            $scope.applyTheCart(response.data);
        });
    };

    $scope.submitOrder = function () {
        var data = {
            order: $scope.cart
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
        }).then(function (response) {
            console.log({ success: response });

            $scope.state.completedOrder = response.data;

            $scope.state.checkout = false;
            $scope.state.submitted = true;
            $scope.resetCart();
        });
    };

}]);