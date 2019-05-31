import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export function authReducer(state = initialState.auth, action) {
  switch (action.type) {
    case types.LOGIN_SUCCESS:
      return { ...state, user: action.user };
    case types.LOAD_USER_DONE:
      return { ...state, user: action.user };
    case types.LOGOUT_DONE:
      return { ...state, user: initialState.auth.user };
    default:
      return state;
  }
}
