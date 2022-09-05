import React from 'react';
import { Link } from 'react-router-dom';

import { userService } from '@/_services';

function Details({ match }) {
    const { path } = match;
    const user = userService.userValue;

    return (
        <div>
            <h1>My Profile</h1>
            <p>
                <strong>Name: </strong> {user.firstName} {user.lastName}<br />
                <strong>Email: </strong> {user.userName}
            </p>
            <p><Link to={`${path}/update`}>Update Profile</Link></p>
        </div>
    );
}

export { Details };