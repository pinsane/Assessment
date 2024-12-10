Prerequisites
Before running the project, make sure you have the following tools installed on your system:
1.Docker
Docker is required to build and run your application inside containers.
•Download and install Docker Desktop from docker-desktop
•Follow the instructions for your specific Linux distribution from the Install Docker Engine

2.Docker Compose
Docker Compose is used to define and manage multi-container Docker applications. It allows you to define the services your app needs (e.g., database, backend, frontend) and run them together.
•Install Docker Compose following the instructions in the https://docs.docker.com/compose/install/.

3..NET SDK 8
To build and run the application locally, you need the .NET SDK 8 installed.
•Download the .NET 8 SDK from the official site: https://dotnet.microsoft.com/download/dotnet/.

Running the Project
Once all dependencies are installed, you can follow these steps to run your project.
Step 1: Clone the Project
Clone the repository to your local machine:

Step 2: Build the Docker Image
In project directory, run the following command to build the Docker image:

Step 4: Run the Application
To start the application, run the following command using Docker Compose:

Step 5: Stop the Application
To stop the running containers, use the following command: