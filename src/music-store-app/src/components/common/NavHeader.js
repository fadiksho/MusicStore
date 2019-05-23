/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useState } from "react";
import { NavLink } from "react-router-dom";

const NavHeader = () => {
  const [showAddMenu, setshowAddMenu] = useState(false);
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
            <ul className="navbar-nav flex-grow-1">
              <li className="nav-item">
                <NavLink to="/Songs" className="nav-link text-dark">
                  Songs
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink to="/Genres" className="nav-link text-dark">
                  Genres
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink to="/Albums" className="nav-link text-dark">
                  Albums
                </NavLink>
              </li>
              <li
                className="nav-item dropdown mr-auto"
                onClick={() => setshowAddMenu(!showAddMenu)}
              >
                <a
                  className="nav-link dropdown-toggle"
                  href="#"
                  id="navbarDropdown"
                  role="button"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  Add New
                </a>
                <div
                  className={"dropdown-menu " + (showAddMenu ? "show" : "")}
                  aria-labelledby="navbarDropdown"
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
              </li>
              <li className="nav-item dropdown">
                <button
                  type="button"
                  className="btn btn-block text-left btn-outline-primary dropdown-toggle rounded-0 border-top-0 border-left-0 border-right-0"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  @User.Identity.Name
                </button>
                <div
                  className="dropdown-menu dropdown-menu-md-right"
                  aria-labelledby="accountDropdown"
                >
                  <a className="dropdown-item">My Profile</a>
                  <a className="dropdown-item">Manage Users</a>
                  <div className="dropdown-divider" />
                  <form asp-controller="Accounts" asp-action="Logout">
                    <button type="submit" className="dropdown-item">
                      Logout
                    </button>
                  </form>
                </div>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default NavHeader;
