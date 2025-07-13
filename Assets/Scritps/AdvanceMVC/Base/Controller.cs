using UnityEngine;
using CompanyName.ProjectName.Managers;

namespace CompanyName.ProjectName.Base
{
    public abstract class Controller : IGameUpdate
    {
        View view;

        public static T CreateContoller<T>() where T : Controller, new()
        {
            var controller = new T();
            controller.RegisterDependencies();

            return controller;
        }

        public static T CreateContoller<T>(View view) where T : Controller, new()
        {
            var controller = new T();

            controller.view = view;
            controller.RegisterDependencies();

            return controller;
        }

        protected Controller()
        {

        }

        protected abstract void OnInjectDependencies(View view, IModel model);
        protected abstract void RegisterDependencies();

        protected void Initialize<T, U>() where T : View, new() where U : IModel, new()
        {
            if (view == null)
            {
                var viewObj = new GameObject(typeof(T).Name);
                view = viewObj.AddComponent<T>();
            }

            Initialize<U>(view);
        }

        protected void Initialize<T>(View view) where T : IModel, new()
        {
            var model = new T();

            OnInjectDependencies(view, model);
            view.OnInjectDependencies(model);
            view.AddContoller(this);

            Start();
            Enable();

            ControllerManager.Instance.Register(this);
        }

        public void Destroy()
        {
            ControllerManager.Instance.Unregister(this);
            OnDestroy();

            Object.Destroy(view.gameObject);
        }

        public void Disable()
        {
            OnDisable();
            view.gameObject.SetActive(false);
        }

        public void Enable()
        {
            OnEnable();
            view.gameObject.SetActive(true);
        }

        void IGameUpdate.Update()
        {
            Update();
        }

        void IGameUpdate.FixedUpdate()
        {
            FixedUpdate();
        }

        protected virtual void Start() { }
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }

        protected virtual void OnDestroy() { }

        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }

        public virtual void OnCollisionEnter(Collision2D collision) { }
        public virtual void OnCollisionExit(Collision2D collision) { }
        public virtual void OnTriggerEnter(Collider2D collider) { }
        public virtual void OnTriggerExit(Collider2D collider) { }
    }
}