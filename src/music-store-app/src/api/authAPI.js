import { handleResponse, handleError } from "./APIUtils";

const baseUrl = process.env.REACT_APP_API_URL + "auth/token";

export function login(loginForm) {
  return fetch(baseUrl, {
    method: "POST",
    headers: { "content-type": "application/json" },
    body: JSON.stringify(loginForm)
  })
    .then(handleResponse)
    .then(jsonToken => {
      return jsonToken.token;
    })
    .catch(handleError);
}
