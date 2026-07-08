Number Guessing Game

A console-based Number Guessing Game built in C#.

Features


Difficulty System — choose a difficulty level that changes the guessing range/attempts
High Score Saving — high scores are saved to a .txt file
Separate Score Tracking — each difficulty level stores its own high score separately
ASCII Art — custom ASCII art displayed in the game
Hint System — players can request a hint about what the number might be


Game Flow

Start Game
   ↓
Generate Random Number
   ↓
Player Guess
   ↓
Check Guess
   ↓
Win? → End
Lose attempts? → End
Else → Repeat
   ↓
Ask to Replay

Tech Stack


C# (.NET)
