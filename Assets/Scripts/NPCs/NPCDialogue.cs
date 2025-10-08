using UnityEngine;

public class NPCDialogue: NPC, ITalkable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController controller;
    private bool isFirstLine = true;
    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        if (isFirstLine)
        {
            // playsound
            isFirstLine = false;
        } 
        bool conversationEnded =  controller.DisplayNextParagraph(dialogueText);
        if (conversationEnded)
        {
            isFirstLine = true;
        }
    }
}
