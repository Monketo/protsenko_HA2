namespace OOAD19.Coding.W05

module BillingStub =
    let mutable customers: string list = []

    type Customer =
        { id: int
          companyName: string }

    let getCustomers() = customers