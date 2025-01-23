import React, { useState, useEffect } from "react";
import { TextField, Button, Container } from "@mui/material";
import { FilmRequest, FilmResponse } from "../../../../types/FilmTypes";

interface FilmFormProps {
  initialData?: FilmResponse;
  onSubmit: (data: FilmRequest) => void;
}

const FilmForm: React.FC<FilmFormProps> = ({ initialData, onSubmit }) => {
  const [formData, setFormData] = useState<FilmRequest>({
    title: "",
    director: "",
    releaseYear: new Date().getFullYear(),
    genre: "",
    rating: 1,
    description: "",
  });

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
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(formData);
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
        />
        <TextField
          label="Director"
          name="director"
          value={formData.director}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Release Year"
          name="releaseYear"
          type="number"
          value={formData.releaseYear}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Genre"
          name="genre"
          value={formData.genre}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Rating"
          name="rating"
          type="number"
          value={formData.rating}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Description"
          name="description"
          value={formData.description}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
        <Button type="submit" variant="contained" color="primary">
          Submit
        </Button>
      </form>
    </Container>
  );
};

export default FilmForm;
