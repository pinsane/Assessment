
## Prerequisites
Before running the project, make sure you have the following tools installed on your system:
1.**Docker**
Docker is required to build and run your application inside containers.
•Download and install Docker Desktop from [docker-desktop](https://www.docker.com/products/docker-desktop.)
•Follow the instructions for your specific Linux distribution from the [Install Docker Engine](https://docs.docker.com/engine/install/)

  2.**Docker Compose**
Docker Compose is used to define and manage multi-container Docker applications. It allows you to define the services your app needs (e.g., database, backend, frontend) and run them together.
•Install Docker Compose following the instructions in the https://docs.docker.com/compose/install/.

3.**.NET SDK 8**
To build and run the application locally, you need the .NET SDK 8 installed.
•Download the .NET 8 SDK from the official site: https://dotnet.microsoft.com/download/dotnet/.
  ### Running the Project
Once all dependencies are installed, you can follow these steps to run your project.
  **Step 1: Clone the Project**
Clone the repository to your local machine:

    git clone https://github.com/pinsane/Assessment.git
    cd cd Assessment

**Step 2: Run the Application**
To start the application, run the following command using Docker Compose:
Ensure that the ports are configured correctly in  `docker-compose.yml` file and that no other application is using the specified ports.

    docker-compose up
    
**Step 3: Stop the Application**
To stop the running containers, use the following command:

    docker-compose down

**Application Access Links**
After the application is up and running, you can access its endpoints using the following links:
- **Documentation (Swagger)**: [http://localhost:50001](http://localhost:50001)
-  **Web Interface**: [http://localhost:50002/](http://localhost:50002)