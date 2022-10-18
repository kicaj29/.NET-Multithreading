# System.Timers.Timer

If the `Elapsed` handler throws exception then it crashed both asp.net core and console app. That`s why we should add try/catch in this handler.