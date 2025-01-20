import React, { useState } from 'react';

const BookingModal = () => {
  const [name, setName] = useState('');
  const [number, setNumber] = useState('');
  const [people, setPeople] = useState(1);
  const [allergies, setAllergies] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    alert(`Reservation made for ${name} with ${people} people. Allergies: ${allergies}`);
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <label>
          Name:
          <input
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </label>
        <br />
        <label>
          Number:
          <input
            type="text"
            value={number}
            onChange={(e) => setNumber(e.target.value)}
          />
        </label>
        <br />
        <label>
          How many people:
          <div>
            <button type="button" onClick={() => setPeople(1)}>1</button>
            <button type="button" onClick={() => setPeople(2)}>2</button>
          </div>
        </label>
        <br />
        <label>
          Allergies?
          <textarea
            value={allergies}
            onChange={(e) => setAllergies(e.target.value)}
          />
        </label>
        <br />
        <button type="submit">Reserve a comfy seat!</button>
      </form>
    </div>
  );
};

export default BookingModal;