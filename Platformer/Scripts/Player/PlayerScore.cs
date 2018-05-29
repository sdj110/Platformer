using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    // Attributes
    [SerializeField] Text m_ScoreText;
    int m_Score;

    public void UpdateScore(int value)
    {
        m_Score += value;
        m_ScoreText.text = "SCORE: " + m_Score;
    }

    public void ResetScore()
    {
        m_Score = 0;
        m_ScoreText.text = "SCORE: " + m_Score;
    }
}
