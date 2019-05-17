import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";

const SongList = ({ songs }) => (
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
              <td>
                <Link
                  to={"Songs/Edit/" + song.id}
                  asp-action="Edit"
                  asp-route-id="@song.Id"
                >
                  Edit
                </Link>
                <form
                  asp-action="Delete"
                  method="post"
                  className="d-inline-block ml-2"
                >
                  <input name="id" asp-for="@song.Id" type="hidden" />
                  <input
                    name="page"
                    asp-for="@Model.CurrentPage"
                    type="hidden"
                  />
                  <input
                    type="submit"
                    value="Delete"
                    className="btn btn-sm btn-outline-danger"
                  />
                </form>
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
    <div className="mt-3">Pagging Component</div>
  </div>
);

SongList.propTypes = {
  songs: PropTypes.array.isRequired
};

export default SongList;
