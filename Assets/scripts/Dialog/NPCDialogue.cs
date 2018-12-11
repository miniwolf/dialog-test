using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class NPCDialogue : MonoBehaviour, Entity
    {
        public DialogueSystem DialogueSystem { private get; set; }
        public DialogueImporter.SceneDialogue SceneDialogue { get; set; }
        public string Name;

        private void Awake()
        {
            Registry.Register(this);
        }

        private void OnTriggerEnter(Collider collider)
        {
            var dialogue = SceneDialogue.Lines
                .Where(codeData => codeData.Character.Equals("Name"))
                .Select(codeData => codeData.Text).ToArray();
            DialogueSystem.AddNewDialogue(dialogue, gameObject.name);
            DialogueSystem.StartDialogue();
        }

        public const string TagString = "NPC";

        public string Tag => TagString;
    }
}