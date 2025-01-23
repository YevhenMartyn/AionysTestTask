import React from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import FilmForm from "../../components/forms/FilmForm";
import { addFilm } from "../../../slices/filmSlice";
import { AppDispatch } from "../../../store";
import { FilmRequest } from "../../../types/FilmTypes";

const CreateFilmPage: React.FC = () => {
  const dispatch: AppDispatch = useDispatch();
  const navigate = useNavigate();

  const handleSubmit = async (data: FilmRequest) => {
    await dispatch(addFilm(data));
    navigate("/");
  };

  return <FilmForm onSubmit={handleSubmit} />;
};

export default CreateFilmPage;
