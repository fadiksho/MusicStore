import { handleResponse, handleError } from "./apiUtils";

const baseUrl = process.env.REACT_APP_API_URL + "/songs/";

export function getSongsPage() {
  return fetch(baseUrl)
    .then(handleResponse)
    .catch(handleError);
}

export function addNewSong(songForCreatingDto) {
  return fetch(baseUrl, {
    method: "POST",
    headers: { "content-type": "application/json" },
    body: JSON.stringify(songForCreatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function updateSong(id, songForUpdatingDto) {
  return fetch(baseUrl + id, {
    method: "PUT",
    headers: { "content-type": "application/json" },
    body: JSON.stringify(songForUpdatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function deleteSong(songId) {
  return fetch(baseUrl + songId, { method: "DELETE" })
    .then(handleResponse)
    .catch(handleError);
}
