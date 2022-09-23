
# mycinema app
Technical test for Appspace v1.0.0

## Description
A cultural organization wants to promote cinema and audiovisual
productsthrough different web sites and applications. They want to
build a common API that provides media recommendations for
different targets.
The API has to be public and users do not have to authenticate to
accessany of the functionalities.
The use cases that the API needs to cover are:
1. Theatre managers can ask the API to build a suggested
intelligent billboard for their theatre for a specific period.

This extra intelligence means: the manager specifies a period of time
and howmany screens are in big rooms and how many of them in
small rooms. According to that, the API finds recommendations for
blockbuster genres (for big rooms) and minority genres (for small
rooms) and returns a suggested movie for each screen. All the
movies will be different from one week to the next one.

## Architecture
I've tried to follow next patterns to develop the technical test.

 - Clean architecture & DDD
	 - I used the Clean arquitecture and DDD pattern to make my app maintainable, scalable and testable in a simple way.
 - Repository pattern
	 - In order to access database, I've used the repository pattern, creating a read repository to get the information from only read database.
 - Option pattern
	 - The options pattern uses classes to provide strongly typed access to groups of related settings.
 - Dependency injection  
	 - I've used the dependency injection to achieve inversion of control between classes and their dependencies.
- Unit & Integration Testing
	- In order to test the implemented feature in app, I've written some unit tests using **xUnit, Fixture and Moq** nuget packages
	- In order to test the integration between API and database, I've written some integration tests using **xUnit** nuget packages
