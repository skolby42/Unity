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
        else if (input.Equals("quit", StringComparison.OrdinalIgnoreCase) || input.Equals("close", StringComparison.OrdinalIgnoreCase))
        {
            Terminal.WriteLine("If on the web, close the tab");
            Application.Quit();
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
                    password = GetRandomPassword();
                    PromptForPassword();
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
            PromptForPassword();
        }
    }

    string GetRandomPassword()
    {
        switch (level)
        {
            case 1:
                {
                    int index = UnityEngine.Random.Range(0, level1Passwords.Length);
                    return level1Passwords[index];
                }
            case 2:
                {
                    int index = UnityEngine.Random.Range(0, level2Passwords.Length);
                    return level2Passwords[index];
                }
            case 3:
                {
                    int index = UnityEngine.Random.Range(0, level3Passwords.Length);
                    return level3Passwords[index];
                }
            default: return "";
        }
    }

    private void PromptForPassword()
    {
        currentScreen = Screen.Password;

        Terminal.ClearScreen();
        Terminal.WriteLine($"Enter your password, hint: {password.Anagram()}");
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
}