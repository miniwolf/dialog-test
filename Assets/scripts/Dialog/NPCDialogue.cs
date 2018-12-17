using UnityEngine;

namespace DefaultNamespace
{
    public class NPCDialogue : MonoBehaviour, Entity
    {
        public DialogueSystem DialogueSystem { private get; set; }
        public string[] Dialogue;
        public string Name;

        private void Awake()
        {
            Registry.Register(this);
        }

        private void OnTriggerEnter(Collider collider)
        {
            DialogueSystem.AddNewDialogue(Dialogue, gameObject.name);
            DialogueSystem.StartDialogue();
        }

        public const string TagString = "NPC";

        public string Tag => TagString;
    }
}