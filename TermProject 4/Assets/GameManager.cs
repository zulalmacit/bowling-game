using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel; // Başlangıç ekranı paneli
    public Button startButton; // Başlangıç butonu
    public PowerBar powerBar; // Güç çubuğu scripti
    public GameObject pins; // Pinlerin referansı

    private bool gameStarted = false; // Oyunun başladığını belirten değişken

    void Start()
    {
        // Başlangıç ekranını göster
        startPanel.SetActive(true);

        // Butonun onClick event'ine StartGame fonksiyonunu ekle
        startButton.onClick.AddListener(StartGame);

        // Pinlerin Rigidbody bileşenlerini ekleyin
        Rigidbody[] pinRigidbodies = pins.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in pinRigidbodies)
        {
            rb.isKinematic = true; // Kinematic olarak ayarla (kod tarafından kontrol edilebilir olacak)
        }
    }

    void Update()
    {
        // Oyun başladıktan sonra ve ikinci fare tıklamasında topu fırlat
        if (gameStarted && Input.GetMouseButtonUp(1))
        {
            powerBar.ThrowBall();
        }
    }

    void StartGame()
    {
        // Başlangıç ekranını gizle
        startPanel.SetActive(false);

        // Oyunu başlat
        gameStarted = true;
        Debug.Log("Oyun başladı!");

        // Pinlerin kinematik özelliklerini kaldır
        Rigidbody[] pinRigidbodies = pins.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in pinRigidbodies)
        {
            rb.isKinematic = false;
        }
    }
}
