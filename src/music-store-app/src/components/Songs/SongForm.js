import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import MultiCheckBoxSelectionControll from "../common/MultiCheckBoxSelectionControll";

function SongForm({ song, genres, isSaving, handleSongFormSubmit }) {
  const [songName, setSongName] = useState("");
  const [albumId, setAlbumId] = useState(0);
  const [genresSelectItems, setGenresSelectItems] = useState([]);
  useEffect(() => {
    setSongName(song.name);
    setGenresSelectItems(
      genres.length === 0
        ? []
        : genres.map(genre => ({
            id: genre.id,
            name: genre.name,
            isSelected: song.genres.filter(g => g.id === genre.id).length > 0
          }))
    );
  }, [genres, song]);

  function handleMultiCheckBoxChange(event) {
    const targetId = event.target.id;
    const value = event.target.checked;
    const items = genresSelectItems.map(item =>
      `${item.name}_${item.id}` === targetId
        ? { ...item, isSelected: value }
        : item
    );
    setGenresSelectItems(items);
  }
  function handleFormSubmit(event) {
    event.preventDefault();
    const songToSave = {
      id: song.id,
      name: songName,
      albumId: albumId,
      genresIds: genresSelectItems.filter(g => g.isSelected).map(g => g.id)
    };
    handleSongFormSubmit(songToSave);
  }
  return (
    <form onSubmit={handleFormSubmit}>
      <div className="form-group">
        <label htmlFor="songNameId">Name</label>
        <input
          id="songNameId"
          name="songName"
          className="form-control"
          placeholder="Enter song name..."
          value={songName}
          onChange={event => setSongName(event.target.value)}
        />
      </div>
      <div className="form-group">
        <label htmlFor="songNameId">Album</label>
        <input
          id="songAlbumId"
          name="songAlbum"
          className="form-control"
          placeholder="Enter album id..."
          value={albumId}
          onChange={event => setAlbumId(event.target.value)}
        />
      </div>
      <span className="d-block mb-2">Genres for song</span>
      <MultiCheckBoxSelectionControll
        items={genresSelectItems}
        handleMultiCheckBoxChange={handleMultiCheckBoxChange}
      />
      <input
        type="submit"
        disabled={isSaving}
        value={isSaving ? "Saving..." : "Save"}
        className="btn btn-success px-5"
      />
      <button type="button" className="btn btn-danger px-5 ml-1">
        Cancel
      </button>
    </form>
  );
}

SongForm.propTypes = {
  song: PropTypes.object.isRequired,
  genres: PropTypes.array.isRequired,
  handleSongFormSubmit: PropTypes.func.isRequired,
  isSaving: PropTypes.bool.isRequired
};

export default SongForm;
