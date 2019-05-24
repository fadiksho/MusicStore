import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import OverlayNavLoader from "./OverlayNavLoader";
import { connect } from "react-redux";
import PropTypes from "prop-types";

function NavHeader({ showLoader }) {
  const [showAddMenu, setshowAddMenu] = useState(false);
  const [showProfileMenu, setshowProfileMenu] = useState(false);
  return (
    <header>
      <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div className="container">
          <NavLink to="/" exact className="navbar-brand">
            Music Store
          </NavLink>
          <button
            className="navbar-toggler"
            type="button"
            data-toggle="collapse"
            data-target=".navbar-collapse"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon" />
          </button>
          <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
            <ul className="navbar-nav">
              <NavLink to="/Songs" className="nav-item nav-link">
                Songs
              </NavLink>
              <NavLink to="/Genres" className="nav-item nav-link">
                Genres
              </NavLink>
              <NavLink to="/Albums" className="nav-item nav-link">
                Albums
              </NavLink>
              <div className="dropdown">
                <button
                  className="btn nav-link btn-link dropdown dropdown-toggle"
                  id="navbarAddDropdown"
                  data-toggle="navbarAddDropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                  onClick={() => setshowAddMenu(!showAddMenu)}
                >
                  Add New
                </button>
                <div
                  className={"dropdown-menu " + (showAddMenu ? "show" : "")}
                  aria-labelledby="navbarAddDropdown"
                >
                  <NavLink
                    to="/Songs/AddNewSong"
                    className="dropdown-item"
                    onClick={() => setshowAddMenu(false)}
                  >
                    Song
                  </NavLink>
                  <NavLink
                    to="/Genres/AddNewGenre"
                    className="dropdown-item"
                    onClick={() => setshowAddMenu(false)}
                  >
                    Genre
                  </NavLink>
                  <NavLink
                    to="/Albums/AddNewAlbum"
                    className="dropdown-item"
                    onClick={() => setshowAddMenu(false)}
                  >
                    Album
                  </NavLink>
                </div>
              </div>

              <div className="dropdown">
                <button
                  type="button"
                  className="btn nav-link btn-link dropdown dropdown-toggle"
                  data-toggle="accountDropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                  onClick={() => setshowProfileMenu(!showProfileMenu)}
                >
                  User Name
                </button>
                <div
                  className={
                    "dropdown-menu dropdown-menu-md-right " +
                    (showProfileMenu ? "show" : "")
                  }
                  aria-labelledby="accountDropdown"
                >
                  <NavLink
                    className="dropdown-item"
                    to="/User/Profile"
                    onClick={() => setshowProfileMenu(!showProfileMenu)}
                  >
                    My Profile
                  </NavLink>
                  <NavLink
                    className="dropdown-item"
                    to="/Users/Manage"
                    onClick={() => setshowProfileMenu(!showProfileMenu)}
                  >
                    Manage Users
                  </NavLink>
                  <div className="dropdown-divider" />
                  <form asp-controller="Accounts" asp-action="Logout">
                    <button type="submit" className="dropdown-item">
                      Logout
                    </button>
                  </form>
                </div>
              </div>
            </ul>
          </div>
        </div>
      </nav>
      <OverlayNavLoader show={showLoader} />
    </header>
  );
}

function mapStateToProps(state) {
  return {
    showLoader: state.apiCallsInProgress > 0
  };
}

NavHeader.propTypes = {
  showLoader: PropTypes.bool.isRequired
};

export default connect(mapStateToProps)(NavHeader);
