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
    Screen currentScreen = Screen.MainMenu;

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
        
    }

    private void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.WriteLine($"You have chosen level {level}");
        Terminal.WriteLine("Please enter your password");
    }
}