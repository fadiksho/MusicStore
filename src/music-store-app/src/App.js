import React from "react";
import { Route, Switch } from "react-router-dom";

import SongsPage from "./components/Songs/SongsPage";
import NavHeader from "./components/common/NavHeader";
import pageNotFound from "./PageNotFound";

function App() {
  return (
    <div>
      <NavHeader />
      <div className="container">
        <main role="main" className="pb-3">
          <Switch>
            <Route exact path="/" component={SongsPage} />
            <Route path="/Songs" component={SongsPage} />
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
