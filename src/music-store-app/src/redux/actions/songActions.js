import * as types from "./actionTypes";

export function addSong(song) {
  return { type: types.ADD_SONG, song };
}
