import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import { isResourseOwener } from "../../services/auth.service";

const SongList = ({ songs, onSongDeleteClick, user }) => (
  <div className="container-fluid">
    <table className="table table-responsive-sm">
      <thead>
        <tr>
          <th scope="col">Name</th>
          <th scope="col">Album</th>
          <th scope="col">Genres</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        {songs.map(song => {
          return (
            <tr key={song.id}>
              <td>{song.name}</td>
              <td>
                {song.album ? (
                  <Link
                    to={"Albums/Details/" + song.album.id}
                    asp-controller="Albums"
                    asp-action="Details"
                    asp-route-id="@song.Album.Id"
                  >
                    <>{song.album.name}</>
                  </Link>
                ) : (
                  <span className="text-warning">not specified</span>
                )}
              </td>
              <td>
                {song.genres ? (
                  song.genres.map(genre => {
                    return <span key={genre.id}>{genre.name} </span>;
                  })
                ) : (
                  <span className="text-warning">not specified</span>
                )}
              </td>
              {isResourseOwener(user, song.owenerId) && (
                <td>
                  <Link to={"Songs/Edit/" + song.id} asp-action="Edit">
                    Edit
                  </Link>
                  <button
                    className="ml-2 btn btn-sm btn-outline-danger"
                    onClick={() => onSongDeleteClick(song)}
                  >
                    Delete
                  </button>
                </td>
              )}
            </tr>
          );
        })}
      </tbody>
    </table>
    <div className="mt-3">Pagging Component</div>
  </div>
);

SongList.propTypes = {
  songs: PropTypes.array.isRequired,
  onSongDeleteClick: PropTypes.func.isRequired,
  user: PropTypes.object.isRequired
};

export default SongList;
