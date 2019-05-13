export async function handleResponse(response) {
  if (response.ok) return response.json();
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
