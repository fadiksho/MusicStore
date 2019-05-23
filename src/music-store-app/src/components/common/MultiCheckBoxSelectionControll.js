import React from "react";
import PropTypes from "prop-types";

function MultiCheckBoxSelectionControll({ items, handleMultiCheckBoxChange }) {
  return (
    <div className="form-group form-inline">
      {items.map(item => {
        return (
          <div key={item.id} className="custom-control custom-checkbox mb-3">
            <input
              id={`${item.name}_${item.id}`}
              name={`${item.id}`}
              type="checkbox"
              checked={item.isSelected}
              onChange={handleMultiCheckBoxChange}
              className="custom-control-input"
            />
            <label
              htmlFor={`${item.name}_${item.id}`}
              type="checkbox"
              className="custom-control-label mr-3"
            >
              {item.name}
            </label>
          </div>
        );
      })}
    </div>
  );
}

MultiCheckBoxSelectionControll.propTypes = {
  items: PropTypes.array.isRequired,
  handleMultiCheckBoxChange: PropTypes.func.isRequired
};

export default MultiCheckBoxSelectionControll;
