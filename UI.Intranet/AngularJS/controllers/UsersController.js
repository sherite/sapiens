app.factory("UsersFactory", function () {

    var lblUsersIU = '';
    var actions = "";
    var usersFactory = {};

    usersFactory.lblUsersIU = lblUsersIU;
    usersFactory.actions = actions;
    usersFactory.data = {
        ID: "",
        Alias: "",
        Name: "",
        LastName: "",
        Status: "",
        Groups: [],
        Options: [],
        Roles:[]
    };

    return usersFactory;
});

app.controller("UsersController", function ($scope, $location, $translate, $http, UsersFactory, $compile) {

    $scope.$on("languageChanged", function (evt, data) {

        var dTable = $("#UsersTable").DataTable();

        $(dTable.column(1).header()).text($translate.instant("index.titleUsers"));
        $(dTable.column(2).header()).text($translate.instant("index.titleName"));
        $(dTable.column(3).header()).text($translate.instant("index.titleLastName"));
        $(dTable.column(4).header()).text($translate.instant("index.titleStatus"));
        $(dTable.column(5).header()).text($translate.instant("index.titleActions"));

        var oSettings = dTable.settings();

        oSettings[0].oLanguage = currentLang;

        dTable.draw();

    });

    $scope.go = function (url, actions, data) {

        $location.path("/" + url);

        UsersFactory.actions = actions;

        UsersFactory.data = {
            ID: null,
            Alias: null,
            Name: null,
            LastName: null,
            Status: null,
            Groups: null,
            Roles: null
        };

        if (actions === "insert") {

            UsersFactory.lblUsersIU = $translate.instant("index.usersCreate");
        }
        else {

            UsersFactory.lblUsersIU = $translate.instant("index.usersEdit");

            UsersFactory.data = {
                ID: data.ID,
                Alias: data.Alias,
                Name: data.Name,
                LastName: data.LastName,
                Status: data.Status,
                Groups: data.Groups,
                Roles: data.Roles
            };
        }
    };

    $scope.Initialize = function () {

        $scope.lblUsersIU = UsersFactory.lblUsersIU = $translate.instant("index.titleUsers");

        var table = $("#UsersTable").DataTable
            ({
                ajax:
                {
                    url: urlBase + "users",
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json;charset=utf8",
                    dataSrc: function (json) {

                        $("#cover-spin").hide(0);

                        return json.length !== null ? json : false;
                    }
                },
                dom: "Bfrtip",
                stateSave: true,
                stateSaveCallback: function (settings, data) {
                    localStorage.setItem("DataTables_" + settings.sInstance, JSON.stringify(data))
                },
                stateLoadCallback: function (settings) {
                    return JSON.parse(localStorage.getItem("DataTables_" + settings.sInstance))
                },
                scrollY: "480px",
                pageLength: 10,
                columns: [
                    {
                        "className": "details-control",
                        "orderable": false,
                        "data": null,
                        "defaultContent": ""
                    },
                    {
                        data: "Alias",
                        title: $translate.instant("index.titleUsers")
                    },
                    {
                        data: "Name",
                        title: $translate.instant("index.titleName")
                    },
                    {
                        data: "LastName",
                        title: $translate.instant("index.titleLastName")
                    },
                    {
                        data: "Status",
                        render: function (data, type, row, meta) {
                            return row.Status === 1 ?
                                $translate.instant("index.active") :
                                $translate.instant("index.inactive");
                        },
                        title: $translate.instant("index.titleStatus")
                    },
                    {
                        data: function (row, type, set, meta) {

                            var buttonView = "<a role='button' ng-click='go(\"usersView\",\"view\"," + JSON.stringify(row) + ")'><span title='" + $translate.instant("index.userView") + "' class='fa fa-eye'></span></a>&nbsp&nbsp";
                            var buttonEdit = "<a role='button' ng-click='go(\"usersEdit\",\"edit\"," + JSON.stringify(row) + ")'><span title='" + $translate.instant("index.userEdit") + "' class='glyphicon glyphicon-pencil'></span></a>&nbsp&nbsp";
                            var buttonDelete = "<a role='button' ng-click='UsersDelete(" + row.ID + ",\"" + row.Alias + "\")'><span title='" + $translate.instant("index.userDelete") + "' class='glyphicon glyphicon-trash'></span></a>&nbsp&nbsp";
                            var buttonSecurity = "<a role='button' ng-click='go(\"gruposSecurity\",\"view\"," + JSON.stringify(row) + ")'><span title='" + $translate.instant("index.userRights") + "' class='glyphicon glyphicon-lock'></a>";

                            return buttonView + buttonEdit + buttonDelete + buttonSecurity;
                        },
                        title: $translate.instant("index.titleActions"),
                        orderable: false,
                        className: "text-left",
                        width: "90px"
                    }],
                createdRow: function (row, data, index) {
                    $compile(row)($scope);
                },
                buttons: {
                    buttons:
                        [
                            "pageLength",
                            {
                                extend: "pdfHtml5",
                                text: "PDF",
                                pageSize: "A4",
                                orientation: "portrait",
                                exportOptions: { columns: [1,2,3,4] },
                                customize: function (doc) {

                                    // PDF STYLE
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

                                    //Header
                                    doc.content.splice(0, 0, {
                                        layout: "noBorders",
                                        table: {
                                            widths: ["auto", "98%"],
                                            body: [
                                                [{
                                                    image: "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAQABAAD/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/hAytodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6M0FDOEY0MzJGQzExMTFFMzgxOEJGNDU2NzM4RTY1ODEiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6M0FDOEY0MzNGQzExMTFFMzgxOEJGNDU2NzM4RTY1ODEiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDozQUM4RjQzMEZDMTExMUUzODE4QkY0NTY3MzhFNjU4MSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDozQUM4RjQzMUZDMTExMUUzODE4QkY0NTY3MzhFNjU4MSIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Pv/bAEMABQMEBAQDBQQEBAUFBQYHDAgHBwcHDwsLCQwRDxISEQ8RERMW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",
                                                    alignment: "left"
                                                },
                                                {
                                                    // style: "title",
                                                    text: "\n" + $translate.instant("index.usersList"),
                                                    fontSize: 20,
                                                    color: "#01397f",
                                                    alignment: 'center'
                                                }]
                                            ]
                                        }
                                    });

                                    // records
                                    doc.content[2].width = "auto";
                                    doc.content[2] = {
                                        columns: [
                                            { width: "*", text: "" },
                                            doc.content[2],
                                            { width: "*", text: "" },
                                        ]
                                    };

                                    // Footer
                                    doc.footer = {
                                        columns: [{
                                            margin: [5, 5, 5, 5],
                                            alignment: "right",
                                            text: Date.now().toLocaleString()
                                        }]
                                    };

                                    return doc;
                                }
                            },
                            {
                                extend: "excel",
                                exportOptions: {
                                    columns: [1, 2, 3, 4]
                                }
                            },
                            {
                                extend: "print",
                                exportOptions: {
                                    columns: [1, 2, 3, 4]
                                }
                            }
                        ]
                },
                language: currentLang !== undefined ? currentLang : spanish
            });

        // Listener for opening and closing details
        $("#UsersTable tbody").on("click", "td.details-control", function () {
            var tr = $(this).closest("tr");
            var row = table.row(tr);

            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                tr.removeClass("shown");
            }
            else {
                // Open this row
                row.child(format(row.data())).show();
                tr.addClass("shown");
            }
        });

        // Formatting function for row details
        function format(d) {

            var groups = "";

            for (var i = 0; i < d.Groups.length; i++)
                groups += d.Groups[i].Name + ", ";

            groups = groups.substr(0, groups.length - 2);

            var roles = "";

            if (d.Roles != undefined) {

                for (var i = 0; i < d.Roles.length; i++)
                    roles += d.Roles[i].Nombre + ", ";

                roles = roles.substr(0, roles.length - 2);
            }

            return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                   "<tr>"  +
                   "<td>"  + $translate.instant("index.groups") + ":&nbsp</td>" +
                   "<td>"  + groups + "</td>" +
                   "</tr>" +
                   "<tr>"  +
                   "<td>"  + $translate.instant("index.roles") + ":&nbsp</td>" +
                   "<td>"  + roles + "</td>" +
                   "</tr>" +
                   "</table>";
        };
    };

    $scope.UsersDelete = function (ID, Description) {
        var record = ID + (Description || undefined ? " - " + Description : "");

        bAlert($translate.instant("index.usersDeleteTitle") + record,
            $translate.instant("index.usersDeleteQuestion"), "",
            bAlertsLevels.DANGER,
            function () {

                $http.delete(urlBase + "/users?id=" + ID).then(function (a, b, c) {

                    bAlert($translate.instant("index.usersDeletedTitle"),
                        $translate.instant("index.usersDeletedMsgOK"), "", bAlertsLevels.SUCCESS, null, $translate.instant("index.close"));
                    $("#UsersTable").DataTable().ajax.reload();
                }, function (a, b, c) {

                    bAlert($translate.instant("index.errorTitle"),
                        $translate.instant("index.errroMsg"),
                        $translate.instant("index.pleaseRetry"), bAlertsLevels.WARNING, null, $translate.instant("index.close"));
                    $("#UsersTable").DataTable().ajax.reload();
                });
            }
        );
    };
});

