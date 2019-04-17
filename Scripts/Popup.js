
$(document).ready(function () {

  
    $('#popupBoxClose').click(function () {
        unloadPopupBox();
    });
    $('#CloseBom').click(function () {
       
        unloadPopupBoxBom();
    });

    $('#container').click(function () {
        //   unloadPopupBox();
    });


    /**********************************************************/
});

function PopupClose() {
    
    unloadPopupBoxBom();
}


function unloadPopupBox() {	// TO Unload the Popupbox
   // document.getElementById('popup_box').style.display = "block";
    $('#popup_box').fadeOut("slow");
    $("#container").css({ // this is just for style		
        "opacity": "1"
    });
}

function loadPopupBox() {
   // document.getElementById('popup_box').style.display = "block"; // To Load the Popupbox
    $('#popup_box').fadeIn("slow");
    $("#container").css({ // this is just for style
        "opacity": "0.3"
    });
}
function unloadPopupBoxBom() {	// TO Unload the Popupbox
    // document.getElementById('popup_box').style.display = "block";

    $('#popup_box_addBom').fadeIn("slow");
    $('#popup_box_addBom').css({ "display": "none" });
    $("#container").css({ // this is just for style		
        "opacity": "1"
    });
}

function loadPopupBoxBom() {
    //alert('hello5');
   // $("##popup_box_addBom").show();
    //document.getElementById('popup_box_Inventery').style.display = "block"; // To Load the Popupbox
    $('#popup_box_addBom').fadeIn("slow");
    $('#popup_box_addBom').css({ "display": "block" });
    $("#container").css({ // this is just for style
        "opacity": "1"
    });
}


function ImgbtnUpProcess_click() {
    $("#ContentPlaceHolder1_ModelPopupBOMUC1_txtBomProcess").val("Up");
    loadPopupBoxBom();
}


function ImgbtnDwnProcess_click() {
    $("#ContentPlaceHolder1_ModelPopupBOMUC1_txtBomProcess").val("Down");
    loadPopupBoxBom();
}
function ImgbtnCopy_click() {
    $("#ContentPlaceHolder1_ModelPopupBOMUC1_txtBomProcess").val("Copy");
    loadPopupBoxBom();

    //added 16 may set dropdowm to seleatable bom only so i make it read only and bom as selected text on copy
    
    var theText = "Bom";
    $("#ContentPlaceHolder1_ModelPopupBOMUC1_ddlBomProcessType option:contains(" + theText + ")").attr('selected', 'selected');
    $('#ContentPlaceHolder1_ModelPopupBOMUC1_ddlBomProcessType').attr("disabled", true); 
   
}
function OpenNewWindow(url) {

    var load = window.open(url, '', 'scrollbars=yes, menubar=yes,fullscreen=yes,resizable=yes,toolbar=yes,location=yes,status=yes');
}

