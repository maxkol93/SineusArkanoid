using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideByClick : MonoBehaviour, IPointerClickHandler
{
    private Animation _anim;

    private void Start()
    {
        _anim = GetComponent<Animation>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _anim.Play();
    }
}
