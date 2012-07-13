MongoFs
=======

**MongoFs** provides a few simple helper functions wrapped around the official Mongo C# client that make it a littler easier to use from F#.

Syntax
=======

You have a few options for using MongoFs. If you wish to use the default local MongoDB connection string, you can do something like this:

    [<CLIMutable>]
	type Person = { 
		_id : ObjectId 
		FirstName : string 
		LastName : string 
	}

	createLocalMongoServer()
	|> getMongoDatabase dbName
	|> getMongoCollection<Person> collectionName

If you wish to define the connection string in a config file, you can use the following as long as the app setting has the name of "MongoDbConnectionString":

	createMongoServer()
	|> getMongoDatabase dbName
	|> getMongoCollection<Person> collectionName
	
To use a different app setting name, do this:

	createMongoServerWithConfig "YourAppSettingName"
	|> getMongoDatabase dbName
	|> getMongoCollection<Person> collectionName
	
To specify a different connection string by just passing in a string, do this:

	createMongoServerWithConnString "DesiredConnectionString"
	|> getMongoDatabase dbName
	|> getMongoCollection<Person> collectionName
	
Once the connection is setup, you can use the rest of the Mongo Driver API. For example:

    { _id = ObjectId.GenerateNewId(); FirstName = "John"; LastName = "Doe" }
    |> people.Insert |> ignore

    let person = people.FindOne()
	
Examples are available in the (integration tests)[https://github.com/dmohl/MongoFs/tree/master/tests/MongoFs.Tests].	
 	  
How To Get It
=======

MongoFs is available on NuGet Gallery as id MongoFs.

Releases
=======
* 0.1.0.0 - Initial release.

MIT License
=======

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.