using UnityEngine;

namespace Northgard.Presentation.Common.View
{
    public abstract class SubView<T> : MonoBehaviour
    {
        private T _data;
        public T Data
        {
            get =>_data;
            private set
            {
                _data = value;
                UpdateView();
            }
        }

        public SubView<T> Instantiate(T data)
        {
            var instance = Instantiate(this);
            instance.Data = data;
            return instance;
        }

        public abstract void UpdateView();
    }
}