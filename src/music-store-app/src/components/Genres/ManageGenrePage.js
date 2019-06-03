import React, { useEffect, useState } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import {
  loadGenres,
  addGenre,
  updateGenre
} from "../../redux/actions/genreActions";
import GenreForm from "./GenreForm";
function ManageGenrePage({
  genreId,
  genres,
  loadGenres,
  history,
  addGenre,
  updateGenre
}) {
  const [pageTitle, setPageTitle] = useState("");
  const [isSaving, setIsSaving] = useState(false);
  useEffect(() => {
    if (genreId === 0) setPageTitle("Add Genre");
    else setPageTitle("Edit Genre");

    if (genres.length === 0) {
      loadGenres().catch(error => {
        throw error;
      });
    }
  }, [genreId, genres.length, loadGenres]);

  function handleFormSubmit(genre) {
    let saveGenre;
    if (genre.id === 0) saveGenre = addGenre;
    else saveGenre = updateGenre;

    setIsSaving(true);
    saveGenre(genre)
      .then(() => {
        history.push("/Genres");
      })
      .catch(error => {
        setIsSaving(false);
      });
  }
  return (
    <>
      <div className="alert alert-success">{pageTitle}</div>
      <GenreForm
        genreId={genreId}
        genres={genres}
        handleGenreFormSubmit={handleFormSubmit}
        isSaving={isSaving}
      />
    </>
  );
}

function mapStateToProps(state, ownProps) {
  const genreId = parseInt(ownProps.match.params.id) || 0;
  const genres =
    state.genresPage.genres.length === 0 ? [] : state.genresPage.genres;
  return {
    genreId,
    genres
  };
}

const mapDispatchToProps = {
  loadGenres,
  addGenre,
  updateGenre
};

ManageGenrePage.propTypes = {
  genreId: PropTypes.number.isRequired,
  genres: PropTypes.array.isRequired,
  loadGenres: PropTypes.func.isRequired,
  addGenre: PropTypes.func.isRequired,
  updateGenre: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ManageGenrePage);
