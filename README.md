
----
# Chat Boot clean architecture with .NET 8


<br />
<p align="center">
  <a href="#">
    <img src="READMEDoc/img/OnionArchitecture_icon.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Onion Architecture</h3>
  
  <!-- TABLE OF CONTENTS -->
## Table of Contents

* [Onion Architecture](#Onion-Architecture)
* [reference](#reference)
* [About the Project](#about-the-project)
* [CI/CD](#CI/CD)
* [Project description](#project-description)
  <!-- * [Built With](#built-with) -->
* [Getting Started](#getting-started)
* [Contact](#contact)
<!-- * [Acknowledgements](#acknowledgements) -->

## Onion Architecture

- Onion Architecture was introduced by Jeffrey Palermo to provide a better way to build         applications in perspective of better testability, maintainability, and dependability on the infrastructures like databases and services

    Onion, Clean or Hexagonal architecture: it's all the same. Which is built on **Domain-Driven Desgin** approach.

    Domain in center and building layer top of it. You can call it as Domain-centric Architecture too.

### Reference
* [It's all the same (Domain centeric architecture) - Mark Seemann](https://blog.ploeh.dk/2013/12/03/layers-onions-ports-adapters-its-all-the-same/)
* [Onion Architecture by Jeffrey Palermo](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/)
* [Clean Architecture by Robert C. Martin (Uncle Bob)
](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
* [Hexagonal Architecture by Dr. Alistair Cockburn](https://alistair.cockburn.us/hexagonal+architecture)
* [Onion Architecture - Amit Naik](https://marketplace.visualstudio.com/items?itemName=AmitNaik.OnionArchitecture)

## About The Project

<!-- [![Product Name Screen Shot][product-screenshot]](https://example.com) -->

- API solution which is built on Onion Architecture with all essential feature using .NET 5.

    ![image](READMEDoc/img/OnionArchitecture.png)

## Project description

- we can see that all the Layers are dependent only on the Core Layers

    <details>
    <summary><b>Domain layer</b></summary>
    <p>
        Domain Layers (Core layer) is implemented in center and never depends on any other layer. Therefore, what we do is that we create interfaces to Persistence layer and these interfaces get implemented in the external layers. This is also known and DIP or Dependency Inversion Principle
    </p>
    </details>
    <details>
    <summary><b>Persistence layer</b></summary>
    <p>
        In Persistence layer where we implement reposistory design pattern. In our project, we have implement Entityframework which already implements a repository design pattern. DbContext will be UoW (Unit of Work) and each DbSet is the repository. This interacts with our database using dataproviders
    </p>
    </details>
    <details>
    <summary><b>Service layer</b></summary>
    <p>
        Service layer (or also called as Application layer) where we can implement business logic. For OLAP/OLTP process, we can implement CQRS design pattern. In our project, we have implemented CQRS design pattern on top of Mediator design pattern via MediatR libraries
    </p>
    <p>In case you want to implement email feature logic, we define an IMailService in the Service Layer.
    Using DIP, it is easily possible to switch the implementations. This helps build scalable applications.
    </p>
    </details>
    <details>
    <summary><b>Infrastructure Layer</b></summary>
    <p>
        In this layer, we add our third party libraries like JWT Tokens Authentication or Serilog for logging, etc. so that all the third libraries will be in one place. In our project, we have implemented almost all important libraries, you can plug & play (add/remove) based on your project requirement in StartUp.cs file.
    </p>
    </details>
    <details>
    <summary><b>Presentation Layer</b></summary>
    <p>
        This can be WebApi or UI.
    </p>
    </details>

<!--
## CI/CD
- In the develop Branch i created a custom continuos integrations (CI) and continuos deployment (CD)  workflow in Github Actions.

    This workflow Build, test and deploy the API solution.

    This workflow contains following features. 

    - [x] App Settings Variable Substitution Identity Connection
    - [x] Build Solution
    - [x] Publish Solution
    - [x] Run Test
    - [x] Upload Artifact for deployment job
    - [x] Deploy Entity Framework Migration IdentityContext (test-api-database.database.windows.net)
    - [x] Deploy Entity Framework Migration ApplicationDbContext (test-api-database.database.windows.net)
    - [x] Deploy To Azure Web App [Here you can sing up and testing API](https://appserviceapijc.azurewebsites.net/OpenAPI/index.html)
        ![image](READMEDoc/img/CI-CD.png)
    - Build
        ![image](READMEDoc/img/CI-CD-Build.png)

    - Deploy
        ![image](READMEDoc/img/CI-CD-Deploy.png)






## Getting Started

### Step 1: Clone this Repository and start solution in visual studio
- Run   
```sh
        $ git clone https://github.com/JohnCubides/Test.Weelo.git
        $ cd Test.Weelo/
        $ start Test.Weelo.sln
```


### Step 2: Project is ready
- 
    ![image](READMEDoc/img/Step2.png)

### Step 3: Configure connection string in appsettings.json

- Make sure to connect proper database

    ```json
    "ConnectionStrings": {
        "ApplpicationWeeloConn": "#{ConnectionString--ApiDb}#",
        "IdentityConnection": "#{ConnectionString--ApiDb}#"
    },
    ```

    and connect to logging in DB or proer path

    ```diff
    "Serilog": {
        "MinimumLevel": "Information",
        "WriteTo": [
        {
            "Name": "RollingFile",
            "Args": {
    --          "pathFormat": "D:\\Logs\\log-{Date}.log",
            "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
            }
        },
        {
            "Name": "MSSqlServer",
            "Args": {
    --          "connectionString": "Data Source=(local)\\sqlexpress01;Initial Catalog=OnionDb;Integrated Security=True",
            "sinkOptionsSection": {
                "tableName": "Logs",
                "schemaName": "EventLogging",
                "autoCreateSqlTable": true
            },
            "restrictedToMinimumLevel": "Warning"
            }
        }
        ],
        "Properties": {
        "Application": "Onion Architecture application"
        }
    },
    ```

### Step 4: Run Migrations in Microsoft SQL Server)

- For running migration:
 
  + Option 1: Using Package Manager Console:
    + Open Package Manager Console, select *Test.Weelo.Persistence* as Default Project
    + Run these commands:
      ```sh
      PM> update-database -Context ApplicationDbContext 
      PM> update-database -Context IdentityContext 
      ```

    ![Migration](READMEDoc/img/Step4.png)

 + Option 2: Using dotnet cli:
    + Install **dotnet-ef** cli:
      ```
      dotnet tool install --global dotnet-ef --version="5.1"
      ```
    + Navigate to [Test.Weelo](https://github.com/JohnCubides/Test.Weelo/tree/develop_task/Test.Weelo/Test.Weelo/) and run these commands:
      ```
      $ dotnet ef database update --context ApplicationDbContext 
      $ dotnet ef database update --context IdentityContext 
      ```
### Step 5: Run unit test

- Choose *"Test-Test Explorer"* on the visual studio, or press *(Ctrl+E, T)*
    ![Migration](READMEDoc/img/Step5.png)
- Run All Test in View or pres *(Ctrl+R, A)*
![Migration](READMEDoc/img/Step5-1.png)


### Step 6: Build and run application

- Health check UI

   Navigate to Health Checks UI https://localhost:44356/healthcheck-ui and make sure everything is green.

   ** Change port number according to your application

    ![image](READMEDoc/img/Step6.png)

- Swagger UI

    Swagger UI https://localhost:44356/OpenAPI/index.html

    ** Change port number according to your application

    ![image](READMEDoc/img/Step6-1.png)
    + Sing up

        ![image](READMEDoc/img/Register-1.png)

    + Response
        ![image](READMEDoc/img/Register-2.png)

    + Cofirm your account 
        ![image](READMEDoc/img/Register-3.png)
    + You can now use the /api/Account/authenticate
    ```json
        {
            "email": "jperez@example.com",
            "password": "*/Jperez123/*"
        }
    ```

    + Using the token in response
    ```json
        {
            "succeeded": true,
            "message": "Authenticated jperez",
            "errors": null,
            "data": {
                "id": "caa32e68-31da-49fc-ba39-1816ccd9fadf",
                "userName": "jperez",
                "email": "jperez@example.com",
                "roles": [
                "Basic"
                ],
                "isVerified": true,
                "jwToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJqcGVyZXoiLCJqdGkiOiIwOTBiYzhmOC1mODA4LTQxODgtYjgzZC1mYTU0ZTdlYmY1MDkiLCJlbWFpbCI6ImpwZXJlekBleGFtcGxlLmNvbSIsInVpZCI6ImNhYTMyZTY4LTMxZGEtNDlmYy1iYTM5LTE4MTZjY2Q5ZmFkZiIsImlwIjoiMTkyLjE2OC4xMzcuMSIsInJvbGVzIjoiQmFzaWMiLCJleHAiOjE2NDIwNTIzNTAsImlzcyI6IklkZW50aXR5IiwiYXVkIjoiSWRlbnRpdHlVc2VyIn0.34zY4mGefMRFPI--_D7OoWHfP3GziAAXmV5CiKCht8s",
                "refreshToken": "096CAD644CAB5334D07D471498A81B0DE8F81AE851BBD4F54A20D15DF15502FE1CAC5943B3100DEC"
            }
            }
    ```
    + Ussing Token in Authorize 
    ![image](READMEDoc/img/Authentication-1.png)

    + you can now use the endpoints
        ![image](READMEDoc/img/Authentication-2.png)
        -->
## Contact

Having any issues or troubles getting started? Drop a mail to john.cubides87@gmail.com or [Raise a Bug or Feature Request](https://github.com/JohnCubides/Test.Weelo/issues/new). Always happy to help.


# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)
