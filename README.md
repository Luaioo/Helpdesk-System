# Helpdesk System

## Introduction
This project is a **Helpdesk System** developed using the **ASP.NET web application framework** with **C#** as the programming language. The system was built from scratch.

**Development Environment:**  
- **IDE:** Visual Studio  
- **Database:** Microsoft SQL Server (connected via Visual Studio)

The system allows users to submit tickets and admins to manage them efficiently.

---

## Project Scope
- Each ticket can be assigned to **one admin only**.  
- Other admins can **view** ticket details but cannot assign the ticket to themselves once it is assigned.  
- The admin who assigned the ticket can **edit it**, unless they drop it (change status back to pending).  
- Users can **delete tickets** only if no admin has assigned them yet.  
- All ticket actions are **timestamped** (created, assigned, completed).

---

## Admin Functions
Admins can:  
- Login using **Email & Password**  
- Recover forgotten password via registered email  
- Assign pending tickets to themselves  
- Add comments to tickets for users to view  
- Drop tickets for other admins to handle  
- Add new users to the system  
- Add new tickets  
- View system statistics via the dashboard  
- Reset user passwords and send them via email  
- Change their own password  

---

## User Functions
Users can:  
- Login using **Email & Password**  
- Recover forgotten password via registered email  
- Create new tickets  
- View all their submitted tickets  
- Delete tickets only if the status has not been changed by an admin  
- View personal statistics via the dashboard  
- Change their own password  

---

## Email Function
- Uses **SMTP (smtp.gmail.com, port 587)**  
- Replace the example email and password in the following files to activate email features:  
  - `Forgotpassword.aspx.cs`  
  - `AdminUserListDetails.aspx.cs`  


---

## System Screenshots
**Admin Dashboard:**  
<img width="2472" height="1207" alt="image" src="https://github.com/user-attachments/assets/0e404d5e-1577-41d4-971d-18728422e9f8" />
- Displays total tickets, completed tickets, in-progress tickets, and pending tickets  
- Shows the latest 4 tickets submitted by all users  

**Ticket Submission Page:**  
<img width="1165" height="810" alt="image" src="https://github.com/user-attachments/assets/ad160124-ca4f-4a56-b822-93c541109b84" />
- Allows users or admins to submit new tickets  


**Ticket List (Admin):**  
<img width="2130" height="799" alt="image" src="https://github.com/user-attachments/assets/61a42bc0-a96d-4184-b8b5-e803364ef0bf" />
- Displays all tickets in the system  
- Admins can assign pending tickets to themselves or view ticket details  
- Includes a search box to filter tickets by any detail  


**Assigned Tickets (Admin):**  
<img width="2132" height="649" alt="image" src="https://github.com/user-attachments/assets/462088b3-13d5-4664-a63d-48acac9be83d" />
- Shows tickets assigned to each admin  
- Admin can mark tickets as completed or drop them for others to handle  


**User Management (Admin):**  
<img width="2125" height="799" alt="image" src="https://github.com/user-attachments/assets/8bd5eaa3-1c18-4508-839f-dc50bd257574" />
- Add new users  
- View all users, edit details, delete users, or reset passwords  


**Password Management:**  
<img width="1142" height="655" alt="image" src="https://github.com/user-attachments/assets/e69d7601-6ef7-4506-a2a4-cf066517e75b" />
- Allows users and admins to change their password if they know the current one  

---

## Technologies Used
- ASP.NET Web Application Framework  
- C# Programming Language  
- Microsoft SQL Server  
- Visual Studio IDE  

---

## Usage Instructions
1. Clone the repository  
2. Open the solution in **Visual Studio**  
3. Configure the **database connection** in Visual Studio to your local SQL Server  
4. Update the **SMTP email settings** in `Forgotpassword.aspx.cs` and `AdminUserListDetails.aspx.cs`  
5. Build and run the application  
