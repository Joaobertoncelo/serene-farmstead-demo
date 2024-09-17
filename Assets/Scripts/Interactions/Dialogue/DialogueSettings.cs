using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;

    public List<Sentences> dialogues = new List<Sentences>();
}

[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages language;
}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings uiDialogueSettings = (DialogueSettings)target;
        Languages uiLanguages = new Languages();
        Sentences uiSentences = new Sentences();

        uiLanguages.portuguese = uiDialogueSettings.sentence;

        uiSentences.profile = uiDialogueSettings.speakerSprite;
        uiSentences.language = uiLanguages;

        if(GUILayout.Button("Create Dialogue"))
        {
            if(uiDialogueSettings.sentence != "")
            {
                uiDialogueSettings.dialogues.Add(uiSentences);
                uiDialogueSettings.speakerSprite = null;
                uiDialogueSettings.sentence = "";
            }
        }
    }
}


#endif