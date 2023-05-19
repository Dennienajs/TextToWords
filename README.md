# Text To Words

- .NET 7 Minimal API
- EF Core 
- SQL server
- MSSQLLocalDB configured in appsettings
- Xunit

Example requests in `Text.http`

Update database by running

- `dotnet ef database update --project Words.Api\Words.Api.csproj \ --startup-project Words.Api\Words.Api.csproj --context Words.Api.Data.WordsDbContext`
