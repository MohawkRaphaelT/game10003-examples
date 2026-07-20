:: %csprojcli% is system variable for https://github.com/RaphaelTetreault/csproj-cli
:: Update .\ directory and subdirectory solutions
%csprojcli% . modify-property --name="TargetFramework" --value=".net10.0"