import { createStore, applyMiddleware, compose } from "redux";
import reduxImutableStateinvariant from "redux-immutable-state-invariant";
import rootReducer from "./reducers";

export default function configureStore(initialState) {
  // support for Redux dev tools CruzyName!
  const composeEnhancers =
    window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

  return createStore(
    rootReducer,
    initialState,
    composeEnhancers(applyMiddleware(reduxImutableStateinvariant()))
  );
}
