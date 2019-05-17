import * as types from "./actionTypes";
import * as albumApi from "../../api/AlbumAPI";

export function loadAlbumsSuccess(albums) {
  return { type: types.LOAD_ALBUM_PAGE_SUCCESS, albums };
}

export function loadAlbumDetailsSuccess() {
  return { type: types.LOAD_ALBUM_DETAILS_SUCCESS };
}

export function loadAlbums() {
  return function(dispatch) {
    return albumApi
      .getAlbums()
      .then(albums => {
        dispatch(loadAlbumsSuccess(albums));
      })
      .catch(error => {
        // ToDo: Implement loadAlbumFailure;
        throw error;
      });
  };
}

export function loadAlbumDetails(id) {
  return function(dispatch) {
    return albumApi
      .getAlbum(id)
      .then(album => {
        dispatch(loadAlbumDetailsSuccess(album));
      })
      .catch(error => {
        // ToDo: Implement loadAlbumFailure;
        throw error;
      });
  };
}
