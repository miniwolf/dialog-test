using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DialogueSystem : Entity
    {
        private string[] dialogueLines;
        private string speaker;
        private int index;
        private readonly GameObject dialoguePanel;
        private readonly Text dialogueText;
        private readonly Text nameText;

        public string Tag => "DialogueSystem";

        public DialogueSystem(GameObject dialoguePanel)
        {
            this.dialoguePanel = dialoguePanel;
            var continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
            continueButton.onClick.AddListener(ContinueDialog);

            dialogueText = dialoguePanel.transform.Find("Text").GetComponent<Text>();
            nameText = dialoguePanel.transform.Find("Name").GetComponent<Text>();
            dialoguePanel.SetActive(false);
        }

        private void ContinueDialog()
        {
            if (index < dialogueLines.Length - 1)
            {
                dialogueText.text = dialogueLines[++index];
            }
            else
            {
                dialoguePanel.SetActive(false);
            }
        }

        public void AddNewDialogue(string[] lines, string speaker)
        {
            dialogueLines = lines;
            index = 0;
            this.speaker = speaker;
            CreateDialogue();
        }

        private void CreateDialogue()
        {
            if (dialogueLines.Length <= index)
            {
                return;
            }
            dialogueText.text = dialogueLines[index];
            nameText.text = speaker;
            dialoguePanel.SetActive(true);
        }
    }
}