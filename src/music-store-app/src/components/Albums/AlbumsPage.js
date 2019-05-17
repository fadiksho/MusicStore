import React from "react";
import { connect } from "react-redux";
import { loadAlbums } from "../../redux/actions/albumActions";
import AlbumList from "./AlbumList";
import PropTypes from "prop-types";

class AlbumsPage extends React.Component {
  componentDidMount() {
    this.props.loadAlbums().catch(error => {
      // ToDo: handle error
      throw error;
    });
  }
  render() {
    return (
      <>
        <div className="alert alert-primary">Albums List</div>
        <AlbumList albums={this.props.albums} />
      </>
    );
  }
}

AlbumsPage.propTypes = {
  loadAlbums: PropTypes.func.isRequired,
  albums: PropTypes.array.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    albums: state.albumsPage.albums
  };
}

const mapDispatchToProps = {
  loadAlbums
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(AlbumsPage);
