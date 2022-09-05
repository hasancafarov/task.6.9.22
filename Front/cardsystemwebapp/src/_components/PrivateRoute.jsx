import React from 'react';
import { Route, Redirect } from 'react-router-dom';

import { userService } from '@/_services';

function PrivateRoute({ component: Component, roles, ...rest }) {
    return (
        <Route {...rest} render={props => {
            const user = userService.userValue;
            if (!user) {
                return <Redirect to={{ pathname: '/user/login', state: { from: props.location } }} />
            }

            if (roles && roles.indexOf(user.role) === -1) {
                return <Redirect to={{ pathname: '/'}} />
            }

            return <Component {...props} />
        }} />
    );
}

export { PrivateRoute };
