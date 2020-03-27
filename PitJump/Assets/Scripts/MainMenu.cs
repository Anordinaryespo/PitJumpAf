using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public string instruction;
    public string playGameLevel;

    public void Instruction()
    {
        Application.LoadLevel(instruction);
    }

    public void PlayGame()
    {
        Application.LoadLevel(playGameLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
