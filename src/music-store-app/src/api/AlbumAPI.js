import { handleResponse, handleError } from "./APIUtils";

const baseUrl = process.env.REACT_APP_API_URL + "album/";

export function getAlbums() {
  return fetch(baseUrl)
    .then(handleResponse)
    .catch(handleError);
}

export function addNewAlbum(albumForCreatingDto) {
  return fetch(baseUrl, {
    method: "POST",
    headers: { "content-type": "application/json" },
    body: JSON.stringify(albumForCreatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function updateAlbum(id, albumForUpdatingDto) {
  return fetch(baseUrl + id, {
    method: "PUT",
    headers: { "content-type": "application/json" },
    body: JSON.stringify(albumForUpdatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function deleteAlbum(albumId) {
  return fetch(baseUrl + albumId, { method: "DELETE" })
    .then(handleResponse)
    .catch(handleError);
}
