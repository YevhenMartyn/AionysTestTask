import React, { useState, useEffect } from "react";
import { TextField, Button, Container } from "@mui/material";
import { FilmRequest, FilmResponse } from "../../../../types/FilmTypes";
import { z } from "zod";

interface FilmFormProps {
  initialData?: FilmResponse;
  onSubmit: (data: FilmRequest) => void;
}

const currentYear = new Date().getFullYear();

const filmSchema = z.object({
  title: z.string().max(50, "Title must not exceed 50 characters"),
  director: z.string().max(50, "Director must not exceed 50 characters"),
  releaseYear: z
    .number()
    .min(1888, "Release year must be between 1888 and current year")
    .max(currentYear, "Release year must be between 1888 and current year"),
  genre: z.string().max(50, "Genre must not exceed 50 characters"),
  rating: z
    .number()
    .min(1, "Rating must be between 1 and 10")
    .max(10, "Rating must be between 1 and 10"),
  description: z
    .string()
    .max(500, "Description must not exceed 500 characters"),
});

const FilmForm: React.FC<FilmFormProps> = ({ initialData, onSubmit }) => {
  const [formData, setFormData] = useState<FilmRequest>({
    title: "",
    director: "",
    releaseYear: currentYear,
    genre: "",
    rating: 1,
    description: "",
  });

  const [errors, setErrors] = useState<Record<string, string>>({});

  useEffect(() => {
    if (initialData) {
      setFormData({
        title: initialData.title,
        director: initialData.director,
        releaseYear: initialData.releaseYear,
        genre: initialData.genre,
        rating: initialData.rating,
        description: initialData.description,
      });
    }
  }, [initialData]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]:
        name === "releaseYear" || name === "rating" ? Number(value) : value,
    });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const result = filmSchema.safeParse(formData);
    if (!result.success) {
      const newErrors: Record<string, string> = {};
      result.error.errors.forEach((error) => {
        if (error.path.length > 0) {
          newErrors[error.path[0]] = error.message;
        }
      });
      setErrors(newErrors);
    } else {
      setErrors({});
      onSubmit(formData);
    }
  };

  return (
    <Container>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Title"
          name="title"
          value={formData.title}
          onChange={handleChange}
          fullWidth
          margin="normal"
          error={!!errors.title}
          helperText={errors.title}
        />
        <TextField
          label="Director"
          name="director"
          value={formData.director}
          onChange={handleChange}
          fullWidth
          margin="normal"
          error={!!errors.director}
          helperText={errors.director}
        />
        <TextField
          label="Release Year"
          name="releaseYear"
          type="number"
          value={formData.releaseYear}
          onChange={handleChange}
          fullWidth
          margin="normal"
          error={!!errors.releaseYear}
          helperText={errors.releaseYear}
        />
        <TextField
          label="Genre"
          name="genre"
          value={formData.genre}
          onChange={handleChange}
          fullWidth
          margin="normal"
          error={!!errors.genre}
          helperText={errors.genre}
        />
        <TextField
          label="Rating"
          name="rating"
          type="number"
          value={formData.rating}
          onChange={handleChange}
          fullWidth
          margin="normal"
          error={!!errors.rating}
          helperText={errors.rating}
        />
        <TextField
          label="Description"
          name="description"
          value={formData.description}
          onChange={handleChange}
          fullWidth
          margin="normal"
          error={!!errors.description}
          helperText={errors.description}
        />
        <Button type="submit" variant="contained" color="primary">
          Submit
        </Button>
      </form>
    </Container>
  );
};

export default FilmForm;
