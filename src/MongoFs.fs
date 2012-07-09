[<AutoOpen>] 
module MongoFs

open MongoDB.Driver
open System.Configuration

let createMongoServerWithConnString (connString:string) = 
    MongoServer.Create connString

let createMongoServerWithConfig (configName:string) = 
    MongoServer.Create ConfigurationManager.AppSettings.[configName]

let createMongoServer () =
    createMongoServerWithConfig "MongoDbConnectionString"

let createLocalMongoServer () =
    createMongoServerWithConnString "mongodb://localhost/?safe=true"

let getMongoDatabase (dbName:string) (mongoServer:MongoServer) =
    mongoServer.GetDatabase dbName

let getMongoCollection<'a> (collectionName:string) (db:MongoDatabase) = 
    if not (db.CollectionExists collectionName) then
        db.CreateCollection collectionName |> ignore
    db.GetCollection<'a> collectionName

