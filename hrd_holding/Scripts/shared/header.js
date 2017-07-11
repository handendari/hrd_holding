var vTheme = 'custom_merah';

var winHeight = 0;
var winWidth = 0;

var vCmbReligion = [
        "ISLAM",
        "KRISTEN",
        "KATOLIK",
        "HINDU",
        "BUDHA",
        "OTHERS"
];

var vCmbRelation = [
        "SUAMI",
        "ISTRI",
        "ANAK",
        "ORANG TUA",
        "SAUDARA"
];

var vCmbGender = [
        "LAKI LAKI",
        "PEREMPUAN"
];

var vCmbEducation = [
        "PRA TK",
        "TK",
        "SD",
        "SLTP",
        "SLTA/SMK",
        "D1",
        "D2",
        "D3",
        "D4",
        "S1",
        "S2",
        "S3"
];

var vCmbSkillLevel = [
        "EXCELLENT",
        "GOOD",
        "AVERAGE",
        "POOR",
        "NO"
];

var vCmbMarital = [
        "SINGLE",
        "MARRIED"
];

function f_PosX(pLebar) {
    winWidth = $(window).width();
    var posX = (winWidth / 2) - ((pLebar).width() / 2) + $(window).scrollLeft();
    return posX;
}

function f_PosY(pTinggi) {
    winHeight = $(window).height();
    var posY = (winHeight / 2) - ((pTinggi).height() / 2) + $(window).scrollTop();
    return posY;
}

function f_MessageBoxShow(pText) {
    $("#modInfoText").html(pText);
    $("#modInfo").jqxWindow("open");

}

function f_NotificationShow(pJxNotif, pNotifContent, pText) {
    (pNotifContent).html(pText);
    (pJxNotif).jqxNotification("open");
}

function f_ShowLoaderModal() {
    $('body').loadingModal({ text: 'Loading Data, Please Wait...' });
    $('body').loadingModal('animation', 'rotatingPlane').loadingModal('backgroundColor', 'red');


    //delay(time)
    //        .then(function() { $('body').loadingModal('animation', 'rotatingPlane').loadingModal('backgroundColor', 'red'); return delay(time);})
    //        .then(function() { $('body').loadingModal('animation', 'wave'); return delay(time);})
    //        .then(function() { $('body').loadingModal('animation', 'wanderingCubes').loadingModal('backgroundColor', 'green'); return delay(time);})
    //        .then(function() { $('body').loadingModal('animation', 'spinner'); return delay(time);})
    //        .then(function() { $('body').loadingModal('animation', 'chasingDots').loadingModal('backgroundColor', 'blue'); return delay(time);})
    //        .then(function() { $('body').loadingModal('animation', 'threeBounce'); return delay(time);})
    //        .then(function() { $('body').loadingModal('animation', 'circle').loadingModal('backgroundColor', 'black'); return delay(time);})
    //        .then(function() { $('body').loadingModal('animation', 'cubeGrid'); return delay(time);})
    //        .then(function() { $('body').loadingModal('animation', 'fadingCircle').loadingModal('backgroundColor', 'gray'); return delay(time);})
    //        .then(function() { $('body').loadingModal('animation', 'foldingCube'); return delay(time); } )
    //        .then(function() { $('body').loadingModal('color', 'black').loadingModal('text', 'Done :-)').loadingModal('backgroundColor', 'yellow');  return delay(time); } )
    //        .then(function() { $('body').loadingModal('hide'); return delay(time); } )
    //        .then(function() { $('body').loadingModal('destroy') ;} );
}

function f_HideLoaderModal() {
    $('body').loadingModal('hide');
}

$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)')
                      .exec(window.location.href);

    if (results != null) {
        return results[1] || 0;
    } else { return "" }
}

$(document).ready(function () {
    winHeight = $(window).height();
    winWidth = $(window).width();

    //KEEP CENTERED WHEN SCROLLING
    $(window).scroll(function () {
        $('#modInfo').jqxWindow({ position: { x: f_PosX($('#modInfo')), y: f_PosY($('#modInfo')) } });
    });

    //KEEP CENTERED EVEN WHEN RESIZING
    $(window).resize(function () {
        $('#modInfo').jqxWindow({ position: { x: f_PosX($('#modInfo')), y: f_PosY($('#modInfo')) } });
    });

    $("#jqxLoader").jqxLoader({ isModal: true, width: 100, height: 60, imagePosition: 'top' });
    $("#btnInfoOke").jqxButton({ theme: vTheme, height: 30, width: 100 });

    $("#modInfo").jqxWindow({
        height: 150, width: 300,
        theme: vTheme, isModal: true,
        autoOpen: false,
        resizable: false
    });

    $('#btnInfoOke').on('click', function (event) {
        $("#modInfo").jqxWindow('close');
    });

});