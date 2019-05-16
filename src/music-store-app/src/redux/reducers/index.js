import { combineReducers } from "redux";
import { songReducer as songsPage } from "./songReducer";
import { genreReducer as genresPage } from "./genreReducer";
import { albumReducer as albumsPage } from "./albumReducer";

const rootReducer = combineReducers({
  songsPage,
  genresPage,
  albumsPage
});

export default rootReducer;
