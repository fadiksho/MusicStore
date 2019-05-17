export async function handleResponse(response) {
  if (response.ok) {
    if (response.status === 204) {
      return;
    }
    return resolveJsonReferences(await response.json());
  }
  if (response.status === 400) {
    // ToDo: Server side validation returns json.
    const error = await response.text();
    throw new Error(error);
  }
  throw new Error("Network response was not ok.");
}

// ToDo: call an error logging service.
export function handleError(error) {
  console.error("API call failed. " + error);
  throw error;
}

function resolveJsonReferences(json) {
  // all objects by id
  let byid = {};
  // references to objects that could not be resolved
  let refs = [];
  json = (function recurse(obj, prop, parent) {
    if (typeof obj !== "object" || !obj)
      // a primitive value
      return obj;
    if ("$ref" in obj) {
      // a reference
      let ref = obj.$ref;
      if (ref in byid) return byid[ref];
      // else we have to make it lazy:
      refs.push([parent, prop, ref]);
      return;
    } else if ("$id" in obj) {
      let id = obj.$id;
      delete obj.$id;
      if ("$values" in obj)
        // an array
        obj = obj.$values.map(recurse);
      // a plain object
      else for (let prop in obj) obj[prop] = recurse(obj[prop], prop, obj);
      byid[id] = obj;
    }
    return obj;
  })(json);

  for (let i = 0; i < refs.length; i++) {
    // resolve previously unknown references
    let ref = refs[i];
    ref[0][ref[1]] = byid[refs[2]];
    // Notice that this throws if you put in a reference at top-level
  }
  return json;
}
