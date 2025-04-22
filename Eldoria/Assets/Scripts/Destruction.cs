using UnityEngine;

public class SelfDestructOnSpecificCollision : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == targetObject && StoryManager.instance.DialogueStateEquals("Hest", "Hest", 1))
        {
            StoryManager.instance.AdvanceDialogueState("Hest");
            Destroy(gameObject);
        }
    }
}