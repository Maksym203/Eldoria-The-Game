using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public enum QuestType { Main, Side }

    [SerializeField] private GameObject questPanel;
    [SerializeField] private Transform questListParent;
    [SerializeField] private GameObject questItemPrefab;
    [SerializeField] private Color mainQuestColor = Color.red;
    [SerializeField] private Color sideQuestColor = Color.blue;

    private bool isVisible = false;

    private class QuestData
    {
        public string name;
        public QuestType type;
        public GameObject uiElement;
    }

    private List<QuestData> activeQuests = new List<QuestData>();

    void Start()
    {
        questPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestPanel();
        }
    }

    private void ToggleQuestPanel()
    {
        isVisible = !isVisible;
        questPanel.SetActive(isVisible);
    }

    public void AddQuest(string questName, QuestType type)
    {
        GameObject questItem = Instantiate(questItemPrefab, questListParent);
        TMP_Text questText = questItem.transform.Find("QuestText").GetComponent<TMP_Text>();
        Image typeIcon = questItem.transform.Find("TypeIcon").GetComponent<Image>();

        questText.text = questName;
        typeIcon.color = (type == QuestType.Main) ? mainQuestColor : sideQuestColor;

        QuestData newQuest = new QuestData
        {
            name = questName,
            type = type,
            uiElement = questItem
        };

        activeQuests.Add(newQuest);
        SortQuests();
    }

    public void CompleteQuest(string questName)
    {
        QuestData quest = activeQuests.Find(q => q.name == questName);
        if (quest != null)
        {
            Destroy(quest.uiElement);
            activeQuests.Remove(quest);
        }
    }

    private void SortQuests()
    {
        activeQuests.Sort((a, b) =>
        {
            if (a.type != b.type)
                return a.type == QuestType.Main ? -1 : 1;
            return a.name.CompareTo(b.name);
        });

        for (int i = 0; i < activeQuests.Count; i++)
        {
            activeQuests[i].uiElement.transform.SetSiblingIndex(i);
        }
    }
}
