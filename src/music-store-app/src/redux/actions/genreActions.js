import * as types from "./actionTypes";
import * as genreApi from "../../api/GenreAPI";
import { beginApiCall, apiCallError } from "./apiStatusAction";

export function loadGenresSuccess(genres) {
  return { type: types.LOAD_GENRES_PAGE_SUCCESS, genres };
}

export function deleteGenreSuccess(genre) {
  return { type: types.DELETE_GENRE_SUCCESS, genre };
}

export function updateGenreSuccess() {
  return { type: types.UPDATE_GENRE_SUCCESS };
}

export function addGenreSuccess() {
  return { type: types.ADD_GENRE_SUCCESS };
}

export function loadGenres() {
  return function(dispatch) {
    dispatch(beginApiCall());
    return genreApi
      .getGenres()
      .then(genres => {
        dispatch(loadGenresSuccess(genres));
      })
      .catch(error => {
        dispatch(apiCallError());
        // ToDo: Implement loadGenreFailure;
        throw error;
      });
  };
}

export function deleteGenre(genre) {
  return function(dispatch) {
    dispatch(beginApiCall());
    return genreApi
      .deleteGenre(genre.id)
      .then(() => {
        dispatch(deleteGenreSuccess(genre));
      })
      .catch(error => {
        // ToDo: Implement loadGenreFailure;
        throw error;
      });
  };
}

export function updateGenre(genre) {
  return function(dispatch) {
    dispatch(beginApiCall());
    return genreApi
      .updateGenre(genre)
      .then(() => {
        dispatch(updateGenreSuccess());
      })
      .catch(error => {
        dispatch(apiCallError());
        // ToDo: Implement loadSongFailure;
        throw error;
      });
  };
}

export function addGenre(genre) {
  return function(dispatch) {
    dispatch(beginApiCall());
    return genreApi
      .addNewGenre(genre)
      .then(() => {
        dispatch(addGenreSuccess());
      })
      .catch(error => {
        dispatch(apiCallError());
        // ToDo: Implement loadSongFailure;
        throw error;
      });
  };
}
