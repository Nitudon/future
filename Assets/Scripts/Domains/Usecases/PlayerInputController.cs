using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// プレイヤーのタップ周りのInput処理
/// </summary>
public class PlayerInputPresenter : IPointerDownHandler
{
    public Action OnPointerDownListener;    

    public PlayerInputPresenter(Action action)
    {
        OnPointerDownListener = action;
    }

    /// <summary>
    /// プレイヤーのタップ処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownListener?.Invoke();
    }

}
