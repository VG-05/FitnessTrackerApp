
# FitnessTracker

FitnessTracker is a web application for tracking nutrition and fitness goals, allowing users to log meals, track body weight, set goals, and visualize progress over time.
## Features

- User Registration and Authentication: Secure user registration and login functionality using ASP.NET Core Identity.

- Profile Management: Users can input and update their profile information such as age, height, weight, and fitness goals.

- Meal Logging: Users can search for food items, select serving sizes, and log their meals with detailed nutritional information automatically populated.

- Body Weight Tracking: Users can log their body weight over time and view their progress through dynamic graphs.

- Nutritional Analysis: Automatic calculation and display of calories, protein, carbohydrates, and fat based on the logged meals.

- Progress Visualization: Visualize weight and nutritional intake progress over time through charts built with Chart.js.

- Goal Setting: Recommendations for calories and nutrients using the Nutrition Calculator API when setting fitness goals.

- Notifications: Toast notifications implemented using Toastr for better user interaction feedback.



## Tech Stack

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- USDA Food Data Central API
- Nutrition Calculator API
- Chart.js
- Luxon
- Toastr
- Bootstrap
- jQuery
- ASP.NET Core Identity



## Run Locally

### Prerequisites

- .NET Core SDK
- SQL Server

### Steps

1. Clone the project

```bash
  git clone https://github.com/VG-05/FitnessTrackerApp
```

2. Go to the project directory

```bash
  cd FitnessTrackerApp
```

3. Set up the database

- Update the appsettings.json file with your SQL Server connection string:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server_name;Database=FitnessTrackerDb;User Id=your_user;Password=your_password;"
}
```

- Run the following commands to apply migrations and create the database:

```bash
  dotnet ef migrations add InitialCreate
  dotnet ef database update
```

4. Run the application

```bash
  dotnet run
```

Note: The project includes demo API keys for quick setup and testing. These keys have limited functionality and are intended for demonstration purposes only.





## Deployment on GitHub Pages

The project is also hosted on GitHub Pages for easy access. You can view it [here](https://VG-05.github.io/FitnessTracker/).



