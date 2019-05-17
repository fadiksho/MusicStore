import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";

const GenreList = ({ genres, onGenreDeleteClick }) => (
  <div className="container-fluid">
    <table className="table table-responsive-sm">
      <thead>
        <tr>
          <th scope="col">Name</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        {genres.map(genre => {
          return (
            <tr key={genre.id}>
              <td>{genre.name}</td>
              <td>
                <Link to={"Genres/Edit/" + genre.id}>Edit</Link>
                <button
                  className="ml-2 btn btn-sm btn-outline-danger"
                  onClick={() => onGenreDeleteClick(genre)}
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

GenreList.propTypes = {
  genres: PropTypes.array.isRequired,
  onGenreDeleteClick: PropTypes.func.isRequired
};

export default GenreList;
