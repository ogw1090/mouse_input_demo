using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public List<MeshRenderer> meshRenderers;
    Plane groundPlane;

    Ray ray;
    float rayDistance;

    void Start()
    {
        groundPlane = new Plane(Vector3.up, 0f);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0)) // 左クリックを押した時
        {
            Vector3 p = GetCursorPosition3D();
            Debug.Log(p);

            foreach (MeshRenderer m in meshRenderers)
            {
                m.material.SetVector("_Vector3", p); // マウスクリックした座標
                Debug.Log("      "+ m.material.GetVector("_Vector3"));
            }

            StartCoroutine("ExpandCircle");
        }
    }

    IEnumerator ExpandCircle()
    {
        float n = 0;

        while (true)
        {
            foreach (MeshRenderer m in meshRenderers)
            {
                m.material.SetFloat("_Float", n); // 距離に足す値をセット
            }

            //gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Float", n);

            n -= 0.5f; // 距離に足す値を減らす

            yield return new WaitForSeconds(0.01f);

            if (n < -20f) // 十分に円が広がると終了
            {
                foreach (MeshRenderer m in meshRenderers)
                {
                    m.material.SetFloat("_Float", 2); // 円の縁を中心へ戻す
                }

                //gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Float", 2);


                yield break;
            }
        }
    }

    Vector3 GetCursorPosition3D()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//マウスカーソルからカメラが向く方向へのレイ
        groundPlane.Raycast(ray, out rayDistance);//レイを飛ばす

        return ray.GetPoint(rayDistance);//Planeとレイがぶつかった点の座標を返す
    }


}