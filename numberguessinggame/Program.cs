    using System;
    using System.IO;

    enum Difficulty { Easy, Medium, Hard }
    class NumberGuessingGame
    {
        const int MIN_NUMBER = 1;      
        const int MAX_NUMBER = 100;
        const int EASY_ATTEMPTS = 12;
        const int MEDIUM_ATTEMPTS = 8;
        const int HARD_ATTEMPTS = 4;





    static void ShowTitle()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(@"
                                                                                                                        
                                                                                                                        
███  ██ ▄▄ ▄▄ ▄▄   ▄▄ ▄▄▄▄  ▄▄▄▄▄ ▄▄▄▄     ▄████  ▄▄ ▄▄ ▄▄▄▄▄  ▄▄▄▄  ▄▄▄▄ ▄▄ ▄▄  ▄▄  ▄▄▄▄    ▄████   ▄▄▄  ▄▄   ▄▄ ▄▄▄▄▄ 
██ ▀▄██ ██ ██ ██▀▄▀██ ██▄██ ██▄▄  ██▄█▄   ██  ▄▄▄ ██ ██ ██▄▄  ███▄▄ ███▄▄ ██ ███▄██ ██ ▄▄   ██  ▄▄▄ ██▀██ ██▀▄▀██ ██▄▄  
██   ██ ▀███▀ ██   ██ ██▄█▀ ██▄▄▄ ██ ██    ▀███▀  ▀███▀ ██▄▄▄ ▄▄██▀ ▄▄██▀ ██ ██ ▀██ ▀███▀    ▀███▀  ██▀██ ██   ██ ██▄▄▄ 
                                                                                                                        ");
        Console.ResetColor();
    }




    static string GetHighScoreFile(Difficulty difficulty)
    {
        if (difficulty == Difficulty.Easy) return "highscore_easy.txt";

        if (difficulty == Difficulty.Medium) return "highscore_medium.txt";

        return "highscore_hard.txt";
    }

        static int LoadHighScore(Difficulty difficulty)
        {
            if (File.Exists(GetHighScoreFile(difficulty)))
            {
                string text = File.ReadAllText(GetHighScoreFile(difficulty));
                if (int.TryParse(text, out int highScore))
                    return highScore;
            }
            return 0;
        }
            static void SaveHighScore(Difficulty difficulty,int highScore) 
            {
                File.WriteAllText(GetHighScoreFile(difficulty), highScore.ToString());
            }
    
        static void Main()
        {

        ShowTitle();
        

        Random random = new Random();
            bool playAgain = true;
            int totalScore = 0;
            

            while (playAgain)
            {
                
            Difficulty difficulty = PickDifficulty();
            Console.Clear();
            int highScore = LoadHighScore(difficulty);
            int roundScore = PlayGame(random, difficulty);
                totalScore += roundScore;
            if (roundScore > highScore)
            {
                highScore = roundScore;
                SaveHighScore(difficulty, highScore);
                Console.WriteLine($"New high score for {difficulty}! {highScore} points!");
            }
            else
            {
                Console.WriteLine($"High score for {difficulty} is still: {highScore} points.");
            }
            while (true)
                {
                    Console.Write("Do you want to play again? (yes/no): ");
                    string response = Console.ReadLine().ToLower();
                    if (response == "yes")
                    {

                        Console.WriteLine("Alright lets play.!");
                        System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    ShowTitle();
                        break;
                    }

                else if (response == "no")
                {
                    Console.WriteLine("It's a shame but whatever you say bro!");
                    Console.WriteLine($"Your total score was: {totalScore}");
                    playAgain = false;
                    break;
                }
                else
                    {
                        Console.WriteLine("Invilad input try again");
                    }
                }

                //playAgain = response == "yes";


            }

        
        }

        static Difficulty PickDifficulty()
        {
            while (true)
            {
                Console.Write("Pick a difficulty (easy/medium/hard): ");
                string input = Console.ReadLine().ToLower();

                if (input == "easy")
                    return Difficulty.Easy;       
                else if (input == "medium")
                    return Difficulty.Medium;       
                else if (input == "hard")
                    return Difficulty.Hard;       
                else
                    Console.WriteLine("Please select a difficulty");  
            }
        }

        static int PlayGame(Random random,Difficulty difficulty)
        {

        int hintsRemaining;
            if (difficulty == Difficulty.Easy) { hintsRemaining = 2; }
            else if (difficulty == Difficulty.Medium) { hintsRemaining = 3; }
            else hintsRemaining = 4;
        bool usedHint = false;
        int currentMin = MIN_NUMBER;
        int currentMax = MAX_NUMBER;

        int secretNumber = random.Next(MIN_NUMBER, MAX_NUMBER + 1);

            int maxAttempts;

            if (difficulty == Difficulty.Easy)
                maxAttempts = EASY_ATTEMPTS;
            else if (difficulty == Difficulty.Medium)
                maxAttempts = MEDIUM_ATTEMPTS;
            else
                maxAttempts = HARD_ATTEMPTS;

            int attemptsUsed = 0;

            Console.WriteLine($"Guess the number between {MIN_NUMBER} and {MAX_NUMBER}!");
        Console.WriteLine($"You have {maxAttempts} attempts.\n");
        
        

            while (attemptsUsed < maxAttempts)
            {
            Console.WriteLine($"Hints remaining: {hintsRemaining}");
            Console.WriteLine($"Attempts remaining: {maxAttempts - attemptsUsed}");
            Console.Write($"Enter your guess (or H for hint): ");
            string input = Console.ReadLine();


            if (input.ToLower() == "h")
            {
                if (hintsRemaining > 0)
                {

                    currentMin = (currentMin + secretNumber) / 2;
                    currentMax = (currentMax + secretNumber) / 2;
                    usedHint = true;
                    hintsRemaining--;
                    Console.WriteLine($"Narrowed range: {currentMin} - {currentMax}");

                }
                

                else
                {
                    Console.WriteLine("No hints remaining");
                }
                continue;
            }


            if (!int.TryParse(input, out int guess))
            {
                Console.WriteLine("Invalid input Please enter a number.\n");
                continue;
            }

                attemptsUsed++;

                if (guess == secretNumber)
                {
                    int score = (maxAttempts - attemptsUsed + 1) * 10;

                if (!usedHint)
                {
                    Console.WriteLine("No hint bonus! Score doubled! 🎯");
                    score *= 2;
                }
                Console.WriteLine($"Correct! You guessed it in {attemptsUsed} attempts.");
                    Console.WriteLine($"You earned {score} points!");
                    return score;
                }
                else if (guess > secretNumber)
                {
                    Console.WriteLine("Too high!\n");
                }
                else
                {
                    Console.WriteLine("Too low!\n");
                }
            }

            Console.WriteLine($"You ran out of attempts! The number was {secretNumber}.");
            return 0;
        }
    }