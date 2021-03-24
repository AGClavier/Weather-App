﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button2 : MonoBehaviour
{
    void OnGUI()
    {
        GUI.contentColor = Color.white;

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 525, 160, 60), "Go Back"))
        {
            SceneManager.LoadScene("WeathAR");
        }
    }
}
