import { handleResponse, handleError } from "./APIUtils";
import { authHeader } from "../helper/authHelper";

const baseUrl = process.env.REACT_APP_API_URL + "Genre/";

export function getGenres() {
  const authHeaderProps = authHeader();
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      ...authHeaderProps
    }
  })
    .then(handleResponse)
    .catch(handleError);
}

export function addNewGenre(genreForCreatingDto) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl, {
    method: "POST",
    headers: { "content-type": "application/json", ...authHeaderProps },
    body: JSON.stringify(genreForCreatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function updateGenre(genreForUpdatingDto) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl + genreForUpdatingDto.id, {
    method: "PUT",
    headers: { "content-type": "application/json", ...authHeaderProps },
    body: JSON.stringify(genreForUpdatingDto)
  })
    .then(handleResponse)
    .catch(handleError);
}

export function deleteGenre(genreId) {
  const authHeaderProps = authHeader();
  return fetch(baseUrl + genreId, {
    method: "DELETE",
    headers: { ...authHeaderProps }
  })
    .then(handleResponse)
    .catch(handleError);
}
