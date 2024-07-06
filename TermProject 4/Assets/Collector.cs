using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    public Text scoreText; // Skorun gösterileceği UI Text

    private int score = 0; // Toplanan nesnelerin sayısını tutar

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score Text referansı atanmadı!");
        }
        else
        {
            UpdateScoreText();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Toplanabilir nesneyle çarpışma kontrolü
        if (other.gameObject.CompareTag("Collectible"))
        {
            // Nesneyi yok et
            Destroy(other.gameObject);

            // Skoru artır
            score++;

            // Skor bilgisini güncelle
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}