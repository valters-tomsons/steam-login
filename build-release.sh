#!/bin/sh

dotnet publish ./src --self-contained -c Release -r linux-x64 -o release_linux
