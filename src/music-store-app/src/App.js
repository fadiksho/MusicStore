import React from "react";
import { Route, Switch } from "react-router-dom";

import SongsPage from "./components/Songs/SongsPage";
import GenresPage from "./components/Genres/GenresPage";
import NavHeader from "./components/common/NavHeader";
import pageNotFound from "./PageNotFound";
import AlbumsPage from "./components/Albums/AlbumsPage";
import AlbumDetails from "./components/Albums/AlbumDetails";

function App() {
  return (
    <div>
      <NavHeader />
      <div className="container">
        <main role="main" className="pb-3">
          <Switch>
            <Route exact path="/" component={SongsPage} />
            <Route path="/Songs" component={SongsPage} />
            <Route path="/Genres" component={GenresPage} />
            <Route path="/Albums/Details/:id" component={AlbumDetails} />
            <Route path="/Albums" component={AlbumsPage} />
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

export default App;
