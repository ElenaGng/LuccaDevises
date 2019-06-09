# LuccaDevises Projet by Elena GENGE ^^
Recrutement Test for Back-end Developer - June 2019
.Net core 2.2 - C#

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

The first line contains:
InputCurrencyFrom in which the amount is displayed [3-character code]
InputAmountToExchage in this initial currency as a positive integer > 0
InputCurrencyToThe is the target currency to which it wants to convert the amount [3-character code]

The information should be in the format:
InputCurrencyFrom;InputAmountToExchage;InputCurrencyTo

**Second line**
It should contains : Number of information lines

**Others lines**

There follows N lines representing the exchange rates represented as follows:
CurrencyFrom : The starting currency [3-character code]
CurrencyTo : The target currency [3-character code]
Rate : The exchange rate [4 decimal with a "." As a decimal separator]

The information should be in the format:
CurrencyFrom;CurrencyTo;Rate

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
## How does the application works ?

**If multiple conversion paths allow you to reach the target currency, the shortest path will be used !**

For example, if you want 550 Euros (EUR) in Yen (JPY), and you have this exchange rate list:

| Currency From |  Currency To  | Exchage Rate  |
| ------------- |:-------------:| -------------:|
|      AUD      |      CHF      |    0.9661     |
|      JPY      |      KWU      |    13.1151    |
|      EUR      |      CHF      |    1.2053     |
|      AUD      |      JPY      |   86.0305     |
|      EUR      |      USD      |    1.2989     |
|      JPY      |      INR      |    0.6571     |


To convert EUR to JPY, you need to convert the expense amount to CHF, then AUD, then JPY, using the following rates:

| Currency From |  Currency To  | Exchage Rate  |
| ------------- |:-------------:| -------------:|
|      AUD      |      CHF      |    0.9661     |
|      EUR      |      CHF      |    1.2053     |
|      AUD      |      JPY      |   86.0305     |


In the case of the example, the result is as follows:

EUR -> CHF: 550 * 1.2053 = 662.9150
CHF -> AUD: 662.9150 * (1 / 0.9661) = 686.1833
(Attention, here we reverse the rate because the rate provided is
AUD -> CHF and we want CHF -> AUD.
The inversion must also be rounded to 4 decimal places, so 1 / 0.9661 = 1.0351
AUD -> JPY: 686.1833 * 86.0305 = 59033 (rounded to the integer)

The expected result is 59033.

## Have fun !!
