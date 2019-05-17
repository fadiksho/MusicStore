import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function albumReducer(state = initialState.albumsPage, action) {
  switch (action.type) {
    case types.LOAD_ALBUM_PAGE_SUCCESS:
      return { ...state, albums: [...action.albums] };
    case types.LOAD_ALBUM_DETAILS_SUCCESS:
      return { ...state, selectedAlbum: action.album };
    case types.DELETE_ALBUM_SUCCESS:
      return {
        ...state,
        albums: state.albums.filter(album => album.id !== action.album.id)
      };
    default:
      return state;
  }
}
