import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";

function AlbumForm({ albumId, albums, isSaving, handleAlbumFormSubmit }) {
  const [albumName, setAlbumName] = useState("");
  const [albumDescription, setAlbumDescription] = useState("");
  useEffect(() => {
    if (albums.length > 0 && albumId !== 0) {
      const album = albums.find(a => a.id === albumId);
      setAlbumName(album.name);
      if (album.description) setAlbumDescription(album);
    } else {
      setAlbumName("");
      setAlbumDescription("");
    }
  }, [albums, albumId]);
  function handleFormSubmit(event) {
    event.preventDefault();
    const album = {
      id: albumId,
      name: albumName,
      description: albumDescription
    };
    handleAlbumFormSubmit(album);
  }

  return (
    <form onSubmit={handleFormSubmit}>
      <div className="form-group">
        <label htmlFor="albumNameId">Name</label>
        <input
          id="albumNameId"
          name="albumName"
          className="form-control"
          placeholder="Enter album name..."
          value={albumName}
          onChange={event => setAlbumName(event.target.value)}
        />
      </div>
      <div className="form-group">
        <label htmlFor="albumDescriptionId">Name</label>
        <textarea
          id="albumDescriptionId"
          name="albumDescription"
          className="form-control"
          placeholder="Enter genre name..."
          value={albumDescription}
          onChange={event => setAlbumDescription(event.target.value)}
        />
      </div>
      <input
        type="submit"
        disabled={isSaving}
        value={isSaving ? "Saving..." : "Save"}
        className="btn btn-success py-1 px-5"
      />
      <button type="button" className="btn btn-danger px-5 py-1 ml-1">
        Cancel
      </button>
    </form>
  );
}

AlbumForm.propTypes = {
  albumId: PropTypes.number.isRequired,
  albums: PropTypes.array.isRequired,
  handleAlbumFormSubmit: PropTypes.func.isRequired,
  isSaving: PropTypes.bool.isRequired
};

export default AlbumForm;
