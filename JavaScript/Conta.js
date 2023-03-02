if (typeof (LogisticsPrincipal) == "undefined") { LogisticsPrincipal = {} }
if (typeof (LogisticsPrincipal.Conta) == "undefined") { LogisticsPrincipal.Conta = {} }

LogisticsPrincipal.Conta =
{
    OnChangeCEP: function (executionContext) {
        var formContext = executionContext.getFormContext();

        var id = Xrm.Page.data.entity.getId();

        var cep = formContext.getAttribute("dcp_cep").getValue();

        if (cep != null) {
            cep = cep.replace(/[^\d]/g, "");

        }


        if (cep != null && cep.length == 8) {
            var execute_dcp_BuscaCep_Request = {
                // Parameters
                entity: { entityType: "account", id: id }, // entity
                CEP: cep, // Edm.String

                getMetadata: function () {
                    return {
                        boundParameter: "entity",
                        parameterTypes: {
                            entity: { typeName: "mscrm.account", structuralProperty: 5 },
                            CEP: { typeName: "Edm.String", structuralProperty: 1 }
                        },
                        operationType: 0, operationName: "dcp_BuscaCep"
                    };
                }
            };

            Xrm.WebApi.online.execute(execute_dcp_BuscaCep_Request).then(
                function success(response) {
                    debugger;
                    if (response.ok) { return response.json(); }
                }
            ).then(function (responseBody) {
                debugger;
                var result = responseBody;
                console.log(result);

                var logradouro = result["logradouro"];
                formContext.getAttribute("dcp_logradouro").setValue(logradouro);
                formContext.getAttribute("dcp_complemento").setValue(result["complemento"]);
                formContext.getAttribute("dcp_bairro").setValue(result["bairro"]);
                formContext.getAttribute("dcp_cidade").setValue(result["localidade"]);
                formContext.getAttribute("dcp_uf").setValue(result["uf"]);
                formContext.getAttribute("dcp_ibge").setValue(result["ibge"]);
                formContext.getAttribute("dcp_ddd").setValue(result["ddd"]);
                var cepFormatado = cep.replace(/(\d{5})(\d{3})/, "$1-$2");
                formContext.getAttribute("dcp_cep").setValue(cepFormatado);

                if (result["erro"]) {
                    LogisticsPrincipal.Conta.DynamicsAlert("CEP inexistente", "CEP inválido").then(function close(data) {

                        formContext.getControl("dcp_cep").setFocus();
                    });;
                }


            }).catch(function (error) {
                debugger;
                console.log(error.message);
            });


        }
        else {
            if (cep == null) {
                return
            }
            else {
                LogisticsPrincipal.Conta.DynamicsAlert("Informe um CEP", "CEP inválido", formContext).then(function close(data) {

                    formContext.getControl("dcp_cep").setFocus();
                });
                formContext.getAttribute("dcp_logradouro").setValue("");
                formContext.getAttribute("dcp_complemento").setValue("");
                formContext.getAttribute("dcp_bairro").setValue("");
                formContext.getAttribute("dcp_numero").setValue("");
                formContext.getAttribute("dcp_cidade").setValue("");
                formContext.getAttribute("dcp_uf").setValue("");
                formContext.getAttribute("dcp_ibge").setValue("");
                formContext.getAttribute("dcp_ddd").setValue("");

            }
        }
    },
    DynamicsAlert: function (alertText, alertTitle) {

        var alerStrings = {
            confimButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };
        var alertOptions = {
            height: 120,
            width: 200
        };

        return Xrm.Navigation.openAlertDialog(alerStrings, alertOptions)

    }
}

    





