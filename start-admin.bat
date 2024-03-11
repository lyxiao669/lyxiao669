
@ECHO OFF
SET ASPNETCORE_ENVIRONMENT=Development
START dotnet run --project ./Src/AdminApi
@REM START dotnet run --project ./Src/AdminApi --urls="http://*:5000" 
EXIT
