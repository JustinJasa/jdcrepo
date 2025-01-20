import Image from "next/image";
import styles from "./page.module.css";
import BookingPage from "@/components/BookingPage";

export default function Home() {
  return (
    <div >
      <main>
        <BookingPage/>
      </main>
    </div>
  );
}
