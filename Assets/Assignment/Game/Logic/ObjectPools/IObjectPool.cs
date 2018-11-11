using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPool<T>
{
    T Get();
    IEnumerable<T> Get(int i_Count);

    void Return(T t);
    void Return(IEnumerable<T> i_GroupOfTs);
}
