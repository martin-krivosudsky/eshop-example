## eshop-example

### How to run:
Clone repo
```
git clone git@github.com:martin-krivosudsky/eshop-example.git
```
Restore solution
```
dotnet restore eshop-example\Eshop\Eshop.sln
```
Build API
```
cd eshop-example\Eshop\Eshop.API
dotnet build
```
Create Database (MSSQL DB should be accessible on localhost)
```
dotnet ef database update
```
Run API
```
dotnet run
```
Swagger API should be available on http://localhost:5000/swagger/index.html

Run WebApp
```
cd ../Eshop.WebApp
dotnet build
dotnet run
```
WebApp should be available on https://localhost:44308/

### How to run unit tests:
```
cd eshop-example\Eshop\Eshop.Services.Tests>
dotnet test
```