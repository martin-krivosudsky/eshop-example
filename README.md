## eshop-example

### How to run:
Clone repo
```
git clone git@github.com:martin-krivosudsky/eshop-example.git
```
Build API
```
cd eshop-example\Eshop\Eshop.API>
```
Create Database (MSSQL DB should be accessible on localhost)
```
dotnet ef database update
```
Run API
```
dotnet run
```
Swagger API should be on http://localhost:5000/swagger/index.html

### How to run unit tests:
```
cd eshop-example\Eshop\Eshop.Services.Tests>
dotnet test
```