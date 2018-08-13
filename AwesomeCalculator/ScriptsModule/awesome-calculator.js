//couple of global variables
var additionCtID = '0x01003e21e0211f544804b9e6c5402e381260';
var subtractionCtId = '0x01003e21e0211f544804b9e6c5402e381261';
var additionCt = 'Addition';
var subtractionCt = 'Subtraction';
var listName = 'Operations';
var number1Field = 'Number1';
var number2Field = 'Number2';
var resultField = 'Result';
var contentTypeIdField = 'ContentTypeId';

var listItem = null;

function save() {
    var operation = $('input[name="operation"]:checked').val();
    console.log(operation);
    if (operation === 'sum') {
        sum();
    } else {
        subtract();
    }
}

function sum() {
    saveItem(additionCtID);
}

function subtract() {
    saveItem(subtractionCtId);
}

function saveItem(contentTypeId) {
    var clientContext = new SP.ClientContext.get_current();
    var list = clientContext.get_web().get_lists().getByTitle(listName);
    clientContext.load(list);

    var itemInfo = new SP.ListItemCreationInformation();
    listItem = list.addItem(itemInfo);
    listItem.set_item(number1Field, number1Value());
    listItem.set_item(number2Field, number2Value());
    listItem.set_item(contentTypeIdField, contentTypeId);

    listItem.update();
    clientContext.load(listItem);

    clientContext.executeQueryAsync(Function.createDelegate(this, success), Function.createDelegate(this, fail));
}

function number1Value() {
    return $('#number1').val();
}

function number2Value() {
    return $('#number2').val();
}

function success() {
    $('#number1Result').text(listItem.get_item(number1Field));
    $('#number2Result').text(listItem.get_item(number2Field));
    $('#result').text(listItem.get_item(resultField));
}

function readItemSuccess() {
    console.log('Read item success');
}

function fail() {
    console.log('fail');
}

function readTotal() {

}