import React from "react";
import { connect } from "react-redux";
import { loadGenres, deleteGenre } from "../../redux/actions/genreActions";
import GenreList from "./GenreList";
import PropTypes from "prop-types";

class GenresPage extends React.Component {
  componentDidMount() {
    this.props.loadGenres().catch(error => {
      // ToDo: handle error
      throw error;
    });
  }

  handleDeleteGenre = genre => {
    this.props.deleteGenre(genre).catch(error => {
      // ToDo: handle error
      throw error;
    });
  };

  render() {
    return (
      <>
        <div className="alert alert-primary">Genres List</div>
        <GenreList
          onGenreDeleteClick={this.handleDeleteGenre}
          genres={this.props.genres}
        />
      </>
    );
  }
}

GenresPage.propTypes = {
  loadGenres: PropTypes.func.isRequired,
  genres: PropTypes.array.isRequired,
  deleteGenre: PropTypes.func.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    genres: state.genresPage.genres
  };
}

const mapDispatchToProps = {
  loadGenres,
  deleteGenre
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(GenresPage);
