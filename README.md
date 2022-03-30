# credit-card-validator
API Requirements
In order to make a payment with the credit card, user has to provide credit card information and it has
to be validated.
Please create an API that validates credit card data.
Input parameters: Card owner, Credit Card number, issue date and CVC.
Logic should verify that all fields are provider, card owner does not have credit card information, credit
card is not expired, number is valid for specified credit card type, CVC is valid for specified credit card
type.
API should return credit card type in case of success: Master Card, Visa or American Express.
API should return all validation errors in case of failure.
Implement only 3 types of credit cards: Master Card, Visa and American Express.

# Tech stack
1. .NET 5
2. Fluent API
3. Fluent Assertion
4. xUnit
5. Swagger 


![Capture](https://user-images.githubusercontent.com/6374652/160878855-83544d4d-2dfd-4b24-a55c-75a15b6d584b.PNG)
