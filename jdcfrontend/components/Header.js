import React from "react";
import styles from "../styles/components/Header.module.css";
import BackgroundImage from "../public/image.webp";
import Image from "next/image";

const Header = () => {
  return (
    <header className={styles.header}>
      <Image
        src={BackgroundImage}
        alt="Image of Dinner"
        objectFit="cover" /* Ensures the image covers the container proportionally */
        quality={100} /* High-quality rendering */
        className={styles.bgimage}
      />
      <div className={styles.herotext}>
        <h1 className={styles.title}>Jasa's Dinner Club</h1>
        <p className={styles.subtext}>
          A place to eat food with friends, and also with Jasa! Dinners every
          month :)
        </p>
      </div>
    </header>
  );
};

export default Header;
