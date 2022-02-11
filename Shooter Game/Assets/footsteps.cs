using UnityEngine;
using System.Collections;
public class footsteps : MonoBehaviour
{
    public AudioSource footstep;
    public AudioClip[] footsepArray;
    bool isGrounded;
    public Transform CheckOnGround;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool playingSound = false;
    float waitTime = 0.3f;

    AudioClip RandomClip() {
        return footsepArray[Random.Range(0, footsepArray.Length)];
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(CheckOnGround.position, groundDistance, groundMask);
        if (!(footstep.isPlaying) && isGrounded && !(playingSound) &&(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                waitTime = 0.15f;
                StartCoroutine(playSound());
            }
            else
            {
                waitTime = 0.3f;
                StartCoroutine(playSound());
            }
                
        }    
    }

    IEnumerator playSound()
    {
        playingSound = true;
        yield return new WaitForSeconds(waitTime);
        if (isGrounded) 
        {
            footstep.PlayOneShot(RandomClip());
        }
        playingSound = false;
    }

}
