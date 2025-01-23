import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams, useNavigate } from "react-router-dom";
import FilmForm from "../../components/forms/FilmForm";
import { editFilm, fetchFilms, selectFilm } from "../../../slices/filmSlice";
import { AppDispatch } from "../../../store";
import { FilmRequest, FilmResponse } from "../../../types/FilmTypes";

const UpdateFilmPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const dispatch: AppDispatch = useDispatch();
  const navigate = useNavigate();
  const { data } = useSelector(selectFilm);
  const [initialData, setInitialData] = useState<FilmResponse | undefined>(
    undefined
  );

  useEffect(() => {
    if (data.length === 0) {
      dispatch(fetchFilms());
    } else {
      const film = data.find((film) => film.id === id);
      setInitialData(film);
    }
  }, [data, dispatch, id]);

  const handleSubmit = async (data: FilmRequest) => {
    if (id) {
      await dispatch(editFilm({ id, film: data }));
      navigate("/");
    }
  };

  return initialData ? (
    <FilmForm initialData={initialData} onSubmit={handleSubmit} />
  ) : null;
};

export default UpdateFilmPage;
