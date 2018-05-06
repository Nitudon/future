using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayerUI : MonoBehaviour {

    [SerializeField]
    private Text _hpUI;

    [SerializeField]
    private Image _popupUI;

	public void Activate()
    {
        _hpUI.enabled = true;
        _popupUI.enabled = true;
    }

    public void UpdatePlayerUI(float hp)
    {
        _hpUI.text = hp.ToString();
    }
}
