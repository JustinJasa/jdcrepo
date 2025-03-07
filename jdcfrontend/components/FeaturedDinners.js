"use client";

import { React, useState } from "react";
import { Button } from "@chakra-ui/react";
import BookingModal from "../components/BookingModal";
import Image from "next/image";
import styles from "../styles/components/FeaturedDinners.module.css";
import AlmafiImage from "../public/almafi.jpg";

const FeaturedDinners = () => {
  const [openModal, setModal] = useState(false);

  let uniqueId = 1002

  return (
    <section className={styles.container}>
      {/* <h2 className={styles.heading}>Upcoming Dinners</h2> */}
      <div className={styles.dinnercontainer}>
        <div className={styles.dinnercontent}>
          <h3 className={styles.dinnertitle}>Dinner in Almafi</h3>
          <div className={styles.eventdate}>
            <p className={styles.date}>Friday, April 5</p>
            <p className={styles.time}>6pm</p>
          </div>
          <p className={styles.eventdescription}>
            Dinner in Amalfi: where your taste buds party harder than your
            friends, the pasta gives better hugs, and the dessert listens better
            than Karen when you vent about life.
          </p>
          <div className={styles.eventattendees}>
            <p className={styles.attendees}>Attendees: XX</p>
            <p className={styles.capacity}>Capacity: XX</p>
          </div>
          <Button
            colorPalette="cyan"
            variant="surface"
            className={styles.button}
            onClick={() => setModal(!openModal)}
          >
            Click for good feed ✅✅
          </Button>
        </div>
        <div className={styles.imagecontainer}>
          <Image
            className={styles.eventimage}
            src={AlmafiImage}
            alt="Event Image"
          />
        </div>
      </div>

      {openModal && <BookingModal modalState={openModal} setModal={setModal} uniqueId={uniqueId}/>}
    </section>
  );
};

export default FeaturedDinners;
