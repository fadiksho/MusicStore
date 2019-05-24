import { combineReducers } from "redux";
import { songReducer as songsPage } from "./songReducer";
import { genreReducer as genresPage } from "./genreReducer";
import { albumReducer as albumsPage } from "./albumReducer";
import apiCallsInProgress from "./apiStatusReducer";
const rootReducer = combineReducers({
  songsPage,
  genresPage,
  albumsPage,
  apiCallsInProgress
});

export default rootReducer;
