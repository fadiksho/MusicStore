import React, { useEffect } from "react";
import { Route, Switch } from "react-router-dom";

import PropTypes from "prop-types";
import { connect } from "react-redux";
import { loadUser } from "./redux/actions/authActions";

import SongsPage from "./components/Songs/SongsPage";
import ManageSongPage from "./components/Songs/ManageSongPage";
import ManageGenrePage from "./components/Genres/ManageGenrePage";
import ManageAlbumPage from "./components/Albums/ManageAlbumPage";
import GenresPage from "./components/Genres/GenresPage";
import NavHeader from "./components/common/NavHeader";
import pageNotFound from "./PageNotFound";
import AlbumsPage from "./components/Albums/AlbumsPage";
import AlbumDetails from "./components/Albums/AlbumDetails";
import PrivateRoute from "./components/Auth/PrivateRoute";
import LoginPage from "./components/Auth/AuthPage";

function App({ loadUser, user }) {
  useEffect(() => {
    console.log("App Effect Magic!");
    loadUser(user);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <div>
      <NavHeader />
      <div className="container">
        <main role="main" className="pb-3">
          <Switch>
            <Route exact path="/" component={SongsPage} />
            <PrivateRoute
              user={user}
              path="/Songs/AddNewSong"
              component={ManageSongPage}
            />
            <PrivateRoute
              user={user}
              path="/Songs/Edit/:id"
              component={ManageSongPage}
            />
            <Route path="/Songs" component={SongsPage} />
            <PrivateRoute
              user={user}
              path="/Genres/AddNewGenre"
              component={ManageGenrePage}
            />
            <PrivateRoute
              user={user}
              path="/Genres/Edit/:id"
              component={ManageGenrePage}
            />
            <Route path="/Genres" component={GenresPage} />
            <PrivateRoute
              user={user}
              path="/Albums/AddNewAlbum"
              component={ManageAlbumPage}
            />
            <PrivateRoute
              user={user}
              path="/Albums/Edit/:id"
              component={ManageAlbumPage}
            />
            <PrivateRoute
              user={user}
              path="/Albums/Details/:id"
              component={AlbumDetails}
            />
            <Route path="/Albums" component={AlbumsPage} />
            <Route path="/login" component={LoginPage} />
            <Route component={pageNotFound} />
          </Switch>
        </main>
      </div>
      <footer className="border-top footer text-muted">
        <div className="container">&copy; 2019 - MusicStore.MVC - Privacy</div>
      </footer>
    </div>
  );
}

function mapStateToProps(state) {
  let user = state.auth.user;
  if (!user.access_token) {
    {
      const userFromCache = JSON.parse(localStorage.getItem("User"));
      if (userFromCache) user = userFromCache;
    }
  }
  return { user };
}

const mapDispatchToProps = {
  loadUser
};

App.propTypes = {
  user: PropTypes.object.isRequired,
  loadUser: PropTypes.func.isRequired
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(App);
