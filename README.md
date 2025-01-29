# Private Dinner Booking Site 🍽️ (Currently In Progress Building)

# ✅ To-Do List

## 📌 Core Features
- [ ] Admin Authentication
- [ ] Twilio Messaging 
- [x] Booking for Dinner

## 🖥️ Frontend (React)
- [x] Design and implement UI components
- [x] Connect frontend to backend API
- [x] Add form validation for bookings

## 🛠️ Backend (.NET)
- [ ] Set up authentication with JWT
- [x] Create endpoints for booking management
- [x] Implement database schema for events and users
- [x] API error handling and validation

## 📦 Database (SQL)
- [x] Design schema for users, bookings, and events
- [x] Implement database migrations
- [ ] Optimize queries for performance


## Overview
This is a small booking site designed for private dinners hosted for friends. The platform allows users to schedule, manage, and reserve dinner events easily. The project is built with:

- **.NET (Backend)** – Handles business logic, user authentication, and booking management.
- **React.js (Frontend)** – Provides a smooth and interactive user interface.
- **SQL (Database)** – Stores dinner event details, reservations, and user information.
- **Docker** – Simplifies deployment by containerizing the frontend, backend, and database.

## Features
- ✅ **User Authentication** – Sign up and log in securely.
- ✅ **Book a Private Dinner** – Users can select a date and reserve a spot.
- ✅ **Host Management** – Hosts can create and manage dinner events.
- ✅ **Database Integration** – All bookings are stored in an SQL database.
- ✅ **Dockerized Setup** – Easy to run with a single command.

## 🛠️ Getting Started (Local Setup)

### Prerequisites
Before running the project, make sure you have the following installed:
- **Node.js** (for React frontend)
- **Docker & Docker Compose** (to containerize the app)
- **.NET SDK** (if running the backend outside Docker)

### Step 1: Clone the Repository
```bash
git clone https://github.com/JustinJasa/jdcrepo.git
cd jdcrepo
```
### Step 2: Set Up Environment Variables
Create a .env file in the backend and frontend directories with the required values:
```
Backend (.env inside backend/):

env
Copy
DATABASE_CONNECTION_STRING=Server=db;Database=PrivateDinners;User Id=sa;Password=YourStrong!Passw0rd;
JWT_SECRET=your_jwt_secret
Frontend (.env inside frontend/):

env
Copy
REACT_APP_API_URL=http://localhost:5000
```
### Step 3: Run the Project with Docker
Simply run:

```
bash
Copy
docker-compose up --build
This command will:

✅ Start the backend (.NET)

✅ Start the frontend (React)

✅ Start the SQL database

Wait until the containers are fully built and running.
```
### Step 4: Access the Application
Frontend (React): http://localhost:3000 or whatever port you have

Backend (API): http://localhost:5000 or whatever port you have

