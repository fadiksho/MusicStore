import React from "react";
import { connect } from "react-redux";
import { loadSongsPage, deleteSong } from "../../redux/actions/songActions";
import SongList from "./SongList";
import PropTypes from "prop-types";

class SongsPage extends React.Component {
  componentDidMount() {
    this.props.loadSongsPage().catch(error => {
      // ToDo: handle error
      throw error;
    });
  }

  handleDeleteSong = song => {
    this.props.deleteSong(song).catch(error => {
      // ToDo: handle error
      throw error;
    });
  };

  render() {
    return (
      <>
        <div className="alert alert-primary">Songs List</div>
        <SongList
          onSongDeleteClick={this.handleDeleteSong}
          songs={this.props.songsPage.tResult}
        />
      </>
    );
  }
}

SongsPage.propTypes = {
  loadSongsPage: PropTypes.func.isRequired,
  songsPage: PropTypes.object.isRequired,
  deleteSong: PropTypes.func.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    songsPage: state.songsPage
  };
}

const mapDispatchToProps = {
  loadSongsPage,
  deleteSong
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(SongsPage);
