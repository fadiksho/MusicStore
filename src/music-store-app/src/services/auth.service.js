import jwt_decode from "jwt-decode";
export const ROLES = {
  Admin: "Administrator"
};
export function convertTokenToUser(token) {
  const jwtJson = jwt_decode(token);
  const user = {
    id_token: jwtJson.jti,
    access_token: token,
    user_id: jwtJson.sub,
    user_name: jwtJson.unique_name,
    start_at: jwtJson.nbf,
    expires_at: jwtJson.exp,
    role:
      jwtJson["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
  };

  return user;
}

export function isAuthenticated(user) {
  return user.id_token !== "" && new Date(user.expires_at * 1000) > new Date();
}

export function isResourseOwener(user, resourseId) {
  return (
    isAuthenticated(user) && (user.user_id === resourseId || isAdmin(user))
  );
}

export function isAdmin(user) {
  return user.role === ROLES.Admin;
}
