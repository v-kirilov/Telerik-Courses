# Final Project_MatchScore


## Project description


Play To Win is an app provides a solution that will streamline the organization and management of sport events.

### Main features:
- Organizing events: 
   - Match – a one-off event 
  - Tournament – knockout or league format 
- Managing events 
  - Mark the score 
  - Change the date 
  - Update the participants 
- Player profiles - Name, Country, Sports Club 
- Score tracking - Tournament and player statistics 
- Registration 
  - Allows event organization and management (if approved) 
  - Associate with player profile 

### Match format:
- Time Limited
- Score Limited
	
### Tournament format:
- Knockout
- League

### Player profiles:
-	Full name – required for registration
-	Country – optional
-	Sport club – optional

### Registered users:
-	Email
-	Password
-	Option to associate with a player profile


### Create a knockout tournament:

Requires a list of participants, containing their full names, and format. The system generates the following tournament scheme and randomly assigns participants to matches. The system tries to link any of the participants to an existing profile by their full name. If a profile is not found, one will be automatically created with no country and no sports club. The system automatically updates the next match when a match finished.

### Create a league tournament:

Scoring is 0 pts for loss, 1 for draw, 3 for win. 
The system calculates the number of rounds are required and randomly creates matches for each round.


### Manage a player profile 
- Automatic creation – when creating a tournament, if a participant cannot be matched to an existing profile 
- Manual creation – by admin or director
- Directors can edit any player profile if not linked to a user
- If linked to a user, only they can edit it 

### Registration 
- Anyone can register with an email (unique in the system) and a password
- After registration
   - Promote-to-director request - if approved by admin, the registered user can organize and manage tournaments
   - Link-profile request – if approved by admin, the player profile is associated with the registered user
   - Receives email notification when added to a tournament


### Administration
- Admin user is predefined in the system
- Can view and approve or decline promote-to-director and link-profile requests
- Can manage any resource
- Only admins can delete

### Third-Party Service
- Integrate with https://dev.mailjet.com/email/guides/send-api-v31/ for email notifications
  
### Email Notifications:
- Promote-to-director request approved/declined
- Link-profile request approved/declined
- Added to tournament
- Added to match
- Match changed – date

### Score and Tournament Tracking
- Information is available without any authentication
- View any past, present or future tournament
- View any match
- View any player profile
- Tournaments played / won
- Matches played / won
- Most often played opponen
- Best opponent – win/loss ratio
- Worst opponent – win/loss ratio

# Link to the Swagger documentation
http://localhost:5000/api/swagger/index.html

# Image of the database relations
https://github.com/v-kirilov/images/issues/1#issue-1488385248






