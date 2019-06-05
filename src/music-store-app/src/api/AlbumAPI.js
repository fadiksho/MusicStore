import { handleResponse, handleError } from "./APIUtils";
import { authHeader } from "../helper/authHelper";

const baseUrl = process.env.REACT_APP_API_URL + "album/";

export function getAlbums() {
  const authHeaderProps = authHeader();
  return fetch(baseUrl, {
    method: "GET",
    headers: { ...authHeaderProps }
  })
    .then(handleResponse)
    .catch(handleError);
}

export function getAlbum(id) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl + id, {
    method: "GET",
    headers: { ...authHeaderProps }
  })
    .then(handleResponse)
    .catch(handleError);
}

export function addNewAlbum(albumForCreatingDto) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl, {
    method: "POST",
    headers: { "content-type": "application/json", ...authHeaderProps },
    body: JSON.stringify(albumForCreatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function updateAlbum(albumForUpdatingDto) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl + albumForUpdatingDto.id, {
    method: "PUT",
    headers: { "content-type": "application/json", ...authHeaderProps },
    body: JSON.stringify(albumForUpdatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function deleteAlbum(albumId) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl + albumId, {
    method: "DELETE",
    headers: {
      ...authHeaderProps
    }
  })
    .then(handleResponse)
    .catch(handleError);
}
