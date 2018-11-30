using UnityEngine;

namespace DefaultNamespace
{
    public class NPCDialogue : MonoBehaviour, Interactable, Entity
    {
        public DialogueSystem DialogueSystem { private get; set; }
        public string[] Dialogue;
        public string Name;

        private void Awake()
        {
            Registry.Register(this);
        }

        public void Execute()
        {
            DialogueSystem.AddNewDialogue(Dialogue, gameObject.name);
        }

        public const string TagString = "NPC";

        public string Tag => TagString;
    }
}