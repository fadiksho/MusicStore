import React from "react";
import { Route, Redirect, withRouter } from "react-router-dom";
import PropTypes from "prop-types";
import { isAuthenticated } from "../../services/auth.service";

function PrivateRoute({ user, location, component: Component, ...props }) {
  return (
    <>
      <Route
        {...props}
        render={props =>
          isAuthenticated(user) ? (
            <Component {...props} />
          ) : (
            <Redirect
              to={{
                pathname: "/login",
                state: { redirectTo: location.pathname }
              }}
            />
          )
        }
      />
    </>
  );
}
PrivateRoute.propTypes = {
  user: PropTypes.object.isRequired,
  component: PropTypes.object.isRequired,
  location: PropTypes.object.isRequired
};

export default withRouter(PrivateRoute);
