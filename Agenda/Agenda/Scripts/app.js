window.onload = () => {
    const contactoInterno = document.getElementById("MainContent_DDContactoInt");
    const organizacion = document.getElementById("MainContent_TxtOrganizacion");
    const area = document.getElementById("MainContent_DDArea");
    if (organizacion.attributes.class == undefined && area.attributes.class == undefined) {
        if (contactoInterno.options[contactoInterno.selectedIndex].text == "SI") {
            organizacion.disabled = true
            organizacion.value = "";
            area.disabled = false;
        } else if (contactoInterno.options[contactoInterno.selectedIndex].text == "NO") {
            organizacion.disabled = false;
            area.disabled = true;
        } else {
            organizacion.disabled = true;
            area.disabled = true;
        };
    }

    contactoInterno.onchange = e => {
        if (contactoInterno.options[contactoInterno.selectedIndex].text == "SI") {
            organizacion.disabled = true
            organizacion.value = "";
            area.disabled = false;
        } else if (contactoInterno.options[contactoInterno.selectedIndex].text == "NO") {
            organizacion.disabled = false;
            area.selectedIndex = 0;
            area.disabled = true;
        } else {
            organizacion.value = "";
            area.selectedIndex = 0;
            organizacion.disabled = true;
            area.disabled = true;
        };
    };
    
}

LimpiarCampos = () => {

    const area = document.getElementById("MainContent_DDArea");
    const organizacion = document.getElementById("MainContent_TxtOrganizacion");

    let inputs = document.getElementById("contenedorFiltros").getElementsByTagName("input");
    let selects = document.getElementById("contenedorFiltros").getElementsByTagName("select");

    for (let input of inputs) input.id != "MainContent_TxtFechaIngresoD" && input.id != "MainContent_TxtFechaIngresoH" ?
        input.value = ""
        :
        input.id == "MainContent_TxtFechaIngresoD" ? input.value = restarDias(new Date(), 30) : input.value = new Date().format("dd/MM/yyyy");
    for (let select of selects) select.selectedIndex = 0;
    area.disabled = true;
    organizacion.disabled = false;

    return false;
}

stringToDate = (fechaString) => {
    const arrayFecha = fechaString.split("/");
    return new Date(arrayFecha[2], arrayFecha[1] - 1, arrayFecha[0]).format("dd/MM/yyyy");
}

validarFormatoFecha = (fechaString) => {
    const regExp = new RegExp(/^([0-2][0-9]|3[0-1])(\/)(0[1-9]|1[0-2])\2(\d{4})$/g);
    return regExp.test(fechaString);
}

validarFechaMenorIgual = (strDateD, strDateH) => {
    let ingresoDDate = stringToDate(strDateD);
    let ingresoHDate = stringToDate(strDateH);
    return ingresoDDate <= ingresoHDate ? true : false;
}

restarDias = (fecha, dias) => {
    return new Date(fecha.getFullYear(),fecha.getMonth(), fecha.getDate() - dias).format("dd/MM/yyyy");
}