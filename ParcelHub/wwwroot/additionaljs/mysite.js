$("#receiverAddress").on('change',function () {
    var addressId = $("#receiverAddress").val();
   
    $.ajax({
        url: '/data/getReceiverAddressId',
        data: JSON.stringify(addressId),
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        async: true,
        success: function (data) {
            $('#receiverName').val(data.receiverName);
            $('#country').val(data.country);
            $('#StreetAddress').val(data.streetAddress);
            $('#Suburb').val(data.suburb);
            $('#City').val(data.city);
            $('#State').val(data.state);
            $('#PostCode').val(data.postCode);


        },
        error: function (data) {
            alert("Failed:" + data[0])//������
        }
    });

});




function box() {

    var warehouseid = $("#chooseWarehouse").val();

    $.ajax({
        url: '/data/getWarehouseDetails',//�������,����һ���ֲ���ͼ,���Ҹ��ֲ���ͼ��������.
        data: JSON.stringify(warehouseid),//����������������(����̨AddUsers()�����Ĳ���,��������Ҫһ�²ſ���)
        type: 'POST',
        contentType: 'application/json;charset=utf-8',//�������ͱ�����
        async: true,//�첽
        success: function (data) //�ɹ���Ļص�����
        {
            $("#address").val(data.addressLine1);//data--���Ƕ�Ӧ�ķֲ���ͼҳ������.
            $("#address1").val(data.addressLine2);
            $("#postcode").val(data.postCode);
            $("#mobile").val(data.mobile);
            $("#receiver").val(data.receiverName);
            var air = data.airService;
            var land = data.landService;
            var ocean = data.oceanFreightService;
            if (air == true) {
                $("#air").val("Avaiable");
            };
            if (land == true) {
                $("#land").val("<h1>GODD</h1>");
            }
            if (ocean == true) {
                $("#ocean").val("Avaiable");
            }

            //ale$("#myDiv").html(data);rt(data)//������
        },
        error: function (data) {
            alert("Failed:" + data[0])//������
        }

    });
}






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