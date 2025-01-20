"use client";

import { useEffect, useState } from "react";
import { getAllDinners } from "@/utils/apicalls";
import styles from '../styles/components/PreviousDinners.module.css'

const PreviousDinners = () => {
  const [dinners, setDinners] = useState();

  const fetchDinners = async () => {
    try {
      const dinnerData = await getAllDinners(); // Await the promise
      setDinners(dinnerData); // Store the resolved data in state
      console.log(dinnerData); // Log the resolved data
    } catch (error) {
      console.error("Error fetching dinners:", error);
    }
  };

  useEffect(() => {
    fetchDinners();
  }, []);

  return (
    <section className={styles.container}>
      <h2 className={styles.heading}>Previous Dinners</h2>
      {dinners &&
        dinners.map((dinner) => {
          return (
            <div key={dinner.dinnerId} className={styles.dinnerinfo}>
              <h3 className={styles.info}>{dinner.name}</h3>
              <p className={styles.info}>{new Date(dinner.date).toLocaleDateString()}</p>
            </div>
          );
        })}
    </section>
  );
};

export default PreviousDinners;
