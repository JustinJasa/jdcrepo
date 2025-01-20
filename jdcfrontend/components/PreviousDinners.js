'use client'

import { useEffect } from "react";
import { getAllDinners } from "@/utils/apicalls";

const PreviousDinners = () => {


    useEffect(() => {
        getAllDinners()
    },[])

  return (
    <section>
      <h2>Previous Dinners</h2>
      <ul>
        <li>Mexican Extravaganza - 04.01.25</li>
        <li>Jasa's Filipino Feast - 10.12.24</li>
        <li>A Dinner in Amalfi Coast - 04.11.24</li>
        <li>Masala Mingle - 015.10.24</li>
      </ul>
    </section>
  );
};

export default PreviousDinners;
