using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Game state
    int level;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
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

        switch (input)
        {
            case "1":
                {
                    level = 1;
                    StartGame();
                    break;
                }
            case "2":
                {
                    level = 2;
                    StartGame();
                    break;
                }
            case "3":
                {
                    level = 3;
                    StartGame();
                    break;
                }
            case "007":
                Terminal.WriteLine("Please make a selection, Mr. Bond");
                break;
            case "menu":
                {
                    ShowMainMenu();
                    break;
                }
            default:
                Terminal.WriteLine("Please enter a valid input.");
                break;
        }
    }

    private void StartGame()
    {
        Terminal.WriteLine($"You have chosen level {level}");
    }
}