# Banking Ledger
### Structural Overview

```
banking-ledger/
  |_BankingLedger/
       |_Program.cs          <- main program that is run
       |_CLInterface.cs      <- provides command line interface
       |_UserUtility.cs      <- handles logic behind creating valid user data
       |_User.cs             <- data store for user data
       |_UsersCollection.cs  <- handles collection of users
       |_Account.cs          <- handles logic behind account transactions
       |_Transaction.cs      <- data store for transaction data
  |_BankingLedger.Tests/
       |_BankingLedger_TestUserUtility.cs
       |_BankingLedger_TestAccount.cs
       |_BankingLedger_TestTransaction.cs
       |_BankingLedger_TestUsersCollection.cs
  |_README.md
```



### Run Program

Inside the directory _banking-ledger/BankingLedger_, the _Program.cs_ file can be run with the command:  

`dotnet run`

<br />

### Test Program

Inside the directory _banking-ledger/BankingLedger.Tests_, run the command:  

`dotnet test`

<br />
<br />

##### Copyright © 2019 Michelle Duer
