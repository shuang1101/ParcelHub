

DynamicText.count = 0;
function DynamicText() {

    RemoveDynamicText.count++;
    DynamicText.count++;
    var division = document.createElement('tr');

    division.innerHTML = DynamicTextBox(DynamicText.count);
    document.getElementById("firstdiv").append(division);
}

function DynamicTextBox(counter) {
    counter.count++;

    var string = ' <tr> < td > <input type="text" class="form-control" asp-for="AddMoreParcel[@i].ShippingCompanyAtOrigin" /></td>\
                                    <td><input type="text" class="form-control" asp-for="AddMoreParcel[@i].OriginTrackingNumber" /></td>\
                                    <td><input type="text" class="form-control" asp-for="AddMoreParcel[@i].Description" /></td>\
                                    <td><input type="text" class="form-control" asp-for="AddMoreParcel[@i].EstimateWeight" /></td>\
                                    <td><input type="text" class="form-control" asp-for="AddMoreParcel[@i].EstimateVolume" /></td>\
                                    <td><input type="number" class="form-control" asp-for="AddMoreParcel[@i].NumberOfUnits" /></td>\
                                    <td><input type="number" class="form-control" asp-for="AddMoreParcel[@i].TotalValue" /></td>\
                                    <td><input type="text" class="form-control" asp-for="AddMoreParcel[@i].Reference" /></td>\
                                    <td> <input type="button" onclick="DynamicText()" value="Add line" /> </td></tr >';
    return string.replaceAll('@i', counter);
}





function RemoveDynamicText() {

    
    var list = document.getElementById("firstdiv");

    list.removeChild(list.lastChild);
    RemoveDynamicText.count--;
    DynamicText.count--;
}

