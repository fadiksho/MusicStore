import * as types from "../actions/actionTypes";

export function songReducer(state = [], action) {
  switch (action.type) {
    case types.ADD_SONG:
      return [...state, { ...action.song }];
    default:
      return state;
  }
}
