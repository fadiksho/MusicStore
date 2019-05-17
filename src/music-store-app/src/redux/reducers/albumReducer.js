import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function albumReducer(state = initialState.albumsPage, action) {
  switch (action.type) {
    case types.LOAD_ALBUM_PAGE_SUCCESS:
      return { ...state, albums: [...action.albums] };
    case types.LOAD_ALBUM_DETAILS_SUCCESS:
      return { ...state, selectedAlbum: action.album };
    default:
      return state;
  }
}
