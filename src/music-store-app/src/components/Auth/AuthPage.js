import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import LoginForm from "./LoginForm";
import { login } from "../../redux/actions/authActions";

function LoginPage({ history, location, login }) {
  const [isSaving, setIsSaving] = useState(false);
  useEffect(() => {
    console.log("LoggingPage Effect Magic!");
  }, []);
  function handleLoginSubmit(user) {
    setIsSaving(true);
    login(user)
      .then(user => {
        const redirectUrl = location.state ? location.state.redirectTo : "/";
        history.push(redirectUrl);
      })
      .catch(error => {
        throw error;
      });
  }

  return (
    <LoginForm handleLoginFormSubmit={handleLoginSubmit} isSaving={isSaving} />
  );
}

const mapDispatchToProps = {
  login
};

LoginPage.propTypes = {
  history: PropTypes.object.isRequired,
  login: PropTypes.func.isRequired,
  location: PropTypes.object.isRequired
};

export default withRouter(
  connect(
    null,
    mapDispatchToProps
  )(LoginPage)
);
