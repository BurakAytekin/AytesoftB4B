var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope, $http, $window) {

            $scope.EditCustomer;
            $scope.products = [];
            $scope.CurrentPage = 1;
            $scope.TotalPage = 0;
            $scope.SelectedFileForUpload = null;
            $scope.addorupdate = false;
            $scope.getDataWithPagination = function (page) {
                $scope.IsLoading = true;
                $http({
                    method: 'GET',
                    url: '/Angular/getProducts',
                }).then(function (response) {
                    angular.forEach(response.data.List, function (value) {
                        value.currentPage = $scope.CurrentPage;
                        $scope.products.push(value);
                    });
                    $scope.TotalPage = response.data.totalPage;
                    $scope.IsLoading = false;
                }, function () {
                    $scope.IsLoading = false;
                })
            }
            $scope.getData = function () {
                $http({
                    method: 'GET',
                    url: '/Product/getProducts',
                }).then(function (response) {
                    $scope.products = response.data;
                    console.log(response.data);
                })
            }
            $scope.search = function () {
                var searchtext = $scope.Searchtext;
                console.log(searchtext);
                $http({
                    method: 'GET',
                    url: '/Product/Search',
                    params: {'key': searchtext},
                }).then(function (response) {
                    $scope.products = [];
                    $scope.products = response.data;
                    console.log(response.data);
                })
            }

            $scope.updateTable = function (updated) {
                console.log(updated);
                $scope.products = [];
                for(var i=0;i<updated+1;i++)
                {
                    $scope.getDataWithPagination(i);
                    $scope.CurrentPage = updated;
                }
            }

            $scope.NextPage = function () {
                if ($scope.CurrentPage < $scope.TotalPage) {
                    $scope.CurrentPage += 1;
                    $scope.getDataWithPagination($scope.CurrentPage);
                }
            }

            $(document).ready(function () {
                $scope.getData();
            });

            $scope.deletepd = function (Pd) {
                var updatedpage = Pd.currentPage;
                swal({
                    title: "Emin Misin?",
                    text: "??r??n bir kez silindi??inde geri getirilemeyecektir!",
                    icon: "warning",
                    confirmButtonText:"Sil",
                    buttons: true,
                    dangerMode: true,
                }).then((willDelete) => {
                    if (willDelete) {
                        $http.post('/Product/deleteProduct/', { id: Pd.id }).success(function () {
                            $scope.errors = [];
                            var index = $scope.products.findIndex(x => x.id == Pd.id);
                            $scope.products.splice(index, 1);
                            swal("??r??n Silindi", Pd.name + " Adl?? ??r??n Ba??ar??yla Silinmi??tir", "info");
                        }).error(function () {
                            swal(data.errors);
                        });
                    } else {
                        swal("????lem ??ptal Edildi","??r??n Silinme ??ptal Edildi","info");
                    }
                });
            };
            

            $scope.getDatawithDate = function () {
                var startDate = $("#startDate").val();
                var endDate = $("#endDate").val();
                console.log("StartDate = " + startDate + " End Date = " + endDate);
                $http({
                    method: 'GET',
                    url: '/Order/getDataWithDate',
                    params: { 'startdate': startDate, 'enddate' : endDate}
                }).then(function (response) {
                    $scope.orders = response.data;
                }, function () {
                    $scope.IsLoading = false;
                })
                
            };

            $scope.imagecheck = function (Pd) {
                if(Pd.ImagePath.length > 0)
                {
                    return false;
                }
                return true;
            }

            $scope.setImage = function (Pd) {
                if (Pd.ImagePath.length > 0)
                {
                    $scope.imagelink = Pd.ImagePath;
                    console.log(Pd.ImagePath);
                }
                else {
                    $scope.imagelink = "../ftp/images/error.jpg";
                }
            };

            $scope.Close = function () {
                $scope.editItem = "";
                $('#popup3').modal('hide');
            }

            $scope.Cancel = function () {
                $('#popup3').modal('hide');
                swal("??ptal Edildi", "Yapmaya ??al????t??????n??z i??lem ba??ar??yla iptal edilmi??tir!", "warning");
            }

            $scope.Edit = function (Pd) {
                if (angular.isUndefined(Pd))
                {
                    $scope.editItem = null;
                    $scope.addorupdate = true;
                }
                else {
                    $scope.editItem = angular.copy(Pd);
                    $scope.addorupdate = false;
                }
            };

            $scope.checkbox = function () {
                console.log("Geliyor");
                $scope.editItem.status != $scope.editItem.status;
                console.log($scope.editItem.status);
            };

            $scope.setDates = function () {
                var startdate = $("#startDate").val();
                var enddate = $("#endDate").val();
                console.log("Start Date = "+ startdate);
                console.log("End Date = " + enddate);

            };
            $scope.Update = function (isValid) {
                if(isValid)
                {
                    console.log($scope.editItem);
                    if ($scope.SelectedFileForUpload != null)
                    {
                            var formData = new FormData();
                            formData.append("file", $scope.SelectedFileForUpload);
                            var description = "image_desc";
                            formData.append("description", description);
                            var updatedpage = $scope.editItem.currentPage;
                            $http.post("/Angular/SaveImage", formData,
                            {
                                withCredentials: true,
                                headers: { 'Content-Type': undefined },
                                transformRequest: angular.identity
                            }).success(function (data) {
                                $scope.editItem.imagepath = data;
                            }).error(function () {
                                swal(data.errors);
                            }).then(function () {
                                $http.post('/Angular/updateProduct', { product: $scope.editItem }).success(function () {
                                    if ($scope.editItem.id > 0) {
                                        swal("D??zenleme Ger??ekle??tirildi", "Se??ilen ??r??n ba??ar?? ile g??ncellendi", "success");
                                    }
                                    else {
                                        swal("Ekleme Ger??ekle??tirildi", "Yeni ??r??n ba??ar??yla ??r??n listesine eklendi.", "success");
                                    }
                                }).then(function () {
                                    pd = $scope.products.filter(x => x.id == $scope.editItem.id);
                                    pd = $scope.editItem;
                                    $scope.Close();
                                });
                            });
                    }
                    else
                    {

                        
                        $http.post('/Angular/updateProduct', { product: $scope.editItem }).success(function () {
                            if ($scope.editItem.id > 0) {
                                swal("D??zenleme Ger??ekle??tirildi", "Se??ilen ??r??n ba??ar?? ile g??ncellendi", "success");
                            }
                            else {
                                swal("Ekleme Ger??ekle??tirildi", "Yeni ??r??n ba??ar??yla ??r??n listesine eklendi.", "success");
                            }
                        }).then(function () {
                            pd = $scope.products.findIndex(x => x.id == $scope.editItem.id);
                            $scope.products[pd] =  $scope.editItem;
                            $scope.Close();
                        });
                    }
                }
            };

            $scope.selectFileforUpload = function (file) {
                $scope.SelectedFileForUpload = file[0];
            }

            $scope.Basket = function (pd) {
                var elementid = 'quantity' + pd.Id;
                console.log(elementid);
                var insertquantity = angular.element(document.getElementById(elementid))[0].value;
                if (pd.Stock > 0 && pd.Stock >= insertquantity)
                {
                    var insertprice = pd.Price * insertquantity;
                    var model = {
                        ProductID: pd.Id,
                        Quantity: insertquantity,
                        Price: insertprice,
                        ProductPrice: pd.Price,
                        ProductName: pd.Name,
                    }
                    if (insertquantity > 0)
                    {
                        $http.post('/Product/insertBasket', { basketitem: model }).success(function () {
                            swal("Sepete Ekleme ????lemi ba??ar??l??", pd.Name + " adl?? ??r??nden " + insertquantity + " adet sepete eklenmi??tir.", "success");
                            pd.Stock = pd.Stock - insertquantity;
                        }).error(function () {
                            swal(data.errors);
                        })
                    }
                    else {
                        swal("??r??n Adedi Belirleyiniz", pd.Name + " adl?? se??mi?? oldu??unuz ??r??nden sepete eklemek i??in adet belirlemeniz gerekmektedir.", "info");
                    }

                }
                else {
                    swal("Stok Hatas??", pd.Name + "adl?? ??r??nden elimizde yeteri kadar bulunmamaktad??r.", "info");
                }
            }

        });

        app.directive('infiniteScroll', function () {
            return {
                restrict: 'A',
                link: function (scope, element, attrs) {
                    element.bind('scroll', function () {
                        if ((element[0].scrollTop + element[0].offsetHeight) === element[0].scrollHeight) {
                            scope.$apply(attrs.infiniteScroll)
                        }
                    });
                }
            }
        });