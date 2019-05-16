import * as types from "./actionTypes";
import * as genreApi from "../../api/GenreAPI";

export function loadGenresSuccess(genres) {
  return { type: types.LOAD_GENRES_PAGE_SUCCESS, genres };
}

export function loadGenres() {
  return function(dispatch) {
    return genreApi
      .getGenres()
      .then(genres => {
        dispatch(loadGenresSuccess(genres));
      })
      .catch(error => {
        // ToDo: Implement loadGenreFailure;
        throw error;
      });
  };
}
