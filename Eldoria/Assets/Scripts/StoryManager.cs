using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;

    public QuestManager questManager;

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

    void Start()
    {
        questManager.AddQuest("Find the Ancient Sword", QuestManager.QuestType.Main);
        questManager.AddQuest("Collect 5 Herbs", QuestManager.QuestType.Side);
    }

    // Get the current dialogue index (0 = first dialogue, etc.)
    public int GetDialogueState(string npcID)
    {
        return npcDialogueStates.ContainsKey(npcID) ? npcDialogueStates[npcID] : 0;
    }

    // Advance to the next dialogue state for an NPC
    public void AdvanceDialogueState(string npcID)
    {
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

    public int CheckSpecificStateResponse(string npcID, int index)
    {
        if (npcID == "Hest" && GetDialogueState(npcID) == 0 && index == 0) return 5;
        else return 0;
    }
}