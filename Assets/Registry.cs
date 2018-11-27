using System.Collections.Generic;
using Interactions;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class Registry : MonoBehaviour
    {
        private static readonly List<Entity> s_Entities = new List<Entity>();

        public static void Register(Entity entity)
        {
            s_Entities.Add(entity);
        }

        void Start()
        {
            var player = GameObject.FindWithTag("Player");
            foreach (var entity in s_Entities)
            {
                switch (entity.Tag)
                {
                    case WorldInteractionSystem.TagString:
                        ((WorldInteractionSystem) entity).playerAgent =
                            player.GetComponent<NavMeshAgent>();
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