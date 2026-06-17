AutoCommitMessage — browser app
===============================

HOW TO RUN
----------
1. Make sure the ".NET 8 ASP.NET Core Runtime" is installed (one free download from
   Microsoft). This single runtime includes everything the app needs.
     https://dotnet.microsoft.com/download/dotnet/8.0
     -> under "ASP.NET Core Runtime 8.x", pick "Hosting Bundle" or the
        "Windows x64" installer.

2. Double-click  AutoCommitMessage.Standalone.exe
   It starts a small local web server and opens the app in your browser
   automatically (http://localhost:3109).

3. In the app, set your Mendix project folder (the folder containing the .mpr
   file and the .git directory). You can paste the path or use the Browse button.

To stop the app, close the console window it opened (or press Ctrl+C in it).

NOTES
-----
- No administrator rights are required.
- Different port?  Run from a terminal:  AutoCommitMessage.Standalone.exe --port 4000
- The optional .env.example shows environment variables you can set; copy it to
  ".env" next to the exe only if you need to override defaults.
