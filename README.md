# Stock Management Web API
This repository contains a web API built using .NET Core, Entity Framework, MySQL database and Swagger to manage stock items, supplies, transfers and withdraw requests. The API has the following entities:

1. **Stock:** Represents a collection of items and their associated quantities.
2. **Item:** Represents a specific item in the stock, including its name, description, and measuring unit.
3. **Request:** Represents a request to either supply, transfer, or withdraw items from the stock.
4. **User:** Represents a user account with a unique username, email, and associated role.
5. **Role:** Represents a role assigned to a user, such as manager, or employee.
6. **Request Type:** Represents the type of request being made, such as supply, transfer, or withdraw.
7. **Measuring Unit:** Represents the unit of measure for an item, such as pounds, ounces, or kilograms.

The API allows users to manage stocks, items, and requests with different levels of access control. The API can be accessed through Swagger UI to interact with the API using a user-friendly interface.

## Technologies Used
- .NET Core 5.0
- Entity Framework
- MySQL database
- Swagger

## Getting Started
To get started with this project, follow these steps:

## Installing
1. Clone the repository to your local machine.
```
git clone https://github.com/seifhassan89/Stock-Management
```
2. Navigate to the project directory.
```
cd stock-management
```
3. Restore the project dependencies.
```
dotnet restore
```

## Database Connection
To connect to your MySQL database, you need to update the connection string in the appsettings.json file with your own database information.

- Here is an **example** of the connection string format:
```
"ConnectionStrings": {
    "DefaultConnection": "server=<server_name>;port=<port_number>;database=<database_name>;user=<user_name>;password=<password>"
}
```
Replace localhost with the name of your MySQL server, and database_name with the name of your database.

- Run the following command to create the database schema:
```
dotnet ef database update
```

## Run the application

- Here is command to run your application:
```
dotnet run
```


## Usage
The API endpoints can be tested using a tool such as Postman. The following endpoints are available:

![image](https://user-images.githubusercontent.com/64795421/233833527-24dda5e4-efd8-4b0a-b570-2778c88c0e2c.png)
![image](https://user-images.githubusercontent.com/64795421/233833555-5fcc8297-3204-4f03-af81-47373326a10e.png)
![image](https://user-images.githubusercontent.com/64795421/233833577-286ecc28-f171-44bf-be5e-80f2abd26bf8.png)
![image](https://user-images.githubusercontent.com/64795421/233833606-46517972-2e6d-4e8c-abef-99cbf773eeb6.png)
![image](https://user-images.githubusercontent.com/64795421/233833638-3b29cff3-4dd5-42e5-bec4-c22fdcbfa4a4.png)
![image](https://user-images.githubusercontent.com/64795421/233833672-57921d6f-e31e-4902-8cd3-a386e5be62ff.png)
![image](https://user-images.githubusercontent.com/64795421/233833700-a1bd1099-bc3d-4df9-ba2f-edddd13ac12a.png)

## Contributing
Contributions are welcome! If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new feature branch.
3. Make your changes.
4. Create a pull request.

## License
This project is licensed under the MIT License - see the LICENSE file for details.
