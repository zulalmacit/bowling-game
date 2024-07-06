using UnityEngine;

public class BowlingGameManager : MonoBehaviour
{
    public GameObject ball; // Topun referansı
    public GameObject pins; // Pinlerin referansı
    public string pinTag = "Pin"; // Pinlerin tagı
    private Vector3 initialBallPosition; // Topun ilk pozisyonu
    private Vector3[] initialPinsPositions; // Pinlerin başlangıç pozisyonu
    private Quaternion[] initialPinsRotations; // Pinlerin başlangıç rotasyonu
    private int throwCount = 0; // Atış sayacı
    private bool isPlayer1Turn = true; // Oyuncu sırasını takip etmek için

    public PowerBar powerBar; // PowerBar referansı

    void Start()
    {
        // Topun başlangıç pozisyonunu kaydet
        initialBallPosition = ball.transform.position;

        // Pinlerin başlangıç pozisyonlarını ve rotasyonlarını kaydet
        initialPinsPositions = new Vector3[pins.transform.childCount];
        initialPinsRotations = new Quaternion[pins.transform.childCount];

        for (int i = 0; i < pins.transform.childCount; i++)
        {
            initialPinsPositions[i] = pins.transform.GetChild(i).position;
            initialPinsRotations[i] = pins.transform.GetChild(i).rotation;
        }
    }

    void Update()
    {
        // Oyuncunun atış yapıp yapmadığını kontrol et
        if (Input.GetMouseButtonUp(0))
        {
            // Atış yapıldığında sayaç artırılır
            throwCount++;
            if (throwCount >= 2)
            {
                // İki atış yapıldığında oyunu sıfırla
                ResetGame();
            }
            else
            {
                // Topu başlangıç pozisyonuna geri döndür
                ResetBall();

                // Atışı yap
                ThrowBall();

                // Oyuncu sırasını değiştir
                isPlayer1Turn = !isPlayer1Turn;
            }
        }
    }

    void ResetBall()
    {
        // Topu başlangıç pozisyonuna geri döndür
        ball.transform.position = initialBallPosition;
        // Topun hızını ve dönüşünü sıfırla
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void ResetPins()
    {
        // Pinleri başlangıç pozisyonuna ve rotasyonuna geri döndür
        for (int i = 0; i < pins.transform.childCount; i++)
        {
            pins.transform.GetChild(i).position = initialPinsPositions[i];
            pins.transform.GetChild(i).rotation = initialPinsRotations[i];

            // Pinlerin hızını ve dönüşünü sıfırla
            Rigidbody pinRigidbody = pins.transform.GetChild(i).GetComponent<Rigidbody>();
            if (pinRigidbody != null)
            {
                pinRigidbody.velocity = Vector3.zero;
                pinRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }

    void ResetGame()
    {
        ResetPins();
        ResetBall();

        // Atış sayacını sıfırla
        throwCount = 0;
    }

    void ThrowBall()
    {
        // Throw the ball with power and direction from PowerBar script
        float powerValue = powerBar.powerBarImage.fillAmount;
        float direction = powerBar.directionSlider.value;

        // Kuvvet vektörünü ayarla
        Vector3 forceDirection = new Vector3(direction, 0, 1).normalized;
        Vector3 force = forceDirection * powerValue * powerBar.maxForce;

        // Kuvveti topa uygula
        ball.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        Debug.Log("Top fırlatıldı! Yön: " + direction + " Kuvvet: " + powerValue);
    }
}
