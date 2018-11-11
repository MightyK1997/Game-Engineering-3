using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


//Using Object pools to create ragdolls before hand.


//This is the default class. To Create Object pools of objects other than Gameobjects and Scripts.
public class ObjectPool<T> : MonoBehaviour, IObjectPool<T> where T:Object
{
    //TODO: Fleshout the default class
    private HashSet<T> m_ObjectList;
    //Abstract Variables
    //public abstract bool AutoFill { get; set; }
    //public abstract int MinSize { get; set; }
    //public abstract bool CreateIfNecessary { get; set; }

    //Public Methods
    public virtual void Create(T i_ObjectType, int i_NumberOfObjects)
    {
        m_ObjectList = new HashSet<T>();
        m_ObjectList.Add(i_ObjectType);
    }

    //Abstract Methods
    //public abstract void Resize(int count);

#region IObjectPool

    public T Get()
    {
        T item;
        item = m_ObjectList.First();
        return item;
    }

    public IEnumerable<T> Get(int i_Count)
    {
        return m_ObjectList.Take(i_Count);
    }

    public void Return(T t) { }
    public void Return(IEnumerable<T> i_GroupOfTs) { }
#endregion
}

//Overload to Create gameobject object pools. This is the one being used for ragdolls.
public class GameObjectPool : ObjectPool<GameObject>
{
    private HashSet<GameObject> m_ObjectPool = new HashSet<GameObject>();
    //public override bool AutoFill { get; set; }
    //public override  int MinSize { get; set; }
    //public override bool CreateIfNecessary { get; set; }
    //public override void Resize(int count) { }
    //Instantiate Necessary Gameobjects and deactivate them in scene.
    public override void Create(GameObject i_GameObject, int m_Number)
    {
        for (int i = 0; i < m_Number; i++)
        {
            GameObject go = Instantiate(i_GameObject, Vector3.zero, Quaternion.identity);
            go.SetActive(false);
            m_ObjectPool.Add(go);
        }
    }

    #region IObjectPool
    //TODO:Finish this stuff
    public new GameObject Get()
    {
        GameObject returnItem;
        if (m_ObjectPool.Count != 0)
        {
            returnItem = m_ObjectPool.First();
            m_ObjectPool.Remove(returnItem);
            return returnItem;
        }
        return null;
    }

    public new IEnumerable<GameObject> Get(int i_Count)
    {
        IEnumerable<GameObject> returnItem;
        if (m_ObjectPool.Count >= i_Count)
        {
            returnItem = m_ObjectPool.Take(i_Count);
            for (int i = 0; i < i_Count; i++)
            {
                m_ObjectPool.Remove(m_ObjectPool.First());
            }
            return returnItem;
        }
        return null;
    }

    public new void Return(GameObject g)
    {
        m_ObjectPool.Add(g);
    }

    public new void Return(IEnumerable<GameObject> lgs)
    {
        foreach (var go in lgs)
        {
            m_ObjectPool.Add(go);
        }
    }
    #endregion


}


//Overload for scripts.
//This Creates a temporary gameobject to which all the scripts are first attached.
//Doing this since instantiating a monobehaviour using new is not recommended by Unity.
public class ComponentPool : ObjectPool<MonoBehaviour>
{
    private List<MonoBehaviour> m_ObjectPool = new List<MonoBehaviour>();

    private GameObject go;
    //public override bool AutoFill { get; set; }
    //public override int MinSize { get; set; }
    //public override bool CreateIfNecessary { get; set; }
    //public override void Resize(int count) { }

    public override void Create(MonoBehaviour i_GameObject, int i_Number)
    {
        go = new GameObject();
        go.SetActive(false);
        for (int i = 0; i < i_Number; i++)
        {
            var comp = go.AddComponent(i_GameObject.GetType()) as MonoBehaviour;
            m_ObjectPool.Add(comp);
        }
    }
    #region IObjectPool
    //TODO:Finish this stuff
    public new MonoBehaviour Get()
    {
        MonoBehaviour returnItem;
        if (m_ObjectPool.Count != 0)
        {
            returnItem = m_ObjectPool.First();
            m_ObjectPool.Remove(returnItem);
            return returnItem;
        }
        return null;
    }

    public new IEnumerable<MonoBehaviour> Get(int i_Count)
    {
        IEnumerable<MonoBehaviour> returnItem;
        if (m_ObjectPool.Count >= i_Count)
        {
            returnItem = m_ObjectPool.Take(i_Count);
            for (int i = 0; i < i_Count; i++)
            {
                m_ObjectPool.Remove(m_ObjectPool.First());
            }
            return returnItem;
        }
        return null;
    }

    public new void Return(MonoBehaviour g)
    {
        m_ObjectPool.Add(g);
    }
    public new void Return(IEnumerable<MonoBehaviour> lgs)
    {
        foreach (var monoBehaviour in lgs)
        {
            m_ObjectPool.Add(monoBehaviour);
        }
    }
    #endregion
}