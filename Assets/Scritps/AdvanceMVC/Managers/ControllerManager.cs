using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyName.ProjectName.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CompanyName.ProjectName.Managers
{
    public class ControllerManager : IManager
    {
        private static ControllerManager instance;

        public static ControllerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ControllerManager();
                }

                return instance;
            }
        }

        readonly List<Controller> controllers = new();
        readonly Dictionary<Type, Controller> controllersMap = new();

        public Task Init() { return null; }

        public void Register(Controller controller)
        {
            if (!controllers.Contains(controller))
                controllers.Add(controller);

            controllersMap.TryAdd(controller.GetType(), controller);
        }

        public void Unregister(Controller controller)
        {
            if (controllers.Contains(controller))
                controllers.Remove(controller);

            controllersMap.Remove(controller.GetType());
        }

        public void Update()
        {
            for (int i = 0; i < controllers.Count; i++)
            {
                var controller = controllers[i];
                try
                {
                    var gameUpdate = (IGameUpdate)controller;
                    gameUpdate.Update();
                }
                catch { }
            }
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < controllers.Count; i++)
            {
                var controller = controllers[i];
                try
                {
                    var gameUpdate = (IGameUpdate)controller;
                    gameUpdate.FixedUpdate();
                }
                catch { }
            }
        }

        public T GetController<T>() where T : Controller
        {
            if (controllersMap.TryGetValue(typeof(T), out var controller))
            {
                return (T)controller;
            }

            throw new Exception("Controller Not Found");
        }

        public T CreateController<T>() where T : Controller, new()
        {
            var controller = Controller.CreateContoller<T>();

            return controller;
        }

        /// <summary>
        /// Get view from gameobject and create the controller
        /// </summary>
        public T CreateController<T, U>(GameObject gameObject) where T : Controller, new() where U : View
        {
            var view = gameObject.GetComponent<U>();
            var controller = Controller.CreateContoller<T>(view);

            return controller;
        }

        /// <summary>
        /// Create new gameobject with given reference
        /// </summary>
        public T CreateControllerWithRef<T, U>(GameObject _ref) where T : Controller, new() where U : View
        {
            var instance = Object.Instantiate(_ref);
            return CreateController<T, U>(instance);
        }
    }
}