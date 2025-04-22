using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;

    // Dictionary to store dialogue progress for each NPC
    private Dictionary<string, int> npcDialogueStates = new Dictionary<string, int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want this across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Get the current dialogue index (0 = first dialogue, etc.)
    public int GetDialogueState(string npcID)
    {
        return npcDialogueStates.ContainsKey(npcID) ? npcDialogueStates[npcID] : 0;
    }

    // Advance to the next dialogue state for an NPC
    public void AdvanceDialogueState(string npcID)
    {
        Debug.Log(npcID + GetDialogueState(npcID));
        if (!npcDialogueStates.ContainsKey(npcID))
        {
            npcDialogueStates[npcID] = 1;
        }
        else
        {
            npcDialogueStates[npcID]++;
        }
    }

    public bool DialogueStateEquals(string npcID, string npcID2, int targetState)
    {
        if (npcID == npcID2)
        {
            return GetDialogueState(npcID2) == targetState;
        }
        else return false;
    }

    public int CheckSpecificState(string npcID)
    {
        if (npcID == "Hest" && GetDialogueState(npcID) == 1) return 1;
        else if (npcID == "Hest" && GetDialogueState(npcID) == 5) return 2;
        else if (npcID == "Interaction1" && GetDialogueState(npcID) == 3 && GetDialogueState("Hest") == 1) { AdvanceDialogueState("Hest"); return 0; }
        else return 0;
    }
}