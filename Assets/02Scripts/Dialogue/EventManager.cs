using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DalbitCafe.Dialogue
{
    enum EventNum
    {
        Prologue = 0,
        FirstChapter,
        SecondChapter,
        ThirdChapter,
        FourthChapter,
        Ending
    }

    public class EventManager : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer ChangeSprite;
        private AudioSource _audioSource;
        [SerializeField] private AudioClip explosion_sound;
        [SerializeField] private AudioClip shine_sound;

        // �̹����� �ȹٲ�µ�
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        public void EventChange(DialogueData dialogueData)
        {
            int eventNum = (int)dialogueData.eventNum;

            if (eventNum == (int)EventNum.Prologue)
            {
                Debug.Log("Ʃ�丮�� ��������Ʈ");
                SpriteChange(dialogueData.ChangeBG);

            }
            //if (eventNum == 1)
            //{
            //    SpriteChange(dialogueData.ChangeBG);
            //    audioSource.PlayOneShot(explosion_sound, 0.3f);
            //    audioSource.Play();
            //}
            //if (eventNum == 2)
            //{
            //    audioSource.PlayOneShot(shine_sound, 0.4f);

            //}


        }

        private void SpriteChange(Sprite change)
        {
            ChangeSprite.sprite = change;
        }
    }

}
