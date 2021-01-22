import apiServiceUrl from './apiServiceUrl.js';

let apiService = {
    getTableNames: () => { return axios.get(apiServiceUrl.getTableNamesUrl()) },
    getColumnNames: (tname) => { return axios.get(apiServiceUrl.getColumnNamesUrl() + "?tname=" + tname) },
    getData: (tname) => { return axios.get(apiServiceUrl.getDataUrl() + "?tname=" + tname) },
    bulkUpdate: (table) => { return axios.post(apiServiceUrl.bulkUpdateUrl(), table) },
    rowUpdate: (table) => { return axios.post(apiServiceUrl.rowUpdateUrl(), table) }
}

export default apiService