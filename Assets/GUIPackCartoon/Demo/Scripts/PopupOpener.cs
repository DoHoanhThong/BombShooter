using UnityEngine;

namespace Ricimi
{
    // it to the UI canvas of the current scene.
    public class PopupOpener : MonoBehaviour
    {
        public GameObject popupPrefab;

        protected Canvas m_canvas;

        protected void Awake()
        {
            m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        public virtual void OpenPopup()
        {
            Debug.LogError("hello!");
            var popup = Instantiate(popupPrefab) as GameObject;
            popup.SetActive(true);
            //popup.transform.localScale = Vector3.zero;
            if (m_canvas == null)
            {
                m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }
            popup.transform.SetParent(m_canvas.transform, false);
            popup.GetComponent<Popup>().Open();
        }
    }
}