app.controller("UsersViewController", function ($scope, $location, $translate, $http, UsersFactory) {

    $scope.myObject = [];

    $scope.go = function (url, actions, data) {

        UsersFactory.lblUsersIU = $translate.instant("index.usersTitle");

        $location.path(url);

        if (actions !== null && actions !== undefined) {

            UsersFactory.actions = actions;
        }
    };

    $scope.Initialize = function () {

        if (UsersFactory.actions === "view") {

            UsersFactory.lblUsersIU = $translate.instant("index.usersView");

            $scope.lblUsersIU = $translate.instant("index.usersView");

            $scope.usr = {
                ID: UsersFactory.data.ID,
                Alias: UsersFactory.data.Alias,
                Name: UsersFactory.data.Name,
                LastName: UsersFactory.data.LastName,
                Status: UsersFactory.data.Status,
                Groups: UsersFactory.data.Groups
            };

            document.getElementById("USR_ESTADO").checked = UsersFactory.data.Status === 1;
        }

        $http({
            method: "GET",
            url: urlBase + "groups"
        })
            .then(function (a, b, c) {

                $("#cover-spin").hide(0);

                $scope.myObject = a.data;
                var len = $scope.myObject.length;
                var listItems = "";
                var option = "";

                for (var i = 0; i < len; i++) {

                    var founded = false;

                    for (var j = 0; j < UsersFactory.data.Groups.length; j++) {

                        if (UsersFactory.data.Groups[j].Name === $scope.myObject[i].Name) {

                            founded = true;

                            break;
                        }
                    }

                    if (founded) {
                        option = "<option selected= 'selected' value= " + $scope.myObject[i].ID + ">" + $scope.myObject[i].Name + "</option>";
                    }
                    else {
                        option = "<option value= " + $scope.myObject[i].ID + ">" + $scope.myObject[i].Name + "</option>";
                    }

                    $("#lstGroups").append(option).change();
                }

                $('#lstGroups').multiselect("updateButtonText");

            })
            .catch(function (a, b, c, ) {

            });

    };
});

