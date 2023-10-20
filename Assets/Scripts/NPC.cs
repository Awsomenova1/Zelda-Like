using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCDialogue _npcDialogue;
    [SerializeField] private string npcText;

    private void Start()
    {
        _npcDialogue = GameObject.Find("Player").GetComponent<NPCDialogue>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _npcDialogue.SetDialogueText(npcText);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       _npcDialogue.RemoveDialogue(); 
    }
}
