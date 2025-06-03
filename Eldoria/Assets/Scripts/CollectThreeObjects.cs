using UnityEngine;

public class CollectThreeObjects : MonoBehaviour
{
    [Tooltip("Assign the 3 objects to collect here")]
    public GameObject[] objectsToCollect = new GameObject[3];

    private int collectedCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");

        if (other.CompareTag("Player"))
        {
            // Check each object if the player collided with it
            for (int i = 0; i < objectsToCollect.Length; i++)
            {
                GameObject obj = objectsToCollect[i];
                if (obj != null && other.gameObject == obj)
                {
                    // Make object disappear
                    obj.SetActive(false);
                    objectsToCollect[i] = null;
                    collectedCount++;

                    Debug.Log("Collected object " + (i + 1));

                    // Check if all collected
                    if (collectedCount >= 3)
                    {
                        AllCollected();
                    }
                    break;
                }
            }
        }
    }

    private void AllCollected()
    {
        Debug.Log("All 3 objects collected! Doing something...");
        // TODO: Replace with your desired action, e.g.:
        // Open door, start quest, give reward, etc.
    }
}
