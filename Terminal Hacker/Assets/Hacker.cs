using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
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
                Terminal.WriteLine("Hacking School");
                break;
            case "2":
                Terminal.WriteLine("Hacking Police Station");
                break;
            case "3":
                Terminal.WriteLine("Hacking NSA");
                break;
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
}