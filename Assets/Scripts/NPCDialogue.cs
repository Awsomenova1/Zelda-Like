using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogue; 
    [SerializeField] private TMP_Text dialogueText;
    private bool _npcNearby;
    private int _currPage;
    private int _maxPages;
    private bool _isShowingDialogue;

    public void Start()
    {
        dialogueText = dialogue.GetComponentInChildren<TMP_Text>();
        dialogue.SetActive(false);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        // Only run when the key is pressed (not released)
        if (!context.started) return;
         
        if (_npcNearby && (!_isShowingDialogue)) 
        {
            DisplayDialogue();
        }
        // If there's no more pages of dialogue 
        else if (_currPage == _maxPages)
        {
            dialogueText.pageToDisplay = 1;
            _isShowingDialogue = false;
            dialogue.SetActive(false);
        }
        // Display the next page of dialogue
        else
        {
            _currPage++;
            dialogueText.pageToDisplay = _currPage;
        }
    }
    
    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
        _npcNearby = true;
    }

    public void RemoveDialogue()
    {
        _npcNearby = false;
    }

    private void DisplayDialogue()
    {
        // show the dialogue GUI
        dialogue.SetActive(true); 
        // textInfo is only regenerated when the mesh updates 
        dialogueText.ForceMeshUpdate();
        _maxPages = dialogueText.textInfo.pageCount;
        _currPage = 1;
        _isShowingDialogue = true;
    }
}