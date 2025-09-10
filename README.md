# Real-time Weather Monitoring Test Suite

This repository contains the test suite for the **Real-time Weather Monitoring** application, a C# console application that monitors weather data in real-time and activates various bots based on predefined conditions.

## Description

The Real-time Weather Monitoring system parses incoming weather data (in JSON or XML format) and processes it through a series of weather bots. Each bot is configured with specific thresholds and messages, activating alerts when conditions are met. This test suite ensures the reliability and correctness of all components, including data parsing, bot activation logic, and service integrations.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later.
- The main application project must be available at the referenced path (`..\..\Real-time weather monitoring\Real-time weather monitoring\Real-time weather monitoring.csproj`).
