using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;

    public QuestManager questManager;

    private bool LinwenLost = false;

    public GameObject wallToRemove;

    public int Mushrooms = 0;

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

    public void SetDialogueState(string npcID, int newState)
    {
        if (npcDialogueStates.ContainsKey(npcID))
        {
            npcDialogueStates[npcID] = newState;
        }
        else
        {
            npcDialogueStates.Add(npcID, newState);
        }
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

    public void CheckMushrooms()
    {
        if (Mushrooms == 3) AdvanceDialogueState("Linwen");
    }

    public int CheckSpecificState(string npcID)
    {
        if (npcID == "Hest" && GetDialogueState(npcID) == 1) return 1;
        else if (npcID == "Hest" && GetDialogueState(npcID) == 5) return 2;
        else if (npcID == "Interaction1" && GetDialogueState(npcID) == 3 && GetDialogueState("Hest") == 1) { AdvanceDialogueState("Hest"); return 0; }
        else if (npcID == "Linwen" && GetDialogueState(npcID) == 3) return 3;
        else if (npcID == "Linwen" && GetDialogueState(npcID) == 9 && LinwenLost) return 1;
        else if (npcID == "Linwen" && GetDialogueState(npcID) == 15 && LinwenLost) return 1;
        else if (npcID == "Linwen" && GetDialogueState(npcID) == 16) { wallToRemove.transform.position = new Vector3(-5000, -5000, 0); return 0; }
        else if (npcID == "Linwen" && GetDialogueState(npcID) == 17) return 1;
        else if (npcID == "Linwen" && GetDialogueState(npcID) == 18) return 1;
        else if (npcID == "Linwen" && GetDialogueState(npcID) == 20) return 2;
        else return 0;
    }

    public int CheckSpecificStateResponse(string npcID, int index)
    {
        if (npcID == "Linwen" && GetDialogueState(npcID) == 0 && index == 0) return 0;
        if (npcID == "Linwen" && GetDialogueState(npcID) == 0 && index == 1) { SetDialogueState("Linwen",1); return 1; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 0 && index == 2) { SetDialogueState("Linwen",2); return 2; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 1 && index == 0) { SetDialogueState("Linwen",4); return 3; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 2 && index == 0) { SetDialogueState("Linwen",4); return 3; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 5 && index == 0) { SetDialogueState("Linwen",5); return 4; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 5 && index == 1) { SetDialogueState("Linwen",6); return 5; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 5 && index == 2) { SetDialogueState("Linwen", 8); LinwenLost = true; return 7; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 6 && index == 0) { SetDialogueState("Linwen",9); return 9; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 8 && index == 0) { SetDialogueState("Linwen", 9); return 9; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 10 && index == 0) { SetDialogueState("Linwen", 11); return 10; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 10 && index == 1) { SetDialogueState("Linwen", 12); return 11; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 10 && index == 2) { SetDialogueState("Linwen", 14); LinwenLost = true; return 13; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 12 && index == 0) { SetDialogueState("Linwen", 15); return 14; }
        if (npcID == "Linwen" && GetDialogueState(npcID) == 14 && index == 0) { SetDialogueState("Linwen", 15); return 14; }

        else return 0;
    }
}