import * as types from "./actionTypes";
import * as albumApi from "../../api/AlbumAPI";
import { beginApiCall } from "./apiStatusAction";

export function loadAlbumsSuccess(albums) {
  return { type: types.LOAD_ALBUM_PAGE_SUCCESS, albums };
}

export function loadAlbumDetailsSuccess(album) {
  return { type: types.LOAD_ALBUM_DETAILS_SUCCESS, album };
}

export function deleteAlbumSuccess(album) {
  return { type: types.DELETE_ALBUM_SUCCESS, album };
}

export function addAlbumSuccess(album) {
  return { type: types.ADD_ALBUM_SUCCESS, album };
}

export function updateAlbumSuccess(album) {
  return { type: types.UPDATE_ALBUM_SUCCESS, album };
}

export function loadAlbums() {
  return function(dispatch) {
    dispatch(beginApiCall());
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
    dispatch(beginApiCall());
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

export function deleteAlbum(album) {
  return function(dispatch) {
    dispatch(beginApiCall());
    return albumApi
      .deleteAlbum(album.id)
      .then(() => {
        dispatch(deleteAlbumSuccess(album));
      })
      .catch(error => {
        // ToDo: Implement loadAlbumFailure;
        throw error;
      });
  };
}

export function updateAlbum(album) {
  return function(dispatch) {
    dispatch(beginApiCall());
    return albumApi
      .updateAlbum(album)
      .then(album => {
        dispatch(updateAlbumSuccess(album));
      })
      .catch(error => {
        // ToDo: Implement loadSongFailure;
        throw error;
      });
  };
}

export function addAlbum(album) {
  return function(dispatch) {
    dispatch(beginApiCall());
    return albumApi
      .addNewAlbum(album)
      .then(album => {
        dispatch(addAlbumSuccess(album));
      })
      .catch(error => {
        // ToDo: Implement loadSongFailure;
        throw error;
      });
  };
}
