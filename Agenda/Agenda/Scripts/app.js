window.onload = function () {
    const fechaIngresoD = document.getElementById("MainContent_TxtFechaIngresoD");
    const fechaIngresoH = document.getElementById("MainContent_TxtFechaIngresoH");

    const contactoInterno = document.getElementById("MainContent_DDContactoInt");
    const organizacion = document.getElementById("MainContent_TxtOrganizacion");
    const area = document.getElementById("MainContent_DDArea");
    const btnClnFilters = document.getElementById("MainContent_BtnLimpiarFiltros");
    const btnBuscar = document.getElementById("MainContent_BtnBuscar");
    const spanAviso = document.getElementById("MainContent_SpanAviso");
    area.disabled = true;

    fechaIngresoD.onblur = e => {
        if (validarFormatoFecha(fechaIngresoD.value)) {
            spanAviso.innerText = "";
            spanAviso.className = "inactivo";

            if (validarFechaMenorIgual(fechaIngresoD.value, fechaIngresoH.value)) {
                spanAviso.innerText = "";
                spanAviso.className = "inactivo";
                btnBuscar.disabled = false;
                btnBuscar.style.opacity = 1;
            } else {
                spanAviso.innerText = "Fecha ingreso desde es invalida, debe ser menor o igual a la fecha ingreso hasta.";
                spanAviso.className = "activo";
                btnBuscar.disabled = true;
                btnBuscar.style.opacity = 0.5;
            }
                
        } else {
            spanAviso.innerText = "Fecha ingreso desde es invalida, debe tener el formato dd/MM/yyyy. Ej: 23/07/1996";
            spanAviso.className = "activo";
            btnBuscar.disabled = true;
            btnBuscar.style.opacity = 0.5;
        }
    }
    fechaIngresoH.onblur = e => {
        
        if (validarFormatoFecha(fechaIngresoH.value)) {
            spanAviso.innerText = "";
            spanAviso.className = "inactivo";

            if (validarFechaMenorIgual(fechaIngresoD.value, fechaIngresoH.value)) {
                spanAviso.innerText = "";
                spanAviso.className = "inactivo";
                btnBuscar.disabled = false;
                btnBuscar.style.opacity = 1;
            }else {
                spanAviso.innerText = "Fecha ingreso hasta es invalida, debe ser mayor o igual a la fecha ingreso desde.";
                spanAviso.className = "activo";
                btnBuscar.disabled = true;
                btnBuscar.style.opacity = 0.5;
            }
        } else {
            spanAviso.innerText = "Fecha ingreso hasta es invalida, debe tener el formato dd/MM/yyyy. Ej: 23/07/1996";
            spanAviso.className = "activo";
            btnBuscar.disabled = true;
            btnBuscar.style.opacity = 0.5;
        };
    }

    contactoInterno.onchange = e => {
        if (contactoInterno.options[contactoInterno.selectedIndex].text == "SI") {
            organizacion.disabled = true
            organizacion.value = "";
            area.disabled = false;
        } else {
            organizacion.disabled = false;
            area.disabled = true;
        };       
    }

    btnClnFilters.onclick = e => {
        e.preventDefault();

        let inputs = document.getElementById("contenedorFiltros").getElementsByTagName("input");
        let selects = document.getElementById("contenedorFiltros").getElementsByTagName("select");

        for (let input of inputs) input.id != "MainContent_TxtFechaIngresoD" && input.id != "MainContent_TxtFechaIngresoH" ?
            input.value = ""
            : 
            input.id == "MainContent_TxtFechaIngresoD" ? input.value = restarDias(new Date(), 30) : input.value = new Date().format("dd/MM/yyyy");
        for (let select of selects) select.selectedIndex = 0;
        area.disabled = true;
        organizacion.disabled = false;

    }

    
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