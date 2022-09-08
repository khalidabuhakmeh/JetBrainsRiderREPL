# JetBrains Rider REPL Experience

By leveraging features of #dotnet 6 and hot reload, along with JetBrains Rider's dotnet-watch run configuration, you can get a very
nice REPL experience for trying out libraries and C# code.

## Dependencies

- Spectre.Console (pretty output)
- JsonFlatFileDataStore (for storage between reloads)

## Getting Started

In JetBrains Rider, start the `REPL RUN` run configuration. It will proceed to run the code in `Program.cs`. Feel free to add dependencies needed to write your code.

## Techniques

I am using a `Global` static class and `global using` statements to give you a seamless context-based programming model.