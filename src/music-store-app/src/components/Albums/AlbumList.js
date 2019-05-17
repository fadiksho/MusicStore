import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";

const AlbumList = ({ albums, onAlbumDeleteClick }) => (
  <div className="container-fluid">
    <table className="table table-responsive-sm">
      <thead>
        <tr>
          <th scope="col">Name</th>
          <th scope="col">Description</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        {albums.map(album => {
          return (
            <tr key={album.id}>
              <td>
                <Link to={"Albums/Details/" + album.id}>{album.name}</Link>
              </td>
              <td>{album.description}</td>
              <td>
                <Link to={"Albums/Edit/" + album.id}>Edit</Link>
                <button
                  className="ml-2 btn btn-sm btn-outline-danger"
                  onClick={() => onAlbumDeleteClick(album)}
                >
                  Delete
                </button>
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
  </div>
);

AlbumList.propTypes = {
  albums: PropTypes.array.isRequired,
  onAlbumDeleteClick: PropTypes.func.isRequired
};

export default AlbumList;
