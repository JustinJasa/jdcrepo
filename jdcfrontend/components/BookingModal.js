import React, { useState } from "react";
import styles from "../styles/components/BookingModal.module.css";
import { IoMdClose } from "react-icons/io";
import { Button } from "@chakra-ui/react";
import { CreateBooking } from "@/utils/apicalls";

const BookingModal = (props) => {
  const [name, setName] = useState("");
  const [number, setNumber] = useState("");
  const [people, setPeople] = useState(1);
  const [allergies, setAllergies] = useState("");
  let modalState = props.modalState;
  let setModal = props.setModal;
  let dinnerId = props.uniqueId

  const handleSubmit = (e) => {
    e.preventDefault();
    try {
      const response = CreateBooking(name, number, people, allergies, dinnerId);
      setName("")
      setNumber("")
      setPeople(1)
      setPeople("")
      setModal(!modalState)
      return response
    } catch (error) {
      console.error("There was an error with the response");
    }
  };

  //send verification number with details 
  const sendVerifcationToUser = () => { 
    
  }


  return (
    <div className={styles.bookingmodal}>
      <form onSubmit={handleSubmit} className={styles.modalcontent}>
        <IoMdClose onClick={() => setModal(!modalState)} />
        <label className={styles.label}>
          Name:
          <input
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </label>
        <br />
        <label className={styles.label}>
          Number:
          <input
            type="text"
            value={number}
            onChange={(e) => setNumber(e.target.value)}
            required
          />
        </label>
        <br />
        <div>
          <label className={styles.label}>
            How many people:
            <div>
              <button
                type="button"
                onClick={() => setPeople(1)}
                className={styles.peopleselector1}
              >
                1
              </button>
              <button
                type="button"
                onClick={() => setPeople(2)}
                className={styles.peopleselector2}
              >
                2
              </button>
            </div>
          </label>
        </div>

        <br />
        <label className={styles.label}>
          Allergies?
          <textarea
            value={allergies}
            onChange={(e) => setAllergies(e.target.value)}
          />
        </label>
        <br />
        <Button className={styles.button} colorPalette="cyan" type="submit">
          Reserve a comfy seat!
        </Button>
      </form>
    </div>
  );
};

export default BookingModal;
