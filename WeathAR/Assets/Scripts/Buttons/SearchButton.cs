using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SearchButton : MonoBehaviour
{
    void OnGUI()
    {
        GUI.contentColor = Color.white;

        if (GUI.Button(new Rect(Screen.width / 2 - -75, Screen.height / 2 + -600, 160, 60), "Search"))
        {
            SceneManager.LoadScene("Search");
        }
    }
}
