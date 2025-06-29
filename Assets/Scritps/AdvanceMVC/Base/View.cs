using Mouizeroo.AdvanceMVC.Managers;
using UnityEngine;

namespace Mouizeroo.AdvanceMVC.Base
{
    public abstract class View : MonoBehaviour
    {
        private IController Controller;

        protected abstract void Register();
        protected abstract void OnInjectDependencies(IModel model);
        
        protected void RegisterControllerModel<T, U>() where T : IController, new() where U : IModel, new()
        {
            var controller = new T();
            var model = new U();

            OnInjectDependencies(model);
            controller.OnInjectDependencies(this, model);
            
            ControllerManager.Instance.Register(controller);
            Controller = controller;
        }

        public T GetController<T>() where T : IController
        {
            return (T)Controller;
        }

        void Awake()
        {
            Register();
        }

        void Start()
        {
            Controller?.Start();
        }

        void Update()
        {
            Controller?.Update();
        }

        void FixedUpdate()
        {
            Controller?.FixedUpdate();
        }

        void OnEnable()
        {
            Controller?.OnEnable();
        }

        void OnDisable()
        {
            Controller?.OnDisable();
        }

        void OnDestroy()
        {
            ControllerManager.Instance.Unregister(Controller);
            Controller?.OnDestroy();
            Controller = null;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Controller?.OnCollisionEnter(collision);
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            Controller?.OnCollisionExit(collision);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            Controller?.OnTriggerEnter(collider);
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            Controller?.OnTriggerExit(collider);
        }
    }
}
