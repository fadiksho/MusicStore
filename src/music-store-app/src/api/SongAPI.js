import { handleResponse, handleError } from "./APIUtils";
import { authHeader } from "../helper/authHelper";

const baseUrl = process.env.REACT_APP_API_URL + "song/";

export function getSongsPage() {
  const authHeaderProps = authHeader();
  return fetch(baseUrl, {
    method: "GET",
    headers: { ...authHeaderProps }
  })
    .then(handleResponse)
    .catch(handleError);
}

export function addNewSong(songForCreatingDto) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl, {
    method: "POST",
    headers: { "content-type": "application/json", ...authHeaderProps },
    body: JSON.stringify(songForCreatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function updateSong(songForUpdatingDto) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl + songForUpdatingDto.id, {
    method: "PUT",
    headers: { "content-type": "application/json", ...authHeaderProps },
    body: JSON.stringify(songForUpdatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function getSong(songId) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl + songId, {
    method: "GET",
    headers: {
      ...authHeaderProps
    }
  })
    .then(handleResponse)
    .catch(handleError);
}

export function deleteSong(songId) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl + songId, {
    method: "DELETE",
    headers: {
      ...authHeaderProps
    }
  })
    .then(handleResponse)
    .catch(handleError);
}
