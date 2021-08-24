var app = angular.module('myApp', ['ui.bootstrap']);
    app.controller('myCtrl', function ($scope, $http, $window) {
        $scope.EditCustomer;
        $scope.products = [];
        $scope.CurrentPage = 1;
        $scope.TotalPage = 0;
        $scope.itemsPerPage = 3,
        $scope.maxSize = 5;

        $scope.numOfPages = function () {
            return Math.ceil($scope.products.length / $scope.itemsPerPage);

        };

        $(document).ready(function () {
            $scope.GetAllData();
        });


        $scope.GetAllData = function () {
            var number = -1;
            $http({
                method: 'GET',
                url: '/Angular/getProducts',
                params: { 'page': number }
            }).then(function (response) {
                $scope.products = [];
                $scope.products = response.data;
                $scope.pagination.totalItems = $scope.products.length;
                $scope.IsLoading = false;
            }, function () {
                $scope.IsLoading = false;
            });

        }

        $scope.pagination = {
            totalItems: $scope.products.length,
            currentPage: 1,
            numPerPage: 3,
        };

        $scope.paginate = function (value) {
            var start, end, index;
            start = ($scope.pagination.currentPage - 1) * $scope.pagination.numPerPage;
            end = start + $scope.pagination.numPerPage;
            index = $scope.products.indexOf(value);
            return (start <= index && index < end);
        };

        $scope.deletepd = function (Pd) {
            swal({
                title: "Emin Misin?",
                text: "�r�n bir kez silindi�inde geri getirilemeyecektir!",
                icon: "warning",
                confirmButtonText: "Sil",
                buttons: true,
                dangerMode: true,
            }).then((willDelete) => {
                if (willDelete) {
                    $http.post('/Angular/deleteProduct/', { id: Pd.id }).success(function () {
                        $scope.errors = [];
                        swal("�r�n Silindi", Pd.name + " Adl� �r�n Ba�ar�yla Silinmi�tir", "info");
                    }).error(function () {
                        swal(data.errors);
                    }).then(function () {
                        $scope.GetAllData();
                    });
                } else {
                    swal("��lem �ptal Edildi", "�r�n Silinme �ptal Edildi", "info");
                }
            });
        };

        $scope.Close = function () {
            $scope.editItem = "";
            $('#popup3').modal('hide');
        }

        $scope.Cancel = function () {
            $('#popup3').modal('hide');
            swal("�ptal Edildi", "Yapmaya �al��t���n�z i�lem ba�ar�yla iptal edilmi�tir!", "warning");
        }

        $scope.Edit = function (Pd) {
            if (Pd === null) {
                $scope.editItem = null;
            }
            else {
                $scope.editItem = angular.copy(Pd);
            }
        };

        $scope.Update = function (isValid) {
            if (isValid) {
                $http.post('/Angular/updateProduct', { product: $scope.editItem }).success(function () {
                    if ($scope.editItem.id > 0) {
                        swal("D�zenleme Ger�ekle�tirildi", "Se�ilen �r�n ba�ar� ile g�ncellendi", "success");
                    }
                    else {
                        swal("Ekleme Ger�ekle�tirildi", "Yeni �r�n ba�ar�yla �r�n listesine eklendi.", "success");
                    }

                }).error(function () {
                    swal(data.errors);
                }).then(function () {
                    $scope.GetAllData();
                    $scope.Close();
                });
            }
        };

        $scope.Basket = function (pd) {
            var elementid = 'quantity' + pd.id
            var insertquantity = angular.element(document.getElementById(elementid))[0].value;
            var insertprice = pd.price * insertquantity;
            var model = {
                productid: pd.id,
                quantity: insertquantity,
                price: insertprice,
                productprice: pd.price,
                productname: pd.name,
            }
            if (insertquantity > 0) {
                $http.post('/Angular/insertBasket', { basketitem: model }).success(function () {
                    swal("Sepete Ekleme ��lemi ba�ar�l�", pd.name + " adl� �r�nden " + insertquantity + " adet sepete eklenmi�tir.", "success");
                }).error(function () {
                    swal(data.errors);
                })
            }
            else {
                swal("�r�n Adedi Belirleyiniz", pd.name + " adl� se�mi� oldu�unuz �r�nden sepete eklemek i�in adet belirlemeniz gerekmektedir.", "info");
            }
        }
    });