using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCDialogue _npcDialogue;
    [SerializeField] private string _npcText;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _npcDialogue.SetDialogueText(_npcText);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       _npcDialogue.RemoveDialogue(); 
    }
}