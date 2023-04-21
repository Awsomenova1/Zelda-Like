using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    private bool _npcNearby;
    private bool _isShowingDialogue;

    public void Interact(InputAction.CallbackContext context)
    {
        if (_npcNearby && (!_isShowingDialogue)) 
        {
            DisplayDialogue();
        }
        else
        {
            _isShowingDialogue = false;
            gameObject.SetActive(false);
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
        _isShowingDialogue = true;
        gameObject.SetActive(true); 
    }
}