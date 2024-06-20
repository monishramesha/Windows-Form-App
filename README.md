
# FormSubApp

Note : All the compulsory requirements are functional and complete, available to execute on the master branch. The optional requirements were tried but I was unable to complete and polish them upto my satisfaction. My progress with the optional requirements can be inspected on the secondary branch of the repository.

## Overview

FormSubApp is a simple form submission application that allows users to create and view workflow submissions. The application is divided into two main parts: a frontend developed using VB.NET (Windows Forms) and a backend developed using Node.js with TypeScript.

### Features

- **Create New Submissions**: Users can fill out a form to create new workflow submissions.
- **View Submissions**: Users can browse through existing workflow submissions.
- **Stopwatch Functionality**: Users can start and stop a stopwatch to time their tasks.
- **Persistent Storage**: Submissions are stored persistently in a JSON file.

## Project Structure

```
FormSubApp/
├── Backend/
│   ├── src/
│   │   ├── routes.ts
│   │   ├── types.ts
│   │   ├──  index.ts
│   |   └── db.json
│   ├── package.json
│   └── tsconfig.json
└── WindowsApp/
    ├── CreateNewSubmission.vb
    ├── MainForm.vb
    └── ViewSubmissions.vb
```

- **Backend**: Contains the Node.js server and TypeScript files for handling API requests and data storage.
- **WindowsApp**: Contains the VB.NET files for the frontend forms application.

## Getting Started

### Prerequisites

- **Node.js**: Ensure you have Node.js installed. You can download it from [nodejs.org](https://nodejs.org/).
- **Visual Studio**: For running the VB.NET application, ensure you have Visual Studio installed with .NET desktop development workload.

### Installation

#### Backend

1. **Navigate to the Backend directory**:

   ```bash
   cd FormSubApp/Backend
   ```

2. **Install dependencies**:

   ```bash
   npm install
   ```

3. **Compile TypeScript files**:

   ```bash
   tsc
   ```

#### WindowsApp

1. **Open the project in Visual Studio**.
2. **Build the project** using Visual Studio to ensure all dependencies are resolved.

### Running the Application

#### Backend

1. **Start the server**:

   ```bash
   npx ts-node-dev src/index.ts
   ```

2. The server will run on `http://localhost:3000`.

#### WindowsApp

1. **Run the application** from Visual Studio by pressing `F5` or selecting the `Start` button.
2. The application will open the main form, allowing you to create and view submissions.

## Usage

### Creating a New Submission

1. **Open the `Create New Submission` form** from the main application window.
2. **Fill out the form** with the required details.
3. **Use the stopwatch** if necessary to time your task.
4. **Submit the form** to save the submission.

### Viewing Submissions

1. **Open the `View Submissions` form** from the main application window.
2. **Navigate through submissions** using the `Next` and `Previous` buttons.

## File Descriptions

### Backend

- **`routes.ts`**: Contains the API routes for handling form submissions and retrieval.
- **`types.ts`**: Defines the types for workflow items and containers.
- **`index.ts`**: Main server file that sets up and starts the Express server.
- **`db.json`**: JSON file for storing workflow submissions.

### WindowsApp

- **`CreateNewSubmission.vb`**: VB.NET form for creating new workflow submissions.
- **`MainForm.vb`**: The main form of the Windows application.
- **`ViewSubmissions.vb`**: VB.NET form for viewing existing submissions.

## Troubleshooting

### Common Issues

- **Error Reading `db.json`**: Ensure the path to `db.json` is correctly specified and the file is accessible.
- **Cannot Read Property 'push'**: Ensure that `db.json` is correctly formatted and includes an array for `Workflows`.
- **TypeScript Compilation Errors**: Ensure all dependencies are installed and TypeScript files are compiled without errors.

## Contributing

If you would like to contribute to this project:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Commit your changes with clear descriptions.
4. Push to your fork and create a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact

For any questions or issues, please contact:

- **Name**: Monish Ramesha
- **Email**: monishramesha@gmail.com
