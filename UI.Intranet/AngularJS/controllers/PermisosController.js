app.factory('PermisosFactory', function () {

    var lblPermisosIU = '';
    var acciones = '';
    var PermisosFactory = {};

    PermisosFactory.lblPermisosIU = lblPermisosIU;
    PermisosFactory.acciones = acciones;
    PermisosFactory.data = {
        ID: '',
        Nombre: '',
        Descripcion: '',
        ID_Estado: ''
    };

    return PermisosFactory;
});

app.controller('PermisosController', function ($scope, $location, $route, $routeParams, $timeout, $compile, $http, $translate,PermisosFactory, CommonDataFactory) {

    $scope.go = function (url, acciones, data) {

        $location.path("/" + url);

        PermisosFactory.lblPermisosIU = acciones === 'edit' ? "EDICION DE PERMISOS - GRUPO: " + data.Name : 'VISUALIZACION DE PERMISOS - GRUPO: ' + data.Name;
        $scope.lblRightsIU = PermisosFactory.lblPermisosIU;

        PermisosFactory.acciones = acciones;

        if (acciones === 'insert') {

            PermisosFactory.data = {
                ID: null,
                Nombre: null,
                Descripcion: null,
                ID_Estado: null
            };
        }
        else {

            PermisosFactory.data = {
                ID: data.ID,
                Nombre: data.Nombre,
                Descripcion: data.Descripcion,
                ID_Estado: data.ID_Estado
            };
        }
    };

    $scope.Initialize = function () {

        $scope.lblRightsIU = $translate.instant("index.titleRights")

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

        var table = $('#GroupsTable').DataTable
            ({
                ajax:
                {
                    url: urlBase + "groups",
                    type: "GET",
                    dataType: 'json',
                    contentType: "application/json;charset=utf8'",
                    dataSrc: function (json) {

                        $('#cover-spin').hide(0);

                        return json.length !== null ? json : false;
                    }
                },
                dom: 'Bfrtip',
                stateSave: true,
                stateSaveCallback: function (settings, data) {
                    localStorage.setItem('DataTables_' + settings.sInstance, JSON.stringify(data))
                },
                stateLoadCallback: function (settings) {
                    return JSON.parse(localStorage.getItem('DataTables_' + settings.sInstance))
                },
                scrollY: "480px",
                pageLength: 10,
                columns: [
                    {
                        "className": 'details-control',
                        "orderable": false,
                        "data": null,
                        "defaultContent": ''
                    },
                    {
                        data: 'Name',
                        title: $translate.instant("index.titleName")
                    },
                    {
                        data: 'Description',
                        title: $translate.instant("index.description")
                    },
                    {
                        data: 'Status',
                        render: function (data, type, row, meta) {

                            return row.Status === 1 ?
                                $translate.instant("index.active") :
                                $translate.instant("index.inactive");

                        },
                        title: $translate.instant("index.titleStatus")
                    },
                    {
                        data: function (row, type, set, meta) {

                            var buttonView = "<a role='button' ng-click='go(\"permisosView\",\"view\"," + JSON.stringify(row) + ")'><span title='" + $translate.instant("index.rightView") + "' class='fa fa-eye'></a>&nbsp&nbsp";
                            var buttonEdit = "<a role='button' ng-click='go(\"permisosEdit\",\"edit\"," + JSON.stringify(row) + ")'><span title='" + $translate.instant("index.rightEdit") + "' class='glyphicon glyphicon-pencil'></span></a>&nbsp&nbsp";
                            var buttonDelete = "<a role='button' ng-click='PermisosDelete(" + row.ID + ")'><span title='" + $translate.instant("index.rightDelete") + "' class='glyphicon glyphicon-trash'></span></a>";

                            return buttonView + buttonEdit + buttonDelete
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
                                                    image: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIEAAAB2CAYAAADvN/DxAAABN2lDQ1BBZG9iZSBSR0IgKDE5OTgpAAAokZWPv0rDUBSHvxtFxaFWCOLgcCdRUGzVwYxJW4ogWKtDkq1JQ5" +
                                                        "ViEm6uf/oQjm4dXNx9AidHwUHxCXwDxamDQ4QMBYvf9J3fORzOAaNi152GUYbzWKt205Gu58vZF2aYAoBOmKV2q3UAECdxxBjf7wiA10277jTG+38yH6ZKAyNguxtlIYgK0L/SqQYxBMygn2oQD4CpTt" +
                                                        "o1EE9AqZf7G1AKcv8ASsr1fBBfgNlzPR+MOcAMcl8BTB1da4Bakg7UWe9Uy6plWdLuJkEkjweZjs4zuR+HiUoT1dFRF8jvA2AxH2w3HblWtay99X/+PRHX82Vun0cIQCw9F1lBeKEuf1UYO5PrYsdwGQ" +
                                                        "7vYXpUZLs3cLcBC7dFtlqF8hY8Dn8AwMZP/fNTP8gAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAX5aVRYdFhNTDpjb20uYWRvYmUueG1wAAAAAAA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcE" +
                                                        "NlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMT" +
                                                        "Y6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIH" +
                                                        "htbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6ZGM9Imh0dHA6Ly9wdXJsLm9yZy9kYy9lbGVtZW50cy8xLjEvIiB4bWxuczpwaG90b3Nob3A9Imh0dHA6Ly9ucy5hZG" +
                                                        "9iZS5jb20vcGhvdG9zaG9wLzEuMC8iIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdEV2dD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeX" +
                                                        "BlL1Jlc291cmNlRXZlbnQjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMjAtMDUtMTJUMjI6NDM6MDQtMDM6MDAiIH" +
                                                        "htcDpNb2RpZnlEYXRlPSIyMDIwLTA1LTEyVDIyOjQ3OjM0LTAzOjAwIiB4bXA6TWV0YWRhdGFEYXRlPSIyMDIwLTA1LTEyVDIyOjQ3OjM0LTAzOjAwIiBkYzpmb3JtYXQ9ImltYWdlL3BuZyIgcGhvdG" +
                                                        "9zaG9wOkNvbG9yTW9kZT0iMyIgcGhvdG9zaG9wOklDQ1Byb2ZpbGU9IkFkb2JlIFJHQiAoMTk5OCkiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6ZmEwOTI2OGEtZmIxMi0yODRjLWExZjEtNWE2MT" +
                                                        "dlZTQ2N2RhIiB4bXBNTTpEb2N1bWVudElEPSJhZG9iZTpkb2NpZDpwaG90b3Nob3A6MjVkMDFlNzUtNTc1Yy02OTRhLTg0NDctYzQ4NDc4NzE2NmFhIiB4bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ9In" +
                                                        "htcC5kaWQ6YjdmZTc1MzYtOGRkNi05ZTQ5LWFiY2EtMDA1NDE0MGQ3NzhjIj4gPHhtcE1NOkhpc3Rvcnk+IDxyZGY6U2VxPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY3JlYXRlZCIgc3RFdnQ6aW5zdG" +
                                                        "FuY2VJRD0ieG1wLmlpZDpiN2ZlNzUzNi04ZGQ2LTllNDktYWJjYS0wMDU0MTQwZDc3OGMiIHN0RXZ0OndoZW49IjIwMjAtMDUtMTJUMjI6NDM6MDQtMDM6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9Ik" +
                                                        "Fkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6ZmEwOTI2OGEtZmIxMi0yODRjLWExZj" +
                                                        "EtNWE2MTdlZTQ2N2RhIiBzdEV2dDp3aGVuPSIyMDIwLTA1LTEyVDIyOjQ3OjM0LTAzOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxOSAoV2luZG93cykiIHN0RX" +
                                                        "Z0OmNoYW5nZWQ9Ii8iLz4gPC9yZGY6U2VxPiA8L3htcE1NOkhpc3Rvcnk+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+NcLG5AAAHU" +
                                                        "RJREFUeJztnX1UXOW977/P3nv2vDIMw/AOgQRCIBAwJBgUJdGoNW1Oc649rmrtcjXW6rqtXXbZXpc97arWe7vqsufYWnt61jnHao+9tvZknVxtY1OjsQkxb2KIiQQSCDBhgGHe39/26/2DQCYwEBL2MB" +
                                                        "jnsxZ/MLPn9/xmz3c/z+95+z2E5/n/C+BZAOcZhpGQ5TMH4Xm+A8AHWQF8diE8z6sZhklk2pEsmYPIspxpH7JkGCrTDmTJPFkRZMmKIAvAZNqBTxvdJ08W+AOBomg0miuJIi0r+CAREIlQBBq1Jmwy5U" +
                                                        "60btw4rpTtecvNBoZXz7HjH1bY7fZap8u50u/z/wfH84qXwTDM0/lms6161arurVtvP6V4AUlkRbAIevv6tBcujDTbRm31Ho/3FUEQFC9DpVL9sLJyRc/GDRv2rK6pERUvAFkRKELf2bPawcGhluHh4f" +
                                                        "WBYPAlpe8pRVFYubLq779y331vKWr4IvQzzzyTDrufKQosFqG2drWNoZkLgiAcDYfDvZIk3aaUfVmWEQqF+qKxaLSmulrxOCHbO1CQDRtaHF994Cu7m5rW7ddqNE8qaVsQhB8PDJxvVdLmFNmaIA3Url" +
                                                        "5tE2XJ7XZ7rBzHfU4puxzHnaBp2raiosKnlE0gWxOkjVvb2/tvvaX99RyD4TtK2ZQk6Ucej6dMKXtTZEWQRjZu2ODouPXW3+t0OsWahmg0alTK1hRZEaSZlpb1rlva299QqVQ/VMKeKImsEnaSyYpgCd" +
                                                        "h0Y6ttQ8v6vYSQZxdrKx09+qwIlog777iju6qyMq0jf9dKVgRLyKZNN76lZHygFFkRLCGra2rEpnXr9pPJNZ3LhqwIlpg779jabTabl2R2cKFkRZAB1tbXdwL4Sab9mOIzuZ7g3Ll+Nh6PGyRJokEAtV" +
                                                        "odXltfH1uq8rds2dx38uOPveFIZKmKnJfrXgTHj39Ycaa3t2NocKglEAoVxeNxgyiKtCzL26euIYTsoShK1Gq14TyTaXzVqlXdzU3r9q9ff4MrXX6tqKzs6e3tTZf5q+K6FcGet//S/rcDB77mdntWSJ" +
                                                        "J01xUu3w4AiUQCfr8fw1YrDnZ27l2xYkXPHVtve/nWW27pV9q/qsrKU319fc/KsvwjpW1fLdelCH72Ty881dvX1yEIwrZrtSEIwrahoaFtr42P1w70n9/10ENfe11JHze0rHd0HjoUCIfDSpq9Jq47Ef" +
                                                        "z8Fy8+sVgBJBOPx3ccOnyY5QVe++gj33hZCZtTFBQUWJeDCK6r3sFf9u5tGxq2tiglgCkEQdj2YddHO974r13br3z1wikqLLAqae9aua5EMGKzNUYi4QfSYZvjuO0nTnRvP3bseIVSNk2mvAlCiFLmrp" +
                                                        "klbQ6Ou8Yq/FyiWJCl6ZmwGoOpa43JwilhPxFP6EUxfftqHQ7Ho31nzx5qa9ukSHxgMOh9apZFPJHZraBpF0G3x17wgWv0yyd8jr9zxKNVoizVJr//1cq1968xWd5Qoiy9Xu9jGAYcp4imZiHLMgbOn2" +
                                                        "/9+ONTe2+4odm7WHuiKNFYBjVBWpuD3RfObn1poPs/37YPvTQeC981UwAAEJcEg1LlNaxd21lSUvICTdNKmZzFxITj8dM9PVuVsDU8PNySyHAtAKSxJvjD8Jkdfx4f/I4rEd0y33VBnrcoVeZNN7VZE4" +
                                                        "nEy0eOHg2M2EZ/HI1GofTyb57ncejQBw+MjIw05pvN4xRFXd1eAEKgYpiYKEms2+2uWA5L/tMigvft1sb3nSM7ryQAAPAkYoqumduyZXNfWXnZT7q7T3YPnD/f6nS6qkKh0INKbgyJx+M7zp3r33G1ny" +
                                                        "OEQK1mYTabYTAoVgEumrSI4Jhn/J7RaGhBN2kiHq5WuvzVNTXi6pqaPQD2/PWdfa3DVusRl9NV4fP7fhAMhtIWM1wJll1+AgDSIILOiQu1w5FAiyAvLEp3J2KKdblScffn7uoC0HXuXD87Oja2a3zcXu" +
                                                        "v1ekoDwWBBIBD8QSgURDyeULzZmIlarUb+MhQAkAYRjMXC9R4utuCqMiGJ2v324eatJSvTuvRqzZpabs2a2lMATgGTW8dcTtdrTperyuv1lfn9/iK/31/sDwQej0QikCTluppqtRr5+fkwGPSK2VQSxU" +
                                                        "UQ5BMFCfGqYqXqD732HekWwUzq6+pi9XV1/QCmJ4e6uj4qdXs8b3g8nlKvz1fq9XrLvF7fU6FQCNcaU0wKwLxsBQCkQQSCLLEyrq5qPe133YZlsOSqtXXjOIDpVT+nP/kk1+Pxvu5yuyucTudKh8NR7X" +
                                                        "K5n1hor0OtVsNiyYdev3wFACyTCSQvFy/dZe3ddm/V2r2Z9iWZpnXrAgACAHoA4NSp0+YJh+ON8fHx2hGbrdFun3hqLkFMCsACvV63xF5fPcqL4BpGwGRZrn3XceHh5SaCmTQ3N3mbAS+ArjNneg3j4+" +
                                                        "O7hqzWlvPnBze63e5Hp5qMT5MAgGVSE8gARmOh+pcHPn7w4dU3vJZpfxZCQ8PacEPD2m4A3Se6u/88ODT8Vu+Z3g6ny/WU0Wj81AgASIcIrrGrJUhS/QGX7cE6o/nwLUUrBhX2Kq1saGlxbGhp2dvX2H" +
                                                        "BgbHT8DevIhWaHw1EtSVLGVw0thGXRHACTtYE7Ed3636MDP8hntd+qzytYsoWfSnGxx3EKwKmDnZ31p09/4vIHAsUAfpBp3+ZjWa0nEGUZ/SHvzj+M9P04074sls0dHX3ffuxbv6pbs+YwRVEZ7/nMx7" +
                                                        "ISAQBwkoiTfuddz5w+9NNzfrfiO3CXmnv/4Ut7b2lvf0OtZr+faV/mIg0xweJNxEWh+YTP0RwTBeP/KFv9XFthuW3xVjPH5o5b+4w5Oe6/HTzIRSKRf860PzNJQ+9AmTH4uCjgk4Drmz4uXmyLhV6+t7" +
                                                        "J+WXcfr8T69Te4VKzq3/e9+x6WmxCWRRdxLnhJwoVo8J4/jpwt7Qt62r9YWvPCDfnFi17RkykaGxrCkij9Zt+779KxePz5TPszxbIWAQBIsowAn2g76hlv6w952zaZS9+8u2Tlv67OzU9LYsd009S0Lh" +
                                                        "CORHYdOHhQK4risgiAl11gOBeCJMEZj279i33opad7Pnjvhb4Pn+jxOpffvOwCuPmmNmtdXd1hLJNNqctmnGAhyAAEWYIrEdvyzsTwlk6X7YG6nPwjtxetePWu0lXdaSs4Ddzz9zv2j42N1fv9/ky78u" +
                                                        "mpCWYiyTIiAt9ywjfx2D+d/fC/vvzBmwd/2nPkRwfs1vpM+7ZQmtY17lcij9FiWfYxwUKQgWovF69+3znS8TfnyIO/Hvx4Yk1O3vF1uQXvNeYWHFi7TEcfN3d09J09129zOp0Z9eO6EEEyMlDt4+LVxz" +
                                                        "z29mMe+xMqiuqzsFrbKoOpu96Yf7jGkNe1wVLiyLSfU9RUr+pyuVxpX942H8tmAild8JJUb49H6u3xyF2H3WOgCOk3qdQTZdqc/lpj3vEafV5XhS6nT6ldUFdLeVlZn16v/244HM7Y2EEaaoLM76iZD0" +
                                                        "mWa71cvNbLxTs+CbgeJiAwqFRdpRpDf70x/0htTt6xFTpjz1KJYs2aNdyJ7pODC92dnI64Ow29A8UtphUZMkI813qO97aeC3kfoAmBSaXprM4xnWzKLXhvTY75SLoHqAoLC6xDw8MLahIYRqW4OK/75u" +
                                                        "BqEWUZHi7W4fHEOj7yTDxuVLGH63MtRzaZS976QnnN4XSUaTLlTTA0DX4Bi1mNxhy30uVnLDCkCEGuSg3qWqsOMjmAFBV58AouD09Gggw/n2g/6h5r7/Y5tr3vvHDstoIVv91esVpRMej1uqBarf4uLw" +
                                                        "jzxgUMwzxdVFio+IKbjIiAyDJqDXmv31xYvktD6GtL1UEIwgJnPulz3nUu5H2Yk9I7ipwQhcbTflfjcDjQbI0GfvvYmo2/Vsp2fV1d7MDBzsiVspkVFhRYW9avVzyZ1pKPGBJZBoJR/EP1hp9srqrtW2" +
                                                        "xxZXZrX+BCongkGlQ0i8hchASu9Z0JK6unVYGdNc2K5THSqNXzPgw6ne7JtWvrO5UqL5klHTEkkgzJH0bEOg5WhCIDOLeXVPWU6wx91BLu84+LQvMRz/i9p72OXKVsMgwzZ8Cn1WqfXFtfd+imtjarUu" +
                                                        "Uls2QiILIMyR9CdMSOaCiMMZ93Vq6Ca6XakNdlYJZ2EdJ4LLwjwCWKlLJHKDKrPaMoCnl5ed9qWrdu/7a77z6mVFkzWZLeAZFlSL4QIqMOxEMRyLIMq9vVDGCfEkXeYCrcd9LnfLU36N4pLVHvhJNEcJ" +
                                                        "KoVcoewzCcWq0GIQQqFQO93vCQJT9/pLKy8nRLGpNqAksQGBJZhugLIWpzIB4OT/eFz7smFDvZq8lcFNgYcO8Zj4WqvVy8Qym786GmabA0rdicxNr6tYfKy8prLuYwCOfk5PjW1NYuyYBVWkVAZEDyhR" +
                                                        "AddSAejkCWLj2lVo+rWcmyHljZsHs0Gqw/7B7riInKn1Q6kzKNYbdJpVZsDqJpXePUlrclJ20xwWQTELzUBMzoy3vCodIPBwdKlSxzW/HKXzWZCl5QU+nLWQQAKopCi7l47zpzUUZ+NKVJiwiIDEje0J" +
                                                        "wCAABeFA3dI8OfV7LcpvziwJfKa5/baC5+Tkunp5KjCcENpsIXb84v3ZWWAjKA4iIgMkTRG0RkzIHEHAKY4ujQwL1Kl78+v8R134r6pz9fsuob+WptJ1FwMkNDM9iUX/qze8prf3q91AJAOvIT+IKq6E" +
                                                        "UBXCnbx4BzYuOZMZuhoaxC0QS/dSYLV2eyvFxnNB8+4h6/94R3YltI4Nqutd/AUjRW6nNfvym/dHerufitWpPlU7nIdS4UF0EiGDHEw9EFpXuJcgnzu72ffL2hrOJFpf0AgC3FVX1biquePeqwvdYb8n" +
                                                        "ScC3k3WSPB5iCfaJfkuVNpUIRAQ9Eo0uh31xhMXXXG/CPVBlN3Q15h5rNRpwHFRSBJEp3cC7gS+86cevQ7d34+LSKY4qaiCutNRRVWAK+d9jpyfVy81MvFS0M8Z45LgkGUZZaAiCxFxfSMKmBSqR15rG" +
                                                        "bMqGLddaaCzKQ6W0IyvrxsIuivfvWDvz2w85bbFD1PYC6aJtvyAIBFz1tcL2R8tbEky+x/nzj+VKb9+CyTcREAgD3or37+r29lhZAhloUIREnS7v3k5DePDJyryrQvn0WWhQgAIBCLVfzLgXcUPV4my8" +
                                                        "JYNiKQZBkDzolNP9j9h19k2pfPGstGBADACYLhQH/vV3753t7HMu3LZwnFRUATwi1mkU8kkSjY3X38+69+8Le0nGWUZTaKi0Cv1gSZRc7i+WPR0t8dO/TTVw69nxXCEqC4CMpMeX1m/eLTBngj4YrXjn" +
                                                        "Y+/4t3335cAbeyzAP9zDPPKG3TPuCwrxr1eZoWu9ArLvA5/Y6JGwcc9ooSo+lQoTH3uh/CzQSKi8CSYxTdoSB93jXREuUS5sXa40VRa/W4buyzj22iCRmpKymzKuBmliTSUROgeUVV7wW3K9/m825eyN" +
                                                        "aqKyHJMlzh4KqTNutd5x32FSzNDFXmF3gUcDUL0iQCACjONR2KJBKsOxwqj/OC6WrPQEhFQuBzzrscbSdHhu8Y9XpMsiQ5Ki1ZMSyWtIkg35Ajb6lr2M8Jgo8TBI0kS6slWQYhAEUoUIRc81+USxT0TY" +
                                                        "zf3m2z3nHWPrYqkojHBVH0ZGOGa4MsRYaM3jGb9mOb9a5ht6slEItaRElSdKeIjlUHV1oKupvKV7y3cWXNsslC8mlhSUSQZXmzrIaNs2SGrAiyZEWQJSuCLMiKIAsWsNq4026tdSaiVaJ8qVvHEIpbaT" +
                                                        "CdbLGUTm+ZfvPC2S0JSTAYGNb3hYraWTl99oz0t0dELg8g+PLKhj3J7+0fH2r2cfGi5DKmICBioUZnLdDobH0Bz82prqFARIOK9VUb8rqSN4bsHR1oC/KcBZBRb7Qcighc3kg02Dj/NyYo1Oist5Ws7J" +
                                                        "l6pdfn1A6H/S0RgTfKkGdNkVKEEq90nN9HrrEiWzTYONd2dpNK4yjQ6KzuRKzCx8VS7tFUU3S4TGfsby0omz7A85jTVmGLBBulVH6BiCZW67izbP68z3OKoN/vpt8c6/9fA0FvW4BP7Eje96+hGdxeVP" +
                                                        "nVFkvp6wDw3vhg85/GBr4b5BPbi7X61y1q3cimpNNKjjpsVX+xDz7mjEfuI4Qcrs0xH19vKXF97LGb3x47//hg2N8S4rntqUYVKULQmFuARpMFu23nkCo3EQGBhmZQpjP8anPhite3la8+BgD77MOPjE" +
                                                        "aDO2UAX6qogzMRwSHnlQ9RWWcqfO62kpXfB4A3R85uOewavc8eCz+aEIWU454MRXWu0Bl7Ns1xQsu/nzux83TAudWdiD0gzLEpp0qfi0ZTAfoCHgyGfSmvUVEUzGrtGx/7HPu+UdvyKgB0eew7DrtGX0" +
                                                        "p1QDkhBHpatfew23Zqc+GK15OFfZn/KUsD8Dtrz/MnvPYnUmUGS0giYoIwnaqFk0RtkE9sD/AJ6Bn2AV6Wnk6+npckNsRz9wX4BChC2gVZUgHA7y/0/u/TPsc3xXnGKihCEBF5xEURQT6BxFwJqnjAmY" +
                                                        "g85kpEq3S06snNJVV9YZE3B/gEZAAJSUBUEBDgE3OWNUVE4IwA8Latv/1NW/+T9nh423zJL1QU1SGkqKEA4N/OnXh4r33wf0YEvmW+MkMCh5goICxw8/roTsTus0fD1QDwjdqWV2OiYAjwCcx1Sr0f8W" +
                                                        "2OeGSbn4sX6WnVD28sLB+feU1KEbxt628/4bVv4yUJhBDkMCw0NI2pTJUamt6To2KvOXuGDOCVgZMPnPG7OqYEYGI1YFMsRqEIgUmlRnJOIhVFQc+w09cLkoSwwIGTRNgioe0HnBd6jKz6hRmP7f1GFW" +
                                                        "su1OjvAbBVkET4+ARkWQY1+cRAy6gmfVFpHEccI1VH3KP3jsfC22TI0NAM9IwKNJkdRjGE7GMpalbCin6/i353YvjrEYFvIZisQXNULFJl/MxXazFzS72BYaG76JMsywgLPOKSgJDAtb7nsMbaLKW7RV" +
                                                        "meFh9NCPLVlw7lFCQRAZ6DIEs4H/btPOiy9d9YWP7cLP9neQPgI699Oy9J9QRAsUa/76FVzd8xsWrHlOsUIeI6c/E178oVJWnstN85rd4KnRGP1NyA1NvJCdQUjbPBSzkcizR63F1SjTrj5Ew1J0l4b8" +
                                                        "KKY+4xREUe/SHfU75E/Clphgq+Vdf661s85W/IAJyxSNVLAydeiYtCs5ZmcFtR5bc7Cit+DwA6WhUYjYXqrZHA4zJk6BkVbi+s/PZNBeW7WYqKz/aQiE35s+9Hb9Bza5BPtAGAhlHhwapG1BjyUiZ409" +
                                                        "AM7LEITngnAAA0obC1uArtlnJQBJBk4GzIgz+PDcAVj8KbiHUMhf3+5BrAoFLjyfpNACYftAjPY9/EMI64R5EQRYxGgymPAUgpgojAG6e+no5mAltKqhTdssXLEsICP526plKfi5a8YsyVgYyTRAyELm" +
                                                        "WW1dIMqvRGNOQWAJg8CKMn4AJDEUAEOFGAOEf12JxfMmXI++vz3SIAMIRCidbQn/QebNEQYuJk05PDsKjLzT+SHJAtBH8iXjwlQ5ZQaLeUoUAz9wnqYf7S/BchQInGgIZcy/R9ESQJGurSTxYThMtS4a" +
                                                        "oINX1PACAscDjlv5RGX5Ak9pzfxa6Zsb8ypQgqdMa+kz4HZMgYj4Vr//Hk+7/IYzUTkAEVRcfKdDn9c0XDfi6OP432P3HUNTodJDkT0aoAf+kBkmfsCB4K+/Hiua6UN4YQgnJdDugkgTjjUfy/0X50Xg" +
                                                        "zy4qKAwbAPEYEHMFm16hnVojITJPcCQgKHffbhR075nNZU1zIUxX2htObFmVvWkyP2mCjgt8OfgEnRnABAoUYHfVIGNlGScMB5AYNhH8ikLYzFwnAlogAm70upLgdDEf/0Z0ICh5+f/fCi/5NlngtOzr" +
                                                        "QTAHpG5Z0pAGAOEdxRVPWb9x3WB8MC3xoTheaTXkfz1I9ACIGaoo8dc4/ds72s5sWZEWdM5NETcH0zOV2tBBmpo2ICQIYjHobn4pebCUUImkyFaMi1XPZlT/ud0ylxJcgQZRnSxfa9Nb8UZrUWSiWoiA" +
                                                        "oCeoPuR88GUy9dUFE02ixluwFY57LBSyIOu0bnLGOlwYTGpKdYBnA+5MNw2D/9miBLmIqh1uSYsdJgwlH32PT7nCjgoHNk+n8Jk3EBAOSy6sM35BWlzBaXUgT15sLYzlXN33t58ONfxkShWZAlCEmPbl" +
                                                        "wU2nqD7jYAyGHYHyZ/VgauOtewKMsQ5dRRP0UIePny1l26+IPPhZFhJ584hZKUyJDBz7PdXoIMSZ7dT7/cBubu2WCyyZsZw8y878noGBXYGbXKXGVoaeZUW37ZW19Zte6tVLbm7CJ+ccWazgq98c537E" +
                                                        "OPDIf9LbwkspIMOiryRj+faBckCQMh78NdHvvDxdpL7RxDKJhY9QEtzUwndIiJosHPxbfM1Y3RMyzMrDrlewQEFlZ7WbzAUjRyVeoDGnoyL3JCErV+LlHESWKjJMs47hlHlcGk1DmdYAiFXJW6U8cwwT" +
                                                        "ne5zTU/DmaKUJQojGAmkOYxRrDrMDYxGoOGxhVkACiKMusn4tbYqLQIgM4E3BhLBa67GFgCNVXotVPJ8CmCSWWaA397QXluz5XVpO6vcUVRgzX55e41ueXXHZs21mfi/2dtef5Dz3jj8dFEY5EBFrmkh" +
                                                        "mLRoevrVz3na2lq05Nvfbu2GDLfw5/csIRT07gLGPqV2rNL8ZjqzfO6YcoS/jAdWkcpkKXg/srG765OSlg/bdzJx7+q33oP8ICB1sshCCfmPVkXSuFGh3ur2z43t3lc9/IK5HDsPg/TR3IUaUWO0UI+g" +
                                                        "JudHnsAACGonBP+Zqf3b+qcfrp7Zy4UPvK0KlfjEVD2xKiiMGwH3zSg5XLql2v3PzFv7ta3646SUVdXgGnsU2qXp6sBi9LYkqBQJ1UCwCAmmLCye0zTcjF/vZkTOCOx6AiFFg6dY3KSeJl7TtFCKZqgS" +
                                                        "lKtIZ+HaNCWOCQEIVFp7+nCBGnKtuYKCAhCXOH9XPAUpeSXUoAggKHIo0eZI5eED3j9eTPA0BHcWX/Hy/0TndFY0k9LAAgINf0pVOK4Je9xx8bjgRmjXDJAEICVzAaDdYCkwFRHquFUXV1q8VYmka5Lg" +
                                                        "cjkQBEebJqe7z7XaTKT0wRgmpDHvLV82eQJTP/W2Q8kKvSOCwa3a4An7jXx8Xxu+Gen75jH7aqqdlZTFUUFftqVeM/zhwrqDHkdRFCBmVZrg7zHJ755BDKtDkpyyvTGlCizcxZnylFMBjxt/YG3A/O90" +
                                                        "FCCFbojLs2mIt+GBY4M4CjCy2UgJRtL13Nn/Y7/xTgEm0yAGsk9dgTRQgYioJpjpghXbRYSlytvok9I5FAIy9J9QE+0Ra4OPAzExVFISRwL2BGRtK2ogpbw4il80zADRlytY+Lw8fNGmsCAERFHvqrfJ" +
                                                        "iUImWnlQIR51rpSxMClqJRrs3Zs6101a87iqv6CS6/ZuZDOLnCGNPvAwQtlhLXzpXN38tjNYdVFAV6vhXGFxuD5P9nPfuEiBQu94Fgbp+Ai1X+PO9/ffX6124vqnpVz7DdzHwrpOepdn7eetdDq3Pyut" +
                                                        "QU3TPvdySzvyMhmJ35PNnn2TauKbVeypqg2pD3EU2RWYMKBICKouOV+tyedkvZG43m4jAA5LGasXpj/s8iIm/MZ7XjRoa97Jweo4p1rzGany7Q6EoJCAyMygcAX6hYfXhVjunO/RPWnbZosEGUpZTToV" +
                                                        "X63JMFal1XQ67lYV6WVOW6nL4c1eVlmFnNWJ3R/GwRryvSUEwkj9X8c22O6cE8lq2SARRpDbOOjVlrLDgUl/huPa0KWtS6lDOA32u46WdNY+f3f+Sd2O7n4kWppmwZQvEzv3My/7Jp2/1/HD6zvSfgui" +
                                                        "0m8PpUminVGvpLtIbuhCg2q2m6niaUmM9qZ41Q1uTkdakvxkMFrPY3oixVh02WW0VZpo2M+prOR8quNs6SXVmUJSuCLMiKIAuyIsiCrAiyAPj/GjsD+ZDrKvEAAAAASUVORK5CYII=",
                                                    alignment: 'left'
                                                },
                                                {
                                                    // style: "title",
                                                    text: '\nListado de Grupos',
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
        $('#GroupsTable tbody').on('click', 'td.details-control', function () {
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

});

app.controller('PermisosViewController', function ($scope, $location, $route, $routeParams, $timeout, $compile, $translate, PermisosFactory) {

    $scope.go = function (url, acciones, data) {

        $location.path(url);

        if (acciones != null || acciones != undefined) {

            PermisosFactory.acciones = acciones;
        };

    };

    $scope.Inicializar = function (accion) {

        $scope.lblRightsIU = PermisosFactory.lblPermisosIU;
    };
});

app.controller('PermisosEditController', function ($scope, $location, $route, $routeParams, $timeout, $compile, $http, $translate, PermisosFactory) {


    $scope.Inicializar = function () {

        $scope.lblRightsIU = PermisosFactory.lblPermisosIU;

    }

    $scope.accion = PermisosFactory.acciones;

    $scope.go = function (url, acciones, datos) {
        $location.path(url);
    };

    //$scope.permiso = {
    //    ID: PermisosFactory.data.ID,
    //    Nombre: PermisosFactory.data.Nombre,
    //    Descripcion: PermisosFactory.data.Descripcion,
    //    ID_Estado: PermisosFactory.data.ID_Estado
    //};

    //$scope.SaveData = function () {

    //    if ($scope.accion === 'edit') {

    //        $scope.permiso.ID = PermisosFactory.data.ID;
    //        $scope.permiso.Nombre = PermisosFactory.data.Nombre;
    //        $scope.permiso.Descripcion = PermisosFactory.data.Descripcion;
    //        $scope.permiso.ID_Estado = PermisosFactory.data.ID_Estado;

    //        PermisosFactory.acciones = "edit";
    //    }
    //    else {

    //        $scope.permiso.ID_Estado = 2;
    //        $scope.permiso.ID = 0;

    //        PermisosFactory.acciones = "insert";
    //    }

    //    $http({
    //        method: PermisosFactory.acciones === 'edit' ? 'PUT' : 'POST',
    //        url: urlBase + 'Permisos',
    //        data: JSON.stringify($scope.permiso)
    //    }).then(function (a, b, c) {
    //        bAlert((PermisosFactory.acciones === 'edit' ? 'ACTUALIZACION DE REGISTRO' : 'ALTA DE REGISTRO'), 'El registro ha sido grabado correctamente.', '', bAlertsLevels.SUCCESS, null, 'Cerrar');

    //        if (PermisosFactory.acciones == 'insert') {

    //            PermisosFactory.data = {

    //                ID: a.data.Objeto.ID,
    //                Nombre: a.data.Objeto.Nombre,
    //                Descripcion: a.data.Objeto.Descripcion,
    //                ID_Estado: a.data.Objeto.ID_Estado
    //            }
    //        }

    //        PermisosFactory.acciones = "view";

    //        $scope.go("permisosView", "view", + PermisosFactory.data);
    //    })
    //        .catch(function (a, b, c) {
    //            bAlert((PermisosFactory.acciones === 'edit' ? 'ACTUALIZACION DE REGISTRO' : 'ALTA DE REGISTRO'), 'El registro no ha podido ser grabado.', '', bAlertsLevels.WARNING, null, 'Cerrar');
    //        });
    //};

    //$scope.lblPermisosIU = PermisosFactory.lblPermisosIU;

    //$scope.checkEstado = function () {

    //    PermisosFactory.data.ID_Estado = document.getElementById("PERMISO_ESTADO").checked ? 1 : 2;
    //};

    //$scope.Inicializar = function (accion) {

    //    if (PermisosFactory.acciones === 'edit') {

    //        $scope.permiso = {
    //            ID: PermisosFactory.data.ID,
    //            Nombre: PermisosFactory.data.Nombre,
    //            Descripcion: PermisosFactory.data.Descripcion,
    //            ID_Estado: PermisosFactory.data.ID_Estado

    //        };

    //        document.getElementById("PERMISO_ESTADO").checked = PermisosFactory.data.ID_Estado === 1;
    //        document.getElementById("divPermiso").style.visibility = "visible";
    //        document.getElementById("divEstado").style.visibility = "visible";
    //    }
    //    else {

    //        document.getElementById("divPermiso").style.visibility = "hidden";
    //        document.getElementById("divEstado").style.visibility = "hidden";
    //    }
    //};
});