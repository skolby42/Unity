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
                    password = GetPassword();
                    StartGame();
                    break;
                }
            case "007":
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
            case 1:
                {
                    string[] passwords = { "bell", "book", "pen", "study", "yard" };
                    return passwords[0];
                }
            case 2:
                {
                    string[] passwords = { "arrest", "enforce", "protect", "ridealong", "uniform" };
                    return passwords[0];
                }
            case 3:
                {
                    string[] passwords = { "cryptography", "hackathon", "listening", "monitoring", "surveillance" };
                    return passwords[0];
                }
            default: return "";
        }
    }

    private void Win()
    {
        currentScreen = Screen.Win;

        Terminal.WriteLine($"Correct password!");

        switch (level)
        {
            case 1: Terminal.WriteLine("Time to change some grades");
                break;
            case 2: Terminal.WriteLine("Welcome to the police network");
                break;
            case 3: Terminal.WriteLine("Welcome to the NSA");
                break;
        }

        Terminal.WriteLine("Press enter to play again");
    }

    private void Lose()
    {
        Terminal.WriteLine("Wrong password! Try again.");
    }

    private void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.WriteLine($"You have chosen level {level}");
        Terminal.WriteLine("Please enter your password");
    }
}