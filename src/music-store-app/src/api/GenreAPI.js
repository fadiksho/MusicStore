import { handleResponse, handleError } from "./APIUtils";

const baseUrl = process.env.REACT_APP_API_URL + "Genre/";

export function getGenres() {
  return fetch(baseUrl)
    .then(handleResponse)
    .catch(handleError);
}

export function addNewGenre(genreForCreatingDto) {
  return fetch(baseUrl, {
    method: "POST",
    headers: { "content-type": "application/json" },
    body: JSON.stringify(genreForCreatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function updateGenre(genreForUpdatingDto) {
  return fetch(baseUrl + genreForUpdatingDto.id, {
    method: "PUT",
    headers: { "content-type": "application/json" },
    body: JSON.stringify(genreForUpdatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function deleteGenre(genreId) {
  return fetch(baseUrl + genreId, { method: "DELETE" })
    .then(handleResponse)
    .catch(handleError);
}
