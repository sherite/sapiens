var CloseText ='Cerrar';
var AcceptText ='Aceptar';

void function ($, window)
{
    if (window.bAlert == undefined)
    {
        var alertModalTemplate = '<div id="nh-modal-alert" class="modal fade" tabindex="-1" role="dialog" data-keyboard="false" data-backdrop="static">' +
            '<div class="modal-dialog" role="document">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
            '<h4 class="modal-title">{{Titulo}}</h4>' +
            '</div>' +
            '<div class="modal-body">' +
            '{{Cuerpo}}' +
            '</div>' +
            '<div class="row" style="width:98%">' +
            '<div class="row" style="display:flex;justify-content:center">' +
            '<button id="bootstrapAlert-btn" type="button" class="btn btn-primary roundedCAP">{{BotonAceptar}}</button>&nbsp&nbsp' +
            '<button type="button" class="btn btn-warning roundedCAP" data-dismiss="modal">{{BotonCerrar}}</button>' + 
            '</div></br>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';

        var $container;

        $(document).ready(function ()
        {
            $('body').append('<div id="bootstrapAlert-container"></div>');
            $container = $('div#bootstrapAlert-container');

        });

        window.bAlertsLevels =
            {
                DANGER: 'alert-danger',
                WARNING: 'alert-warning',
                SUCCESS: 'alert-success',
                INFO: 'alert alert-info'
            };

        Object.seal(window.bAlertsLevels);

        window.bAlert = function (title, bodyLine1, bodyLine2, alertLevel, confirmCallback)
        {
            var body = bodyLine1 + "</br></br>" + bodyLine2;

            $container.html("");

            $container.wrapInner(

                alertModalTemplate
                    .replace('{{Titulo}}', title)
                    .replace('{{Cuerpo}}', body)
                    .replace('{{BotonAceptar}}', AcceptText)
                    .replace('{{BotonCerrar}}', CloseText)
            );

            var $Modal = $('div#nh-modal-alert');

            $("div.modal-header", $Modal).addClass(alertLevel);

            if (typeof (confirmCallback) === "function") {

                $("button#bootstrapAlert-btn").click(function () {

                    $Modal.modal('hide');

                    setTimeout(confirmCallback, 1500);
                });

            } else
            {
                $("button#bootstrapAlert-btn").remove();
            }

            $Modal.modal('show');

            //FIX para que no se modifique el layout de la pagina cuando se llama al bAlert
            $Modal.on("hidden.bs.modal", function (e) { $("body").removeAttr("style"); });
        }
    }
}(jQuery, window);