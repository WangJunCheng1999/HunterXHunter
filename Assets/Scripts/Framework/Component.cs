using UnityEditor;

namespace GameFramework {

    internal interface IComponent { }

    public class Component : IComponent {

        public virtual void OnInitialize() { }

        public virtual void OnUnInitialize() { }

        public virtual void OnStart() { }

        public virtual void OnStop() { }

        public virtual void OnUpdate(int deltaTime) { }

        public virtual void OnFixedUpdate(int deltaTime) { }

        public virtual void OnLateUpdate(int deltaTime) {
        }

        internal TComponent GetComponent<TComponent>() where TComponent : class {
            return null;
        }

    }

}