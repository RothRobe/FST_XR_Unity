using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace CityAR
{
    public class ShowOnTouch : MonoBehaviour, IMixedRealityTouchHandler
    {
        // Start is called before the first frame update
        private Color _default;
        void Start()
        {
            _default = gameObject.GetComponent<MeshRenderer>().material.color;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnTouchStarted(HandTrackingInputEventData eventData)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        public void OnTouchCompleted(HandTrackingInputEventData eventData)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<MeshRenderer>().material.color = _default;
        }

        public void OnTouchUpdated(HandTrackingInputEventData eventData)
        {
            //throw new System.NotImplementedException();
        }
    }
}
