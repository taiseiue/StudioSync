#!/bin/sh
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 6.0 -InstallDir ./dotnet6
./dotnet6/dotnet workload install wasm-tools
./dotnet6/dotnet workload install wasm-tools-net6
cd StudioSync
../dotnet6/dotnet --version
../dotnet6/dotnet publish -c Release -o output
