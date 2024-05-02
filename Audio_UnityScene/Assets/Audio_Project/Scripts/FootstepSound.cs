using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepsOnSand;
    public AudioClip[] footstepsOnMetal;

    public string material;
    
    [Range(0.01f, 1f)]  public float stepInterval = 0.2f;
    [Range(0.01f, 5f)]  public float fadeOutDuration = 2f;


    private AudioSource myAudioSource;
    private float stepTimer = 0f;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void PlayFootstepSound()
    {
        myAudioSource.volume = Random.Range(0.9f, 1.0f);
        myAudioSource.pitch = Random.Range(0.8f, 1.2f);

        stepTimer += Time.deltaTime;

        print(stepTimer);

        if (stepTimer >= stepInterval)
        {
            switch (material)
            {
                case "Sand":
                    myAudioSource.PlayOneShot(footstepsOnSand[Random.Range(0, footstepsOnSand.Length)]);
                    break;

                case "Metal":
                    myAudioSource.PlayOneShot(footstepsOnMetal[Random.Range(0, footstepsOnMetal.Length)]);
                    break;

                default:
                    break;
            }
            stepTimer = 0f;
        }
    }

    IEnumerator FadeOutAndStop(float fadeDuration)
    {
        float startVolume = myAudioSource.volume;

        while (myAudioSource.volume > 0)
        {
            myAudioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        myAudioSource.Stop();
        myAudioSource.volume = startVolume; // Reset volume to its original value
    }

    void StopFootstepSound()
    {
        if (myAudioSource.isPlaying)
        {
            StartCoroutine(FadeOutAndStop(fadeOutDuration));
        }
    }

    public bool IsPlaying()
    { 
        return myAudioSource.isPlaying;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Sand":
                material = collision.gameObject.tag;
                break;
            case "Metal":                
                material = collision.gameObject.tag;
                break;

            default:
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Sand":
                if(myAudioSource.isPlaying)
                   myAudioSource.Stop();
                break;
            case "Metal":
                if (myAudioSource.isPlaying)
                    myAudioSource.Stop();
                break;

            default:
                break;
        }
    }
}
