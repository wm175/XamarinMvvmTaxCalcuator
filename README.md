# MvvmTaxCalculator
Simple Xamarin MVVM Tax Calculator 

## You must add your own TaxJar API key!!
replace "YOUR API_KEY GOES HERE" in TaxJarCalculator.cs in order for TaxJarCalculator (and therefore the app) to return results


### Assumptions Made:
- All orders To and From locations are in the US
- All orders must have non-negatve order totals and shipping values
- Tax calculator will indicate bad result with -1
- that it works the same on iOS... not the best thing to assume but I do not have a personal Mac for iOS development
