
(function () {

    var additionCt = 'Addition';
    var subtractionCt = 'Subtraction';
    var listName = 'Operations';
    var number1Field = 'Number1';
    var number2Field = 'Number2';
    var resultField = 'Result';
    var contentTypeIdField = 'ContentTypeId';

    var additionCtID;
    var subtractionCtId;

    var listContentTypes = null;
    var listItem = null;

    bindEvents(contentTypeRequestPending, contentTypeRequestPending);

    ExecuteOrDelayUntilScriptLoaded(getListContentTypeId, "sp.js");

    ExecuteOrDelayUntilScriptLoaded(getLatestExchangeRate, "sp.js");

    function getListContentTypeId() {
        console.log('Reading contentTypes list');
        var clientContext = new SP.ClientContext.get_current();
        var list = clientContext.get_web().get_lists().getByTitle(listName);
        clientContext.load(list);

        listContentTypes = list.get_contentTypes();
        clientContext.load(listContentTypes)
        clientContext.executeQueryAsync(Function.createDelegate(this, contentTypeRetrievedSuccess), Function.createDelegate(this, fail));
    }

    function contentTypeRetrievedSuccess(sender, args) {
        console.log('ContentTypes retrieved')
        var ctEnumerator = listContentTypes.getEnumerator();
        while (ctEnumerator.moveNext()) {
            var ct = ctEnumerator.get_current();

            if (ct.get_name() == additionCt) {
                additionCtID = ct.get_id();
                console.log('Addition CT id found: ' + additionCtID);
            }

            if (ct.get_name() == subtractionCt) {
                subtractionCtId = ct.get_id();
                console.log('Subtraction CT id found: ' + subtractionCtId);
            }
        }

        if (additionCtID != null && subtractionCt != null) {
            unbindEvents('click');
            bindEvents(save, readTotal);
        }
    }

    function getLatestExchangeRate() {
        var url = 'http://apilayer.net/api/live?access_key=e3ea0c2f0d19edcf3657dae02857c76f&currencies=CHF&source=USD&format=1';
        $.get(url, function (data) {
            alert('success 1!')
        })
        .done(function (data) {
            alert('success 2!')
        })
        .fail(function (error) {
            alert('fail...' + error.statusText)
        })
        .always();
    }

    function bindEvents(saveHandler, readTotalHandler) {
        $('#btnSaveItem').click(saveHandler);
        $('#btnTotal').click(readTotalHandler);
    }

    function unbindEvents(event) {
        $('#btnSaveItem').unbind(event);
        $('#btnTotal').unbind(event);
    }

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

    function contentTypeRequestPending() {
        alert('The application is still trying to retrive the information about the Operation list');
    }

    function readTotal() {

    }
})();