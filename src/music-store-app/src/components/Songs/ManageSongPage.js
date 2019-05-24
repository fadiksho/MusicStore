import React from "react";
import SongForm from "./SongForm";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import initialState from "../../redux/reducers/initialState";
import { loadSong, addSong, updateSong } from "../../redux/actions/songActions";
import { loadGenres } from "../../redux/actions/genreActions";

class ManageSongPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      pageTitle: "",
      isSaving: false
    };
  }
  componentDidMount() {
    if (this.props.songId === 0) {
      this.setState({
        pageTitle: "Add Song"
      });
    } else {
      this.setState({
        pageTitle: "Edit Song"
      });
    }

    if (this.props.songId !== 0 && this.props.songId !== this.props.song.id) {
      console.log("loading song");
      this.props.loadSong(this.props.songId).catch(error => {
        // ToDo: handle error
        throw error;
      });
    }
    if (this.props.genres.length === 0) {
      console.log("loading genres");
      this.props.loadGenres().catch(error => {
        throw error;
      });
    }
  }

  handleFormSubmit = song => {
    let saveSong;
    if (song.id === 0) saveSong = this.props.addSong;
    else saveSong = this.props.updateSong;

    this.setState({
      isSaving: true
    });
    saveSong(song).then(() => {
      this.props.history.push("/Songs");
    });
  };
  render() {
    return (
      <>
        <div className="alert alert-success">{this.state.pageTitle}</div>
        <SongForm
          song={this.props.song}
          genres={this.props.genres}
          handleSongFormSubmit={this.handleFormSubmit}
          isSaving={this.state.isSaving}
        />
      </>
    );
  }
}

ManageSongPage.propTypes = {
  songId: PropTypes.number.isRequired
};

function mapStateToProps(state, ownProps) {
  const songId = parseInt(ownProps.match.params.id) || 0;
  const song =
    state.songsPage.selectedSong.id === songId
      ? state.songsPage.selectedSong
      : initialState.songsPage.selectedSong;
  return {
    songId,
    song,
    genres: state.genresPage.genres
  };
}

const mapDispatchToProps = {
  loadSong,
  loadGenres,
  addSong,
  updateSong
};

ManageSongPage.propTypes = {
  song: PropTypes.object.isRequired,
  songId: PropTypes.number.isRequired,
  loadSong: PropTypes.func.isRequired,
  addSong: PropTypes.func.isRequired,
  updateSong: PropTypes.func.isRequired,
  loadGenres: PropTypes.func.isRequired,
  genres: PropTypes.array.isRequired,
  history: PropTypes.object.isRequired
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ManageSongPage);
