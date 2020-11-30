define([], () => {
    const productApiUrl = "api/products";
    const categoriesApiUrl = "api/categories";

    let getJson = (url, callback) => {
        fetch(url).then(response => response.json()).then(callback);
    };

    let getProducts = (url, callback) => {
        if (url === undefined) {
            url = productApiUrl;
        }
        getJson(url, callback);
    };

    let getProductsUrlWithPageSize = size => productApiUrl + "?pageSize=" + size;


    return {
        getProducts,
        getProduct: getJson,
        getProductsUrlWithPageSize
    };
});