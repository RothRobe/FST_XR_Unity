using UnityEngine;

namespace HandsOn
{
    public class Scale : MonoBehaviour
    {
        private bool moveBack;
        private bool started;

        private float scale;
        // Start is called before the first frame update
        void Start()
        {
            moveBack = false;
            started = false;
            scale = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if (started)
            {
                if (!moveBack)
                {
                    scale += 0.01f;
                }
                else
                {
                    scale -= 0.01f;
                }

                if (scale > 2)
                {
                    moveBack = true;
                }
                if (scale <= 1)
                {
                    moveBack = false;
                    started = false;
                }

                transform.localScale = new Vector3(scale, scale, scale);
            }
       
        }

        public void startScaling()
        {
            started = true;
        }
    }
}