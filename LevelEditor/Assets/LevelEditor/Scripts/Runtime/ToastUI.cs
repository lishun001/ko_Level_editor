using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class ToastUI : MonoBehaviour
    {
        public Text txt_msg;
        
        private Coroutine _coroutine;
        private static ToastUI _uiToast;
        
        public static void ShowToast(string content)
        {
            _uiToast.ShowToastInner(content);   
        }

        private void Awake()
        {
            Debug.Log("awake");
            _uiToast = this;
        }

        public void ShowToastInner(string msg)
        {
            if (_coroutine != null)
            {
                _uiToast.StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _coroutine = StartCoroutine(ShowToastCoroutine(msg));
        }

        private IEnumerator ShowToastCoroutine(string content)
        {
            txt_msg.text = content;
            txt_msg.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            txt_msg.gameObject.SetActive(false);
        }
    }
}