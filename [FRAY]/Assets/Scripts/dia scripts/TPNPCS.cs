using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class TPNPCS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool firstlevelaccepted = ((Ink.Runtime.BoolValue)DialogueManager
               .GetInstance()
               .GetVariableState("firstlevelaccepted")).value;

        if(firstlevelaccepted == true)
        {
            Debug.Log("tps cutely");
            SceneManager.LoadScene("LevelOnePlaceholder");
        }
    }
}
