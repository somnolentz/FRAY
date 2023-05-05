using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool TPbacktofirstLevel = ((Ink.Runtime.BoolValue)DialogueManager
               .GetInstance()
               .GetVariableState("TPbacktofirstLevel")).value;

        if (TPbacktofirstLevel == true)
        {
            Debug.Log("tps cutely");
            SceneManager.LoadScene("seconddavehub");
        }
    }
}
