# VM
Please build the project using Visual Studio 2017. 
Nuget packages have been excluded for keeping repository size minimum.
Once you build http://localhost/vm is the root of the api and will have some info about the methods it exposes.

# Assumptions

The Machine just accepts coins and Credit Card payment. No bank note is accepted.

As CreditCard payment needs an external Gateway, just frontend part is developed.

The maximum amount paid in each transaction is $10.Any more coins inserted then $10 will be paid back immidiately.

and the Machine is full of 5cents, 10cents, 20cents, 50cents, $1 and $2 coins to pay the changes back. 

If your have not paid any money no selection will be available for you.

