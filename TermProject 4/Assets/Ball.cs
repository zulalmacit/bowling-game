using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public ScoreManager scoreManager;
    public string pinTag = "Pin"; // Ensure your pins have this tag
    public float standingThreshold = 15f; // Threshold to determine if a pin is standing
    public float stopThreshold = 0.1f; // Threshold to consider the ball stopped
    public float waitTime = 10f; // Time to wait after the ball stops

    private Rigidbody rb;
    private bool isScoring = false;
    private bool ballStopped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Assign the ScoreManager (ensure it's assigned properly)
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }
    }

    void Update()
    {
        // Check if the ball has stopped
        if (!ballStopped && rb.velocity.magnitude < stopThreshold)
        {
            ballStopped = true;
            StartCoroutine(WaitAndCalculateScore());
        }
    }

    IEnumerator WaitAndCalculateScore()
    {
        // Wait for a few seconds to ensure all pins have settled
        yield return new WaitForSeconds(waitTime);

        // Calculate the score
        int score = CalculateScore();
        scoreManager.OnBallThrown(score);

        // Reset for the next throw
        ballStopped = false;
    }

    int CalculateScore()
    {
        GameObject[] pins = GameObject.FindGameObjectsWithTag(pinTag);
        int score = 0;

        foreach (GameObject pin in pins)
        {
            if (IsPinKnockedDown(pin))
            {
                score++;
            }
        }

        return score;
    }

    bool IsPinKnockedDown(GameObject pin)
    {
        // Check if the pin is standing by comparing its up vector with the world's up vector
        return Vector3.Angle(pin.transform.up, Vector3.up) > standingThreshold;
    }

    // Optional: Reset the ball's velocity if it moves unexpectedly
    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }
}
