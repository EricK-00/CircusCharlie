using UnityEngine;
using UnityEngine.SceneManagement;

public static partial class Functions
{
    //! 특정 게임오브젝트의 자식들을 탐색해서 찾는 메서드
    public static GameObject FindChildGameObject(this GameObject parentGO, string childName)
    {
        GameObject searchTarget;

        Debug.Log($"PGO{parentGO}, CDN{childName}");

        for (int i = 0; i < parentGO.transform.childCount; i++)
        {
            searchTarget = parentGO.transform.GetChild(i).gameObject;
            if (searchTarget.name.Equals(childName))
            {
                Debug.Log(searchTarget);
                return searchTarget;
            }
            else
            {
                GameObject go = FindChildGameObject(searchTarget, childName);
                if (go != null)
                {
                    Debug.Log(searchTarget);
                    return go;
                }
            }
        }

        return null;
    }

    //! 루트 게임오브젝트를 탐색해서 찾는 메서드
    public static GameObject GetRootGameObject(string goName)
    {
        Scene activeScene = GetActiveScene();
        GameObject[] rootGOs = activeScene.GetRootGameObjects();

        foreach (GameObject go in rootGOs)
        {
            if (go.name.Equals(goName))
            {
                return go;
            }
        }

        return default;
    }

    //! 현재 활성화된 씬을 가져오는 메서드
    public static Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    //! RectTransform에서 sizeDelta를 리턴하는 함수
    public static Vector2 GetRectSizeDelta(this GameObject go)
    {
        return go.GetComponent<RectTransform>().sizeDelta;
    }

    //! localPosition 변경하는 메서드
    public static void SetLocalPos(this GameObject go, float x, float y, float z)
    {
        go.GetComponent<RectTransform>().localPosition = new Vector3(x, y, z);
    }

    //! localPosition 연산하는 메서드
    public static void AddLocalPos(this GameObject go, float x, float y, float z)
    {
        go.transform.localPosition = go.transform.localPosition + new Vector3(x, y, z);
    }


    #region Translate wrapper method for Vector2
    public static void Translate(this Transform transform, Vector2 moveVector)
    {
        transform.Translate(moveVector.x, moveVector.y, 0f);
    }
    #endregion

    #region GetComponent wrapper method
    public static T GetComponentMust<T>(this GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        Functions.Assert(component.IsValid<T>(), $"[{go.name}] {component.GetType().Name} 없음");

        return component;
    }
    public static T GetComponentMustt<T>(this GameObject go)
    {
        T component = go.GetComponent<T>();
        Functions.Assert(component.IsValid<T>(), $"[{go.name}] {component.GetType().Name} 없음");

        return component;
    }
    #endregion

}