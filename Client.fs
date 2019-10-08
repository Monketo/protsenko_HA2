namespace OOAD19.Coding.W05

open FSharp.Json
open AccountingStub
open BillingStub
open System

module Client =

    let syncCustomers(): unit = 

        let accountingCust = AccountingStub.getCustomers() 
                             |> List.map(Json.deserialize<AccountingStub.Customer>) 

        BillingStub.getCustomers()
        |> List.map( Json.deserialize<BillingStub.Customer> >> customerSync accountingCust)

        printf("Done syncing")

    let createNewReport(customer: BillingStub.Customer, func: string->unit): unit = 
        let ac = 
            {AccountingStub.Customer.DisplayName = customer.companyName
             AccountingStub.Customer.Id = None
             AccountingStub.Customer.BillingId = Some customer.id}

        let ac2 = 
            {AccountingStub.Customer.DisplayName = customer.companyName + " (Prepaid)"
             AccountingStub.Customer.Id = None
             AccountingStub.Customer.BillingId = Some customer.id}
        [ac,ac2]
            |> List.map(Json.serialize >> func)
            |> ignore
      
    let customerSync(accCustomers:AccountingStub.Customer list,bCust: BillingStub.Customer ): unit =
        let filteredCust = List.filter (fun x-> x.BillingId = Some bCust.id) accCustomers
        let result = if List.isEmpty filteredCust
                     then createNewReport bCust addCustomer
                     else createNewReport bCust updateCustomer
        result
    
        
         


   
     
        
       
        

         


        
