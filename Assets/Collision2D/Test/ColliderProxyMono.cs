using Lockstep.UnsafeCollision2D;
using Lockstep.Math;
using UnityEngine;

namespace Lockstep.Collision2D {
    public class ColliderProxyMono : MonoBehaviour {
        public ColliderProxy proxy;

        //上一帧位置
        public Vector2 prePos;
        //上一帧旋转角度
        public float preDeg;
        public bool hasInit = false;

        private Material mat;
        private bool hasCollided = false;  //是否发生碰撞
        public Color rawColor = Color.yellow;
        private Renderer render;
        private Material rawmat;
        public static bool IsReplaceNullMat;

        private void Start(){
            render = GetComponent<SkinnedMeshRenderer>();
            if (render == null) {
                render = GetComponentInChildren<SkinnedMeshRenderer>();
            }

            if (render == null) {
                render = GetComponent<Renderer>();
                if (render == null) {
                    render = GetComponentInChildren<Renderer>();
                }
            }

            if (render != null) {
                rawmat = render.material;
                mat = new Material(render.material);
                render.material = mat;
            }

            preDeg = 0;
        }

        public void Update(){
            if (proxy == null) return;
            if (!hasInit) {
                hasInit = true;
                proxy.OnTriggerEvent += OnTriggerEvent;
                prePos = (transform.position - Vector3.up).ToVector2XZ();
            }

            var curPos = transform.position.ToVector2XZ();
            if (prePos != curPos) {
                prePos = curPos;
                proxy.pos = curPos.ToLVector2();
            }

            var curDeg = transform.localRotation.eulerAngles.y;
            if (Mathf.Abs(curDeg - preDeg) > 0.1f) {
                preDeg = curDeg;
                proxy.deg = curDeg.ToLFloat();
            }

            if (IsDebug) {
                int i = 0;
            }

            if (mat != null) {
                //碰撞发生的表現
                mat.color = hasCollided ? rawColor * 0.2f : rawColor;
                if (IsReplaceNullMat) {
                    render.material = hasCollided ? null : rawmat;
                }
            }

            if (hasCollided) {
                hasCollided = false;
            }
        }

        public bool IsDebug = false;

        void OnTriggerEvent(ColliderProxy other, ECollisionEvent type){
            hasCollided = true;
            if (IsDebug) {
                if (type != ECollisionEvent.Stay) {
                    Debug.Log(type);
                }
            }

            if (proxy.IsStatic) {
                int i = 0;
            }
        }
    }
}