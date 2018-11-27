using UnityEngine;

namespace DefaultNamespace
{
    public class NPCDialogue : MonoBehaviour, Interactable, Entity
    {
        public DialogueSystem DialogueSystem { private get; set; }
        public string[] Dialogue;

        private void Awake()
        {
            Registry.Register(this);
        }

        public void Execute()
        {
            DialogueSystem.AddNewDialogue(Dialogue, "Man at the Cave");
        }

        public string Tag => "NPC";
    }
}