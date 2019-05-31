import { combineReducers } from "redux";
import { songReducer as songsPage } from "./songReducer";
import { genreReducer as genresPage } from "./genreReducer";
import { albumReducer as albumsPage } from "./albumReducer";
import apiCallsInProgress from "./apiStatusReducer";
import { authReducer as auth } from "./authReducer";
const rootReducer = combineReducers({
  songsPage,
  genresPage,
  albumsPage,
  apiCallsInProgress,
  auth
});

export default rootReducer;
