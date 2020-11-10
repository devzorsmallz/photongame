﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class PlayerUI : MonoBehaviour {
        #region Private Fields

        [Tooltip ("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;

        [Tooltip ("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;
        private PlayerController target;
        [Tooltip ("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3 (0f, 30f, 0f);
        float characterControllerHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;

        #endregion

        #region MonoBehaviour Callbacks
        void Update () {
            if (playerHealthSlider != null) {
                playerHealthSlider.value = target.Health;
            }
            if (target == null) {
                Destroy (this.gameObject);
                return;
            }
        }
        void Awake () {
            this.transform.SetParent (GameObject.Find ("Canvas").GetComponent<Transform> (), false);
            _canvasGroup = this.GetComponent<CanvasGroup> ();
        }

        void LateUpdate () {
            if (targetRenderer != null) {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }

            if (targetTransform != null) {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                this.transform.position = Camera.main.WorldToScreenPoint (targetPosition) + screenOffset;
            }
        }

        #endregion

        #region Public Methods
        public void SetTarget (PlayerController _target) {
            if (_target == null) {
                Debug.LogError ("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }
            target = _target;
            if (playerNameText != null) {
                playerNameText.text = target.photonView.Owner.NickName;
            }
            targetTransform = this.target.GetComponent<Transform> ();
            targetRenderer = this.target.GetComponent<Renderer> ();
            CharacterController characterController = _target.GetComponent<CharacterController> ();
            // Get data from the Player that won't change during the lifetime of this Component
            if (characterController != null) {
                characterControllerHeight = characterController.height;
            }
        }

        #endregion
    }