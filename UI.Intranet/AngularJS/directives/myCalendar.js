app.directive('miCalendario', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attr, ngModel) {
            $(el).datepicker({
                renderer: $.ui.datepicker.defaultRenderer,
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
                    'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
                    'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                days: ['Domingo', 'Lunes', 'Martes', 'Mi&eacute;rcoles', 'Jueves', 'Viernes', 'S&aacute;bado'],
                daysShort: ['Dom', 'Lun', 'Mar', 'Mi&eacute;', 'Juv', 'Vie', 'S&aacute;b'],
                daysMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S&aacute;'],
                format: 'dd/mm/yyyy',
                firstDay: 1,
                prevText: '&#x3c;Ant',
                prevStatus: '',
                prevJumpText: '&#x3c;&#x3c;',
                prevJumpStatus: '',
                nextText: 'Sig&#x3e;',
                nextStatus: '',
                nextJumpText: '&#x3e;&#x3e;',
                nextJumpStatus: '',
                currentText: 'Hoy',
                currentStatus: '',
                todayText: 'Hoy',
                todayStatus: '',
                clearText: '-',
                clearStatus: '',
                closeText: 'Cerrar',
                closeStatus: '',
                yearStatus: '',
                monthStatus: '',
                weekText: 'Sm',
                weekStatus: '',
                dayStatus: 'DD d MM',
                defaultStatus: '',
                autoclose: 'true',
                isRTL: false,
                orientation: 'auto',
                onSelect: function (dateText, inst) {
                    scope.$apply(function () {
                        ngModel.$setViewValue(dateText);
                    });
                }
            });
        }
    };
});