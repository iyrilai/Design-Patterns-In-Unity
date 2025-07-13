using UnityEngine;

namespace CompanyName.ProjectName.Base
{
    public abstract class View : MonoBehaviour
    {
        Controller controller;

        public abstract void OnInjectDependencies(IModel model);

        public void AddContoller(Controller controller)
        {
            this.controller = controller;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            controller?.OnCollisionEnter(collision);
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            controller?.OnCollisionExit(collision);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            controller?.OnTriggerEnter(collider);
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            controller?.OnTriggerExit(collider);
        }
    }
}
