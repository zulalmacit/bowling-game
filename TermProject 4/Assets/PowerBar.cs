using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Image powerBarImage; // Power bar görüntüsü
    public Rigidbody bowlingBall; // Bowling topu
    public Slider directionSlider; // Yön slider'ı
    public float powerBarSpeed = 1.0f; // Power bar hız ayarı
    public float maxForce = 20.0f; // Maksimum kuvvet

    private bool increasing = true; // Power bar artış durumu
    private float powerValue = 0.0f; // Power bar değeri
    private bool gameStarted = false; // Oyun başladı mı?

    void Start()
    {
        // Debug.Log ile referansların atandığını kontrol edin
        if (powerBarImage == null)
        {
            Debug.LogError("Power Bar Image referansı atanmadı!");
        }
        if (bowlingBall == null)
        {
            Debug.LogError("Bowling Ball referansı atanmadı!");
        }
        if (directionSlider == null)
        {
            Debug.LogError("Direction Slider referansı atanmadı!");
        }

        // Oyun başladığında power bar'ı başlat
        StartGame();
    }

    void Update()
    {
        // Power bar'ı hareket ettir sadece oyun başladıysa
        if (gameStarted)
        {
            if (increasing)
            {
                powerValue += powerBarSpeed * Time.deltaTime;
                if (powerValue >= 1.0f)
                {
                    powerValue = 1.0f;
                    increasing = false;
                    Debug.Log("Power bar azalmaya başladı.");
                }
            }
            else
            {
                powerValue -= powerBarSpeed * Time.deltaTime;
                if (powerValue <= 0.0f)
                {
                    powerValue = 0.0f;
                    increasing = true;
                    Debug.Log("Power bar artmaya başladı.");
                }
            }

            // Power bar'ı güncelle
            if (powerBarImage != null)
            {
                powerBarImage.fillAmount = powerValue;
                Debug.Log("Power bar fill amount: " + powerValue);
            }

            // Tıklama kontrolü ile topu fırlat
            if (Input.GetMouseButtonUp(0))
            {
                ThrowBall();
            }
        }
    }

    public void StartGame()
    {
        // Power bar görüntüsünü başlat
        powerBarImage.fillAmount = 0.0f;
        gameStarted = true;
        Debug.Log("Oyun başladı! Power bar fill amount sıfırlandı.");
    }

    public void ThrowBall()
    {
        if (bowlingBall != null)
        {
            // Slider değerini al
            float direction = directionSlider.value;

            // Kuvvet vektörünü ayarla
            Vector3 forceDirection = new Vector3(direction, 0, 1).normalized;
            Vector3 force = forceDirection * powerValue * maxForce;

            // Kuvveti topa uygula
            bowlingBall.AddForce(force, ForceMode.Force);

            Debug.Log("Top fırlatıldı! Yön: " + direction + " Kuvvet: " + powerValue * maxForce);
        }
    }
}
