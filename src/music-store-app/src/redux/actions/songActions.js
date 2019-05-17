import * as types from "./actionTypes";
import * as songApi from "../../api/SongAPI";

export function addSong(song) {
  return { type: types.ADD_SONG, song };
}

export function loadSongsPageSuccess(songsPage) {
  return { type: types.LOAD_SONGS_PAGE_SUCCESS, songsPage };
}

export function deleteSongSuccess(song) {
  return { type: types.DELETE_SONG_SUCCESS, song };
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

export function deleteSong(song) {
  return function(dispatch) {
    return songApi
      .deleteSong(song.id)
      .then(() => {
        dispatch(deleteSongSuccess(song));
      })
      .catch(error => {
        // ToDo: Implement loadAlbumFailure;
        throw error;
      });
  };
}
