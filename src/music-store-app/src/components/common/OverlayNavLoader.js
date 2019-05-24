import React from "react";
import PropTypes from "prop-types";

const OverlayNavLoader = ({ show }) => {
  return (
    <>
      {show && (
        <div
          id="loaderId"
          className="mt-3 text-center w-100 position-fixed"
          style={{ zIndex: "1029", top: "55px" }}
        >
          <div className="spinner-grow text-success" role="status">
            <span className="sr-only">Loading...</span>
          </div>
          <div
            className="spinner-grow text-warning mx-4"
            style={{ animationDelay: "0.3s" }}
            role="status"
          >
            <span className="sr-only">Loading...</span>
          </div>
          <div className="spinner-grow text-primary" role="status">
            <span className="sr-only">Loading...</span>
          </div>
        </div>
      )}
    </>
  );
};

OverlayNavLoader.propTypes = {
  show: PropTypes.bool.isRequired
};

export default OverlayNavLoader;
