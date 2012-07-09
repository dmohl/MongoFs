module MongoFs.IntegrationTests

open NUnit.Framework
open FsUnit
open System.Linq
open MongoDB.Bson
open MongoDB.Driver

let dbName = "resources"
let collectionName = "people"

type Person = { 
    mutable _id : ObjectId 
    mutable FirstName : string 
    mutable LastName : string 
}

let verifyAddDocument (people:MongoCollection<Person>) =
    { _id = ObjectId.GenerateNewId(); FirstName = "John"; LastName = "Doe" }
    |> people.Insert |> ignore

    let person = people.FindOne()
    person.FirstName |> should equal "John"
    person.LastName |> should equal "Doe"
    person._id |> should not' (be Null)

[<Test>]
let ``When using the default URL it should be able to create documents``() =
    let people = 
        createLocalMongoServer()
        |> getMongoDatabase dbName
        |> getMongoCollection<Person> collectionName
    
    people.RemoveAll() |> ignore

    verifyAddDocument people

    people.FindAll().ToList() |> should haveCount 1

[<Test>]
let ``When using the default app config approach and the collection already exists, things should still work``() =
    let db = createMongoServer() |> getMongoDatabase dbName
    db.DropCollection collectionName |> ignore

    db.CollectionExists collectionName |> should not' (be True)

    db |> getMongoCollection<Person> collectionName |> ignore
    let people = db |> getMongoCollection<Person> collectionName

    verifyAddDocument people
    
    db |> getMongoCollection<Person> collectionName |> ignore

    verifyAddDocument people
