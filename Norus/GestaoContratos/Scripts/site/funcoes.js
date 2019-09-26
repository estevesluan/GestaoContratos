//Funções de utilidade

window.isNullOrEmpty = function (string) {
    return string === undefined || string === null || string === "";
};

//Formata um objeto date em string "yyyy-mm-dd"
window.formatDate = function (date) {
    let d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('/');
}

//Cria um objeto date a partir da string json criada pelo asp net mvc
window.parseDate = function (value) {
    return new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
}

//Cria um objeto date a partir de uma string no formato dd/MM/yyyy
window.createDate = function (string) {

    if (isNullOrEmpty(string))
        return null;

    if (string.length < 10)
        return null;

    var diaMesAno = string.substring(0, 10).split("/");

    return new Date(diaMesAno[2] + "-" + diaMesAno[1] + "-" + diaMesAno[0] + "T00:00:00Z");

};
window.atualizarMenu = function (menuAtivoId) {
    $(".nav-link").removeClass("active");

    if (menuAtivoId != "") {
        $("#" + menuAtivoId).addClass("active");
    }
}

$("body").on("change", ".custom-file-input", function (e) {
    var fileName = e.target.files[0].name;
    $(this).next('.custom-file-label').html(fileName);
});

//Define padrões de mensagens PNotify
PNotify.defaults.styling = "bootstrap4";
PNotify.defaults.icons = 'fontawesome4';
PNotify.defaults.stack = {
    dir1: 'down',
    dir2: 'right',
    firstpos1: 25,
    firstpos2: 25,
    spacing1: 36,
    spacing2: 36,
    push: 'top',
    context: document.body
};

PNotify.defaults.modules = {
    Buttons: {
        sticker: false
    }
};