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

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void ShowMainMenu()
    {
        Terminal.ClearScreen();

        string Name = "Scott";
        Terminal.WriteLine($"Hello {Name}");

        Terminal.WriteLine("Where would you like to hack into?\n");
        Terminal.WriteLine("1. School");
        Terminal.WriteLine("2. Police Station");
        Terminal.WriteLine("3. NSA\n");
        Terminal.WriteLine("Enter your selection:");
    }
}