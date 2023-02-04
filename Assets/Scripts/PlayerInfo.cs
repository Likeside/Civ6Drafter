using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {
  [SerializeField] TextMeshProUGUI _playerName;
  [SerializeField] TextMeshProUGUI _playerCivs;
  [SerializeField] Button _deleteBtn;
  [SerializeField] Toggle _newCivsBan;


  public bool NewCivsBanned => _newCivsBan.isOn;
  public event Action<PlayerInfo> OnDeleteBtnPressed;

  
  void Start() {
      _deleteBtn.onClick.RemoveAllListeners();
      _deleteBtn.onClick.AddListener((() => OnDeleteBtnPressed?.Invoke(this)));
      _deleteBtn.onClick.AddListener((() => Destroy(gameObject)));
      _newCivsBan.isOn = false;
  }

  public void SetName(string playerName) {
      _playerName.text = playerName;
  }


  public void AddCiv(string playerCiv) {
     // _playerCivs.text = String.Join(',', playerCivs);
     _playerCivs.text += playerCiv + "; ";
  }

  public void Remove() {
      Destroy(gameObject);
  }
}
