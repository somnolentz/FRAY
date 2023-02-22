
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


public class InkyVarManager 
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public InkyVarManager(TextAsset loadglobals)
    {
        Story gstory = new Story(loadglobals.text);

        //initialize dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in gstory.variablesState)
        {
            Ink.Runtime.Object value = gstory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("initialized global dialogue variable");
        }
    }

    public void StartListening (Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }
    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        //might be an issue
        variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink variable null");
        }
        return variableValue;
    }
}
