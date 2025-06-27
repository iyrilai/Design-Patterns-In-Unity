using System.Collections.Generic;
using System.Threading.Tasks;
using Mouizeroo.AdvanceMVC.Base;
using UnityEngine;

namespace Mouizeroo.AdvanceMVC.Managers
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

        readonly List<IController> controllers = new();

        public Task Init() { return null; }

        public void Register(IController controller)
        {
            if (!controllers.Contains(controller))
                controllers.Add(controller);
        }

        public void Unregister(IController controller)
        {
            if (controllers.Contains(controller))
                controllers.Remove(controller);
        }

        public T GetController<T>() where T : IController
        {
            foreach (var controller in controllers)
            {
                if (controller is T typedController)
                    return typedController;
            }

            throw new System.Exception("Controller Not Found");
        }

        public T GetController<T>(GameObject @object) where T : IController
        {
            try
            {
                var controller = @object.GetComponent<View>().GetController<T>();
                if (controller != null)
                {
                    return controller;
                }
            }
            catch { }

            throw new System.Exception("Controller Not Found");
        }

        public T ViewGenerator<T>() where T : View
        {
            var type = typeof(T);
            var obj = new GameObject(type.Name, type);

            return obj.GetComponent<T>();
        }
    }
}