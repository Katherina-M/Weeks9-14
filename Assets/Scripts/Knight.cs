using Cinemachine;
using UnityEngine;


public class Knight : MonoBehaviour
{
    SpriteRenderer sr;
    Animator animator;
    public float speed = 2;
    public bool canRun = true;
    AudioSource audio;

    public AudioClip[] audioClips;
    public CinemachineImpulseSource impulseSource;
 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {


        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            impulseSource.GenerateImpulse();

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            impulseSource.GenerateImpulse();
        }

        float directiion = Input.GetAxis("Horizontal");


        sr.flipX = (directiion < 0);

        animator.SetFloat("movement", Mathf.Abs(directiion));


        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            canRun = false;
        }

        if (canRun == true)
        {
            transform.position += transform.right * directiion * speed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            int randomNumber = Random.Range(0, audioClips.Length);
            audio.PlayOneShot(audioClips[randomNumber]);
            canRun = false;
        }
    }


    public void AttackHasFinished()
    {
        Debug.Log("The attack is finsihed");
        canRun = true;
    }
    
    public void FootStepSound()
    {
        Debug.Log("Here is foot step sound!");
        if (canRun == true)
        {
            int randomNumber = Random.Range(0, audioClips.Length);
            audio.PlayOneShot(audioClips[randomNumber]);
            canRun = false;
        }

        if (!audio.isPlaying)
        {
            int randomNumber = Random.Range(0, audioClips.Length);
            audio.PlayOneShot(audioClips[randomNumber]);
        }
    }
}