require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest.debug",
        text: "lib/require-text/text.min"
    }
});


require(['knockout'], (ko) => {

    ko.applyBindings({});
});