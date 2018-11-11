using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    private Round m_GameRound;
    [SerializeField]
    private App app;
    [SerializeField]
    private int m_MaxNumberOfRounds;
    [SerializeField]
    private Island Island;

    [SerializeField]
    private Text m_AnnouncementTextBox;

    [SerializeField] private ObstacleSpawner m_ObstacleSpawner;
    [SerializeField] private UIController m_UIController;

    [SerializeField]
    private GameObject m_StoreUI;
    [SerializeField]
    private CloseButton m_CloseButton;

    private bool isStoreClosed = true;

    private List<Unit> m_AllUnits = new List<Unit>();
    private Dictionary<Unit, Vector3> m_PositionsList = new Dictionary<Unit, Vector3>();
    private int m_RoundNumber = 1;
    private int m_PlayerWins = 0;
    private int m_AIWins = 0;
    private int m_PlayerDeaths = 0;
    private int m_AIDeaths = 0;
    private bool m_isMainMenuCalled = false;
    private void Start()
    {
        GlobalCooldowns.Start();
        m_StoreUI.SetActive(true);
        m_AllUnits = new List<Unit>(GameObject.FindObjectsOfType<Unit>());
        foreach (var unit in m_AllUnits)
        {
            m_PositionsList.Add(unit, unit.transform.position);
        }

        foreach (var mAllUnit in m_AllUnits)
        {
            if (mAllUnit.Owner.IsLocal)
            {
                m_StoreUI.GetComponent<StoreController>().UpdateStore(mAllUnit);
            }
        }
        StartCoroutine(WaitForStoreClose());
        m_GameRound = new Round();
        m_ObstacleSpawner.SpawnObstacles();
        StartCoroutine(AnnouncementCoRoutine());
        m_GameRound.StartRound();
    }
    private void Update()
    {
        if (isStoreClosed)
        {
            m_GameRound.Update();
            if ((m_GameRound.Winner != null) && (m_RoundNumber != m_MaxNumberOfRounds))
            {
                NewRound();
            }

            if (m_RoundNumber == m_MaxNumberOfRounds)
            {
                if (!m_isMainMenuCalled)
                {
                    app.Scenes.GoToMainMenu();
                    m_isMainMenuCalled = true;
                }

            }
        }
    }

    private void NewRound()
    {
        if (m_GameRound.Winner.IsLocal)
        {
            m_PlayerWins++;
            m_AIDeaths--;
        }
        else
        {
            m_AIWins++;
            m_PlayerDeaths--;
        }
        StartCoroutine(RoundEndCoroutine());
        foreach (var unit in m_AllUnits)
        {
            unit.transform.position = m_PositionsList[unit]; 
            unit.Controller.StopAll();
        }
        m_GameRound = new Round();
        m_GameRound.StartRound();
        m_StoreUI.SetActive(true);
        StartCoroutine(WaitForStoreClose());
        Island.ResetIsland();
        m_ObstacleSpawner.SpawnObstacles();
        m_RoundNumber++;
    }

    public List<int> GetWins()
    {
        List<int> m_ReturnList = new List<int>();
        m_ReturnList.Add(m_PlayerWins);
        m_ReturnList.Add(m_AIWins);
        return m_ReturnList;
    }

    public List<int> GetDeaths()
    {
        List<int> m_ReturnList = new List<int>();
        m_ReturnList.Add(m_PlayerDeaths);
        m_ReturnList.Add(m_AIDeaths);
        return m_ReturnList;
    }
    IEnumerator RoundEndCoroutine()
    {
        m_UIController.RoundEnd(GetWins(), GetDeaths(), m_AllUnits);
        yield return new WaitForSeconds(2);
        StartCoroutine(AnnouncementCoRoutine());
    }

    IEnumerator WaitForStoreClose()
    {
        isStoreClosed = false;
        yield return StartCoroutine(m_CloseButton.WaitForClose());
        isStoreClosed = true;
        m_StoreUI.SetActive(false);
    }

    IEnumerator AnnouncementCoRoutine()
    {
        m_UIController.UpdateRound(m_RoundNumber);
        yield return new WaitForSeconds(2);
        m_UIController.HideAnnouncement();
    }

    public void WakeUnits()
    {
        foreach (var mAllUnit in m_AllUnits)
        {
            mAllUnit.GetComponent<Rigidbody>().WakeUp();
        }
    }
}
