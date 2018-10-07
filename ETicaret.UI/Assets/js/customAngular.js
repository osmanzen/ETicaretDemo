var module = angular.module('comApp', []);

module.controller('creditCard', function ($scope, $http) {
    $scope.isEmpty = false;

    //Default Card
    $scope.defaultCard = function () {
        $scope.img = 'cardtemp.png';
        $scope.yil = "00";
        $scope.ay = "00";
        $scope.cardNoInput = "";
        $scope.adSoyad = "";
        $scope.cardNo = "**** **** **** ****";
        $scope.secCode = "";
        $scope.showAdd = true;
    };

    //Kredi Kartına Yazdırma
    $scope.giris = function () {
        $scope.cardNo = "";
        for (var i = 0; i < $scope.cardNoInput.length + 2; i++) {
            if ($scope.cardNoInput[i] !== undefined)
                $scope.cardNo = $scope.cardNo + $scope.cardNoInput[i];
            if (i % 4 === 3) { $scope.cardNo += " "; }
        }
        if ($scope.cardNoInput[0] === '5') { $scope.img = 'mastercardtemp.png'; }
        else if ($scope.cardNoInput[0] === '4') { $scope.img = 'visacardtemp.png'; }
        else { $scope.img = 'cardtemp.png'; }
    };

    //Kartlarım
    $scope.cardList = [];

    $scope.getCardList = function () {
        $http.post("/UserDetail/GetCardsList").then(function (response) {
            $scope.cardList = response.data;
            if ($scope.cardList === false) { $scope.isEmpty = true; }
            else { $scope.isEmpty = false; }
            $scope.defaultCard();
        });
    };

    //Kart Ekle
    $scope.addCard = function () {
        var newCard = {
            ExpDate: "01-" + $scope.ay + " -20" + $scope.yil,
            FullName: $scope.adSoyad,
            SecCode: $scope.secCode,
            UserCardNo: $scope.cardNoInput
        };

        $http.post("/UserDetail/AddCard", newCard).then(function (response) {
            $scope.getCardList();
        });
    };

    //Kart Sil
    $scope.removeCard = function (id) {
        $http.post("/UserDetail/RemoveCard/" + id).then(function (response) {
            $scope.getCardList();
        });
    };
});

module.controller('searchBar', function ($scope, $http) {
    $scope.searchText;
    $scope.quantity = 5;
    $scope.searchShow = false;
    $scope.searchResult;

    $scope.showFunc = function () {
        setTimeout(function () {
            $scope.$apply(function () {
                $scope.searchShow = !$scope.searchShow;
            });
        }, 500);
    };

    $scope.searchRun = function () {

        setTimeout(function () {
            $scope.$apply(function () {
                $http.get("/Home/SearchList/" + $scope.searchText.itemName).then(function (result) {
                    $scope.searchResult = result;
                });
            });
        }, 500);
    };
});

module.controller('categoryListController', function ($scope, $http) {

    $scope.ProductList = [];
    $scope.i = 1;
    $scope.pageList = [];

    $scope.makeFilterID;

    $scope.ChangeFilter = function (filterID, minVal, maxVal) {
        //console.log(filterID);
        //console.log(minVal, maxVal);
        //console.log(i);
        $scope.makeFilterID = filterID;
        $http({
            method: 'Post',
            url: '/Category/GetFilterProduct',
            data: { filterID: $scope.makeFilterID, minVal: minVal, maxVal: maxVal }
        }).then(function success(response) {
            $scope.ProductList = response.data;

            //console.log($scope.ProductList);
            $('#productInModel').remove();
            //  $('#modelPageList').remove();
            //console.log($scope.ProductList.length);
            $scope.pageList.splice(0);
            if ($scope.ProductList.length > 2) {
                $scope.index = 2;
                var productCount = $scope.ProductList.length / 2;
                for (i = 0; i < $scope.ProductList.length / 2; i++) {
                    $scope.index += i;
                    $scope.pageList.push({ value: $scope.index });
                }
            }
            console.log($scope.pageList);
        });
    };

    $scope.ChangeIndex = function (index) {

        $scope.i = index;
        //console.log($scope.i);
    };

});


module.controller("productDetailController", function ($scope, $http) {

    var productList = [];
    $scope.commentList = [];
    $scope.SelectPicture = function (newPath) {
        $scope.mainPicture = '/Assets/img/products/' + newPath;
    };

    //$scope.SelectProduct = function (id) {

    //    $http({
    //        url: '/Product/ProductDetail/' + id,
    //        method: 'GET'
    //    }).then(function (response) {
    //    });
    //};

    $scope.SendComment = function (text, pId) {
        $http({
            method: 'POST',
            url: "/Product/AddCommentJson",
            data: { comment: text, id: pId }
        }).then(function (response) {
            console.log(response.data);
            $scope.commentList.unshift(response.data);
            //$scope.data.splice(0, 0, response.data);
            $scope.commentText = "";
        });
    };

    $scope.DeteleCommentNew = function (id, index) {
        alert(index);
        $scope.DeteleComment(id);
        $scope.commentList.splice(index, 1);
    };

    $scope.DeteleCommentOld = function (id, e) {
        $scope.DeteleComment(id);
        e.path[2].remove();
    };

    $scope.DeteleComment = function (id) {

        $http({
            method: 'GET',
            url: "/Product/DeleteComment/" + id
        }).then(function (response) {
            if (response) {
            }
        });
    };

    $scope.Error = function (value) {
        if (value) {
            alert("Ürün tükenmiştir.")
        }
    }

});