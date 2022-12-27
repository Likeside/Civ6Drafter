using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace {
    public class Drafter: MonoBehaviour {
        [SerializeField] Button _addPlayer;
        [SerializeField] TMP_InputField _playerName;
        [SerializeField] TMP_InputField _civAmount;
        [SerializeField] GameObject _infoPrefab;
        [SerializeField] Transform _infoParent;
        [SerializeField] Button _draft;
        [SerializeField] TextAsset _civsText;
        [SerializeField] Button _refresh;
        [SerializeField] Button _setAmount;

        List<PlayerInfo> _infos;
        List<string> _civs;
        int _amount = 1;
        void Start() {
            _refresh.onClick.RemoveAllListeners();
            _refresh.onClick.AddListener(Refresh);
            _infos = new List<PlayerInfo>();
            _civs = new List<string>();
            _civs = _civsText.text.Split('\n').ToList();
            _addPlayer.onClick.RemoveAllListeners();
            _addPlayer.onClick.AddListener(CreatePlayerInfo);
            _draft.onClick.RemoveAllListeners();
            _draft.onClick.AddListener(Draft);
            
            _setAmount.onClick.RemoveAllListeners();
            _setAmount.onClick.AddListener(SetAmount);
            
        }

        void SetAmount() {
            int.TryParse(_civAmount.text, out int amount);
            _amount = amount;
        }

        void Draft() {
            if (_amount != 0) {
                for (int i = 0; i < _amount; i++) {

                    foreach (var info in _infos) {
                        var randomIndex = Random.Range(0, _civs.Count);
                        info.AddCiv(_civs[randomIndex]);
                        _civs.RemoveAt(randomIndex);
                    }

                }
            }
        }

        void CreatePlayerInfo() {
            if (_playerName.text != String.Empty) {
                var infoObj = Instantiate(_infoPrefab, _infoParent);
                var info = infoObj.GetComponent<PlayerInfo>();
                info.SetName(_playerName.text);
                info.OnDeleteBtnPressed += DeleteInfo;
                _infos.Add(info);
            }
        }

        void DeleteInfo(PlayerInfo info) {
            if (_infos.Contains(info)) {
                _infos.Remove(info);
            }
        }

        void Refresh() {
            foreach (var info in _infos) {
                info.Remove();
            }
            _infos = new List<PlayerInfo>();
            _civs = new List<string>();
            _civs = _civsText.text.Split('\n').ToList();
            _addPlayer.onClick.RemoveAllListeners();
            _addPlayer.onClick.AddListener(CreatePlayerInfo);
            _draft.onClick.RemoveAllListeners();
            _draft.onClick.AddListener(Draft);
            _amount = 1;
            _civAmount.text = String.Empty;
            _playerName.text = String.Empty;

        }
    }
}