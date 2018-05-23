using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// プレイヤーのUIビュー
/// </summary>
public class GamePlayerUI : MonoBehaviour{
    
    /// <summary>
    /// HPのUI
    /// </summary>
    [SerializeField]
    private Text _hpUI;

    /// <summary>
    /// ポップアップUI
    /// </summary>
    [SerializeField]
    private Image _popupUI;

    /// <summary>
    /// UIのアクティベート
    /// </summary>
	public void Activate()
    {
        _hpUI.enabled = true;
        _popupUI.enabled = true;
    }

    /// <summary>
    /// PlayerのUI更新
    /// </summary>
    /// <param name="hp"></param>
    public void UpdatePlayerUI(float hp)
    {
        _hpUI.text = hp.ToString();
    }
}
