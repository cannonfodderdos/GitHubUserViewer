# GitHubUserViewer

MVC/Web API Application which queries GitHubs User API, retrieves specific information and includes details of repos ordered by Stargazer count.

The application is also available at https://githubuserviewer.azurewebsites.net

Please note the GitHub API will deny requests to their Core API's after a certain limit is hit (around 60 in an hour for
unauthenticated apps).

## Architecture

This solution was heavily inspired by the Onion Architecture 
(https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/). 

* Core.ApplicationServices - Route into the core for outer layers
* Core.Common - Basic shared library objects
* Core.Domain - Domain models and interfaces
* Infrastructure.GitHubServiceV3 - Provides implementation detail of IGitHubService interface, and converts responses to domain models
* Test.UnitTests - Unit tests Core and MVC/Web API controllers using Moq
* Web.UserViewer - MVC (.NET Framework 4.7.2) web application

## Web Application

The app has two routes to query GitHubs API and view returned results.

* ~/User/Index - Utilises Ajax form to call the Web.UserController and display result through Partial Views/DisplayTemplates
* ~/User/Vue - Simple single page Vue.js application calling API.UserController

The app utilisies Autofac as it's IoC container for DI, and Microsoft.Extensions.Logging for logging 
(with a basic implementation provided that writes to the VS output window). It also has an ExceptionHandler/ExceptionLogger
for any unhandled exceptions, which hide sensitive error information from any API clients but still allow it to be recorded.

While the application is quite trivial, I've tried to demonstrate how I approach seperation of concerns from Infrastructure to Domain,
and then Domain to client while still maintaining a testable dependency free code base.

## Libraries Used
* Automapper
* Microsoft.Extensions.Logging (ILoggerFactory / ILogger)
* Moq
* Vue

## Todo

* IntegrationTests
* Refactor Vue.js application to use templates
* Environment specific config files
