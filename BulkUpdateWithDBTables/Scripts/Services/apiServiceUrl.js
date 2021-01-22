let url = "https://localhost:44392/";

let apiServiceUrl = {
    getTableNamesUrl: () => { return url + 'home/getTableNames' },
    getColumnNamesUrl: () => { return url + 'home/getColumnNames' },
    bulkUpdateUrl: () => { return url + 'home/bulkUpdate' },
    rowUpdateUrl: () => { return url + 'home/rowUpdate' },
    getDataUrl: () => { return url + 'home/getData' }
}

export default apiServiceUrl