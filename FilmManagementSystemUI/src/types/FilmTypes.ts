export type FilmRequest = {
  title: string;
  director: string;
  releaseYear: number;
  genre: string;
  rating: number;
  description: string;
};

export type FilmResponse = {
  id: string;
  title: string;
  director: string;
  releaseYear: number;
  genre: string;
  rating: number;
  description: string;
};
