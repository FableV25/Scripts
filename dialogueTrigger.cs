using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : singleton<DialogueTrigger>
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public GameObject contButton;
    public Text dialogueText;
    public Text nameText;
    public Image characterImage;

    [Header("Dialogue Settings")]
    public float wordSpeed;
    public bool isClose;
    public bool isTyping = false; // Track if we're currently typing

    [System.Serializable] public class DialogueEntry
    {
        [TextArea(3, 10)]
        public string dialogueText;
        public string characterName;
        public Sprite characterPicture;
    }

    public DialogueEntry[] dialogueLines;
    private int currentIndex;
    private int savedProgressIndex = 0;
    private Coroutine typingCoroutine; // Reference to the typing coroutine

    protected override void Awake()
    {
        base.Awake();
        // Initialize any necessary components here
    }
        
    void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) && isClose)
            {
                if(dialoguePanel.activeInHierarchy)
                {
                    SaveDialogueProgress();
                    ResetDialogueUI();
                }
                else 
                {
                    StartDialogue();
                }
            }

            if (!isTyping && dialogueText.text == dialogueLines[currentIndex].dialogueText)
            {
                contButton.SetActive(true);
            }
        }

    void StartDialogue()
    {
         // Stop any existing typing coroutine
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialoguePanel.SetActive(true);
        currentIndex = savedProgressIndex;
        dialogueText.text = ""; // Clear text before starting
        UpdateCharacterInfo();
        isTyping = true;
        typingCoroutine = StartCoroutine(TypeText());
    }

    void ResetDialogueUI()
    {
        // Stop any existing typing coroutine
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
        }

        dialogueText.text = "";
        nameText.text = "";
        characterImage.sprite = null;
        dialoguePanel.SetActive(false);
    }

    void SaveDialogueProgress()
    {
        savedProgressIndex = currentIndex;
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        dialogueText.text = ""; // Ensure text is clear before typing

        foreach(char letter in dialogueLines[currentIndex].dialogueText.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed); 
        }

        isTyping = false;
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (currentIndex < dialogueLines.Length - 1)
        {
            currentIndex++;
            dialogueText.text = "";
            UpdateCharacterInfo();
            StartCoroutine(TypeText());
        }
        else 
        {
            savedProgressIndex = 0;
            ResetDialogueUI();
        }
    }

    void UpdateCharacterInfo()
    {
        nameText.text = string.IsNullOrEmpty(dialogueLines[currentIndex].characterName) ? 
            "" : dialogueLines[currentIndex].characterName;

        characterImage.sprite = dialogueLines[currentIndex].characterPicture;
        characterImage.gameObject.SetActive(characterImage.sprite != null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isClose = false;
            SaveDialogueProgress(); // Save when walking away
            ResetDialogueUI();
        }
    }

    // Call this when you want to completely reset the dialogue progress
    public void ResetDialogueProgress()
    {
        savedProgressIndex = 0;
        currentIndex = 0;
    }
}