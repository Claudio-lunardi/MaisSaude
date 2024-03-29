﻿$(document).ready(function () {

    let funcoes = {
        DataTable: () => {
            $('#myTable').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
                },
                "lengthMenu": [10, 15, 20, 30, 100]
            });
        },
        InputMask: () => {
            $('.maskTelefone').inputmask({ mask: ['(99) 9999-99999'] });
            $('.maskCelular').inputmask({ mask: ['(99) 99999-9999'] });
            $('.maskCPF').inputmask({ mask: ['999.999.999-99'] });
            $('.maskCNPJ').inputmask({ mask: ['99.999.999/9999-99'] });
        },
        ValidarSenha: (Senha, NovaSenha) => {
            if (Senha != NovaSenha) {
                alert("SENHAS DIFERENTES!\\nFAVOR DIGITAR SENHAS IGUAIS");
                $("#senha").css("border-color", "#f00");
                return false;
            }
            return true;
        }
    };

funcoes.InputMask();
funcoes.DataTable();

    $("#senhaNova").blur(function (e) {
        Senha = $("#Senha").val();
        NovaSenha = $("#senhaNova").val();
        funcoes.ValidarSenha(Senha, NovaSenha);
    });





    $("#Cep").on('blur', function (e) {
        var cep = $(this).val().replace(/[^0-9]/, '')
        if (cep != "") {
            $.ajax({
                type: "GET",
                url: "https://viacep.com.br/ws/" + cep + "/json/",
                dataType: "json",
                contentType: "application/json",
                success: function (dados) {
                    $("#Cep").val(dados.cep);

                    $("#Cidade").val(dados.localidade);
                    $("#Cidade").attr("readonly", true)

                    $("#Logradouro").val(dados.bairro + ", " + dados.logradouro);
                    $("#Logradouro").attr("readonly", true)

                    $("#Estado").val(dados.uf);
                    $("#Estado").attr("readonly", true)
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    $("#Cidade").attr("readonly", false)
                    $("#Logradouro").attr("readonly", false)
                    $("#Estado").attr("readonly", false)
                    alert("Cpf inválido!")
                }

            });
        }
    });

});