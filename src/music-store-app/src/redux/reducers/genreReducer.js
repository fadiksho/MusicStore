import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function genreReducer(state = initialState.genresPage, action) {
  switch (action.type) {
    case types.LOAD_GENRES_PAGE_SUCCESS:
      return { ...state, genres: action.genres };
    case types.DELETE_GENRE_SUCCESS:
      return {
        ...state,
        genres: action.genres.filter(genre => genre.id !== action.genre.id)
      };
    default:
      return state;
  }
}
