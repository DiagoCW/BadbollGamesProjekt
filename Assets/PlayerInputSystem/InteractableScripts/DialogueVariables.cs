using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        Story globalVariablesStory = new Story(loadGlobalsJSON.text);

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value =
                globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log($"Loaded global variable: {name} = {value}");
        }
    }
    public void StartListening(Story story)
    {
        SaveVariables(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log($"Variable changed: {name} = {value}");
        variables[name] = value;
    }

    void SaveVariables(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }

        bool foundSnusdosa = story.variablesState.TryGetDefaultVariableValue("foundSnusdosa");
        bool foundSnusdosa1 = story.variablesState.GetVariableWithName("foundSnusdosa");
        // Debug.Log($"Saved variable: foundSnusdosa = {foundSnusdosa}");
        string foundSnusdosa2 = story.variablesState.GetVariableWithName("foundSnusdosa").ToString();
        var foundSnusdosa3 = story.variablesState["foundSnusdosa"];
        if (foundSnusdosa1.Equals(true))
        {
            Debug.Log($"Saved variable: foundSnusdosa1 = {foundSnusdosa1}");
        }
        if (foundSnusdosa.Equals(true))
        {
            Debug.Log($"Saved variable: foundSnusdosa2 = {foundSnusdosa}");
        }
        if (foundSnusdosa3.Equals(true))
        {
            Debug.Log($"Saved variable: foundSnusdosa3 = {foundSnusdosa3}");
        }
        Debug.Log(foundSnusdosa3);
        var counter = story.variablesState["counter"];

        if (counter.Equals(3))
            Debug.Log("hihihi");
        else
            Debug.Log($"Counter is not 3, it is {counter}");
        object countervärde = "5";
        Debug.Log($"Counter value before assignment: {counter}");
        counter = countervärde;
        Debug.Log($"Counter value after assignment: {counter}");
        Debug.Log($"Counter value in story before assignment: {story.variablesState["counter"]}");
        story.variablesState["counter"] = countervärde;
        Debug.Log($"Counter value in story after assignment: {story.variablesState["counter"]}");

    }
}
