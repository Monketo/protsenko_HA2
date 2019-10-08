namespace OOAD19.Coding.W05

open FSharp.Json
open System

module AccountingStub =

    let mutable private customers: string list = []

    type Customer =
        { Id: int option
          DisplayName: string
          BillingId: int option }

    let getCustomers() = customers
    let wipe() = customers <- []
    let addCustomer json: int =
        let inC = Json.deserialize<Customer> json

        let exC =
            getCustomers()
            |> List.map (fun c -> Json.deserialize<Customer> c)
            |> List.exists (fun c -> c.DisplayName = inC.DisplayName)

        if exC then failwithf "Customer with name %s already exists" inC.DisplayName

        let newId = List.length customers + 2000

        let newC =
            { Customer.Id = Some newId
              DisplayName = inC.DisplayName
              BillingId = inC.BillingId }

        customers <- Json.serialize newC :: customers

        newId

    let updateCustomer json =
        let inC = Json.deserialize<Customer> json

        let conflict =
            getCustomers()
            |> List.map (fun c -> Json.deserialize<Customer> c)
            |> List.exists (fun c -> c.DisplayName = inC.DisplayName && c.Id <> inC.Id)

        if conflict then failwithf "Another customer with name %s already exists" inC.DisplayName

        let exc =
            getCustomers()
            |> List.map (fun c -> Json.deserialize<Customer> c)
            |> List.tryFind (fun c -> c.Id = inC.Id)

        if exc.IsNone then failwith "Customer does not exist"

        let uc = { exc.Value with DisplayName = inC.DisplayName }

        let otherCustomers =
            getCustomers()
            |> List.map (fun c -> Json.deserialize<Customer> c)
            |> List.filter (fun c -> c.Id <> inC.Id)
            |> List.map Json.serialize


        customers <- Json.serialize uc :: otherCustomers
