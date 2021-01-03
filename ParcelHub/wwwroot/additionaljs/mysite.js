

DynamicText.count = 0;
function DynamicText() {

    RemoveDynamicText.count++;
    DynamicText.count++;
    var division = document.createElement('tr');

    division.innerHTML = DynamicTextBox(DynamicText.count);
    document.getElementById("firstdiv").append(division);
}

function DynamicTextBox(counter) {
   

    var string = ' <td><input type="text" class="form-control" name="ShippingCompanyAtOrigin[@i]" /></td>\
        < td > <input type="text" class="form-control" name="OriginTrackingNumber[@i]" /></td>\
                            <td><input type="text" class="form-control" name="Description[@i]" /></td>\
                            <td><input type="text" class="form-control" name="EstimateWeight[@i]" /></td>\
                            <td><input type="text" class="form-control" name="EstimateVolume[@i]" /></td>\
                            <td><input type="number" class="form-control" name="NumberOfUnits[@i]" /></td>\
                            <td><input type="number" class="form-control" name="TotalValue[@i]" /></td>\
                            <td><input type="text" class="form-control" name="Reference[@i]"  /></td>\
                                    <td> <input type="button" onclick="DynamicText()" value="Add line" /> </td>\
                                      < td > <input type="button" onclick="RemoveDynamicText()" value="RemoveLine" /> </td></tr >';

    return string.replaceAll('@i', counter); //
}





function RemoveDynamicText() {

    
    var list = document.getElementById("firstdiv");

    list.removeChild(list.lastChild);
    RemoveDynamicText.count--;
    DynamicText.count--;
}






EditDynamicText.count = 100;
function EditDynamicText() {

    EditRemoveDynamicText.count++;
    EditDynamicText.count++;
    var division = document.createElement('tr');

    division.innerHTML = EditDynamicTextBox(EditDynamicText.count);
    document.getElementById("firstdiv-edit").append(division);
}

function EditDynamicTextBox(counter) {
   

    var string = ' <td><input type="hidden" name="parcelId"/><input type="text" class="form-control" name="OriginCourierCompany" required /></td>\
        < td > <input type="text" class="form-control" name="OriginTrackingNumber" required /></td>\
                            <td><input type="text" class="form-control" name="Description" required /></td>\
                            <td><input type="text" class="form-control" name="EstimateWeight" /></td>\
                            <td><input type="text" class="form-control" name="EstimateVolume" /></td>\
                            <td><input type="number" class="form-control" name="NumberOfUnits" required /></td>\
                            <td><input type="number" class="form-control" name="TotalValue" required /></td>\
                            <td><input type="text" class="form-control" name="Reference" /></td>\
                            <td> <input type="button" onclick="DynamicText()" value="Add line" /> </td>\
                             <td> <input type="button" onclick="DynamicText()" value="Add line" /> </td>';

    return string.replaceAll('@i', counter); //
}





function EditRemoveDynamicText() {


    var list = document.getElementById("firstdiv-edit");

    list.removeChild(list.lastChild);
   EditRemoveDynamicText.count--;
    EditDynamicText.count--;
}