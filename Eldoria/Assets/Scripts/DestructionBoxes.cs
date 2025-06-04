using UnityEngine;

public class SelfDestructOnProximityBoxes : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float proximityDistance = 5f;

    private bool hasTriggered = false;

    void Update()
    {
        if (hasTriggered || targetObject == null)
            return;

        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        if (distance <= proximityDistance)
        {
            Destroy(gameObject);
            hasTriggered = true;
            StoryManager.instance.Boxes++;
            Debug.Log("COLLISION: " + StoryManager.instance.Boxes);
            StoryManager.instance.CheckBoxes();
        }
    }
}