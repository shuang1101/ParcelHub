function DynamicText() {

    var division = document.createElement('tr');
    division.innerHTML = DynamicTextBox("");
    document.getElementById("firstdiv").append(division);
}

function DynamicTextBox() {
    return ' < td > <select class="form-control"><option>YuanTong</option><option>ShenTong</option></select> </td >\
                        <td><input type="text" class="form-control" value="kd11223344" /></td>\
                        <td><input type="text" class="form-control" value="toy" /></td>\
                        <td><input type="text" class="form-control" value="12kg" /></td>\
                        <td><input type="text" class="form-control" value="12RMB" /></td>\
                        <td><input type="text" class="form-control" value="1cmb" /></td>\
                        <td><input type="text" class="form-control" value="24" /></td>\
                        <td>$505.79</td>\
                        <td> <input type="button" onclick="DynamicText()" value="Add line" /></td>\
                    ';
}





function RemoveDivision() {
    document.getElementById("firstdiv").removeChild(div.parentNode.parentNode);
}

