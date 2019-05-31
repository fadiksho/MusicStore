import * as types from "./actionTypes";
import * as authApi from "../../api/authAPI";
import { beginApiCall } from "./apiStatusAction";
import * as authService from "../../services/auth.service";

export function loginSuccess(user) {
  return { type: types.LOGIN_SUCCESS, user };
}

export function loadUserDone(user) {
  return { type: types.LOAD_USER_DONE, user };
}

export function logoutSuccess() {
  return { type: types.LOGOUT_DONE };
}

export function login(loginForm) {
  return function(dispatch) {
    dispatch(beginApiCall());
    return authApi
      .login(loginForm)
      .then(token => {
        dispatch(setToken(token));
      })
      .catch(error => {
        // ToDo: Implement loginFailure;
        throw error;
      });
  };
}

export function setToken(token) {
  return function(dispatch) {
    const user = authService.convertTokenToUser(token);
    localStorage.setItem("User", JSON.stringify(user));
    dispatch(loginSuccess(user));
  };
}

export function loadUser(user) {
  return function(dispatch) {
    if (authService.isAuthenticated(user)) {
      dispatch(loadUserDone(user));
    } else {
      // ToDo: Implement loadUserFailure
    }
  };
}

export function logout() {
  return function(dispatch) {
    localStorage.removeItem("User");
    dispatch(logoutSuccess());
  };
}
