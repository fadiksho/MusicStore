import { combineReducers } from "redux";
import { songReducer as songsPage } from "./songReducer";
import { genreReducer as genresPage } from "./genreReducer";

const rootReducer = combineReducers({
  songsPage,
  genresPage
});

export default rootReducer;
