$(document).ready(function () {
    $('#categoriesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",

                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {

                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Category/GetAllCategories/")',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#categoriesTable').hide();
                        },
                        success: function (data) {
                            const categoryListDto = jQuery.parseJSON(data);
                            console.log(categoryListDto);
                            if (categoryListDto.ResultStatus === 0) {//0==success
                                let tableBody = "";
                                $.each(categoryListDto.Categories.$values, function (index, category) {
                                    tableBody += `
                                                <tr name=${category.ID}>
                                                    <td>
                                                        ${category.ID}
                                                    </td>
                                                    <td>
                                                        ${category.Name}
                                                    </td>
                                                     <td>
                                                    <button class="btn btn-danger text-light btn-delete" data-id="${category.ID}">Sil</button>
                                                    </td>
                                                    <td>
                                                    <button class="btn btn-success text-light btn-update" data-id="${category.ID}">Güncelle</button>
                                                     </td>
                                                    <td>
                                                     <button class="btn btn-primary text-light btn-list" data-id="${category.ID}">Listele</button>
                                                    </td>
                                               </tr>`;
                                });
                                $('#categoriesTable > tbody').replaceWith(tableBody);//tbody ile elimizdeki tableBody değiştirildi.
                                $('#categoriesTable').fadeIn(1500);

                            }
                            else {
                                toastr.error(`${categoryListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function () {
                            console.log(err);
                            $('#categoriesTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!')

                        }
                    });
                }
            }
        ],
        language: {
            "emptyTable": "Tabloda herhangi bir veri mevcut değil",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt yok",
            "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "infoThousands": ".",
            "lengthMenu": "Sayfada _MENU_ kayıt göster",
            "loadingRecords": "Yükleniyor...",
            "processing": "İşleniyor...",
            "search": "Ara:",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "paginate": {
                "first": "İlk",
                "last": "Son",
                "next": "Sonraki",
                "previous": "Önceki"
            },
            "aria": {
                "sortAscending": ": artan sütun sıralamasını aktifleştir",
                "sortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "1": "1 kayıt seçildi"
                },
                "cells": {
                    "1": "1 hücre seçildi",
                    "_": "%d hücre seçildi"
                },
                "columns": {
                    "1": "1 sütun seçildi",
                    "_": "%d sütun seçildi"
                }
            },
            "autoFill": {
                "cancel": "İptal",
                "fillHorizontal": "Hücreleri yatay olarak doldur",
                "fillVertical": "Hücreleri dikey olarak doldur",
                "fill": "Bütün hücreleri <i>%d<\/i> ile doldur"
            },
            "buttons": {
                "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                "colvis": "Sütun görünürlüğü",
                "colvisRestore": "Görünürlüğü eski haline getir",
                "copySuccess": {
                    "1": "1 satır panoya kopyalandı",
                    "_": "%ds satır panoya kopyalandı"
                },
                "copyTitle": "Panoya kopyala",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Bütün satırları göster",
                    "_": "%d satır göster"
                },
                "pdf": "PDF",
                "print": "Yazdır",
                "copy": "Kopyala",
                "copyKeys": "Tablodaki veriyi kopyalamak için CTRL veya u2318 + C tuşlarına basınız. İptal etmek için bu mesaja tıklayın veya escape tuşuna basın.",
                "createState": "Şuanki Görünümü Kaydet",
                "removeAllStates": "Tüm Görünümleri Sil",
                "removeState": "Aktif Görünümü Sil",
                "renameState": "Aktif Görünümün Adını Değiştir",
                "savedStates": "Kaydedilmiş Görünümler",
                "stateRestore": "Görünüm -&gt; %d",
                "updateState": "Aktif Görünümün Güncelle"
            },
            "searchBuilder": {
                "add": "Koşul Ekle",
                "button": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "condition": "Koşul",
                "conditions": {
                    "date": {
                        "after": "Sonra",
                        "before": "Önce",
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "number": {
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "gt": "Büyüktür",
                        "gte": "Büyük eşittir",
                        "lt": "Küçüktür",
                        "lte": "Küçük eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "string": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "endsWith": "İle biter",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "startsWith": "İle başlar",
                        "notContains": "İçermeyen",
                        "notStarts": "Başlamayan",
                        "notEnds": "Bitmeyen"
                    },
                    "array": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "without": "Hariç"
                    }
                },
                "data": "Veri",
                "deleteTitle": "Filtreleme kuralını silin",
                "leftTitle": "Kriteri dışarı çıkart",
                "logicAnd": "ve",
                "logicOr": "veya",
                "rightTitle": "Kriteri içeri al",
                "title": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "value": "Değer",
                "clearAll": "Filtreleri Temizle"
            },
            "searchPanes": {
                "clearMessage": "Hepsini Temizle",
                "collapse": {
                    "0": "Arama Bölmesi",
                    "_": "Arama Bölmesi (%d)"
                },
                "count": "{total}",
                "countFiltered": "{shown}\/{total}",
                "emptyPanes": "Arama Bölmesi yok",
                "loadMessage": "Arama Bölmeleri yükleniyor ...",
                "title": "Etkin filtreler - %d",
                "showMessage": "Tümünü Göster",
                "collapseMessage": "Tümünü Gizle"
            },
            "thousands": ".",
            "datetime": {
                "amPm": [
                    "öö",
                    "ös"
                ],
                "hours": "Saat",
                "minutes": "Dakika",
                "next": "Sonraki",
                "previous": "Önceki",
                "seconds": "Saniye",
                "unknown": "Bilinmeyen",
                "weekdays": {
                    "6": "Paz",
                    "5": "Cmt",
                    "4": "Cum",
                    "3": "Per",
                    "2": "Çar",
                    "1": "Sal",
                    "0": "Pzt"
                },
                "months": {
                    "9": "Ekim",
                    "8": "Eylül",
                    "7": "Ağustos",
                    "6": "Temmuz",
                    "5": "Haziran",
                    "4": "Mayıs",
                    "3": "Nisan",
                    "2": "Mart",
                    "11": "Aralık",
                    "10": "Kasım",
                    "1": "Şubat",
                    "0": "Ocak"
                }
            },
            "decimal": ",",
            "editor": {
                "close": "Kapat",
                "create": {
                    "button": "Yeni",
                    "submit": "Kaydet",
                    "title": "Yeni kayıt oluştur"
                },
                "edit": {
                    "button": "Düzenle",
                    "submit": "Güncelle",
                    "title": "Kaydı düzenle"
                },
                "error": {
                    "system": "Bir sistem hatası oluştu (Ayrıntılı bilgi)"
                },
                "multi": {
                    "info": "Seçili kayıtlar bu alanda farklı değerler içeriyor. Seçili kayıtların hepsinde bu alana aynı değeri atamak için buraya tıklayın; aksi halde her kayıt bu alanda kendi değerini koruyacak.",
                    "noMulti": "Bu alan bir grup olarak değil ancak tekil olarak düzenlenebilir.",
                    "restore": "Değişiklikleri geri al",
                    "title": "Çoklu değer"
                },
                "remove": {
                    "button": "Sil",
                    "confirm": {
                        "_": "%d adet kaydı silmek istediğinize emin misiniz?",
                        "1": "Bu kaydı silmek istediğinizden emin misiniz?"
                    },
                    "submit": "Sil",
                    "title": "Kayıtları sil"
                }
            },
            "stateRestore": {
                "creationModal": {
                    "button": "Kaydet",
                    "columns": {
                        "search": "Kolon Araması",
                        "visible": "Kolon Görünümü"
                    },
                    "name": "Görünüm İsmi",
                    "order": "Sıralama",
                    "paging": "Sayfalama",
                    "scroller": "Kaydırma (Scrool)",
                    "search": "Arama",
                    "searchBuilder": "Arama Oluşturucu",
                    "select": "Seçimler",
                    "title": "Yeni Görünüm Oluştur",
                    "toggleLabel": "Kaydedilecek Olanlar"
                },
                "duplicateError": "Bu Görünüm Daha Önce Tanımlanmış",
                "emptyError": "Görünüm Boş Olamaz",
                "emptyStates": "Herhangi Bir Görünüm Yok",
                "removeConfirm": "Görünümü Silmek İstediğinize Eminminisiniz?",
                "removeError": "Görünüm Silinemedi",
                "removeJoiner": "ve",
                "removeSubmit": "Sil",
                "removeTitle": "Görünüm Sil",
                "renameButton": "Değiştir",
                "renameLabel": "Görünüme Yeni İsim Ver -&gt; %s:",
                "renameTitle": "Görünüm İsmini Değiştir"
            }
        }
    });
    //Datatable burda bitiyor.

    //Ajax get
    $(function () {
        const url = '/Category/Add/'//bu actiona gider viewi alır ve gelir.
        const modalDiv = $('#modalIsHere');
        $('#btnAdd').click(function () {
            //ajax get
            $.get(url).done(function (data) {
                modalDiv.html(data);
                modalDiv.find(".modal").modal('show');
            });
        });

        //Ajax add post
        modalDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-category-add');//form seçildi
            const actionUrl = form.attr('action');//actiona ulaşmak için
            const dataToSend = form.serialize();//gönderilecek veriyi aldık
            $.post(actionUrl, dataToSend).done(function (data) {
                console.log(data);
                const categoryAddAjaxModel = jQuery.parseJSON(data);
                console.log(categoryAddAjaxModel);
                const newFormBody = $('.modal-body', categoryAddAjaxModel.CategoryAdd);
                modalDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';

                if (isValid) {
                    modalDiv.find('.modal').modal('hide');//ekrandan gitmesini sağlar
                    const newTableRow =
                        `
                                    <tr name="${categoryAddAjaxModel.CategoryDto.Category.ID}">
                                    <td>
                                        ${categoryAddAjaxModel.CategoryDto.Category.ID}
                                    </td>
                                    <td>
                                       ${categoryAddAjaxModel.CategoryDto.Category.Name}
                                    </td>
                                    <td>
                                      <button class="btn btn-danger text-light btn-delete" data-id="${categoryAddAjaxModel.CategoryDto.Category.ID}"> Sil</button>
                                    </td>
                                    <td>
                                      <button class="btn btn-success text-light btn-update" data-id="${categoryAddAjaxModel.CategoryDto.Category.ID}">Güncelle</button>
                                    </td>
                                     <td>
                                      <button class="btn btn-primary text-light btn-list" data-id="${categoryAddAjaxModel.CategoryDto.Category.ID}">Listele</button>
                                    </td>

                                   </tr>`;
                    const newTableRowObject = $(newTableRow);//js objesi haline geldi.
                    newTableRowObject.hide();
                    $('#categoriesTable').append(newTableRowObject);//tablo sonuna yeni satırımızı ekledik.
                    newTableRowObject.fadeIn(3000);//3sn de görünücek
                    toastr.success(`${categoryAddAjaxModel.CategoryDto.Message}`, 'Başarılı İşlem!');
                }
                else {
                    let summaryText = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText = `*${text}\n`;
                    });
                    toastr.warning(summaryText);
                }
            });

        });
    });

    //Ajax Delete Post
    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');//tıklanan butonunu idsini yakalar.
        //sweatallert.com
        const tableRow = $(`[name="${id}"]`);
        const categoryName = tableRow.find('td:eq(1)').text();//1. index 2.sıradaki td nin ismini(name) çektik.
        Swal.fire({
            title: 'Emin Misiniz?',
            text: `${categoryName} adlı kategori silinecektir.`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet.',
            cancelButtonText: 'Hayır.'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { categoryId: id },//460
                    url: '/Category/Delete/',
                    success: function (data) {
                        const result = jQuery.parseJSON(data);
                        if (result.ResultStatus === 0) {
                            Swal.fire(
                                'Silindi!',
                                ` kategori silinmiştir.`,
                                'success'
                            );
                            tableRow.fadeOut(3000);
                        }
                        else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Başarısız İşlem!',
                                text: `${result.Message}`,
                            });
                        }
                    },
                    error: function (err) {
                        console.log(err);
                        toastr.error(`${err.responseText}`, "Hata!");
                    }
                });
            }
        });
    });

    //Ajax Update Get
    $(function () {
        const url = '/Category/Update/';
        const modalDiv = $('#modalIsHere');
        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { categoryId: id }).done(function (data) {
                modalDiv.html(data);
                modalDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("hata");
            });
        });


        //Ajax Update Post
        modalDiv.on('click', '#btnUpdate', function (event) {
            event.preventDefault();
            const form = $('#form-category-update');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                const categoryUpdateAjaxModel = jQuery.parseJSON(data);
                console.log(categoryUpdateAjaxModel);
                const newFormBody = $('.modal-body', categoryUpdateAjaxModel.CategoryUpdate);
                modalDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    modalDiv.find('.modal').modal('hide');
                    const newTableRow =
                        `
                                    <tr name="${categoryUpdateAjaxModel.CategoryDto.Category.ID}">
                                    <td>
                                        ${categoryUpdateAjaxModel.CategoryDto.Category.ID}
                                    </td>
                                    <td>
                                       ${categoryUpdateAjaxModel.CategoryDto.Category.Name}
                                    </td>
                                    <td>
                                      <button class="btn btn-danger text-light btn-delete" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.ID}"> Sil</button>
                                    </td>
                                    <td>
                                      <button class="btn btn-success text-light btn-update" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.ID}">Güncelle</button>
                                    </td>
                                     <td>
                                      <button class="btn btn-primary text-light btn-list" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.ID}">Listele</button>
                                    </td>

                                   </tr>`;
                    const newTableRowObject = $(newTableRow);
                    const categoryTableRow = $(`[name="${categoryUpdateAjaxModel.CategoryDto.Category.ID}"]`);// tableRow un name özelliğinden bulunduğu yeri(satırı )yakaladık.
                    newTableRowObject.hide();
                    categoryTableRow.replaceWith(newTableRowObject);//newTableRowObject ile categoryTableRow(eski) u yer değiştirdik.
                    newTableRowObject.fadeIn(3000);
                    toastr.success(`${categoryUpdateAjaxModel.CategoryDto.Message}`, "Başarılı İşlem!");
                } else {
                    let summaryText = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText = `*${text}\n`;
                    });
                    toastr.warning(summaryText);
                }

            }).fail(function (response) {
                console.log(response);
            });

        });

    });

    //Ajax GetByCategories List
    $(function () {
        const url = '/Book/GetByCategory/';
        const modalDiv = $('#modalIsHere');
        $(document).on('click', '.btn-list', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { categoryId: id }).done(function (data) {
                modalDiv.html(data);
                modalDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("hata");
            });
        });


    });

});