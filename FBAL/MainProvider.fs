// The SqlEntityConnection (Entity Data Model) TypeProvider allows you to write code that uses 
// a live connection to a database that is represented by the Entity Data Model. For more information, 
// please go to 
//    http://go.microsoft.com/fwlink/?LinkId=229210

module MainProvider

#if INTERACTIVE
#r "System.Data"
#r "System.Data.Entity"
#r "FSharp.Data.TypeProviders"
#endif

open System.Data
open System.Data.Entity
open Microsoft.FSharp.Data.TypeProviders
open System.Data.SqlClient
open Models

// You can use Server Explorer to build your ConnectionString.
//type internal SqlConnection = Microsoft.FSharp.Data.TypeProviders.SqlEntityConnection<ConnectionString = @"Data Source=(LocalDB)\v11.0;Initial Catalog=tempdb;Integrated Security=True">
//let internal db = SqlConnection.GetDataContext()

type internal MainConnection = SqlEntityConnection<ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True">
let internal dataBase = MainConnection.GetDataContext()

let internal retriveCategory category =
    query {
        for example in dataBase.Examples do
        where (example.Category = category)
        select example
    }

let internal retrieveBy id category =
    query {
        for example in dataBase.Examples do
        where (example.Category = category && example.Id = id)
        select example
    }

//let internal table = query {
//    for r in db.SomeTable do
//    select r
//    }
//
//for p in table do
//    printfn "%s" p.SomeProperty

