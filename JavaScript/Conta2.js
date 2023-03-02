if (typeof (AlfaPeople) == "undefined") { AlfaPeople = {} }
if (typeof (AlfaPeople.Conta) == "undefined") { AlfaPeople.Conta }

AlfaPeople.Conta = {
    OnChengeCNPJ: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var cnpj = formContext.getAttribute("dcp_cnpjcpf").getValue();
        cnpj.replace(/[^\d]/g, "");

        if (cpf.length == 14) {
            var cnpjFormatado = cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");






        }
        
    },
    DynamicsAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };

        var alertOptions = {
            height: 120,
            width: 200
        };

        Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
    }
    }