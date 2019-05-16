import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function genreReducer(state = initialState.genres, action) {
  switch (action.type) {
    case types.LOAD_GENRES_PAGE_SUCCESS:
      return action.genres;
    default:
      return state;
  }
}
