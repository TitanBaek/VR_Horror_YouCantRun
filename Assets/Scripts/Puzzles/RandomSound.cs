using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ldw
{
    public class RandomSound : MonoBehaviour
    {
        public AudioSource audioSource;

        [SerializeField]
        private AudioClip[] sounds;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // �̺�Ʈ�� �����ų �Լ�
        public void PlayRandomSound()
        {
            int rand = Random.Range(0, sounds.Length);
            PlaySE(sounds[rand]);
        }

        private void PlaySE(AudioClip _clip)
        {
            audioSource.clip = _clip;
            audioSource.Play();
        }
    }
}