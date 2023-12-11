app.factory('RolesFactory', function () {

    var lblRolesIU = '';
    var acciones = '';
    var rolesFactory = {};

    rolesFactory.lblRolesIU = lblRolesIU;
    rolesFactory.acciones = acciones;
    rolesFactory.data = {
        ID: '',
        Nombre: '',
        Descripcion: '',
        ID_Estado: '',
        Users: [],
        Options:[]
    };

    return rolesFactory;
});

app.controller('RolesController', function ($scope, $location, $route, $routeParams, $timeout, $compile, $http, $translate, RolesFactory, CommonDataFactory) {

    $scope.$on('languageChanged', function (evt, data) {

        var dTable = $('#RolesTable').DataTable();

        $(dTable.column(1).header()).text($translate.instant('index.titleUsers'));
        $(dTable.column(2).header()).text($translate.instant('index.titleName'));
        $(dTable.column(3).header()).text($translate.instant('index.description'));
        $(dTable.column(4).header()).text($translate.instant('index.titleStatus'));

        var oSettings = dTable.settings();

        oSettings[0].oLanguage = currentLang;

        dTable.draw();
    });

    $scope.go = function (url, acciones, data) {

        $location.path("/" + url);

        RolesFactory.lblRolesIU = acciones === 'edit' ? $translate.instant('index.rolesEdit') : $translate.instant('index.rolesCreate');
        RolesFactory.acciones = acciones;

        if (acciones === 'insert') {

            RolesFactory.data = {
                ID: null,
                Nombre: null,
                Descripcion: null,
                ID_Estado: null,
                Users: null
            };
        }
        else {

            RolesFactory.data = {
                ID: data.ID,
                Nombre: data.Nombre,
                Descripcion: data.Descripcion,
                ID_Estado: data.ID_Estado,
                Users: data.Users
            };
        }
    };

    $scope.Initialize = function () {

        //$('#cover-spin').show(0);

        /* Formatting function for row details - modify as you need */
        function format(d) {
            var users = '';

            for (var i = 0; i < d.Users.length; i++)
                users += d.Users[i].Name + ' ' + d.Users[i].LastName + ', ';

            users = users.substr(0, users.length - 2);

            // `d` is the original data object for the row
            return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                '<tr>' +
                '<td>' + $translate.instant('index.users') + ':&nbsp</td>' +
                '<td>' + users + '</td>' +
                '</tr>' +
                '</table>';
        };

        var table = $('#RolesTable').DataTable
            ({
                ajax:
                    {
                        url: urlBase + "roles",
                        type: "GET",
                        dataType: 'json',
                        contentType: "application/json;charset=utf8'",
                        dataSrc: function (json) {

                            $('#cover-spin').hide(0);
                            if (json.length !== null) {
                                return json;
                            }
                            else {
                                return false;
                            }
                        }
                    },
                dom: 'Bfrtip',
                pageLength: 10,
                stateSave: true,
                aoColumnDefs: [{'bVisible':false, 'aTargets':[1]}],
                stateSaveCallback: function (settings, data) {
                    localStorage.setItem('DataTables_' + settings.sInstance, JSON.stringify(data))
                },
                stateLoadCallback: function (settings) {
                    return JSON.parse(localStorage.getItem('DataTables_' + settings.sInstance))
                },
                scrollY: "480px",
                columns: [
                    {
                        "className": 'details-control',
                        "orderable": false,
                        "data": null,
                        "defaultContent": ''
                    },
                    {
                        data: 'Nombre',
                        title: $translate.instant('index.titleName')
                    },
                    {
                        data: 'Descripcion',
                        title: $translate.instant('index.description')
                    },
                    {
                        data: 'ID_Estado',
                        render: function (data, type, row, meta) {
                            return row.Status === 1 ?
                                $translate.instant("index.Inactive") :
                                $translate.instant("index.active");
                        },
                        title: $translate.instant('index.titleStatus')
                    },
                    {
                        data: function (row, type, set, meta) {

                            var buttonView = "<a role='button' ng-click='go(\"rolesView\",\"view\"," + JSON.stringify(row) + ")'><span title='" + $translate.instant("index.rolView") + "' class='fa fa-eye'></a>&nbsp&nbsp";
                            var buttonEdit = "<a role='button' ng-click='go(\"rolesEdit\",\"edit\"," + JSON.stringify(row) + ")'><span title='" + $translate.instant("index.rolEdit") + "' class='glyphicon glyphicon-pencil'></span></a>&nbsp&nbsp";
                            var buttonDelete = "<a role='button' ng-click='RolesDelete(" + row.ID + ")'><span title='" + $translate.instant("index.rolDelete") + "' class='glyphicon glyphicon-trash'></span></a>";;

                            return buttonView + buttonEdit + buttonDelete;
                        },
                        title: $translate.instant("index.titleActions"),
                        orderable: false,
                        className: 'text-left',
                        width: '90px'
                    }],
                createdRow: function (row, data, index) {
                    $compile(row)($scope);
                },
                buttons: {
                    buttons:
                        [
                            'pageLength',
                            {
                                extend: 'pdfHtml5',
                                text: 'PDF',
                                pageSize: 'A4',
                                orientation: 'portrait',
                                exportOptions: { columns: ':visible' },
                                customize: function (doc) {

                                    //APLICO ESTILOS AL PDF
                                    doc.styles["detalleHeaderStyle"] = {
                                        fontSize: 9,
                                        bold: true,
                                        fillColor: "#FAFAFA",
                                        color: "black",
                                        alignment: "center"
                                    };
                                    doc.styles["detalleInfoStyle"] = {
                                        fontSize: 9,
                                        bold: false,
                                        fillColor: "#FAFAFA",
                                        color: "black"
                                    };

                                    doc.styles["tableBodyEven"] = {
                                        fillColor: '#FAFAFA'
                                    };
                                    doc.styles["tableBodyOdd"] = {
                                        fillColor: '#D8D8D8'
                                    };

                                    //Inserto la cabecera
                                    doc.content.splice(0, 0, {
                                        layout: "noBorders",
                                        table: {
                                            widths: ['auto', '98%'],
                                            body: [
                                                [{
                                                    image: 'data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/hAytodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6M0FDOEY0MzJGQzExMTFFMzgxOEJGNDU2NzM4RTY1ODEiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6M0FDOEY0MzNGQzExMTFFMzgxOEJGNDU2NzM4RTY1ODEiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDozQUM4RjQzMEZDMTExMUUzODE4QkY0NTY3MzhFNjU4MSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDozQUM4RjQzMUZDMTExMUUzODE4QkY0NTY3MzhFNjU4MSIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Pv/bAEMABQMEBAQDBQQEBAUFBQYHDAgHBwcHDwsLCQwRDxISEQ8RERMW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',
                                                    alignment: 'left'
                                                },
                                                {
                                                    // style: "title",
                                                    text: '\nListado de Roles',
                                                    fontSize: 20,
                                                    color: "#01397f",
                                                    alignment: 'center'
                                                }]
                                            ]
                                        }
                                    });

                                    //Inserto registros de detalle
                                    doc.content[2].width = 'auto';
                                    doc.content[2] = {
                                        columns: [
                                            { width: '*', text: '' },
                                            doc.content[2],
                                            { width: '*', text: '' },
                                        ]
                                    };

                                    // Inserto el footer
                                    doc.footer = {
                                        columns: [{
                                            margin: [5, 5, 5, 5],
                                            alignment: 'right',
                                            text: Date.now().toLocaleString()
                                        }]
                                    };

                                    return doc;
                                }
                            },
                            'excel', 'print'
                        ]
                },
                language: currentLang !== undefined ? currentLang : spanish
            });
        
        // Add event listener for opening and closing details
        $('#RolesTable tbody').on('click', 'td.details-control', function () {
            var tr = $(this).closest('tr');
            var row = table.row(tr);

            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                tr.removeClass('shown');
            }
            else {
                // Open this row
                row.child(format(row.data())).show();
                tr.addClass('shown');
            }
        });
    };

    // BAJA DE ROLES
    $scope.RolesDelete = function (ID, Description) {
        var record = ID + (Description || undefined ? " - " + Description : "");

        bAlert($translate.instant("index.usersDeleteTitle") + record,
            $translate.instant("index.usersDeleteQuestion"), "",
            bAlertsLevels.DANGER,
            function () {

                $http.delete(urlBase + "/Roles/" + ID).then(function (a, b, c) {

                    bAlert($translate.instant("index.usersDeletedTitle"),
                        $translate.instant("index.usersDeletedMsgOK"), '', bAlertsLevels.SUCCESS, null, 'Cerrar');
                    $('#RolesTable').DataTable().ajax.reload();
                }, function (a, b, c) {

                        bAlert($translate.instant("index.errorTitle"),
                            $translate.instant("index.errroMsg"),
                            $translate.instant("index.pleaseRetry"), bAlertsLevels.WARNING, null, 'Cerrar');
                    $('#RolesTable').DataTable().ajax.reload();
                });
            }
        );
    };
});

