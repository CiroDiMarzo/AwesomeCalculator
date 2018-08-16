
(function () {
    /*
    * Wrapper around jQuery for the API calls
    */
    var ApiService = function () {
        this.get = function (url, success, fail) {
            $.get(url)
                .done(function (data) {
                    success(data);
                })
                .fail(function (error) {
                    fail(error.statusError)
                });
        }
    }

    /*
    * Wrapper around the SharePoint JavaScript object model
    */
    var SharePointService = function () {
        this.listItemCollection = null;
        this.listContentTypes = null;
        this.listItem = null;

        this.getAll = function (listName, viewXml, success, fail) {
            var clientContext = new SP.ClientContext.get_current();
            var list = clientContext.get_web().get_lists().getByTitle(listName);
            clientContext.load(list);

            var camlQuery = new SP.CamlQuery();
            camlQuery.set_viewXml(viewXml);
            this.listItemCollection = list.getItems(camlQuery);

            clientContext.load(this.listItemCollection);
            clientContext.executeQueryAsync(
                Function.createDelegate(this, success),
                Function.createDelegate(this, fail));
        }

        this.getListContentTypes = function (listName, success, fail) {
            var clientContext = new SP.ClientContext.get_current();
            var list = clientContext.get_web().get_lists().getByTitle(listName);
            clientContext.load(list);
            this.listContentTypes = list.get_contentTypes();
            clientContext.load(this.listContentTypes)
            clientContext.executeQueryAsync(
                Function.createDelegate(this, success),
                Function.createDelegate(this, fail));
        }

        this.save = function (listName, values, success, fail) {
            var clientContext = new SP.ClientContext.get_current();
            var list = clientContext.get_web().get_lists().getByTitle(listName);
            clientContext.load(list);

            var itemInfo = new SP.ListItemCreationInformation();
            this.listItem = list.addItem(itemInfo);

            for (var i = 0; i < values.length; i++) {
                this.listItem.set_item(values[i].fieldName, values[i].fieldValue);
            }

            this.listItem.update();
            clientContext.load(this.listItem);

            clientContext.executeQueryAsync(
                Function.createDelegate(this, success),
                Function.createDelegate(this, fail));
        }
    }

    /*
    * Implements the business logic of the awesome calculator. Depends on the SharePointService to
    * communicate with SharePoint and on the ApiService to communicate with external REST endpoints
    */
    var CalculatorService = function () {
        this.sharePointService = new SharePointService();
        this.apiService = new ApiService();

        this.listName = 'Operations';
        this.resultFieldName = 'Result';
        this.additionCtName = 'Addition';
        this.subtractionCtName = 'Subtraction';
        this.number1FieldName = 'Number1';
        this.number2FieldName = 'Number2';
        this.contentTypeIdFieldName = 'ContentTypeId';
        this.additionCtId = '';
        this.subtractionCtId = '';
        this.exchangeRate = 0;

        this.apiData = {
            url: 'http://apilayer.net/',
            action: 'api/live?',
            accessKey: 'access_key=e3ea0c2f0d19edcf3657dae02857c76f&',
            currencies: 'currencies=CHF&',
            source: 'source=USD&format=1'
        }

        this.init = function (success, fail) {
            var parent = this;
            this.sharePointService.getListContentTypes(this.listName,
                function () {
                    parent.contentTypeRetrievedSuccess();
                    parent.getExchangeRate(function () {
                        success();
                    });
                },
                this.initFail);
        }

        this.contentTypeRetrievedSuccess = function () {
            var ctEnumerator = this.sharePointService.listContentTypes.getEnumerator();
            while (ctEnumerator.moveNext()) {
                var ct = ctEnumerator.get_current();

                if (ct.get_name() == this.additionCtName) {
                    this.additionCtId = ct.get_id();
                }
                if (ct.get_name() == this.subtractionCtName) {
                    this.subtractionCtId = ct.get_id();
                }
            }
        }

        this.getExchangeRate = function (success) {
            var parent = this;
            this.apiService.get(this.apiData.url + this.apiData.action + this.apiData.accessKey + this.apiData.currencies + this.apiData.source,
                function (data) {
                    parent.exchangeRate = parseFloat(data.quotes['USDCHF']).toFixed(4);
                    success();
                },
                function(errorMessage) {
                    parent.getExchangeRateFail(errorMessage);
                })
        }

        this.sum = function (number1, number2, success, fail) {
            this.save(number1, number2, this.additionCtId, success, fail);
        }

        this.subtract = function (number1, number2, success, fail) {
            this.save(number1, number2, this.subtractionCtId, success, fail);
        }

        this.save = function (number1, number2, contentTypeId, success, fail) {
            var parent = this;
            var values = [
                { fieldName: this.number1FieldName, fieldValue: number1 },
                { fieldName: this.number2FieldName, fieldValue: number2 },
                { fieldName: this.contentTypeIdFieldName, fieldValue: contentTypeId }
            ];

            this.sharePointService.save(this.listName, values,
                function() {
                    parent.readResult(success);
                },
                this.saveFail
            );
        }

        this.readResult = function (callback) {
            var number1Value = this.sharePointService.listItem.get_item(this.number1FieldName);
            var number2Value = this.sharePointService.listItem.get_item(this.number2FieldName);
            var resultValue = this.sharePointService.listItem.get_item(this.resultFieldName);
            callback(number1Value, number2Value, resultValue);
        }

        this.getTotal = function (success, fail) {
            var parent = this;
            var viewXml = '<View><ViewFields><FieldRef Name="Result" /></ViewFields></View>';

            this.sharePointService.getAll(this.listName, viewXml,
                function () {
                    parent.sumResults(success);
                },
                this.getTotalFail
            );
        }

        this.sumResults = function (callback) {
            var total = 0;
            var enumerator = this.sharePointService.listItemCollection.getEnumerator();
            while (enumerator.moveNext()) {
                var listItem = enumerator.get_current();
                var itemValue = listItem.get_item(this.resultFieldName);
                var numberValue = parseFloat(itemValue);
                if (!isNaN(numberValue)) {
                    total += numberValue;
                }
            }
            callback(total, total * this.exchangeRate);
        }

        this.getTotalFail = function () {
            console.log('get total fail');
        }

        this.saveFail = function () {
            console.log('save fail');
        }

        this.initFail = function () {
            console.log('init fail');
        }

        this.getExchangeRateFail = function (errorMessage) {
            console.log('get exchange rate fail: ' + errorMessage);
        }
    }

    /*
    * Below there's data and event binding
    */
    var calculatorService = new CalculatorService();

    bindEvents(contentTypeRequestPending, contentTypeRequestPending);

    ExecuteOrDelayUntilScriptLoaded(init, "sp.js");

    function init() {
        calculatorService.init(function () {
            displayExchangeRate();
            unbindEvents('click');
            bindEvents(save, getTotal);
        }, fail);
    }

    function displayExchangeRate() {
        $('#txtExRate').text('exchange rate: ' + calculatorService.exchangeRate);
        $('#quotesSource').attr('href', calculatorService.apiData.url);
        $('#quotesSource').css('visibility', 'visible');
        $('#quotesSource').attr('target', '_blank');
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
        if (operation === 'sum') {
            sum();
        } else {
            subtract();
        }
    }

    function sum() {
        calculatorService.sum($('#number1').val(), $('#number2').val(), function (number1Value, number2Value, resultValue) {
            displayResult(number1Value, number2Value, resultValue);
        }, fail);
    }

    function subtract() {
        calculatorService.subtract($('#number1').val(), $('#number2').val(), function (number1Value, number2Value, resultValue) {
            displayResult(number1Value, number2Value, resultValue);
        }, fail);
    }

    function displayResult(number1Value, number2Value, resultValue) {
        $('#number1Result').text(number1Value);
        $('#number2Result').text(number2Value);
        $('#result').text(resultValue);
    }

    function getTotal() {
        calculatorService.getTotal(function (total, totalConverted) {
            $('#txtTotal').text('Total: ' + total + ' USD / ' + totalConverted + ' CHF');
        },
        fail);
    }

    function fail(error) {
        console.log('fail');
    }

    function contentTypeRequestPending() {
        alert('The application is still trying to retrive the information about the Operation list');
    }
})();