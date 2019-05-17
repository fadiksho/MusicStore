import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function genreReducer(state = initialState.genres, action) {
  switch (action.type) {
    case types.LOAD_GENRES_PAGE_SUCCESS:
      return action.genres;
    case types.DELETE_GENRE_SUCCESS:
      return state.filter(genre => genre.id !== action.genre.id);
    default:
      return state;
  }
}