app.controller('RolesViewController', function ($scope, $location, $route, $routeParams, $timeout, $compile, $http, $translate, RolesFactory) {

    $scope.myObject = [];

    $scope.go = function (url, acciones, data) {
        $location.path(url);

        if (acciones != null || acciones != undefined) {

            RolesFactory.acciones = acciones;
        };

    };

    $scope.Initialize = function (accion) {

        //$("#cover-spin").hide(0);

        if (RolesFactory.acciones === 'view') {
            $scope.rol = {
                ID: RolesFactory.data.ID,
                Nombre: RolesFactory.data.Nombre,
                Descripcion: RolesFactory.data.Descripcion,
                Estado: RolesFactory.data.ID_Estado,
                Users: RolesFactory.data.Users
            };

            document.getElementById("ROL_ESTADO").checked = RolesFactory.data.ID_Estado === 1;
        }

        $http(
            {
                method: 'GET',
                url: urlBase + 'users'
            })
            .then(function (a, b, c) {

                $('#cover-spin').hide(0);

                $scope.myObject = a.data;
                var len = $scope.myObject.length;
                var listItems = "";
                var option = "";

                for (var i = 0; i < len; i++) {

                    var founded = false;

                    for (var j = 0; j < RolesFactory.data.Users.length; j++) {

                        if (RolesFactory.data.Users[j].Name === $scope.myObject[i].Name) {

                            founded = true;

                            break;
                        }
                    }

                    if (founded) {
                        option = '<option selected= "selected" value= ' + $scope.myObject[i].ID + '>' + $scope.myObject[i].Name + ' ' + $scope.myObject[i].LastName + '</option>';
                    }
                    else {
                        option = '<option value= ' + $scope.myObject[i].ID + '>' + $scope.myObject[i].Name + ' ' + $scope.myObject[i].LastName + '</option>';
                    }

                    $("#lstUsers").append(option).change();
                }

                $('#lstUsers').multiselect("updateButtonText");

            });
    };
});

