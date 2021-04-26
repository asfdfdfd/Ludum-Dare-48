using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Level0Controller : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameState.CurrentLevelName = "Level 1 Final";
            GameState.PreviousLevelName = null;
            SceneManager.LoadScene("Level 1 Final");
        }
    }
}
