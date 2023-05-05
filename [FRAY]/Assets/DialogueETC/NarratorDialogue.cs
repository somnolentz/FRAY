using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NarratorDialogue : MonoBehaviour
{
    public TextMeshProUGUI captionText;
    public float displayTimePerCharacter = 0.05f;
    public string[] dialogueLines;
    //static string[] currentDialogueLines;
    static NarratorDialogue instance;

    private int currentLineIndex = 0;
    private bool inDialogue;

    void Start()
    {
        // Disable the caption text initially
        captionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            //currentLineIndex = 0;

            // Show the caption text
            captionText.gameObject.SetActive(true);

            if (instance != null)
            {
                StopCoroutine(instance.ShowDialogue());
                instance.gameObject.SetActive(false);
                //currentDialogueLines = dialogueLines;
            }

            instance = this;
            // Start displaying the dialogue lines recursively
            StartCoroutine(ShowDialogue());
        }

    }

    private IEnumerator ShowDialogue()
    {
        // Check if there are any more dialogue lines to display
        if (currentLineIndex < dialogueLines.Length)
        {
            // Set the caption text to the current line of dialogue
            string currentLine = dialogueLines[currentLineIndex];
            captionText.text = "";

            // Animate the dialogue text letter by letter
            for (int i = 0; i < currentLine.Length; i++)
            {
                captionText.text += currentLine[i];
                yield return new WaitForSeconds(displayTimePerCharacter);
            }

            // Calculate the display time based on the length of the caption text
            float displayTime = currentLine.Length * displayTimePerCharacter;

            // Hide the caption text after a certain amount of time
            yield return new WaitForSeconds(displayTime);

            // Increment the current line index to move to the next line of dialogue
            currentLineIndex++;

            // Call the coroutine again to display the next line of dialogue
            yield return StartCoroutine(ShowDialogue());
        }
        else
        {
            // Hide the caption text if there are no more lines of dialogue
            captionText.gameObject.SetActive(false);
            inDialogue = false;
        }
    }
}