app.controller("UsersEditController", function ($scope, $location, $translate, $http, UsersFactory) {

    $scope.myObject = [];

    $scope.go = function (url, actions, data) {

        UsersFactory.lblUsersIU = $translate.instant("index.usersTitle");

        $location.path(url);

        if (actions !== null && actions !== undefined) {

            UsersFactory.actions = actions;
        }
    };

    $scope.Initialize = function () {

        if (UsersFactory.actions === "edit") {

            $scope.lblUsersIU = $translate.instant("index.usersEdit");

            $scope.usr =
            {
                ID: UsersFactory.data.ID,
                Alias: UsersFactory.data.Alias,
                Name: UsersFactory.data.Name,
                LastName: UsersFactory.data.LastName,
                Status: UsersFactory.data.Status,
                Groups: UsersFactory.data.Groups
            };

            document.getElementById("USR_ESTADO").checked = UsersFactory.data.Status === 1;
            document.getElementById("divUsr").style.visibility = "visible";
            document.getElementById("divEstado").style.visibility = "visible";
        }
        else {

            $scope.lblUsersIU = $translate.instant("index.usersCreate");

            document.getElementById("divUsr").style.visibility = "hidden";
            document.getElementById("divEstado").style.visibility = "hidden";
        }

        $http(
            {
                method: "GET",
                url: urlBase + "groups"
            })
            .then(function (a, b, c) {

                $("#cover-spin").hide(0);

                $scope.myObject = a.data;
                var len = $scope.myObject.length;
                var listItems = "";
                var option = "";

                for (var i = 0; i < len; i++) {

                    if (UsersFactory.actions === "edit") {

                        var founded = false;

                        for (var j = 0; j < UsersFactory.data.Groups.length; j++) {

                            if (UsersFactory.data.Groups[j].Name === $scope.myObject[i].Name) {

                                founded = true;

                                break;
                            }
                        }

                        if (founded) {
                            option = "<option selected= 'selected' value= " + $scope.myObject[i].ID + ">" + $scope.myObject[i].Name + "</option>";
                        }
                        else {
                            option = "<option value= " + $scope.myObject[i].ID + ">" + $scope.myObject[i].Name + "</option>";
                        }
                    }
                    else {

                        option = "<option value= " + $scope.myObject[i].ID + ">" + $scope.myObject[i].Name + "</option>";
                    }

                    $("#lstGroups").append(option).change();
                }

                $('#lstGroups').multiselect("updateButtonText");

            });
    };

    $scope.action = UsersFactory.actions;

    $scope.usr = {
        ID: UsersFactory.data.ID,
        Alias: UsersFactory.data.Alias,
        Name: UsersFactory.data.Name,
        LastName: UsersFactory.data.LastName,
        Status: UsersFactory.data.Status,
        Groups: UsersFactory.data.Groups,
        Options: UsersFactory.data.Options
    };

    $scope.SaveData = function () {

        $scope.usr.Options = $("#lstGroups").val();

        UsersFactory.data.Groups = [];

        for (var i = 0; i < $scope.usr.Options.length; i++) {
            for (var j = 0; j < $scope.myObject.length; j++) {

                if ($scope.myObject[j].ID.toLocaleString() === $scope.usr.Options[i]) {
                    UsersFactory.data.Groups.push($scope.myObject[j]);
                }
            }
        }

        $scope.usr.Groups = UsersFactory.data.Groups;

        if ($scope.action !== "edit") {

            $scope.usr.Status = 2;
            $scope.usr.ID = 0;

            UsersFactory.actions = "insert";

            UsersFactory.data.ID = $scope.usr.ID;
            UsersFactory.data.Alias = $scope.usr.Alias;
            UsersFactory.data.Name = $scope.usr.Name;
            UsersFactory.data.LastName = $scope.usr.LastName;
            UsersFactory.data.Groups = $scope.usr.Groups;
            UsersFactory.data.Status = $scope.usr.Status;
        }
        else {

            $scope.usr.Status = UsersFactory.data.Status;
        }

        $http({
            method: UsersFactory.actions === "edit" ? "PUT" : "POST",
            url: urlBase + "users",
            data: JSON.stringify($scope.usr)
        }).then(function (a, b, c) {

            bAlert(UsersFactory.actions === "edit" ?
                $translate.instant("index.recordUpdateTitle") :
                $translate.instant("index.recordCreateTitle"),
                $translate.instant("index.recordCreateMsgOK"), "", bAlertsLevels.SUCCESS, null, "Cerrar");

            if (UsersFactory.actions === "insert") {

                UsersFactory.data = {
                    ID: a.data.Object.ID,
                    Alias: a.data.Object.Alias,
                    Name: a.data.Object.Name,
                    LastName: a.data.Object.LastName,
                    Status: a.data.Object.Status,
                    Groups: $scope.usr.Groups
                };
            }
            else {

                UsersFactory.data = {
                    ID: $scope.usr.ID,
                    Alias: $scope.usr.Alias,
                    Name: $scope.usr.Name,
                    LastName: $scope.usr.LastName,
                    Groups: $scope.usr.Groups,
                    Status: $scope.usr.Status
                };
            }

            UsersFactory.actions = "view";

            $scope.go("usersView", "view", + UsersFactory.data);
        })
            .catch(function (a, b, c) {

                var errorMessage = $translate.instant("index.recordFail") + "\r\n";

                if (a.data.Errores !== undefined && a.data.Errores.error !== undefined) {

                    errorMessage += a.data.Errors.error;
                }

                bAlert(UsersFactory.actions === "edit" ?
                    $translate.instant("index.recordUpdateTitle") :
                    $translate.instant("index.recordCreateTitle"),
                    errorMessage, '', bAlertsLevels.WARNING, null, "Cerrar");
            });
    };

    $scope.checkStatus = function () {

        UsersFactory.data.Status = document.getElementById("USR_ESTADO").checked ? 1 : 2;
    };
});