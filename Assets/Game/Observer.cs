using System;

public class Observer<T>
{
    public event Action<(T oldValue, T newValue)> OnValueChangeEvent;

    public T Value
    {
        get => _value;
        set
        {
            if (Equals(_value, value) == false)
            {
                T tmp = _value;
                _value = value;

                OnValueChangeEvent?.Invoke((oldValue: tmp, newValue: _value));
            }
        }
    }

    private T _value;

    public Observer()
    {
        if (typeof(T).IsValueType)
        {
            Value = default(T);
        }
        else
        {
            Value = (T)System.Activator.CreateInstance(typeof(T));
        }
    }

    public Observer(T newValue)
    {
        Value = newValue;
    }

    public static implicit operator T(Observer<T> observer)
    {
        return observer.Value;
    }
}