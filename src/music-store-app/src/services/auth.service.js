import jwt_decode from "jwt-decode";

export function convertTokenToUser(token) {
  const jwtJson = jwt_decode(token);
  const user = {
    id_token: jwtJson.jti,
    access_token: token,
    user_Name: jwtJson.sub,
    start_at: jwtJson.nbf,
    expires_at: jwtJson.exp
  };

  return user;
}

export function isAuthenticated(user) {
  return user.id_token !== "" && new Date(user.expires_at * 1000) > new Date();
}
