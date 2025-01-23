import axios from "axios";
import { FilmRequest, FilmResponse } from "../types/FilmTypes";

const baseUrl = import.meta.env.VITE_BACKEND_API_BASE_URL;
const apiUrl = `${baseUrl}/films`;

export const getFilms = async () => {
  const response = await axios.get<FilmResponse[]>(apiUrl);
  return response.data;
};

export const getFilmById = async (id: string) => {
  const response = await axios.get<FilmResponse>(`${apiUrl}/${id}`);
  return response.data;
};

export const createFilm = async (film: FilmRequest) => {
  const response = await axios.post(apiUrl, film);
  return response.data;
};

export const updateFilm = async (id: string, film: FilmRequest) => {
  const response = await axios.put(`${apiUrl}/${id}`, film);
  return response.data;
};

export const deleteFilm = async (id: string) => {
  await axios.delete(`${apiUrl}/${id}`);
};
