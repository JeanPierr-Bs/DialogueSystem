using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.1f;
    int index;

    private InputSystem_Actions inputActions;
    private bool enterPressed = false;

    private void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();

        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        inputActions.Player.Attack.performed += ctx => enterPressed = true;
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    void Update()
    {
        if (enterPressed)
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
            enterPressed = false;
        }
    }
    
    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
