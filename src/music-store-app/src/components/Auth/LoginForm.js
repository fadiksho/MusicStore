import React, { useState } from "react";
import PropTypes from "prop-types";
import { NavLink } from "react-router-dom";
function LoginForm({ handleLoginFormSubmit, isSaving }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  function handleSubmit(event) {
    event.preventDefault();
    const user = {
      email,
      password
    };
    handleLoginFormSubmit(user);
  }
  return (
    <form onSubmit={handleSubmit}>
      <div className="form-group">
        <label htmlFor="Email">Email</label>
        <input
          id="emailId"
          name="email"
          placeholder="Enter Email Address"
          className="form-control"
          value={email}
          onChange={event => setEmail(event.target.value)}
        />
      </div>
      <div className="form-group">
        <label htmlFor="Password">Password</label>
        <input
          id="passwordId"
          name="password"
          type="password"
          placeholder="Enter Password"
          className="form-control"
          value={password}
          onChange={event => setPassword(event.target.value)}
        />
      </div>
      <div className="form-group d-flex align-items-start">
        <input
          type="submit"
          disabled={isSaving}
          value={isSaving ? "Login you in..." : "Login"}
          className="btn btn-success px-5 py-1"
        />
        <div className="ml-auto text-center">
          <NavLink to="ForgetPassword" className="d-block">
            Forget your Password?
          </NavLink>
          <NavLink to="NewAccount" className="d-block">
            Create new account!
          </NavLink>
        </div>
      </div>
    </form>
  );
}

LoginForm.propTypes = {
  isSaving: PropTypes.bool.isRequired,
  handleLoginFormSubmit: PropTypes.func
};

export default LoginForm;
