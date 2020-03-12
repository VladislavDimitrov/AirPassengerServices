<h1>AirPassengerServices</h1>
<br>
<h2>Setup Guide</h2>
Please follow the below steps in order to setup the app before using it:<br>
1. Go to appsettings.json and enter your connection string.<br>
2. Use the Package Manager Console to apply the migrations to the database. Use the command "update-datbase" for the purpose.<br>
3. Register a new account to gain access to the "Member" features.<br>
4. Go to Web.Utilities.DatabaseSeed in order to locate the seeded administrator credentials.<br>
5. Use the credentials from 4. to login and go to "Manage Users" panel. Find the account you registered at 3.<br>
Promote yourself to "Administrator" so you can gain access to the full functionality of the app.<br>
6. Have fun exploring and testing!<br>
<br>
<h2>App Purpose and Features</h2>
The application mocks a website of airline claim handling comapny.<br> It is using a Administrator/Member hierarchy and offers the following features:<br>
1. Register/Login (ASP.NET Identity)<br>
2. Submit Claim(Member) - This feature uses C# port of Google's <a href="https://github.com/googlei18n/libphonenumber">libphonenumber library</a> for phone number validations.<br>
3. My Claims(Member, located in a dropdown menu at your username, top-right corner) - Displays the claims submitted by the loged in user.<br>
4. Claim Details(Member) - Displays all claim details + Update information feature if you (the loged in user) are the one who submitted the claim.<br>
5. Update Claim(Member) - Reflects all changes to the claim.<br>
6. Reporting(Admin) - Offers a reporting panel with 4 filter options - Airline, Flight Number, Starting Date, End Date.<br>
7. Manage Users(Admin) - Offers a panel through which administrator can promote/demote users to Administrator/Member respectively.<br>

<h3>Technologies</h3>
C#<br>
ASP.NET Core 3.1<br>
Entity Framework Core 3.1<br>
SQL Server<br>
Ajax<br>
jQuery<br>
MOQ<br>
