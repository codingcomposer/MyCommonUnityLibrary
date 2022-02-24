using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DKCommon.UI
{
    public class TabButton : MonoBehaviour, IPointerClickHandler

    {
        [SerializeField]
        public TabGroup tabGroup;
        public GameObject page;

        protected AudioSource audioSource;
        public AudioClip audioClip;

        public Image Image { get; private set; }

        protected virtual void Awake()
        {
            Image = GetComponent<Image>();
            audioSource = tabGroup.GetComponent<AudioSource>();
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
            if (audioSource)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}