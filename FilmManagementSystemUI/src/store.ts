import { configureStore } from "@reduxjs/toolkit";
import filmReducer from "./slices/filmSlice";

const store = configureStore({
  reducer: {
    film: filmReducer,
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;

export default store;
