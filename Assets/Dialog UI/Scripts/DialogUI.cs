using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EasyUI.Dialogs {

    public class Dialog {
        public string Title = "Default Title";
        public string Message = "Default Message";
    }

    public class DialogUI : MonoBehaviour {
        [SerializeField] GameObject canvas;
        [SerializeField] TextMeshProUGUI titleUIText;
        [SerializeField] TextMeshProUGUI messageUIText;
        [SerializeField] Button closeUIButton;

        Dialog dialog = new Dialog();

        public static DialogUI Instance;
        
        void Awake() {
            Instance = this;
            
            // Add close event listener
            closeUIButton.onClick.RemoveAllListeners();
            closeUIButton.onClick.AddListener(Hide);
        }

        // Set Dialog Title
        public DialogUI SetTitle (string title) {
            dialog.Title = title;
            return Instance;
        }

        // Set Dialog Message
        public DialogUI SetMessage (string message) {
            dialog.Message = message;
            return Instance;
        }

        // Show Dialog
        public void Show() {
            titleUIText.text = dialog.Title;
            messageUIText.text = dialog.Message;

            canvas.SetActive(true);
        }

        // Hide Dialog
        public void Hide() {
            canvas.SetActive(false);

            // Reset Dialog
            dialog = new Dialog();
        }
    }
}
