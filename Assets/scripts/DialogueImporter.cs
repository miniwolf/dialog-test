using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.AssetImporters;
using System.IO;
using System.Linq;

[ScriptedImporter(1, "story")]
public class DialogueImporter : ScriptedImporter
{
    [Serializable]
    public class LineCodeData
    {
        public string ID;

        public string Character;
        public string Text;
        public string SoundFile;
        public string Emotion;
        public float VolumeDB;
    }

    [Serializable]
    public class SceneDialogue : ScriptableObject
    {
        public List<LineCodeData> Lines;
    }

    public override void OnImportAsset(AssetImportContext ctx)
    {
        var content = File.ReadLines(ctx.assetPath);

        var lines = ParseContent(content);
        var sceneDialogue = ScriptableObject.CreateInstance<SceneDialogue>();
        sceneDialogue.Lines = lines;

        ctx.AddObjectToAsset("sceneDialogue", sceneDialogue);
        ctx.SetMainObject(sceneDialogue);
    }

    private List<LineCodeData> ParseContent(IEnumerable<string> content)
    {
        var res = new List<LineCodeData>();
        var enumerator = content.GetEnumerator();
        do
        {
            var dialogueLine = ReadDialogueLine(enumerator);
            res.Add(dialogueLine);
        } while (enumerator.MoveNext());

        return res;
    }

    private LineCodeData ReadDialogueLine(IEnumerator<string> enumerator)
    {
        LineCodeData data = new LineCodeData();
        while (enumerator.MoveNext())
        {
            var line = enumerator.Current;
            if (line == null)
            {
                continue;
            }

            if (line.Contains('{'))
            {
                data.ID = line.Split(' ')[0];
            }
            else if (line.Contains('}'))
            {
                return data;
            }
            else if (line.Contains("Character"))
            {
                data.Character = line.Split('"')[1];
            }
            else if (line.Contains("VolumeDB"))
            {
                var split = line.Split('=', ';');
                data.VolumeDB = float.Parse(split[1]);
            }
            else if (line.Contains("Text"))
            {
                data.Text = line.Split('"')[1];
            }
            else if (line.Contains("SoundFile"))
            {
                data.SoundFile = line.Split('"')[1];
            }
            else if (line.Contains("Emotion"))
            {
                data.Emotion = line.Split('"')[1];
            }
        }

        throw new DialogueParseException("Reached end of input without hitting a }");
    }
}

internal class DialogueParseException : Exception {
    public DialogueParseException(string message) : base(message) { }
}