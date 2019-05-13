import { combineReducers } from "redux";
import { songReducer as songs } from "./songReducer";

const rootReducer = combineReducers({
  songs
});

export default rootReducer;
