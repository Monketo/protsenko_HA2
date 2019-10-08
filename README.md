# Coding assignment. Week 5 (2019).

[![Join the chat at https://gitter.im/kmaooad/coding-19W05](https://badges.gitter.im/kmaooad/coding-19W05.svg)](https://gitter.im/kmaooad/coding-19W05?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
<NEVER BUILT>
 
### Task

Implement customers sync for accounting app.

You are provided with billing and accounting API stubs:
 - `AccountingStub` module:
    1. `type Customer = { Id : int option; DisplayName : string; BillingId : int option }`
    2. `getCustomers() : string list` returns JSON list of customers in Accounting
    3. `addCustomer (json: string) : int` tries to add customer; if customer with given name exists, throws an exception, otherwise creates customer and returns assigned ID
    4. `updateCustomer (json: string) : unit` tries to update customer; if customer does not exist or provided name is taken, throws an exception, otherwise updates customer.
 - `BillingStub` module:
    1. `type Customer = { id : int; companyName : string }`
    2. `getCustomers() : string list` returns JSON list of customers in Billing

You have to implement syncing customers from Billing to Accounting (`Client.fs`) according to these rules:
1. For every customer in Billing create **two** customers in Accounting: one named exactly as in Billing, another – with added "(Prepaid)" in the end, e.g. "Charlie Corporation (Prepaid)"
2. If customer already exists in Accounting, but name has been changed in Billing, it has to be updated in Accounting. 

### (Optional) Peer review

As before, it is highly recommended to do some code review for your classmates and ask for code review from others. Discuss your points of view and chosen approaches.

### (Optional) Analyze dependencies

Again, dependencies analysis is especially crucial at these early stages of your deep dive into OOD. This time try not only to find dependencies, but also classify them: do you think you can group dependencies according to some criteria? Do you see any typical *responsibilities* in your code? Can you split all dependencies and components (types, functions, modules) into some *abstraction levels*? Try to analyze these issues and their influence on maintenance and evolution of your code. 
