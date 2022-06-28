# library-code-challenge

My personal laptop is a macbook so I could not use the given template for the project and started from scratch.
Its a .net 6 based implementation, but most of the code is transferable to .net frameworks.

## How to run
```bash
cd <path of Library.Api>
dotnet run
```

1. http://localhost:5123/ - UI
2. http://localhost:5123/api/books - Calling the API
3. http://localhost:5123/swagger - For when run in debug mode to view Swagger UI

## API Implementation
I have also created a `load-test` folder with a locust file, that I used to test the performance of the implementation.
Please see the report in that folder for the latest run results.

## UI
I know it was recommended to not use any additional dependencies. But building `CSS` from scratch can take lot of time. So I used `CDN` based `Bootstrap`.
Also implementation is a simple file with multiple functions. As the app grows we may have to start employing some code organisation strategy.
ES6 support classes and simple `OOP` based implementation with code separating using multiple js files will help. 


## UI Testing
For UI testing I prefer <a href=https://developer.chrome.com/docs/puppeteer/>Puppeteer</a>. For small projects, it provides a developer friendly headless chrome with simple `API`.


## Load testing
My preferred load testing tool is locust. Its very simple to setup and can be configured to simulate real life user behaviours.
Best part is if you have web ui, we can export `HAR` files from chrome and convert it straight to load-test scripts. 
