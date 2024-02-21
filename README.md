# Table of Contents
- [Overview](#overview)
- [User manual](#user-manual)
  - [How to build the program](#how-to-build-the-program)
  - [How to run the program](#how-to-run-the-program)
  - [How to test the program](#how-to-test-the-program)
- [Using the program](#using-the-program)
- [Team members](#team-members)
- [Credits](#credits)
- [License](#license)

## Overview
This project constitutes the work I did together with my Group at the IT University of Copenhagen in the course Analysis, Design and Software Architecture at the 3rd semester of the Software Development-bachelor at the IT University of Copenhagen. To comply with the restriction of not being able to publicly upload the repository during development in an educational setting, I have reuploaded it here for you to view the project. The number of commits does not accurately represent our workload. Below is a picture illustrating our efforts for the original project, with my colleagues' names blurred out for ethical reasons.
![Commits](https://github.com/lrtzndrsn/chirp/assets/123448847/f91d66d1-4305-4368-b553-2ebfd1353577)

## What we learned and worked with during this project:
* CI/CD Pipelines, using Github Actions to automate workflows and SQLServer Azure for data persistence.
* Working iteratively and in increments (Going from CLI app to minimal web API, to a live ASP.NET Core app using Razor pages, EF Core, etc.)
* Working with issues and user stories
* Working with onion architecture
* Working with design principles
* Implementing various design patterns
* Dependency Injection
* Trunk-based development
* Organizing our projects in a intelligeble and structured manner

## About Chirp!
The Chirp! application is an X-like (formerly known as Twitter) application that encompasses much of the same functionality. As such, it is possible for a user of the application to - among other things - send cheeps (this applications version of tweets) to other users, like other users cheeps, and to view their individual timelines. To create the application, the group went through many iterative and incremental steps, starting from creating a command line application with a CSV-file as database, to then using SQLite and ASP.NET Core, and finally Azure and SQLServer for the final live application that was handed in for the exam.

## User manual

### How to build the program
In the root folder of the project, run the command: `dotnet build`

### How to run the program
Navigate to src/Chirp.Web and run the command: `dotnet run`

### How to test the program
- Running all tests: In the root folder of the project, run the command: `dotnet test`
- Running individual tests: If, for whatever reason, one wants to run test for individual parts of the application, navigate to the folder, for example test/Chirp.Web.Tests, and run the command: `dotnet test`

## Using the program
When someone first visits the application, they are unauthorized and will therefore only be able to view the public timeline, but not interact with it in any other way. From here, one should navigate to the 'Register' section, where a new account can be created in one of two ways. Either an account can be created by simply choosing a username, email and password. Alternatively, an account can be created through the use of GitHub authorization, thus using the credentials of ones GitHub account. After an account is created, the new user is able to fully interact with the application. The user can send a new cheep by inputting text in the input form above the timeline and then clicking on the 'Share' button. Additionally, the user can choose to follow other users and/or like their cheeps by clicking the 'Follow' and 'Like' buttons respectively. In the header of the application, additional buttons are located giving the user freedom to navigate the site. The 'Home' button simply redirects to the homepage, thereby showing the public timeline. The '[username]'s Timeline' button redirects to a users private timeline. The 'Your Cheeps' button shows the cheeps authored by the user currently existing in the application. The 'About me' button shows the information, for example the name and email, that the application currently stores about the user. Finally, by clicking the 'Logout' button, the user logs out of the application.

## Team members
- Me
- Anton Friis (anlf@itu.dk)
- Clara Walter (clwj@itu.dk)
- Oline (okre@itu.dk)
- Johan Sandager (jsbe@itu.dk)
- Jonas Kramer (kram@itu.dk)

## Credits
A vast amount of sources were used in the creation of this project, and appreciation goes out to all of those. A special thanks should be given to [Andrew Lock](https://github.com/andrewlock), the author of [ASP.NET Core in Action, Third Edition](https://www.manning.com/books/asp-net-core-in-action-third-edition). This book proved an invaluable resource in understanding the intricacies of ASP.NET Core and many of the technologies used in this project.

## License
MIT License

Copyright (c) 2023 ITU-BDSA23-GROUP9

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
