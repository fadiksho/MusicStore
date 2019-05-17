import React, { useEffect } from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import { loadAlbumDetails } from "../../redux/actions/albumActions";
import initialState from "../../redux/reducers/initialState";

function AlbumDetails({ albumId, album, loadAlbumDetails }) {
  useEffect(() => {
    console.log("Effect Magic!");
    // ToDo: Clear previous selected album
    // ToDo: Implement a proper caching
    if (albumId !== album.id) {
      loadAlbumDetails(albumId).catch(error => {
        // ToDo: handle error
        throw error;
      });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [album.id]);

  return (
    <div className="border shadow rounded-lg p-3">
      <div className="d-flex">
        <h1 className="font-weight-light text-primary mr-auto">{album.name}</h1>
        <Link to={"Album/Edit/" + album.id}>Edit</Link>
      </div>
      <p className="lead">{album.description}</p>
      <hr className="my-4" />
      <h5 className="border-left border-info pl-2">Songs</h5>
      <ul className="list-group">
        {album.songs.map(song => {
          return (
            <li key={song.id} className="list-group-item rounded border">
              <h6 className="text-info mb-1">{song.name}</h6>
              <small className="ml-2 font-weight-bold">Genres: </small>
              {song.genres ? (
                song.genres.map(genre => {
                  return (
                    <small key={genre.id} className="font-italic">
                      {genre.name}
                    </small>
                  );
                })
              ) : (
                <small className="font-italic">none</small>
              )}
            </li>
          );
        })}
      </ul>
    </div>
  );
}

function mapStateToProps(state, ownProps) {
  // ToDo: Check if this good follow the redux best practices
  const albumId = parseInt(ownProps.match.params.id);
  const album =
    state.albumsPage.selectedAlbum.id === albumId
      ? state.albumsPage.selectedAlbum
      : initialState.albumsPage.selectedAlbum;
  return {
    albumId,
    album
  };
}

const mapDispatchToProps = {
  loadAlbumDetails
};

AlbumDetails.propTypes = {
  album: PropTypes.object.isRequired,
  loadAlbumDetails: PropTypes.func.isRequired,
  albumId: PropTypes.number.isRequired
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(AlbumDetails);
