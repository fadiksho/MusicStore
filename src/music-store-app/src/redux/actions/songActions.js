import * as types from "./actionTypes";
import * as songApi from "../../api/SongAPI";

export function addSong(song) {
  return { type: types.ADD_SONG, song };
}

export function loadSongsPageSuccess(songsPage) {
  return { type: types.LOAD_SONGS_PAGE_SUCCESS, songsPage };
}

export function loadSongsPage() {
  return function(dispatch) {
    return songApi
      .getSongsPage()
      .then(songsPage => {
        dispatch(loadSongsPageSuccess(songsPage));
      })
      .catch(error => {
        // ToDo: Implement loadSongFailure;
        throw error;
      });
  };
}
