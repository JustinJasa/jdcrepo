import React from 'react';
import Header from '../components/Header';
import Tagline from '../components/Tagline';
import FeaturedDinners from '../components/FeaturedDinners';
import PreviousDinners from '../components/PreviousDinners';
import styles from '../styles/components/BookingPage.module.css'

const BookingPage = () => {
  return (
    <div className={styles.pagecontainer}>
      <Header />
      <Tagline />
      <FeaturedDinners />
      <PreviousDinners />
    </div>
  );
};

export default BookingPage;