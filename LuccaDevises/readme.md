# LuccaDevises Projet by Elena GENGE 8-)

## How to run the application using CLI ?
1. Create/modify a Currency Exchange File following the rules ;)
.csv or .txt are both accepted !

2. Place yourself in the repository folder that contains LuccaDevises.exe 

For example --> C:\Users\Elena\source\repos\LuccaDevises\LuccaDevises\bin\Release\netcoreapp2.2\win10-x64

Then enter this command in your Command Prompt (Do not use Git Bash !)

```
$ LuccaDevises.exe <FullFilePath to Currency Exchange File>
```

----
## How should i create or modify a Currency Exchange File ?

Element should be separated with ;

**First line** 
It should contains the input of the request : InputCurrencyFrom;InputAmountToExchage;InputCurrencyTo

**Second line** 
It should contains : Number of information lines

**Others lines** 
It should contains all the rates information : CurrencyFrom;CurrencyTo;Rate

For example:

EUR;550;JPY  
6
AUD;CHF;0.9661
JPY;KRW;13.1151
EUR;CHF;1.2053
AUD;JPY;86.0305
EUR;USD;1.2989
JPY;INR;0.6571

----
## Have fun !!