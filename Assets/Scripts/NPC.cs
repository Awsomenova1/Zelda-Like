using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCDialogue npcDialogue;
    [SerializeField] private string npcText;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            npcDialogue.SetDialogueText(npcText);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       npcDialogue.RemoveDialogue(); 
    }
}