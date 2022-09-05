import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

import { cardService } from '@/_services';

function List({ match }) {
    const { path } = match;
    const [cards, setcards] = useState(null);

    useEffect(() => {
        cardService.getAll().then(x => setcards(x));
    }, []);

    function deletecard(id) {
        setcards(cards.map(x => {
            if (x.id === id) { x.isDeleting = true; }
            return x;
        }));
        cardService.delete(id).then(() => {
            setcards(cards => cards.filter(x => x.id !== id));
        });
    }

    return (
        <div>
            <h1>cards</h1>
            <Link to={`${path}/add`} className="btn btn-sm btn-success mb-2">Add card</Link>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th style={{ width: '30%' }}>Name</th>
                        <th style={{ width: '30%' }}>card Number</th>
                        <th style={{ width: '10%' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {cards && cards.map(card =>
                        <tr key={card.id}>
                            <td>{card.name}</td>
                            <td>{card.cardNumber}</td>
                            <td style={{ whiteSpace: 'nowrap' }}>
                                <Link to={`${path}/edit/${card.id}`} className="btn btn-sm btn-primary mr-1">Edit</Link>
                                <button onClick={() => deletecard(card.id)} className="btn btn-sm btn-danger" style={{ width: '60px' }} disabled={card.isDeleting}>
                                    {card.isDeleting 
                                        ? <span className="spinner-border spinner-border-sm"></span>
                                        : <span>Delete</span>
                                    }
                                </button>
                            </td>
                        </tr>
                    )}
                    {!cards &&
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