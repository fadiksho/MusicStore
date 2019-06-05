import React from "react";
import { connect } from "react-redux";
import { loadAlbums, deleteAlbum } from "../../redux/actions/albumActions";
import AlbumList from "./AlbumList";
import PropTypes from "prop-types";

class AlbumsPage extends React.Component {
  componentDidMount() {
    this.props.loadAlbums().catch(error => {
      // ToDo: handle error
      throw error;
    });
  }

  handleDeleteAlbum = album => {
    this.props.deleteAlbum(album).catch(error => {
      // ToDo: handle error
      throw error;
    });
  };

  render() {
    return (
      <>
        <div className="alert alert-primary">Albums List</div>
        <AlbumList
          onAlbumDeleteClick={this.handleDeleteAlbum}
          albums={this.props.albums}
          user={this.props.user}
        />
      </>
    );
  }
}

AlbumsPage.propTypes = {
  loadAlbums: PropTypes.func.isRequired,
  albums: PropTypes.array.isRequired,
  deleteAlbum: PropTypes.func.isRequired,
  user: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    albums: state.albumsPage.albums,
    user: state.auth.user
  };
}

const mapDispatchToProps = {
  loadAlbums,
  deleteAlbum
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(AlbumsPage);
