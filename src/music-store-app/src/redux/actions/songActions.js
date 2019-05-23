import * as types from "./actionTypes";
import * as songApi from "../../api/SongAPI";

export function loadSongsPageSuccess(songsPagedList) {
  return { type: types.LOAD_SONGS_PAGE_SUCCESS, songsPagedList };
}

export function deleteSongSuccess(song) {
  return { type: types.DELETE_SONG_SUCCESS, song };
}

export function loadSongSuccess(song) {
  return { type: types.LOAD_SONG_SUCCESS, song };
}

export function updateSongSuccess(song) {
  return { type: types.UPDATE_SONG_SUCCESS, song };
}

export function addSongSuccess(song) {
  return { type: types.ADD_SONG_SUCCESS, song };
}

export function loadSongsPage() {
  return function(dispatch) {
    return songApi
      .getSongsPage()
      .then(songsPagedList => {
        dispatch(loadSongsPageSuccess(songsPagedList));
      })
      .catch(error => {
        // ToDo: Implement loadSongFailure;
        throw error;
      });
  };
}

export function updateSong(songForUpdatingDto) {
  return function(dispatch) {
    return songApi
      .updateSong(songForUpdatingDto)
      .then(song => {
        dispatch(updateSongSuccess(song));
      })
      .catch(error => {
        // ToDo: Implement loadSongFailure;
        throw error;
      });
  };
}

export function addSong(songForCreatingDto) {
  return function(dispatch) {
    return songApi
      .addNewSong(songForCreatingDto)
      .then(song => {
        console.log(song);
        dispatch(addSongSuccess(song));
      })
      .catch(error => {
        // ToDo: Implement loadSongFailure;
        throw error;
      });
  };
}

export function loadSong(id) {
  return function(dispatch) {
    return songApi
      .getSong(id)
      .then(song => {
        dispatch(loadSongSuccess(song));
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
