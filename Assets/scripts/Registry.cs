using System.Collections.Generic;
using Interactions;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class Registry : MonoBehaviour
    {
        private static readonly List<Entity> s_Entities = new List<Entity>();

        public DialogueImporter.SceneDialogue Dialogue;

        public static void Register(Entity entity)
        {
            s_Entities.Add(entity);
        }

        void Start()
        {
            var player = GameObject.FindWithTag("Player");
            var dialoguePanel = GameObject.FindWithTag("DialoguePanel");

            var dialogueSystem = new DialogueSystem(dialoguePanel);
            foreach (var entity in s_Entities)
            {
                switch (entity.Tag)
                {
                    case WorldInteraction.TagString:
                        ((WorldInteraction) entity).playerAgent =
                            player.GetComponent<NavMeshAgent>();
                        break;
                    case NPCDialogue.TagString:
                        ((NPCDialogue)entity).DialogueSystem = dialogueSystem;
                        ((NPCDialogue)entity).SceneDialogue = Dialogue;
                        break;
                    default:
                        Debug.LogError("Unknown entity: " + entity.Tag);
                        break;
                }
            }
        }
    }

    public interface Entity
    {
        string Tag { get; }
    }
}