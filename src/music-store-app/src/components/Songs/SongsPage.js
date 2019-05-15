import React from "react";
import { connect } from "react-redux";
import * as songActions from "../../redux/actions/songActions";
import SongList from "./SongList";
import PropTypes from "prop-types";

class SongsPage extends React.Component {
  componentDidMount() {
    this.props.loadSongsPage().catch(error => {
      // ToDo: handle error
      throw error;
    });
  }
  render() {
    return (
      <>
        <div className="alert alert-primary">Songs List</div>
        <SongList songs={this.props.songsPage.tResult} />
      </>
    );
  }
}

SongsPage.propTypes = {
  loadSongsPage: PropTypes.func.isRequired,
  songsPage: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    songsPage: state.songsPage
  };
}

const mapDispatchToProps = {
  loadSongsPage: songActions.loadSongsPage
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(SongsPage);
