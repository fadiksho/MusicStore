import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function albumReducer(state = initialState.albums, action) {
  switch (action.type) {
    case types.LOAD_ALBUM_PAGE_SUCCESS:
      return action.albums;
    default:
      return state;
  }
}
