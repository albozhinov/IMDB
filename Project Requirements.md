# Databases: Teamwork Assignment

## Project Description

Create a project of your choice and implement it using Code First approach with Entity Framework. You must use SQL Server 2017 as your database. Part of the data in SQL Server must be provided via external files (Excel, XML, JSON, zip, etc.) of your choice. You should create PDF reports based on your application logic. They should consists of meaningful data.

- Project examples:
  - Sports ranking
  - Online store
  - Movie ranking
  - Book store
  
## Preliminary Requirements

Before you start writing code and building databases, please take your time to write a simple project specification. Together with your team members, read the requirements below and answer the following questions in a (README in your repo) in a style of your choosing.

- What is the name of your team?
- Who is your team leader?
- Who are your team members?
- What is your project going to be about?
- What features will it consist of? Explain their purpose. (Try to be as granular as possible.)
- Create a kanban board with the following data, fill it and keep it updated:
  - Name of Feature
  - Feature Owner (who will write it?)
  - Estimated time it would take (in hours, **don't overthink it**)
  - Actual time it took (in hours)
  - Estimated time it would take to unit test (in hours)
  - Actual time it took to unit test (in hours)
- For the board you could use Trello or GitLab's project system.
  - If your selected tool does not support time estimation (for example Trello), just write it in the card's description or use an addon.

Try to adhere to this project specification and make your project as close to it as possible. As you implement each feature, write down the time it really took you and compare them with the estimate. Do not be surprised if the difference between them is great, that's completely normal when you do something like this for the first time. Also, don't go crazy on features, implement a few but implement them amazingly! 

You have **until Wednesday afternoon** to present this specification to either me or Edo in person or via Slack, and commit it to your repository.

## General Requirements

- Completely finished project is not obligatory required. It will not be a big problem if your project is not completely finished or is not working greatly
  - This team work project is for educational purposes
- Always remember, quality over quantity!

## Must Requirements

- Use Code First approach
- Use Entity Framework
- Use SQL Server 2017
- At least five tables in the SQL Server database
- Provide at least two type of relations in the database and use both attributes and the Fluent API (Model builder) for configuration
- The user should be able to manipulate the database through the client (basic CRUD)
- Provide some usable user interface for the client (preferably console)
- Write unit tests for the majority of your application's features. Unisolated tests are not considered valid.
- Follow the SOLID principles and the OOP principles. The lack of SRP or DI will be punished by death.

## Should Requirements

- Load some of the data from external files (Either Excel, XML, JSON, zip, etc.) of your choice
  - For XML files should be read / written through the standard .NET parsers (of your choice)
  - For JSON serializations use a non-commercial free library / framework of your choice.
- Generate PDF reports based on your application's data. The PDF doesn't have to be pretty.
  - For PDF reports use a non-commercial free library / framework of your choice.

## Could Requirements

- You could use Repository pattern or Service layer of your choice
  - Research the options and choose your preferable way to do it


# ASP.NET MVC

This document describes the **project assignment** for the **ASP.NET Core MVC** course at Telerik Academy.

## Project Description

Design and implement an **ASP.NET Core MVC application**. This application should utilize and extend the already existing business logic from the Databases course.

The application should have:

* **public part** (Must) (accessible without authentication)
* **private part** (Must) (available for registered users)
* **administrative part** (Should) (available for administrators only)

### Public Part

The **public part** of your projects should be **visible without authentication**.

This public part could be the application start page, the user login and user registration forms, as well as the public data of the users, e.g. the blog posts in a blog system, the public offers in a bid system, the products in an e-commerce system, etc.

### Private Part (Users only)

**Registered users** should have private part in the web application accessible after **successful login**.

This part could hold for example the user's profiles management functionality, the user's offers in a bid system, the user's posts in a blog system, the user's photos in a photo sharing system, the user's contacts in a social network, etc.

### Administration Part

**System administrators** should have administrative access to the system and permissions to administer all major information objects in the system, e.g. to create/edit/delete users and other administrators, to edit/delete offers in a bid system, to edit/delete photos and album in a photo sharing system, to edit/delete posts in a blogging system, edit/delete products and categories in an e-commerce system, etc.

## General Requirements

* Completely finished project is not obligatory required.
  * This team work project is for educational purposes
  * Always remember, quality over quantity!  

## Must Requirements (60 points)

* Must have **Public** and **Private** parts
* Must use **ASP.NET Core MVC 2.1**
* Must use **Razor** template engine for generating the UI
* Must use **MS SQL Server** as database back-end
  * Must use **Entity Framework Core** to access your database
* Must have pages with **tables with data** with **paging and sorting** for a model entity
  * You can use Kendo UI grid, jqGrid, any other library or generate your own HTML tables
* Must apply proper **data validation** (both client-side and server-side)
* Must apply proper **error handling** (both client-side and server-side)
* Write **unit tests** for the majority of your application's features. Unisolated tests are not considered valid.
* Follow the SOLID principles and the OOP principles. The lack of SRP or DI will be punished by death.

## Should Requirements (25 points)

* Should have **Administrative** part
  * Use the standard **ASP.NET Identity System** for managing users and roles
  * Your registered users should have at least one of the two roles: **user** and **administrator**
* Should use at least **1 area** in your project (e.g. for administration)
* Should use **caching** of data where it makes sense (e.g. starting page)

## Could Requirements (15 points)

* Chould have **usable and responsive UI**
  * You may use **Bootstrap** or **Materialize**
  * You may change the standard theme and modify it to apply own web design and visual styles

## Challenges

These extra requirements can give bonus points if everything in **Must** and **Should** is completed

* Research and use (simple) gitflow (master and development branches)
* Host your application in Azure (or any other public hosting provider)
