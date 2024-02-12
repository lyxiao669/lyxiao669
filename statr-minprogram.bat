@ECHO OFF
SET ASPNETCORE_ENVIRONMENT=Development
START dotnet run --project ./Src/Juzhen.AiYanJing.MiniApi --urls="http://*:5001" 
EXIT