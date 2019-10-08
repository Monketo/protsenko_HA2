namespace OOAD19.Coding.W05

open System
open Xunit
open FSharp.Json
open Client
open FsUnit.Xunit


module Spec =
    
    let private sampleCustomer() =
        { BillingStub.Customer.companyName = DateTime.UtcNow.Ticks |> string
          BillingStub.Customer.id = DateTime.UtcNow.Second }

    let assertCustomer (c: BillingStub.Customer) =
        let l = AccountingStub.getCustomers()
        l |> should haveLength 2
        let j1 :: j2 :: [] = l
        let c1 = Json.deserialize<AccountingStub.Customer> j1
        let c2 = Json.deserialize<AccountingStub.Customer> j2
        c1.BillingId |> should equal c2.BillingId
        c1.BillingId |> should equal (Some c.id)
        c1.DisplayName.Replace(" (Prepaid)", "") |> should equal c.companyName
        c2.DisplayName.Replace(" (Prepaid)", "") |> should equal c.companyName
        c1.DisplayName |> should not' (equal c2.DisplayName)

    [<Fact>]
    let ``Add new customer``() =
        AccountingStub.wipe()
        let c = sampleCustomer()
        BillingStub.customers <- [ Json.serialize c ]
        syncCustomers()
        assertCustomer c

    [<Fact>]
    let ``Add same customer twice``() =
        AccountingStub.wipe()
        let c = sampleCustomer()
        BillingStub.customers <- [ Json.serialize c ]
        syncCustomers()
        syncCustomers()
        assertCustomer c

    [<Fact>]
    let ``Add missing prepaid customer``() =
        AccountingStub.wipe()
        let c = sampleCustomer()
        BillingStub.customers <- [ Json.serialize c ]
        let ac =
            { AccountingStub.Customer.DisplayName = c.companyName
              AccountingStub.Customer.Id = None
              AccountingStub.Customer.BillingId = Some c.id }
        ac
        |> Json.serialize
        |> AccountingStub.addCustomer
        |> ignore
        syncCustomers()
        assertCustomer c

    [<Fact>]
    let ``Update customer``() =
        AccountingStub.wipe()
        let c = sampleCustomer()
        BillingStub.customers <- [ Json.serialize c ]
        syncCustomers()
        let un = "upd " + c.companyName
        let uc = { c with companyName = un }
        BillingStub.customers <- [ Json.serialize uc ]
        syncCustomers()
        assertCustomer uc

    [<Fact>]
    let ``Add missing customer and update existing``() =
        AccountingStub.wipe()
        let c = sampleCustomer()
        BillingStub.customers <- [ Json.serialize c ]
        let ac =
            { AccountingStub.Customer.DisplayName = c.companyName
              AccountingStub.Customer.Id = None
              AccountingStub.Customer.BillingId = Some c.id }
        ac
        |> Json.serialize
        |> AccountingStub.addCustomer
        |> ignore
        syncCustomers()
        let un = "upd " + c.companyName
        let uc = { c with companyName = un }
        BillingStub.customers <- [ Json.serialize uc ]
        syncCustomers()
        assertCustomer uc
