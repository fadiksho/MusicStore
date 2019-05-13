import React from "react";
import { connect } from "react-redux";
import * as songActions from "../../redux/actions/songActions";
import PropTypes from "prop-types";

class SongsPage extends React.Component {
  state = {
    song: {
      name: ""
    }
  };

  handleChange = event => {
    const song = { ...this.state.course, name: event.target.value };
    this.setState({ song });
  };

  handleSubmit = event => {
    event.preventDefault();
    this.props.addSong(this.state.song);
  };

  render() {
    return (
      <form onSubmit={this.handleSubmit}>
        <h2>Songs </h2>
        <h3>Add Song</h3>
        <input
          type="text"
          onChange={this.handleChange}
          value={this.state.song.name}
        />
        <input type="submit" value="Save" />
        {this.props.songs.map(song => (
          <div key={song.name}>{song.name}</div>
        ))}
      </form>
    );
  }
}

SongsPage.propTypes = {
  addSong: PropTypes.func.isRequired,
  songs: PropTypes.array.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    songs: state.songs
  };
}

const mapDispatchToProps = {
  addSong: songActions.addSong
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(SongsPage);
