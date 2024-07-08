using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private Text InteractionHint;
    [SerializeField] private Text dialogText;
    [SerializeField] private GameObject dialogPanel;
    private bool isPlayerInRange = false;
    private bool isDialogActive = false;
    private int currrentDialogIndex = 0;

    private string[] dialogLines = { 
        "这里都是敌人",
        "打败他们，拯救城市吧"
    };

    private void Start()
    {
        if (InteractionHint != null)
        {
            InteractionHint.enabled = false;
        }
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
        if (dialogText != null)
        {
            dialogText.text = "";
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            HideInteractionHint();  //make sure that interaction hint disappears when dialogs begin and last.
            if (!isDialogActive)
            {
                StartDialog();
            }
            else
            {
                DisplayNextDialogLine();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            ShowInteractionHint();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            HideInteractionHint();
            EndDialog();
        }
    }

    void ShowInteractionHint()
    {
        if (InteractionHint != null)
        {
            InteractionHint.enabled = true;
        }
    }

    void HideInteractionHint()
    {
        if (InteractionHint != null)
        {
            InteractionHint.enabled = false;
        }
    }

    void StartDialog()
    {
        isDialogActive = true;
        dialogPanel.SetActive(true);
        currrentDialogIndex = 0;
        dialogText.text = dialogLines[currrentDialogIndex];
    }

    void DisplayNextDialogLine()
    {
        currrentDialogIndex++;
        if (currrentDialogIndex < dialogLines.Length)
        {
            dialogText.text = dialogLines[currrentDialogIndex];
        }
        else
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        isDialogActive = false;
        dialogPanel.SetActive(false);
        dialogText.text = "";
    }
}
