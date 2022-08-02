function saveAsExcel(fileName, byteContent) {
    var link = document.createElement('a');
    link.download = fileName;
    console.log(fileName);
    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + byteContent;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function ShowTextWithDots(el) {
    var classList = el.classList;
    if (classList.contains('textWithDots')) {
        el.classList.remove('textWithDots');
    }
    else {
        el.classList.add('textWithDots');
    }
}

function Print() {
    var layout = document.getElementsByClassName('mud-layout')[0];
    var drawer = document.getElementsByClassName('mud-drawer')[0];
    var layoutClassList = layout.classList;
    var drawerClassList = drawer.classList;
    var eles = document.getElementsByClassName('hideOnPrint');
    for (var i = 0; i < eles.length; i++) {
        eles[i].hidden = true;
    }
    var isLayoutOpened = layoutClassList.value.indexOf("open") > -1;
    var isDrawerOpened = drawerClassList.value.indexOf("open") > -1;
    layoutClassList.value = isLayoutOpened ? layoutClassList.value.replace(/open/g, "close") : layoutClassList.value;
    drawerClassList.value = isDrawerOpened ? drawerClassList.value.replace(/open/g, "closed") : drawerClassList.value;
    setTimeout(function () {
        window.print();
        for (var i = 0; i < eles.length; i++) {
            eles[i].hidden = false;
        }
        layoutClassList.value = isLayoutOpened ? layoutClassList.value.replace(/close/g, "open") : layoutClassList.value;
        drawerClassList.value = isDrawerOpened ? drawerClassList.value.replace(/closed/g, "open") : drawerClassList.value;
    }, 500);


}