app.controller('RolesEditController', function ($scope, $location, $route, $routeParams, $timeout, $compile, $translate, $http, RolesFactory) {

    $scope.myObject = [];

    $scope.accion = RolesFactory.acciones;

    $scope.go = function (url, acciones, datos) {
        $location.path(url);
    };

    $scope.rol = {
        ID: RolesFactory.data.ID,
        Nombre: RolesFactory.data.Nombre,
        Descripcion: RolesFactory.data.Descripcion,
        ID_Estado: RolesFactory.data.ID_Estado,
        Users: RolesFactory.data.Users,
        Options: RolesFactory.data.Options
    };
       
    $scope.SaveData = function () {

        $scope.rol.Options = $("#lstUsers").val();

        RolesFactory.data.Users = [];

        for (var i = 0; i < $scope.rol.Options.length; i++) {
            for (var j = 0; j < $scope.myObject.length; j++) {

                if ($scope.myObject[j].ID.toLocaleString() == $scope.rol.Options[i]) {
                    RolesFactory.data.Users.push($scope.myObject[j]);
                }
            }
        }

        $scope.rol.Users = RolesFactory.data.Users;

        if ($scope.accion !== 'edit') {

            $scope.lblRolesIU = $translate.instant("index.rolesEdit")

            $scope.rol.ID_Estado = 1;
            $scope.rol.ID = 0;

            RolesFactory.data.ID = $scope.rol.ID;
            RolesFactory.data.Nombre = $scope.rol.Nombre;
            RolesFactory.data.Descripcion = $scope.rol.Descripcion;
            RolesFactory.data.ID_Estado = $scope.rol.ID_Estado;
            RolesFactory.data.Users = $scope.rol.Users;

            RolesFactory.acciones = "insert";
        }
        else {

            $scope.lblRolesIU = $translate.instant("index.rolesCreate")

            $scope.rol.ID_Estado = RolesFactory.data.ID_Estado;
        }

        $http({
            method: RolesFactory.acciones === 'edit' ? 'PUT' : 'POST',
            url: urlBase + 'roles',
            data: JSON.stringify($scope.rol)
        }).then(function (a, b, c) {

            bAlert(RolesFactory.acciones === 'edit' ?
                $translate.instant("index.recordUpdateTitle") :
                $translate.instant("index.recordCreateTitle"),
                $translate.instant("index.recordCreateMsgOK"), '', bAlertsLevels.SUCCESS, null, 'Cerrar');

            if (RolesFactory.acciones === 'insert') {

                RolesFactory.data = {
                    ID: a.data.Objeto.ID,
                    Nombre: a.data.Objeto.Nombre,
                    Descripcion: a.data.Objeto.Descripcion,
                    ID_Estado: a.data.Objeto.ID_Estado,
                    Users: $scope.rol.Users
                };
            }
            else {

                RolesFactory.data = {
                    ID: $scope.rol.ID,
                    Name: $scope.rol.Nombre,
                    Description: $scope.rol.Descripcion,
                    Status: $scope.rol.ID_Estado,
                    Users: $scope.rol.Users
                };
            }

            RolesFactory.acciones = "view";

            $scope.go("rolesView", "view", + RolesFactory.data);
        })
            .catch(function (a, b, c) {

                var errorMessage = $translate.instant("index.recordFail") + "\r\n";

                if (a.data.Errores !== undefined && a.data.Errores.error !== undefined) {

                    errorMessage += a.data.Errors.error;
                }

                bAlert(RolesFactory.acciones === 'edit' ?
                    $translate.instant("index.recordUpdateTitle") :
                    $translate.instant("index.recordCreateTitle"),
                    errorMessage, '', bAlertsLevels.WARNING, null, 'Cerrar');
            });
    };

    $scope.lblRolesIU = RolesFactory.lblRolesIU;

    $scope.checkEstado = function () {

        RolesFactory.data.ID_Estado = document.getElementById("ROL_ESTADO").checked ? 1 : 2;
    };

    $scope.Initialize = function (accion) {

        //$('#cover-spin').show(0);

        if (RolesFactory.acciones === 'edit') {

            $scope.rol = {
                ID: RolesFactory.data.ID,
                Nombre: RolesFactory.data.Nombre,
                Descripcion: RolesFactory.data.Descripcion,
                ID_Estado: RolesFactory.data.ID_Estado,
                Users: RolesFactory.data.Users
            };

            document.getElementById("ROL_ESTADO").checked = RolesFactory.data.ID_Estado === 1;
            document.getElementById("divRol").style.visibility = "visible";
            document.getElementById("divEstado").style.visibility = "visible";
        }
        else {

            document.getElementById("divRol").style.visibility = "hidden";
            document.getElementById("divEstado").style.visibility = "hidden";
        }

        $http(
            {
                method: 'GET',
                url: urlBase + 'users'
            })
            .then(function (a, b, c) {

                $('#cover-spin').hide(0);

                $scope.myObject = a.data;
                var len = $scope.myObject.length;
                var listItems = "";
                var option = "";

                for (var i = 0; i < len; i++) {

                    if (RolesFactory.acciones === 'edit') {

                        var founded = false;

                        for (var j = 0; j < RolesFactory.data.Users.length; j++) {

                            if (RolesFactory.data.Users[j].Name === $scope.myObject[i].Name) {

                                founded = true;

                                break;

                            }
                        }

                        if (founded) {
                            option = '<option selected= "selected" value= ' + $scope.myObject[i].ID + '>' + $scope.myObject[i].Name + ' ' + $scope.myObject[i].LastName + '</option>';
                        }
                        else {
                            option = '<option value= ' + $scope.myObject[i].ID + '>' + $scope.myObject[i].Name + ' ' + $scope.myObject[i].LastName + '</option>';
                        }
                    }
                    else {

                        option = '<option value= ' + $scope.myObject[i].ID + '>' + $scope.myObject[i].Name + ' ' + $scope.myObject[i].LastName + '</option>';
                    }

                    $("#lstUsers").append(option).change();
                }

                $('#lstUsers').multiselect("updateButtonText");

            });



    };
});