using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Interactions
{
    public class WorldInteraction : MonoBehaviour, Entity
    {
        public NavMeshAgent playerAgent { private get; set; }

        private void Awake()
        {
            Registry.Register(this);
        }

        private void Update()
        {
            var isButtonDown = Input.GetMouseButtonDown(0);
            var isOverGameObject = EventSystem.current.IsPointerOverGameObject();
            if (isButtonDown && !isOverGameObject)
            {
                var interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(interactionRay, out var interactionInfo, Mathf.Infinity))
                {
                    return;
                }

                GetInteraction(playerAgent, interactionInfo);
            }
        }

        private static void GetInteraction(NavMeshAgent agent, RaycastHit interactionInfo)
        {
            var interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.CompareTag("Interactable Object"))
            {
                interactedObject.GetComponent<Interactable>().Execute();
            }
            else if (interactedObject.CompareTag("Moveable area"))
            {
                agent.destination = interactionInfo.point;
            }
        }

        public const string TagString = "WorldInteraction";

        public string Tag => TagString;
    }
}