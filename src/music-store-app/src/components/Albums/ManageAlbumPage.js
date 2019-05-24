import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";

import {
  loadAlbums,
  addAlbum,
  updateAlbum
} from "../../redux/actions/albumActions";
import AlbumForm from "./AlbumForm";

function ManageAlbumPage({
  albumId,
  albums,
  loadAlbums,
  history,
  addAlbum,
  updateAlbum
}) {
  const [pageTitle, setPageTitle] = useState("");
  useEffect(() => {
    if (albumId === 0) setPageTitle("Add Album");
    else setPageTitle("Edit Album");

    if (albums.length === 0) {
      loadAlbums().catch(error => {
        throw error;
      });
    }
  }, [albumId, albums.length, loadAlbums]);

  function handleFormSubmit(album) {
    let saveGenre;
    if (album.id === 0) saveGenre = addAlbum;
    else saveGenre = updateAlbum;

    saveGenre(album).then(() => {
      history.push("/Albums");
    });
  }

  return (
    <>
      <div className="alert alert-success">{pageTitle}</div>
      <AlbumForm
        albumId={albumId}
        albums={albums}
        handleAlbumFormSubmit={handleFormSubmit}
      />
    </>
  );
}

function mapStateToProps(state, ownProps) {
  const albumId = parseInt(ownProps.match.params.id) || 0;
  const albums =
    state.albumsPage.albums.length === 0 ? [] : state.albumsPage.albums;

  return {
    albumId,
    albums
  };
}

const mapDispatchToProps = {
  loadAlbums,
  addAlbum,
  updateAlbum
};

ManageAlbumPage.propTypes = {
  albumId: PropTypes.number.isRequired,
  albums: PropTypes.array.isRequired,
  loadAlbums: PropTypes.func.isRequired,
  addAlbum: PropTypes.func.isRequired,
  updateAlbum: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ManageAlbumPage);
