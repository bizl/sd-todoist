# sd-todoist
Seamless Digital Todoist app challenge 


## PART 1

### Application stack 
REST-based ASP.Net MVC .Net 8 API  accessible via  
http://localhost:<selected-port>/swagger/index.html
http://localhost:5213/swagger/index.html on my local 


#### How to Run the site (windows) 

-1/ Checkout the git repository https://github.com/bizl/sd-todoist develop branch

-2/  Start command prompt and navigate to the web application root folder  <local-folder>\\sd-todoist\\src\\SeamlessDigital.Todo.API  
  
-3/ Type "dotnet run". The site will build and display the server url and endpoint. If this hangs up press the space or any other key to get it moving (or at any other point if the build seems to have hung up).  Ctrl + C to stop the server and repeat the command. 
   
-4/ Success should be where copying and pasting http://localhost:5213/api/todos?title=dish in browser returns
[
    {
        "userId": 11,
        "id": 3,
        "completed": false,
        "title": "Do dishes",
        "category": "Home",
        "priority": 0,
        "location": {
            "latitude": 51.8556603,
            "longitude": 1.0650984,
            "weather": {
                "current": {
                    "temp_c": 5.2,
                    "temp_f": 41.4,
                    "condition": {
                        "text": "Mist"
                    }
                }
            }
        },
        "dueDate": "2024-12-13T03:18:29.77"
    }
]



  
#### Potential issue
  If any issues with database connection
  
  1/ update connection string in appSettings.json. Replace |DataDirectory| with the system file path to your web solution e.g. c:\\|full-path|\\sd-todoist\\src\\SeamlessDigital.Todo.API\\App_Data\\todoist.mdf. Repeat Step 3 above. 
  
  
 2/  if issues persist, navigate to the App_Data folder,  find "todoist.mdf", right click. go to Security tab and  give "Full control" to "Authenticated Users" 
   
  3/ If issues exist, try loading the database in SSMS (Sql Server Management Studio) and using your customer server name and credentials 
  
  


##### Notes 
All db rules and relationships apply 


Create a simple To-Do backend with APIs, using a local SQL database. - - - - 
Fetch the To-Do list from https://dummyjson.com/todos and store it.  [DONE] 
Stored To-Do items along with the following additional fields: 

o Category (optional, relation to category list) [DONE - referential integrity applied] 

o Priority (1 to 5; 1 means top priority, 5 means low priority, default is 3) [DONE - defaulted  in DB] 

o Location (optional, latitude & longitude) [DONE - decimal columns used joined together in API ] 

o Due date (optional, Datetime) [DONE] 


Default storage of Category List:  [DONE - Created in DB]  
o Title (String) 

o Parent category (option, key) 





Client needs a new REST API which will: 

o Support CRUD operations for a To-Do item. [DONE - Rough and ready with Dapper]  

o Combine current weather information and a To-Do item if the Location is set. [DONE  -returned under $.Location node ] 

§ Weather data can be obtained from: https://www.weatherapi.com/ 

§ Return only the current temperature and current condition in text (e.g. Sunny, Partly 
Cloudy, etc.) [DONE] 





## PART  2 

Q1 Best to always use Async Task over Async void for better error handling, tracking outcomes/results, overall more predictable. Async void fires and forgets - not very prudent

Q2 static variables in multi-threaded application contexts share values acrossthreads. Without properly using synchronisation contexts simultaneous reading and writing of values to the static variable can easily cause unpredictable behaviour. Values can leak across sessions and not isolated properly. Scalabilty problems may arise. Bottlenecks also. Violates several SOLID principles-  Single Responsibility, Dependency Inversion for example 

Q3 It's the difference between reference type equality and value type equality. Both resolve to the same in value types. In reference types "it depends". Equals can also have its logic overwritten allowing for custom comparison logic.

Q4 No garbage collection. Setting values to nulls can help. 

Q5 CTEs I prefer for one off queries. They are compiled and can be fast if TSQL is well written. Temporary tables are not at all 'temporary'; can be used beyond the scope of a single query. But are good for larget datasets where indexing maybe needed or more complex transformations.

Q6  Preferrably simplify and keep predicate logic within LINQ-to-entities. This may be less resource hungry and easier for LINQ to translate. I'd find balance between filtering in SQL vs in-memory 


var books = dbContext.Books
    .Where(p => p.Title.StartsWith("A") && p.Title.EndsWith("Z"))
    .ToList(); 





## PART  3 
I would typically log global exceptions in middleware Global asax etc, Api/Controller Exceptions, in the service layers 

It is important to create a pattern with standard domain objects. As with older Microsoft apps logging to a central queryable table still works. Standard proprietary error codes may also help 

It is good to capture useful context for troubleshooting typically from the stack trace 

To clients, clear and simple, all technical details goes into back end 