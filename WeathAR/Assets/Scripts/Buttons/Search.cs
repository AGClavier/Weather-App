using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Search : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene("London");
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("Berlin");
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene("Lagos");
    }

    public void LoadScene4()
    {
        SceneManager.LoadScene("Kolkata");
    }

    public void LoadScene5()
    {
        SceneManager.LoadScene("Paris");
    }

    public void LoadScene6()
    {
        SceneManager.LoadScene("Shanghai");
    }
}
