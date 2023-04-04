# F# Domain Driven Design Showcase

I've been inspired by the [DDD-To-The-Code Workshop - Sample Code](https://github.com/cstettler/ddd-to-the-code-workshop-sample) by Christian Stettler
to create a similar example (roughly the same task) but this time written in F# and with an Angular frontend.

This showcase demonstrates how I would tackle such an exercise.

### Warning
Some functions are far from production ready (they are annotated as such). Please do not blindly copy everything from this repo.

### Prerequisits
The following software should be installed on your machine:
- Either Visual Studio or JetBrains Rider (tested with version 2023.1)
- Node JS (tested with version 18.13.0)

### How to start?
Simply run `npm run dev` in the terminal. This will start the backend and the frontend. The frontend will be available at `http://localhost:5001`.

### How to navigate through the code?
#### Backend
Open `BikeRentalExercise.sln` in Visual Studio or JetBrains Rider and open the `Startup.fs` file in the `Starter` project (the `Starter` project can be found in the `03_EntryPoints` folder).
This file contains the web api setup code as well as the glue/adapter-code between the subservices.

#### Frontend
The frontend counterpart for `Startup.fs` is the `main.ts` file in the `Frontend` folder.
This file contains the vue setup code as well as the glue/adapter-code between the subservices.

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
