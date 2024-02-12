
@ECHO OFF
SET ASPNETCORE_ENVIRONMENT=Development
START dotnet run --project ./Src/Juzhen.AiYanJing.AdminApi --urls="http://*:5000" 
EXIT
