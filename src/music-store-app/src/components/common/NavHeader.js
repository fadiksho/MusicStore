import React, { useState, useEffect } from "react";
import { NavLink, withRouter } from "react-router-dom";
import OverlayNavLoader from "./OverlayNavLoader";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { isAuthenticated, isAdmin } from "../../services/auth.service";
import { logout } from "../../redux/actions/authActions";

function NavHeader({ showLoader, user, logout, history }) {
  const [showAddMenu, setshowAddMenu] = useState(false);
  const [showProfileMenu, setshowProfileMenu] = useState(false);
  const [collapseNav, setCollapseNav] = useState(true);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  function handleLogout() {
    setshowProfileMenu(false);
    logout();
    history.push("/login");
  }
  useEffect(() => {
    console.log("Nav Effect Magic!");
    if (isAuthenticated(user)) setIsLoggedIn(true);
    else setIsLoggedIn(false);
  }, [user]);
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
            onClick={() => setCollapseNav(!collapseNav)}
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon" />
          </button>
          <div
            className={
              "navbar-collapse d-sm-inline-flex flex-sm-row-reverse " +
              (collapseNav ? "collapse" : "")
            }
          >
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
              {isLoggedIn && (
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
                    {isAdmin(user) && (
                      <NavLink
                        to="/Genres/AddNewGenre"
                        className="dropdown-item"
                        onClick={() => setshowAddMenu(false)}
                      >
                        Genre
                      </NavLink>
                    )}
                    <NavLink
                      to="/Albums/AddNewAlbum"
                      className="dropdown-item"
                      onClick={() => setshowAddMenu(false)}
                    >
                      Album
                    </NavLink>
                  </div>
                </div>
              )}
              {!isLoggedIn ? (
                <NavLink to="/login" className="nav-item nav-link">
                  Login
                </NavLink>
              ) : (
                <div className="dropdown">
                  <button
                    type="button"
                    className="btn nav-link btn-link dropdown dropdown-toggle"
                    data-toggle="accountDropdown"
                    aria-haspopup="true"
                    aria-expanded="false"
                    onClick={() => setshowProfileMenu(!showProfileMenu)}
                  >
                    {user.user_name}
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
                    <div className="dropdown-divider" />
                    <button
                      onClick={handleLogout}
                      type="button"
                      className="dropdown-item"
                    >
                      Logout
                    </button>
                  </div>
                </div>
              )}
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
    showLoader: state.apiCallsInProgress > 0,
    user: state.auth.user
  };
}

const mapDispatchToProps = {
  logout
};

NavHeader.propTypes = {
  showLoader: PropTypes.bool.isRequired,
  user: PropTypes.object.isRequired,
  logout: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired
};

export default withRouter(
  connect(
    mapStateToProps,
    mapDispatchToProps
  )(NavHeader)
);
