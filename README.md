# Databases Teamwork: Store management system

## Preliminary Requirements
- Design and implement a Store management system Console Application.
- Functional Requirements:

- Application should support multiple Suppliers. Each suppliers should have: 
  - Company name. It will be a string up to 20 symbols and should be unique in the application.
  -	Identification’s number. It should be a integer of 10 figures.
  - Person in charge. It should be a string up to 20 symbols.
  - Principal place of business (company address). It will be a string up to 20 symbols.
  - Phone number. The information is not requiert. 
  - Should support input of Supplaiers in JSON format.

- Application should support multiple Customers. Each suppliers should have:
  - First name. It will be a string up to 20 symbols.
  - Last name. It will be a string up to 20 symbols.
  -	E-mail. The information is not requiert.

- Application should support multiple Products. Each Product should have:
  - Product name. It will be a string up to 20 symbols and should be unique in the application.
  - Buy Price. It should be a decimal value.
  - Sell Price. It should be a decimal value.
  - Available quantity as integer

- Application should support Categories with name up to 20 symbols.

- Application should support multiple orders. Each order sould have:
  - Order date. 
  - Should keep information about the store Name, Person in charge and address.
  - Should keep information about the Product and the quontity of the sold Product.
  - By realized sale should create an order in PDF format by request.

- Application should support the following commands:
  - Add new suppliers 
  - Add new customer  
  - Add new product   
  - Add new categiry- 
  - Create a sale order 
  - Storno of an order   
  - Change RepresentedBy
  - Change company address  
  - Show top 5 most expensive products 
  - Show all products with mapped Categories  
  - Show all products with mapped Categories and sorted by name 
  - Show all products in a category 
  - Show all products with quantity less than given number 
  - Show all products sorted by available quantity 
  - Calculate the profit by a given category 
  - Export an order in PDF format
  - Input Supplaiers from file in JSON format  

## - Create a kanban board with the following data, fill it and keep it updated:
  - Name of Feature
  - Feature Owner (who will write it?)
  - Estimated time it would take (in hours, **don't overthink it**)
  - Actual time it took (in hours)
  - Estimated time it would take to unit test (in hours)
  - Actual time it took to unit test (in hours)
- For the board you could use Trello or GitLab's project system.
  - If your selected tool does not support time estimation (for example Trello), 
  just write it in the card's description or use an addon.
Try to adhere to this project specification and make your project as close to it as possible. 
As you implement each feature, write down the time it really took you and compare them with the estimate. 
Do not be surprised if the difference between them is great, that's completely normal when you do something like this
for the first time. Also, don't go crazy on features, implement a few but implement them amazingly!

## Must Requirements

- Use Code First approach
- Use Entity Framework Core 2.0+
- Use SQL Server 2017
- At least five tables in the SQL Server database
- Provide at least two type of relations in the database and 
use both attributes and the Fluent API (Model builder) for configuration
- The user should be able to manipulate the database through the client (basic CRUD)
- Provide some usable user interface for the client (preferably console)
- Write unit tests for the majority of your application's features. 
Unisolated tests are not considered valid.
- Follow the SOLID principles and the OOP principles. The lack of SRP or DI will be punished by death.

## Should Requirements

- Load some of the data from external files (Either Excel, XML, JSON, zip, etc.) of your choice
  - For XML files should be read / written through the standard .NET parsers (of your choice)
  - For JSON serializations use a non-commercial free library / framework of your choice.
- Generate PDF reports based on your application's data. The PDF doesn't have to be pretty.
  - For PDF reports use a non-commercial free library / framework of your choice.

## Could Requirements

- You could use Service Layer of your choice

## Project Defense

Each team member will have around 20 minutes to:

- Present the project overall
- Explain how they have contributed to the project
- Explain the source code of their teammates
- Answer some theoretical questions related to this course and all the previous ones.

## Give Feedback about Your Teammates

You will be invited to provide feedback about all your teammates, their attitude to this project, 
their technical skills, their team working skills, their contribution to the project, etc. 
**The feedback is important part of the project evaluation so take it seriously and be honest.**