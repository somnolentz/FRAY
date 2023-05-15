using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Ink.Runtime;
using UnityEngine.EventSystems;


public class DialogueManager : MonoBehaviour
{
    [Header("globals ink file")]
    [SerializeField] private TextAsset loadglobals;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    private static DialogueManager instance;

    public bool dialogueIsPlaying { get; private set; }

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    private InkyVarManager dialogueVariables;

    private List<Choice> activeChoices = new List<Choice>();

    public bool choicesAreDisplaying = false;

    private PlayerController pc;
    private TwoDPlayerCont tpc;
    private Jump j;
    private Dash d;
    private TwoDDash td;
    private Animator a;

    public GameObject player;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DM");
        }
        instance = this;
        dialogueVariables = new InkyVarManager(loadglobals);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        pc = player.GetComponent<PlayerController>();
        tpc = player.GetComponent<TwoDPlayerCont>();
        j = player.GetComponent<Jump>();
        d = player.GetComponent<Dash>();
        td = player.GetComponent<TwoDDash>();
        a = player.GetComponentInChildren<Animator>();

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && choicesAreDisplaying == false && dialogueIsPlaying == true || Input.GetMouseButtonDown(0) && choicesAreDisplaying == false && dialogueIsPlaying == true)
        {
            ContinueStory();
        }

      
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        ContinueStory();

    }
    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("more choices were given than the UI can support." + currentChoices.Count);
        }
        //enable and initialize the choices up to the amount of choices 
        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
            choicesAreDisplaying = true;
        }
        //go through remaining choices the ui supports and make sure theyre hidden
        for  (int i = index; i <choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        choicesAreDisplaying = false;

    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableValue == null)
        {
            Debug.Log("ink variable null");
        }
        return variableValue;
    }

}
