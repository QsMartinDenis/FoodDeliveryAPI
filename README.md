<h1 align="center">.NET REST API PROJECT</h1>

## **Introduction**

REST is an acronym for Representational State Transfer and consists of a set of guidelines that software can use to communicate over the internet in order to access a resource. A resource can be any piece of information that the API can provide to the requester. For example, a resource can be an image, a product info, a pdf file, update  an order etc. In short, REST is an architectural style for building web services and APIs. HTTP is the communication protocol used in REST systems to request and get data. Basic HTTP methods that we will use in this project are:
1. GET – used to get data;
2. POST – used to insert data;
3. PUT – used to modify data;
4. DELETE – to delete something;

Note that the action representing an HTTP verb is by convention, in reality, we  can use GET to drop a database for example. We will discuss about this during project development so it will be more clear.
The Project We will build a set of APIs in .NET Core to serve a food delivery app. 

What you’ll learn
* to create rest APIs in .Net with C#;
* to use Dapper to make connections to the DB and to perform CRUD operations;
* basic security concepts like Authorization and Authentication;
* how to interact with the database through code;
* how to represent data in JSON format;
* document the APIs using swagger;
* create and apply JWT tokens;
* how to create projects in Visual Studio

# **Part 1 – Database**
The first task in carrying out this project is to create a database. Information about customers, restaurants, orders, etc. will be stored in this database. Below is a diagram and based on this diagram you will have to create the database. You will notice that this database might contain too little information compared to a real DB, but it is enough to learn concepts related to APIs.

![db]()

## **Requirements**
* create a database based on diagram provided named FoodDelivery;
* make a list of constraints that you might implement when creating the database;
* you may insert some dummy data into db once it is ready
  
## **Tables explained**

* FoodCategory – contains data to group different types of food. Example that data it may contain: sous, beverage etc.
* Restaurant – contains data about restaurant that wants to sell using our APIs. 
Example of data it may contain: Andy’s, Star Kebab.
* OrderStatus – the status of the order. Will contain: Created, Cancelled and Delivered.
* Address – contains addresses, for delivery and for restaurants;
* Customer – contains information about customers;
* CustomerAddress – contains a reference to the address and customer tables. A customer can have multiple addresses. Example: at the office, at home.
* User – will store information about internal users;
* Role – internal users have a role in the company, such admin, driver;
* UserRole – to cover the scenario if a user will need to get more permissions;
* FoodItem – restaurants can insert a food menu and the price for each item.
* FoodOrder – stores customers orders;
* FoodOrderItem – stores details about the order. Basically, list of foods that customer bought;

✔ [Learn dapper](https://www.learndapper.com/)

✔ [Using dapper in .NET Core](https://www.youtube.com/watch?v=C763K-VGkfc)

✔ [REST API Introduction](https://www.techtarget.com/searchapparchitecture/definition/RESTful-API)

✔ [Build APIs Playlist](https://www.youtube.com/playlist?list=PL82C6-O4XrHdiS10BLh23x71ve9mQCln0)


<table>
  <tr>
    <th>Route</th>
    <th>Verb</th>
    <th>Result</th>
  </tr>
  <tr>
    <td>/restaurants</td>
    <td>GET</td>
    <td>List of restaurants</td>
  </tr>
  <tr>
    <td>/restaurants/add</td>
    <td>POST</td>
    <td>Insert new restaurant to the db</td>
  </tr>
  <tr>
    <td>/restaurants/{id}</td>
    <td>GET</td>
    <td>Get restaurant details</td>
  </tr>
  <tr>
    <td>/restaurants/edit/{id}</td>
    <td>PUT</td>
    <td>Update restaurant name</td>
  </tr>
  <tr>
    <td>/restaurants/menu/{id}</td>
    <td>GET</td>
    <td>Get menu for a restaurant</td>
  </tr>
  <tr>
    <td>/customers</td>
    <td>GET</td>
    <td>List of customers</td>
  </tr>
  <tr>
    <td>/customers/add</td>
    <td>POST</td>
    <td>Add new customer</td>
  </tr>
  <tr>
    <td>/customers/edit/{id}</td>
    <td>PUT</td>
    <td>Update customer details</td>
  </tr>
  <tr>
    <td>/customers/{id}</td>
    <td>GET</td>
    <td>Get customer by id</td>
  </tr>
  <tr>
    <td>/addresses/edit/{id}</td>
    <td>PUT</td>
    <td>Update address information</td>
  </tr>
  <tr>
    <td>/addresses/{id}</td>
    <td>GET</td>
    <td>Get address details</td>
  </tr>
  <tr>
    <td>/foodcategory/{id}</td>
    <td>GET</td>
    <td>Get food categories for a restaurant</td>
  </tr>
  <tr>
    <td>/foodcategory/add/{id}</td>
    <td>POST</td>
    <td>Add a category for a restaurant</td>
  </tr>
  <tr>
    <td>/foodcategory/edit/{id}</td>
    <td>PUT</td>
    <td>Change name of a category</td>
  </tr>
  <tr>
    <td>/fooditems/{id}</td>
    <td>GET</td>
    <td>Get food items by category</td>
  </tr>
  <tr>
    <td>/fooditems/search/?query</td>
    <td>GET</td>
    <td>Return a list of foods items which names contains the search argument</td>
  </tr>
  <tr>
    <td>/fooditems/edit/{id}</td>
    <td>PUT</td>
    <td>Update food item</td>
  </tr>
  <tr>
    <td>/foodorders/add</td>
    <td>POST</td>
    <td>Create an order</td>
  </tr>
  <tr>
    <td>/foodorders/cancel/{id}</td>
    <td>PUT</td>
    <td>Cancel an order</td>
  </tr>
  <tr>
    <td>/foodorders/setdriver/{id}</td>
    <td>PUT</td>
    <td>Set driver for the order</td>
  </tr>
</table>

## **Part 3 – Secure APIs**

In this section we will learn how to secure ASP.NET core APIs with JWT (JSON  Web Token). JWT is an open standard used to share security information between two  parties – a client and a server. This blog article will help you understand the component parts of the token and how it works. In a real app tokens are generated by an IP or Identity Provider, such as Identity Server, but in this project we will write a piece of  code that will generate tokens for us and we will use those tokens to get access to resources. Lets start with bellow tutorials to learn more about tokens and how to secure APIs.

✔ [Create tokens](https://www.youtube.com/watch?v=kM1fPt1BcLc)

✔ Adding JWT Authentication and Authorization

✔ Encryption key generator

Now that you know more about tokens, we will start applying authorization to  some endpoints. Below we will give some examples, then you will have to think about  how you will secure the other endpoints.

➔ /users/add – only admin;

➔ /restaurants/ - accessible for everyone;

➔ /foodorders/setdriver/{id} – drivers and admins;

➔ /foodcategory/add/{id} – all authenticated users;

## **Requirements**

3. Add a generate token method;
4. Apply API security;
