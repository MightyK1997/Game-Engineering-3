using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTextScript : MonoBehaviour {

    [SerializeField]
    private Text m_PlayerScoreText;

    [SerializeField]
    private Text m_AIScoreText;

    [SerializeField]
    private Game m_MainGame;

    List<int> m_ScoresList = new List<int>();


    void Update () {
         m_ScoresList = m_MainGame.GetWins();
        m_PlayerScoreText.text = "Wins : " + m_ScoresList[0];
        m_AIScoreText.text = "Wins : " + m_ScoresList[1];
	}
}
