using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Screen
{
    MainMenu,
    Password,
    Win
}

public class Hacker : MonoBehaviour
{
    string[] level1Passwords = { "bell", "book", "pen", "study", "yard" };
    string[] level2Passwords = { "arrest", "enforce", "protect", "ridealong", "uniform" };
    string[] level3Passwords = { "cryptography", "hackathon", "listening", "monitoring", "surveillance" };

    // Game state
    int level;
    string password;
    Screen currentScreen;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;

        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?\n");
        Terminal.WriteLine("1. School");
        Terminal.WriteLine("2. Police Station");
        Terminal.WriteLine("3. NSA\n");
        Terminal.WriteLine("Enter your selection:");
    }

    private void OnUserInput(string input)
    {
        if (currentScreen == Screen.Win)
        {
            ShowMainMenu();
            return;
        }

        if (string.IsNullOrEmpty(input)) return;

        if (input.Equals("menu", StringComparison.OrdinalIgnoreCase))
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    private void RunMainMenu(string input)
    {
        switch (input)
        {
            case "1":
            case "2":
            case "3":
                {
                    int.TryParse(input, out level);
                    StartGame();
                    break;
                }
            case "007":  // Easter egg
                Terminal.WriteLine("Please make a selection, Mr. Bond");
                break;
            default:
                Terminal.WriteLine("Please enter a valid input.");
                break;
        }
    }

    private void CheckPassword(string input)
    {
        if (input == password)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }

    string GetPassword()
    {
        switch (level)
        {
            case 1: return level1Passwords[0];  // TODO make random
            case 2: return level2Passwords[0];
            case 3: return level3Passwords[0];
            default: return "";
        }
    }

    private void Win()
    {
        currentScreen = Screen.Win;

        Terminal.WriteLine($"Correct password!");
        PrintLevelWinMessage();

        Terminal.WriteLine("Press enter to play again");
    }

    private void PrintLevelWinMessage()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Time to change some grades");
                break;
            case 2:
                Terminal.WriteLine("Welcome to the police network");
                break;
            case 3:
                Terminal.WriteLine("Welcome to the NSA");
                break;
        }
    }

    private void Lose()
    {
        Terminal.WriteLine("Wrong password! Try again.");
    }

    private void StartGame()
    {
        currentScreen = Screen.Password;
        password = GetPassword();

        Terminal.ClearScreen();
        Terminal.WriteLine("Please enter your password");
    }
}