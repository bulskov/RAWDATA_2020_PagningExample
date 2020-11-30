require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest.debug",
        text: "lib/require-text/text.min",
        dataservice: "services/dataService"
    }
});

require(['knockout', 'text'], (ko) => {

    ko.components.register('product-list', {
        viewModel: { require: "components/product/productList"},
        template: { require: "text!components/product/productList.html" }

    });


});


require(['knockout', 'viewModel'], (ko, vm) => {

    ko.applyBindings(vm);
});