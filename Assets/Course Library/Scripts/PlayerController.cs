using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEditorInternal;
using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private ParticleSystem deathParticles;
    private ParticleSystem dustParticles;

    private ButtonPressed buttonPressedScript;

    public AudioClip jumpSound;
    public AudioClip deathSound;
    [SerializeField] AudioClip moneyAudio;

    private AudioSource playerSourceSound;

    public float jumpForce = 10;
    private float gravityModifier = 18f;
    private bool isOnGround = true;
    private int doubleJumpCount = 0;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        deathParticles = GameObject.Find("FX_Explosion_Smoke").GetComponent<ParticleSystem>();
        dustParticles = GameObject.Find("FX_DirtSplatter").GetComponent<ParticleSystem>();
        playerSourceSound = GetComponent<AudioSource>();
        Physics.gravity = new Vector3(0, -gravityModifier, 0);

        buttonPressedScript= GameObject.Find("Button").GetComponent<ButtonPressed>();

        StartCoroutine(IntroPause());
    }

    IEnumerator IntroPause() {
        playerAnim.speed = .5f;
        yield return new WaitForSeconds(1.5f);
        playerAnim.speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!buttonPressedScript.buttonPressed) {
            if (!gameOver && Input.GetKeyDown(KeyCode.Space) && doubleJumpCount < 2) { //Input.GetKeyDown(KeyCode.Space)  //Fire1 is mouse click which is the same as a click on the phone //Input.GetButtonDown("Fire1") 

                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                doubleJumpCount++;
                playerAnim.SetTrigger("Jump_trig");
                dustParticles.Stop();
                playerSourceSound.PlayOneShot(jumpSound, 0.05f);

                //Debug.Log("double jump: "+ doubleJumpCount);
            }
       // }
        if (isOnGround == true) {
            doubleJumpCount = 0;
        }
        
        if (!gameOver && Input.GetKeyDown(KeyCode.LeftShift)) {
            playerAnim.speed = 3;
        }
        if (!gameOver && Input.GetKeyUp(KeyCode.LeftShift)) {
            playerAnim.speed = 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dustParticles.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            playerSourceSound.PlayOneShot(deathSound, 0.1f);

            gameOver = true;
            //Debug.Log("GameOver");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            deathParticles.Play();
            dustParticles.Stop();
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Money")) {
            playerSourceSound.PlayOneShot(moneyAudio, 0.2f);
        }
    }
}
