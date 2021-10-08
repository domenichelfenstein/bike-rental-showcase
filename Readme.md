# F# Domain Driven Design Showcase

I've been inspired by the [DDD-To-The-Code Workshop - Sample Code](https://github.com/cstettler/ddd-to-the-code-workshop-sample) by Christian Stettler
to create a similar example (roughly the same task) but this time written in F# and with an Angular frontend.

This showcase demonstrates how I would tackle such an exercise.

### Warning
Some functions are far from production ready (they are annotated as such). Please do not blindly copy everything from this repo.

### Prerequisits
The following software should be installed on your machine:
- Either Visual Studio or JetBrains Rider (tested with version 2021.2.1)
- Node JS (tested with version 14.17.0)

### How to start?
Simply open `BikeRentalExercise.sln` in Visual Studio or JetBrains Rider and run the `Starter` project (In the `03_EntryPoint` folder). From this project
(and `Startup.fs` in particular) you can navigate yourself through the code.

`Starter` contains the web api setup code as well as the glue/adapter-code between the subservices. The frontend counterpart for `Starter` is the `main-frontend-app` folder
(the frontend gets automagically compiled and started, when you run the `Starter` project).

`02_Subservices` contains the three services `Registration`, `Accounting` and `Rental`. Here you find the business logic (frontend and backend).

### Why not simply use SignalR?
Because I wanted to learn to write this aspect of the application by myself and because it's not that hard (as long as it doesn't have to scale) to tinker a bit with WebSockets.

### You're a self-claimed TDD and Developer-Test advocate. Why are there so few tests?
Actually there are probably more tests then it first seems: F# provides us with such a strong type system that many tests are embedded in these types.

Another aspect is the use of types such as `Result` in combination with the corresponding computational expression: Using this combination it is quite hard to write defective code.
The only thing that can go wrong is if an operation misses something (e.g. the bike rent operation would not withdraw money from the users balance). Here I'm on the fence if tests
actually help us developers. Because if we miss a sub task we might miss it in the test as well. However, this is up for debate.

### Issues
I'm happy to get feedback for my code and tips how to improve it. Please be fair and behave nicely in the issues section.

### License
You're free to use the ideas in this repo as you wish. (Legal disclaimer: I'm not responsible for any damage my code causes your application/company)
