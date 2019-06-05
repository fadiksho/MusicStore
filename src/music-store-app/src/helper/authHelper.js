export function authHeader() {
  let user = JSON.parse(localStorage.getItem("User"));

  if (user && user.access_token) {
    return { Authorization: "Bearer " + user.access_token };
  } else {
    return {};
  }
}
