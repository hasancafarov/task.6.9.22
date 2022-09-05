import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

import { accountService } from '@/_services';

function List({ match }) {
    const { path } = match;
    const [accounts, setAccounts] = useState(null);

    useEffect(() => {
        accountService.getAll().then(x => setAccounts(x));
    }, []);

    function deleteAccount(id) {
        setAccounts(accounts.map(x => {
            if (x.id === id) { x.isDeleting = true; }
            return x;
        }));
        accountService.delete(id).then(() => {
            setAccounts(accounts => accounts.filter(x => x.id !== id));
        });
    }

    return (
        <div>
            <h1>Accounts</h1>
            <Link to={`${path}/add`} className="btn btn-sm btn-success mb-2">Add Account</Link>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th style={{ width: '30%' }}>Name</th>
                        <th style={{ width: '30%' }}>Balance</th>
                        <th style={{ width: '30%' }}>Account Type</th>
                        <th style={{ width: '10%' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {accounts && accounts.map(account =>
                        <tr key={account.id}>
                            <td>{account.balance}</td>
                            <td>{account.balance}</td>
                            <td>{account.accountType}</td>
                            <td style={{ whiteSpace: 'nowrap' }}>
                                <Link to={`${path}/edit/${account.id}`} className="btn btn-sm btn-primary mr-1">Edit</Link>
                                <button onClick={() => deleteAccount(account.id)} className="btn btn-sm btn-danger" style={{ width: '60px' }} disabled={account.isDeleting}>
                                    {account.isDeleting 
                                        ? <span className="spinner-border spinner-border-sm"></span>
                                        : <span>Delete</span>
                                    }
                                </button>
                            </td>
                        </tr>
                    )}
                    {!accounts &&
                        <tr>
                            <td colSpan="4" className="text-center">
                                <span className="spinner-border spinner-border-lg align-center"></span>
                            </td>
                        </tr>
                    }
                </tbody>
            </table>
        </div>
    );
}

export { List };