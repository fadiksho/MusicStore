import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function songReducer(state = initialState.songsPage, action) {
  switch (action.type) {
    case types.ADD_SONG:
      return [...state, { ...action.song }];
    case types.LOAD_SONGS_PAGE_SUCCESS:
      return action.songsPage;
    default:
      return state;
  }
}
