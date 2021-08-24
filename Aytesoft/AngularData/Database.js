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
                    text: "Ürün bir kez silindiğinde geri getirilemeyecektir!",
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
                            swal("Ürün Silindi", Pd.name + " Adlı Ürün Başarıyla Silinmiştir", "info");
                        }).error(function () {
                            swal(data.errors);
                        });
                    } else {
                        swal("İşlem İptal Edildi","Ürün Silinme İptal Edildi","info");
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
                if(Pd.imagepath.length > 0)
                {
                    return false;
                }
                return true;
            }

            $scope.setImage = function (Pd) {
                if (Pd.imagepath.length > 0)
                {
                    $scope.imagelink = Pd.imagepath;
                    console.log(Pd.imagepath);
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
                swal("İptal Edildi", "Yapmaya çalıştığınız işlem başarıyla iptal edilmiştir!", "warning");
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
                                        swal("Düzenleme Gerçekleştirildi", "Seçilen ürün başarı ile güncellendi", "success");
                                    }
                                    else {
                                        swal("Ekleme Gerçekleştirildi", "Yeni ürün başarıyla ürün listesine eklendi.", "success");
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
                                swal("Düzenleme Gerçekleştirildi", "Seçilen ürün başarı ile güncellendi", "success");
                            }
                            else {
                                swal("Ekleme Gerçekleştirildi", "Yeni ürün başarıyla ürün listesine eklendi.", "success");
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
                var elementid = 'quantity' + pd.id;
                var insertquantity = angular.element(document.getElementById(elementid))[0].value;
                if (pd.stock > 0 && pd.stock >= insertquantity)
                {
                    var insertprice = pd.price * insertquantity;
                    var model = {
                        productid: pd.id,
                        quantity: insertquantity,
                        price: insertprice,
                        productprice: pd.price,
                        productname: pd.name,
                    }
                    if (insertquantity > 0)
                    {
                        $http.post('/Product/insertBasket', { basketitem: model }).success(function () {
                            swal("Sepete Ekleme İşlemi başarılı", pd.name + " adlı üründen " + insertquantity + " adet sepete eklenmiştir.", "success");
                            pd.stock = pd.stock - insertquantity;
                        }).error(function () {
                            swal(data.errors);
                        })
                    }
                    else {
                        swal("Ürün Adedi Belirleyiniz",pd.name + " adlı seçmiş olduğunuz üründen sepete eklemek için adet belirlemeniz gerekmektedir." ,"info");
                    }

                }
                else {
                    swal("Stok Hatası", pd.name + "adlı üründen elimizde yeteri kadar bulunmamaktadır.", "info");
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