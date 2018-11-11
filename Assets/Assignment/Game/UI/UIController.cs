using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //This controls all the UI of the game
    [SerializeField]
    private Text m_AnnouncementTextBox;

    [SerializeField] private Round m_Round;

    [SerializeField] private GameObject m_AnnouncementPanel;

    [SerializeField] private GameObject m_UnitTextBox;

    private List<Text> m_allTextBoxes;

    private void Start()
    {
        m_allTextBoxes = new List<Text>();
    }
    public void UpdateRound(int m_RoundNumber)
    {
        m_AnnouncementPanel.SetActive(true);
        m_AnnouncementTextBox.enabled = true;
        m_AnnouncementTextBox.text = "Round : " + m_RoundNumber + "FIGHT!!!!";
    }

    //Gets called at round end to show the round end panel
    public void RoundEnd(List<int> m_Wins, List<int> m_Deaths, List<Unit> m_allUnits)
    {
        m_AnnouncementPanel.SetActive(true);
        for (int i = 0; i < m_allUnits.Count; i++)
        {
            GameObject go = Instantiate(m_UnitTextBox).gameObject;
            go.transform.SetParent(m_AnnouncementPanel.transform);
            go.transform.localScale = new Vector3(1, 1, 1);

            Text t = go.GetComponent<Text>();
            t.rectTransform.localPosition = new Vector3(20 * (i +1), 20 * (i + 1), 0);
            m_allTextBoxes.Add(t);
            t.text = m_allUnits[i].Name + "wins : " + m_Wins[i] + " " + "Deaths : " + m_Deaths[i];
        }
    }

    public void HideAnnouncement()
    {
        m_AnnouncementPanel.SetActive(false);
        m_AnnouncementTextBox.enabled = false;
        if (m_allTextBoxes.Count > 0)
        {
            foreach (var mAllTextBox in m_allTextBoxes)
            {
                Destroy(mAllTextBox.gameObject);
            }

            m_allTextBoxes.Clear();
        }
    }

}
