
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

## Screenshots

Bodyweight Tracking:

![Bodyweight Tracking Screenshot](https://github.com/VG-05/FitnessTrackerApp/assets/136686473/3da1a2c0-baf9-426c-9a4d-8c71c5cfe8e1)

Nutrition Tracking:

![Nutrition Tracking Screenshot](https://github.com/VG-05/FitnessTrackerApp/assets/136686473/711824fb-46de-4d90-aa4c-938f27efff2d)

Meal Tracking:

![Meal Tracking Screenshot](https://github.com/VG-05/FitnessTrackerApp/assets/136686473/03c1ce33-604b-41eb-ad14-e6677c26aa79)

Food Search (using [USDA FoodData Central API](https://fdc.nal.usda.gov/api-guide.html)):

![Food Search Screenshot](https://github.com/VG-05/FitnessTrackerApp/assets/136686473/e7444479-3aa0-4d15-8401-3b2fd11f277f)

Food Details:

![Food Details Screenshot](https://github.com/VG-05/FitnessTrackerApp/assets/136686473/4b78e8d5-10cf-4b32-baba-593d1a90a81c)

Set Goals (with recommended macros using [Nutrition Calculator Rapid API](https://rapidapi.com/sprestrelski/api/nutrition-calculator)):

![Set Goal Screenshot](https://github.com/VG-05/FitnessTrackerApp/assets/136686473/342717cc-5141-4129-8611-7b59dae9dc3a)

Goal Tracking:

![Goal Tracking Screenshot](https://github.com/VG-05/FitnessTrackerApp/assets/136686473/c44ba6e1-d2d3-4baf-915f-16affdd19ef1)

## Technologies Used

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






