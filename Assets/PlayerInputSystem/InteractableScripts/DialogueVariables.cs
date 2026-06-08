using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

/// <summary>
/// Gets variables from within INK and saves any changes to their states. Is used by DialogueManager to listen in on
/// any changes made to variables to persist between dialogue. 
/// Made by @ShapedByRainStudios on Youtube, no other changes made. Is only used with the functionality of variable persistence
/// </summary>
public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    private Story globalVariablesStory;
    private const string SaveVariablesKey = "INK_VARIABLES";
    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        globalVariablesStory = new Story(loadGlobalsJSON.text);

        //if (PlayerPrefs.HasKey(SaveVariablesKey))
        //{
        //    string jsonState = PlayerPrefs.GetString(SaveVariablesKey);
        //    globalVariablesStory.state.LoadJson(jsonState);
        //}

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value =
                globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log($"Loaded global variable: {name} = {value}");
        }
    }

    /// <summary>
    /// Call this method to save the current variable state in INK.
    /// Should be called on Application quit or from the pause menu to save
    /// </summary>
    //public void SaveVariables()
    //{
    //    if (globalVariablesStory != null)
    //    {
    //        VariablesToStory(globalVariablesStory);
    //        PlayerPrefs.SetString(SaveVariablesKey, globalVariablesStory.state.ToJson());
    //    }
    //}

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
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
    }
}
