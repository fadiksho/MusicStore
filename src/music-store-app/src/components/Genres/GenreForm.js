import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";

function GenreForm({ genreId, genres, handleGenreFormSubmit, isSaving }) {
  const [genreName, setGenreName] = useState("");
  useEffect(() => {
    if (genres.length > 0 && genreId !== 0) {
      const genre = genres.find(c => c.id === genreId);
      setGenreName(genre.name);
    } else {
      setGenreName("");
    }
  }, [genres, genreId]);
  function handleFormSubmit(event) {
    event.preventDefault();
    const genre = {
      id: genreId,
      name: genreName
    };
    handleGenreFormSubmit(genre);
  }
  return (
    <form onSubmit={handleFormSubmit}>
      <div className="form-group">
        <label htmlFor="genreNameId">Name</label>
        <input
          id="genreNameId"
          name="genreName"
          className="form-control"
          placeholder="Enter genre name..."
          value={genreName}
          onChange={event => setGenreName(event.target.value)}
        />
      </div>
      <input
        type="submit"
        disabled={isSaving}
        value={isSaving ? "Saving..." : "Save"}
        className="btn btn-success py-1 px-5"
      />
      <button type="button" className="btn btn-danger px-5 py-1 ml-1">
        Cancel
      </button>
    </form>
  );
}

GenreForm.propTypes = {
  genreId: PropTypes.number.isRequired,
  genres: PropTypes.array.isRequired,
  handleGenreFormSubmit: PropTypes.func.isRequired,
  isSaving: PropTypes.bool.isRequired
};

export default GenreForm;
