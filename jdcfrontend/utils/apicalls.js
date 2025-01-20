const baseUrl = "http://localhost:5217";

// GET - All dinners
export const getAllDinners = async () => {
  try {
    const response = await fetch(`${baseUrl}/dinners`);

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error fetching dinners:", error);
    throw error; // Optional: rethrow the error if needed
  }
};
