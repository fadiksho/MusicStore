import { combineReducers } from "redux";
import { songReducer as songsPage } from "./songReducer";

const rootReducer = combineReducers({
  songsPage
});

export default rootReducer;
