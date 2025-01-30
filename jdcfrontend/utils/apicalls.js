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

// POST - Create a booking

export const CreateBooking = async (
  name,
  number,
  people,
  allergies,
  uniqueId
) => {
  // booking payload
  const bookingDetails = {
    dinnerId: uniqueId,
    attendeeName: name,
    attendeeNumber: number,
    bookingCapacity: people,
    request: allergies || null,
  };

  try {
    const response = await fetch(`${baseUrl}/bookings`, {
      method: "POST",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(bookingDetails),
    });

    alert("Booking has gone through, Thank you!");

    if (!response.ok) {
      const error = await response.json();
      alert(`Error: ${error.message || "Failed to create booking."}`);
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
  } catch (error) {
    console.error("Error", error);
    alert("An error has occured");
  }
};
