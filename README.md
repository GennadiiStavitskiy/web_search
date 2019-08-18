Search Api
The SearchApi solution represents simple web base api for:

1) Fetch search result from the different search engines with specific keyword;

2) Calculate matches for result links with provided company url by parameter; 

Solution file (VS2017/2019):\src\SearchApi.sln

Source code: \src

Solution projects:

Api - .NET Core 2.1 web api with mvc approach

SearchEngine - .Net Core 2.1 class library includes backend services such as InMemoryCachingService and different strategies 

Shared - .Net Core 2.1 class library includes shared helpers and utilities classes

Web - .Net Core 2.1 Mvc client to communicate with Api by web interface.

SearchEngine.Test - xunit

API
URL: <host:port>api/search/google?url=www.sympli.com.au&text=e-settlements (http://localhost:59022/api/google?url=www.sympli.com.au&text=e-settlements)

METHODS:

* GET (READ)
GET /api/search 

Parameters:

engine =[string] - specify search engine
url=[string] - company url
text=[string] - search text statement


Response: Code:200 OK - succeed

Code: 404 (NotFound) if result is null or empty 
Code: 500 (InternalServerError) If exception was thrown search

NOTE
The Api project includes Swagger tools for documenting APIs built on ASP.NET Core API It could be useful for starting familiar with API and also for simple tests. To fetch doc: http://localhost:50590/swagger/

Cache configuration settings
Cache expiration time could be configured by ExpirationTimeHours setting in appsettings.json configuration file The default is 1 hour.

Design Decisions
Most of design decisions in this project are related to Microsoft Best Practice approaches with following SOLID principles. 
The ASP.NET Core dependency injection container was used in this case.

The Solution includes set of services which represents particular logic:

CachingService - includes a logic related to caching data to MemoryCache

Design Patterns are included:

Strategy - SearchStrategies (SortingService) - Represents search strategies by particular engins.

Web Clients
1) Web - .Net Core 2.1 Mvc.
2) Dirrect Web browser request:  http://localhost:59022/api/google?url=www.sympli.com.au&text=e-settlements
Note: only google or bing sould be provided

Future impovement:
1) Provide distributed cach by implementing ICachingService (could be ingected in Api/Startup).
2) Update Web client application to include input validation logic.



