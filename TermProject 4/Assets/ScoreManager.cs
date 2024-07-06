using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text currentTurnText;
    public Text winnerText;

    private int[] player1Scores = new int[10];
    private int[] player2Scores = new int[10];
    private int currentTurn = 0;
    private int currentPlayer = 1;
    private int throwCount = 0;

    void Start()
    {
        UpdateUI();
    }

    public void OnBallThrown(int score)
    {
        if (currentPlayer == 1)
        {
            player1Scores[currentTurn] += score;
        }
        else
        {
            player2Scores[currentTurn] += score;
        }

        throwCount++;

        if (throwCount >= 2)
        {
            throwCount = 0;
            currentPlayer = (currentPlayer == 1) ? 2 : 1;

            if (currentPlayer == 1)
            {
                currentTurn++;
            }

            if (currentTurn >= 10)
            {
                EndGame();
                return;
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        player1ScoreText.text = "Player 1 Score: " + GetTotalScore(player1Scores);
        player2ScoreText.text = "Player 2 Score: " + GetTotalScore(player2Scores);
        currentTurnText.text = "Turn: " + (currentTurn + 1) + " Player: " + currentPlayer;
    }

    int GetTotalScore(int[] scores)
    {
        int total = 0;
        foreach (int score in scores)
        {
            total += score;
        }
        return total;
    }

    void EndGame()
    {
        int player1TotalScore = GetTotalScore(player1Scores);
        int player2TotalScore = GetTotalScore(player2Scores);

        if (player1TotalScore > player2TotalScore)
        {
            winnerText.text = "Player 1 Wins!";
        }
        else if (player1TotalScore < player2TotalScore)
        {
            winnerText.text = "Player 2 Wins!";
        }
        else
        {
            winnerText.text = "It's a Draw!";
        }
    }
}

