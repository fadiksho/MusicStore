import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function songReducer(state = initialState.songsPage, action) {
  switch (action.type) {
    case types.LOAD_SONGS_PAGE_SUCCESS:
      return { ...state, songsPagedList: action.songsPagedList };
    case types.LOAD_SONG_SUCCESS:
      return {
        ...state,
        selectedSong: action.song
      };
    case types.ADD_SONG_SUCCESS:
      return {
        ...state,
        selectedSong: action.song
      };
    case types.UPDATE_SONG_SUCCESS:
      return {
        ...state,
        selectedSong: action.song
      };
    case types.DELETE_SONG_SUCCESS:
      return {
        ...state,
        songsPagedList: {
          ...state.songsPagedList,
          tResult: state.songsPagedList.tResult.filter(
            song => song.id !== action.song.id
          )
        }
      };
    default:
      return state;
  }
}
