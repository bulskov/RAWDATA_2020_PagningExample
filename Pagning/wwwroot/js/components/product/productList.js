define(['knockout', 'dataservice'], (ko, dataservice) => {
    return function(params) {
        let products = ko.observableArray();
        let pageSizes = ko.observableArray();
        let selectedPageSize = ko.observableArray([10]);
        let prev = ko.observable();
        let next = ko.observable();

        let getData = url => {
            dataservice.getProducts(url, data => {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                products(data.items);
            });
        }

        let showPrev = product => {
            console.log(prev());
            getData(prev());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = product => {
            console.log(next());
            getData(next());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(dataservice.getProductsUrlWithPageSize(size));
        });

        getData();



        return {
            pageSizes,
            selectedPageSize,
            products,
            showPrev,
            enablePrev,
            showNext,
            enableNext
        };

    }
})