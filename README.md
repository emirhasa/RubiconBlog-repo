![alt text](https://raw.githubusercontent.com/emirhasa/RubiconBlog-repo/master/demo.jpg)

______________________________________________________
Testing - two methods: 

Method 1:
-why bother with the 2nd method when you can just try it live? I went ahead and took the liberty of hosting it quickly on the
subdomain I use for the portfolio
https://api.rblog.brodev.info 
https://api.rblog.brodev.info/swagger/index.html

Method 2:
-download the solution code
-restore nugets, add the migration and execute it
-run the project
______________________________________________________


The API is implemented with the following architecture:

Request <-> Controller <-> Interface <-> Service <-> SQL Server

Using .NET Core Framework
Since the specification wasn't very specific on certain things I decided to go with simplicity.
Of course, certain parts can be upgraded/coded differently but I believe this is just for demonstration purposes
and as such it's my opinion it will serve well.

Took a few liberties to add some extras:
-BaseGetController which was used for implementing the TagsController just to demonstrate it's use
-The PostsController is implemented without a BaseCRUD/BaseGet 
-I added an error filter as a means to streamline handling exceptions
-I intended to implement an extra with Author and BasicAuthorization but didn't do it just to not complicate things at the moment, if you'd like to see that implemented feel free to as

Added a few comments throughout the code for clarification. All the best!

