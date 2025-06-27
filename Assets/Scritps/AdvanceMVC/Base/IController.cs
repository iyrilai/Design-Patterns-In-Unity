namespace Mouizeroo.AdvanceMVC.Base
{
    public interface IController
    {    
        void OnInjectDependencies(View view, IModel model);

        void Start() { }
        void Update() { }
        void FixedUpdate() {}

        void OnDestroy() { }

        void OnEnable() { }
        void OnDisable() { }

        void OnCollisionEnter(UnityEngine.Collision2D collision) { }
        void OnCollisionExit(UnityEngine.Collision2D collision) { }
        void OnTriggerEnter(UnityEngine.Collider2D collider) { }
        void OnTriggerExit(UnityEngine.Collider2D collider) { }
    }
}