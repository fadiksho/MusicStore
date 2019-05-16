import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";

const GenreList = ({ genres }) => (
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
                <form
                  asp-action="Delete"
                  method="post"
                  className="d-inline-block ml-2"
                >
                  <input name="id" type="hidden" />
                  <input name="page" type="hidden" />
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
  </div>
);

GenreList.propTypes = {
  genres: PropTypes.array.isRequired
};

export default GenreList;
