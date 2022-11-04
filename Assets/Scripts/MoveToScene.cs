using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour
{
    public void MoveScene()
    {
        SceneManager.LoadScene(1);
    }
}
