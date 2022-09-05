import { BehaviorSubject } from 'rxjs';

import config from 'config';
import { fetchWrapper, history } from '@/_helpers';

const accountSubject = new BehaviorSubject(null);
const baseUrl = `${config.apiUrl}/account`;

export const accountService = {
    getAll,
    getById,
    create,
    update,
    delete: _delete,
    account: accountSubject.asObservable(),
    get accountValue () { return accountSubject.value }
};

function getAll() {
    return fetchWrapper.get(baseUrl);
}

function getById(id) {
    return fetchWrapper.get(`${baseUrl}/${id}`);
}

function create(params) {
    return fetchWrapper.post(baseUrl, params);
}

function update(id, params) {
    return fetchWrapper.put(`${baseUrl}/update/${id}`, params)
        .then(account => {
            if (account.id === accountSubject.value.id) {
                account = { ...accountSubject.value, ...account };
                accountSubject.next(account);
            }
            return account;
        });
}

function _delete(id) {
    return fetchWrapper.delete(`${baseUrl}/${id}`)
        .then(x => {
            if (id === accountSubject.value.id) {
                logout();
            }
            return x;
        });
}

// helper functions

let refreshTokenTimeout;

function startRefreshTokenTimer() {
    const jwtToken = JSON.parse(atob(accountSubject.value.jwtToken.split('.')[1]));

    const expires = new Date(jwtToken.exp * 1000);
    const timeout = expires.getTime() - Date.now() - (60 * 1000);
    refreshTokenTimeout = setTimeout(refreshToken, timeout);
}

function stopRefreshTokenTimer() {
    clearTimeout(refreshTokenTimeout);
}
