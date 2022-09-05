import { BehaviorSubject } from 'rxjs';

import config from 'config';
import { fetchWrapper, history } from '@/_helpers';

const cardSubject = new BehaviorSubject(null);
const baseUrl = `${config.apiUrl}/card`;

export const cardService = {
    getAll,
    getById,
    create,
    update,
    delete: _delete,
    card: cardSubject.asObservable(),
    get cardValue () { return cardSubject.value }
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
        .then(card => {
            if (card.id === cardSubject.value.id) {
                card = { ...cardSubject.value, ...card };
                cardSubject.next(card);
            }
            return card;
        });
}

function _delete(id) {
    return fetchWrapper.delete(`${baseUrl}/${id}`)
        .then(x => {
            if (id === cardSubject.value.id) {
                logout();
            }
            return x;
        });
}

// helper functions

let refreshTokenTimeout;

function startRefreshTokenTimer() {
    const jwtToken = JSON.parse(atob(cardSubject.value.jwtToken.split('.')[1]));

    const expires = new Date(jwtToken.exp * 1000);
    const timeout = expires.getTime() - Date.now() - (60 * 1000);
    refreshTokenTimeout = setTimeout(refreshToken, timeout);
}

function stopRefreshTokenTimer() {
    clearTimeout(refreshTokenTimeout);
}
