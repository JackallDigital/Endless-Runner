using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController playerControllerScript;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI textStartCounter;
    
    private ParticleSystem pickupParticles;

    public float score;
    float scoreSpeedMultiplication;
    float pointIncreasePerSecond;


    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        pickupParticles = GameObject.Find("FX_Fireworks_Yellow_Small").GetComponent<ParticleSystem>();

        score = 0f;
        pointIncreasePerSecond = 12f;
    }

    // Update is called once per frame
    void Update()
    {
        if( playerControllerScript.GetComponent<Animator>().speed > 1){
            scoreSpeedMultiplication = 2;
        }
        else {
            scoreSpeedMultiplication = 1;
        }

        if(!playerControllerScript.gameOver)
            score += pointIncreasePerSecond * scoreSpeedMultiplication * Time.deltaTime;

        //Debug.Log("score: " + score + ", score multi: " + scoreSpeedMultiplication + ", points ps: " + pointIncreasePerSecond);

        textScore.SetText("SCORE: " + (int)score);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Money")) {
            Destroy(collision.gameObject);
            score += 50;
            
            textScore.SetText("SCORE: " + (int)score);
        }

        if (collision.gameObject.CompareTag("Obstacle")) {
            StartCoroutine(OnGameOver());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Money")) {
            pickupParticles.Play();

            Destroy(other.gameObject);
            score += 50;
            textScore.SetText("SCORE: " + (int)score);
        }
    }


    IEnumerator OnGameOver() {
        int timer = 3;
        textStartCounter.gameObject.SetActive(true);

        for(int i = timer; i > 0; i--) {
            textStartCounter.SetText("START IN " +i+"..");
            yield return new WaitForSeconds(1);
        }
        textStartCounter.SetText("GO");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(0);
        textStartCounter.gameObject.SetActive(false);
    }
}
