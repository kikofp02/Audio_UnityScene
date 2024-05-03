using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootstepSound : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    AudioMixerGroup[] GetAudioMixerGroups
    {
        get { return audioMixer.FindMatchingGroups(string.Empty); }
    }

    [SerializeField] AudioClip[] footstepsOnSand;
    [SerializeField] AudioClip[] footstepsOnMetal;

    public string material;

    [SerializeField] [Range(0.01f, 1f)] public float stepInterval = 0.2f;
    [SerializeField] [Range(0.01f, 5f)] public float fadeOutDuration = 2f;

    private AudioSource audioSource;
    private float stepTimer = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlayFootstepSound()
    {
        audioSource.volume = Random.Range(0.9f, 1.0f);
        audioSource.pitch = Random.Range(0.8f, 1.2f);

        stepTimer += Time.deltaTime;



        if (stepTimer >= stepInterval)
        {
            switch (material)
            {
                
                case "Sand":
                    audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Sand")[0];
                    audioSource.PlayOneShot(footstepsOnSand[Random.Range(0, footstepsOnSand.Length)]);
                    break;

                case "Metal":
                    audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Metal")[0];
                    audioSource.PlayOneShot(footstepsOnMetal[Random.Range(0, footstepsOnMetal.Length)]);
                    break;

                default:
                    break;
            }
            print(audioSource.volume);

            stepTimer = 0f;
        }
    }

    IEnumerator FadeOutAndStop(float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume to its original value
    }

    void StopFootstepSound()
    {
        if (audioSource.isPlaying)
        {
            StartCoroutine(FadeOutAndStop(fadeOutDuration));
        }
    }

    public bool IsPlaying()
    { 
        return audioSource.isPlaying;
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
                if(audioSource.isPlaying)
                   audioSource.Stop();
                break;
            case "Metal":
                if (audioSource.isPlaying)
                    audioSource.Stop();
                break;

            default:
                break;
        }
    }
}
