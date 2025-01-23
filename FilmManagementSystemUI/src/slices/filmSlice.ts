import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { AxiosError } from "axios";
import { RootState } from "../store";
import { FilmRequest, FilmResponse } from "../types/FilmTypes";
import {
  createFilm,
  deleteFilm,
  getFilms,
  updateFilm,
} from "../services/filmService";

interface FilmsState {
  data: FilmResponse[];
  loading: boolean;
  error: string | null;
}

const initialState: FilmsState = {
  data: [],
  loading: false,
  error: null,
};

export const fetchFilms = createAsyncThunk(
  "film/fetchFilms",
  async (_, { rejectWithValue }) => {
    try {
      const films = await getFilms();
      return films;
    } catch (error) {
      const axiosError = error as AxiosError;
      return rejectWithValue(axiosError.message);
    }
  }
);

export const addFilm = createAsyncThunk(
  "film/createFilm",
  async (film: FilmRequest, { rejectWithValue }) => {
    try {
      const newFilm = await createFilm(film);
      return newFilm;
    } catch (error) {
      const axiosError = error as AxiosError;
      return rejectWithValue(axiosError.message);
    }
  }
);

export const editFilm = createAsyncThunk(
  "film/editFilm",
  async (
    { id, film }: { id: string; film: FilmRequest },
    { rejectWithValue }: { rejectWithValue: (value: any) => void }
  ) => {
    try {
      const updatedFilm = await updateFilm(id, film);
      return updatedFilm;
    } catch (error) {
      const axiosError = error as AxiosError;
      return rejectWithValue(axiosError.message);
    }
  }
);

export const removeFilm = createAsyncThunk(
  "film/removeFilm",
  async (id: string, { rejectWithValue }) => {
    try {
      await deleteFilm(id);
      return id;
    } catch (error) {
      const axiosError = error as AxiosError;
      return rejectWithValue(axiosError.message);
    }
  }
);

const filmSlice = createSlice({
  name: "film",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchFilms.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchFilms.fulfilled, (state, action) => {
        state.loading = false;
        state.data = action.payload;
      })
      .addCase(fetchFilms.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      .addCase(addFilm.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(addFilm.fulfilled, (state, action) => {
        state.loading = false;
        state.data.push(action.payload);
      })
      .addCase(addFilm.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      .addCase(editFilm.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(editFilm.fulfilled, (state, action) => {
        state.loading = false;
        const index = state.data.findIndex(
          (film) => film.id === action.payload.id
        );
        if (index !== -1) {
          state.data[index] = action.payload;
        }
      })
      .addCase(editFilm.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      .addCase(removeFilm.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(removeFilm.fulfilled, (state, action) => {
        state.loading = false;
        state.data = state.data.filter((film) => film.id !== action.payload);
      })
      .addCase(removeFilm.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export default filmSlice.reducer;
export const selectFilm = (state: RootState) => state.film;
