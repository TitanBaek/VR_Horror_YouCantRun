using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public AudioClip lockSound;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    public void LockSound()
    {
        // ��������� lockSound ���
        if (rb.isKinematic)
        {
            audioSource.clip = lockSound;
            audioSource.Play();
        }
    }
}
