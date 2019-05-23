import { handleResponse, handleError } from "./APIUtils";

const baseUrl = process.env.REACT_APP_API_URL + "song/";

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

export function updateSong(songForUpdatingDto) {
  return fetch(baseUrl + songForUpdatingDto.id, {
    method: "PUT",
    headers: { "content-type": "application/json" },
    body: JSON.stringify(songForUpdatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function getSong(songId) {
  return fetch(baseUrl + songId)
    .then(handleResponse)
    .catch(handleError);
}

export function deleteSong(songId) {
  return fetch(baseUrl + songId, { method: "DELETE" })
    .then(handleResponse)
    .catch(handleError);
}
