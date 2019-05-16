import * as types from "./actionTypes";
import * as albumApi from "../../api/AlbumAPI";

export function loadAlbumsSuccess(albums) {
  return { type: types.LOAD_ALBUM_PAGE_SUCCESS, albums };
}

export function loadAlbums() {
  return function(dispatch) {
    return albumApi
      .getAlbums()
      .then(genres => {
        dispatch(loadAlbumsSuccess(genres));
      })
      .catch(error => {
        // ToDo: Implement loadAlbumFailure;
        throw error;
      });
  };
}
