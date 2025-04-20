using UnityEngine;

public class SelfDestructOnSpecificCollision : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == targetObject && StoryManager.instance.DialogueStateEquals("Test1", "Test1", 1))
        {
            StoryManager.instance.AdvanceDialogueState("Test1");
            Destroy(gameObject);
        }
    }
}
