using UnityEngine;
using UnityEngine.UI;

public class WinZoneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (StoryManager.instance.GetDialogueState("Linwen") >= 20)
            {
                ShowWinScreen();
            }
            else
            {
                Debug.Log("Cannot advance yet");
            }
        }
    }

    private void ShowWinScreen()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
            Debug.Log("You Win!");
        }
        else
        {
            Debug.LogWarning("Win screen UI is not assigned.");
        }
    }
}
