namespace Core {
    public class Component {
        public virtual void OnInitialize() {
        }

        public virtual void OnUnInitialize() {
        }

        public virtual void OnUpdate(int deltaTime) {
        }

        public virtual void OnFixedUpdate(int deltaTime) {
        }

        public virtual void OnLateUpdate(int deltaTime) {
        }
    }
}