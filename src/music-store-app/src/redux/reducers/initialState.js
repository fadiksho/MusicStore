export default {
  songsPage: {
    songsPagedList: {
      tResult: [],
      totalItems: 0,
      totalPages: 0,
      currentPage: 0,
      pageSize: 0,
      hasPrevious: false,
      hasNext: false
    },
    selectedSong: {
      id: 0,
      name: "",
      owenerId: "",
      genres: [],
      album: {}
    }
  },
  genresPage: {
    genres: []
  },
  albumsPage: {
    albums: [],
    selectedAlbum: {
      songs: []
    }
  },
  apiCallsInProgress: 0
};
