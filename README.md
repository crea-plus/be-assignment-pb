# Phonebook Project

Repository contains codebase focused on managing user, contracts and favorites. 
Below is an overview of the key features implemented, tasks to be completed, and potential improvements for future development.

I opted for an N-layer architecture, however, if there would be no time constraints I'd have chosen a Clean Architecture instead.
While the N-layer approach offers efficiency, Clean Architecture would have provided better maintainability & testability.
Current architecture serves as a solid foundation for the application's functionalities.

### Things Done
- Authentication & Authorization Logic: implemented Bearer token authentication and authorization mechanisms to secure the application.
- Contract operations: endpoints for handling GET, POST, and PUT requests.
- Favorite Management: implemented functionality for users to add retrieve favorites using GET and POST requests.
- Docker Support: integrated Docker for containerization.

### TODO
Due to time constraints, some tasks were not completed as intended. 
This includes QuoteApi integration (issues with apikey generatation by QuoteApi web client), the absence of integration and unit tests, and the unavailability of functionality to unmark favorites.

### Possible Improvements
- JWT Refreshment: enhance user experience by implementing JWT token refreshment mechanisms.
- Azure Blob Storage: integrate Azure Blob storage for efficient handling of images associated with contracts.
- Dotnet Identity: implement Dotnet Identity for advanced user management functionalities and role-based access control.

---

Fork this starter project repo, make the app according to the specification below and then submit a Pull Request. We will get back to you with feedback.

Limit yourself to 1 or at most 2 hours of work. You do not have to finish all the requirements - after an hour is up, check the state of your work, finish up what you are doing and submit a PR. Focus on a small set of features and make those work well, write clean code and make use of appropriate design principles. This is more important than finishing everything. On the other hand if you find the task too simple and finish early, feel free to surprise us with some additional features.

### Assignment Info

You are going to create a working version of a product called Phonebook.

The app is used for searching contacts in your local phone book. Users can add new contact, their details and mark contact as favorite.

Your task is to go through the requirements and determine what needs to be done and implement it.

Focus on what you believe is most important and instead of aiming for perfection, demonstrate your familiarity with different patterns and approaches, and let us know what you would improve if you had more time. Anything that is not specifically written is up to you to decide. Keep it simple.

### Requirements

You will build a backend for a RESTful API as a .NET web app. Ideally, the latest stable version. The solution to this assignment should represent your level of expertise and we trust that you will solve it yourself. You must be able to explain the decisions that you made while planning and developing this app. Communication is JSON over HTTP, you can select whichever database you like (PostgreSQL, SQL Server, MySQL) and the use of Docker is a plus.

The project will be submitted via git. Write your commit messages so it is clear what changes they contain and push regularly.

Please make sure that the app supports the following user stories:

#### 1. User

Expose an endpoint that enables user registration. We want to persist the following data points about users: username, password, email. Implement request validation that makes the most sense to you.

#### 2. Contact

Expose endpoints to get, create and update contact (name, contact avatar image ref., phone number, email).

#### 3. Authentication

Each request except for user registration and retrieval of contacts should only be allowed to registered i.e. authenticated users. To achieve this it is sufficient to implement Basic Authentication (Wiki on Basic Access Authentication), but any other form of security for the exposed endpoints will also be acceptable. There is no need to implement authorization but a basic authorization configuration will count as a bonus point.

#### 4. Favorites

Expose endpoints for marking and unmarking contact as favorite. User can only see his own favorites.

#### 5. Quote of the day

Expose endpoint that returns a random motivational quote to display on UI. You can get free quote of the day by calling quotes.rest API.

#### Additional recommendations

Pay special attention to the construction of the REST API and add different layers of tests. Application is only handling image references, not the images themself. Use some md file to notify a reviewer about anything you believe is relevant.

We are not looking for a perfect app or 100% test coverage. It is more important to us that design ideas are properly demonstrated.

Good luck!

