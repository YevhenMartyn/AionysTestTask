import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Container, Typography, CircularProgress, Button } from "@mui/material";
import { fetchFilms, removeFilm, selectFilm } from "../../../slices/filmSlice";
import { AppDispatch } from "../../../store";
import FilmsTable from "../../components/tables/FilmsTable.tsx";
import { useNavigate } from "react-router-dom";

const FilmsPage: React.FC = () => {
  const dispatch: AppDispatch = useDispatch();
  const { data, loading, error } = useSelector(selectFilm);
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(fetchFilms());
  }, [dispatch]);

  const handleAddFilm = () => {
    navigate("/create");
  };

  const handleEdit = (id: string) => {
    navigate(`/update/${id}`);
  };

  const handleDelete = (id: string) => {
    dispatch(removeFilm(id));
  };

  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Films
      </Typography>
      <Button variant="contained" color="primary" onClick={handleAddFilm}>
        Add Film
      </Button>
      {loading && <CircularProgress />}
      {error && <Typography color="error">{error}</Typography>}
      {!loading && !error && (
        <FilmsTable
          films={data}
          handleDelete={handleDelete}
          handleEdit={handleEdit}
        />
      )}
    </Container>
  );
};

export default FilmsPage;
