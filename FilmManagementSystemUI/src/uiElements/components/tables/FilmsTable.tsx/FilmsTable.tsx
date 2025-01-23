import React from "react";
import { FilmResponse } from "../../../../types/FilmTypes";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
} from "@mui/material";

interface FilmTableProps {
  films: FilmResponse[];
  handleEdit: (id: string) => void;
  handleDelete: (id: string) => void;
}

const FilmTable: React.FC<FilmTableProps> = ({
  films,
  handleEdit,
  handleDelete,
}) => {
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Title</TableCell>
            <TableCell>Genre</TableCell>
            <TableCell>Director</TableCell>
            <TableCell>Release Year</TableCell>
            <TableCell>Rating</TableCell>
            <TableCell>Description</TableCell>
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {films.map((film) => (
            <TableRow key={film.id}>
              <TableCell>{film.title}</TableCell>
              <TableCell>{film.genre}</TableCell>
              <TableCell>{film.director}</TableCell>
              <TableCell>{film.releaseYear}</TableCell>
              <TableCell>{film.rating}</TableCell>
              <TableCell>{film.description}</TableCell>
              <TableCell>
                <Button
                  variant="contained"
                  color="primary"
                  onClick={() => handleEdit(film.id)}
                >
                  Edit
                </Button>
                <Button
                  variant="contained"
                  color="error"
                  onClick={() => handleDelete(film.id)}
                >
                  Delete
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default FilmTable;
