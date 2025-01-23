import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import FilmsPage from "./uiElements/pages/FilmsPage/FilmsPage";
import CreateFilmPage from "./uiElements/pages/CreteFilmPage";
import UpdateFilmPage from "./uiElements/pages/UpdateFilmPage";

const App: React.FC = () => {
  return (
    <Router>
      <AppContent />
    </Router>
  );
};

const AppContent: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<FilmsPage />} />
      <Route path="/create" element={<CreateFilmPage />} />
      <Route path="/update/:id" element={<UpdateFilmPage />} />
    </Routes>
  );
};

export default App;
