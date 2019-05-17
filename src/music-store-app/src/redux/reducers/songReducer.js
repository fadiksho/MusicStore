import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function songReducer(state = initialState.songsPage, action) {
  switch (action.type) {
    case types.LOAD_SONGS_PAGE_SUCCESS:
      return action.songsPage;
    case types.DELETE_SONG_SUCCESS:
      return {
        ...state,
        tResult: state.tResult.filter(song => song.id !== action.song.id)
      };
    default:
      return state;
  }
}
