import React from "react";
import { connect } from "react-redux";
import { loadGenres } from "../../redux/actions/genreActions";
import GenreList from "./GenreList";
import PropTypes from "prop-types";

class GenresPage extends React.Component {
  componentDidMount() {
    this.props.loadGenres().catch(error => {
      // ToDo: handle error
      throw error;
    });
  }
  render() {
    return (
      <>
        <div className="alert alert-primary">Genres List</div>
        <GenreList genres={this.props.genres} />
      </>
    );
  }
}

GenresPage.propTypes = {
  loadGenres: PropTypes.func.isRequired,
  genres: PropTypes.array.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    genres: state.genresPage
  };
}

const mapDispatchToProps = {
  loadGenres
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(GenresPage);
