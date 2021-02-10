using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bottom : MonoBehaviour
{
    public void RestartBottom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
