using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [Header("Scrub Settings")]
    [Tooltip("需要累计的“摩擦距离”（世界坐标单位）。数值越大越难擦掉。")]
    [SerializeField] private float scrubDistanceToClean = 2.0f;

    [Tooltip("满足条件时，按比例增加距离（1 = 距离等比累计；>1 更快；<1 更慢）")]
    [SerializeField] private float scrubDistanceMultiplier = 1.0f;

    [Tooltip("不满足条件时每秒回退的距离（世界坐标单位/秒）")]
    [SerializeField] private float scrubLoseDistancePerSecond = 0.6f;

    private float progressDistance = 0f;

    private GameManager gm;
    private Transform towel;

    private Vector3 lastTowelPos;
    private bool hasLastPos = false;

    private float nextDebugTime = 0f;

    private Collider2D myCol;
    private Collider2D towelCol;

    private readonly List<Collider2D> overlapResults = new List<Collider2D>(8);
    private ContactFilter2D overlapFilter;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        var towelObj = GameObject.Find("Towel");
        if (towelObj != null) towel = towelObj.transform;

        myCol = GetComponent<Collider2D>();
        if (towel != null) towelCol = towel.GetComponent<Collider2D>();

        // 不过滤图层（避免你项目里 Layer Collision Matrix 关闭导致误判）
        overlapFilter = new ContactFilter2D();
        overlapFilter.useTriggers = true;
        overlapFilter.useLayerMask = false;
        overlapFilter.useDepth = false;
    }

    private void Update()
    {
        bool selected = gm != null && gm.towelSelected;
        bool mouseHeld = Input.GetMouseButton(0);
        bool overlapping = selected && mouseHeld && IsTowelOverlapping();

        float moved = 0f;

        if (gm == null || !selected || towel == null || myCol == null || towelCol == null)
        {
            ResetTrackingAndDecay();
            return;
        }

        if (!mouseHeld)
        {
            ResetTrackingAndDecay();
            return;
        }

        if (!overlapping)
        {
            ResetTrackingAndDecay();
            return;
        }

        moved = GetTowelMovedDistance();
        progressDistance += moved * scrubDistanceMultiplier;


        if (progressDistance >= scrubDistanceToClean)
        {
            Destroy(gameObject);
        }
    }

    private bool IsTowelOverlapping()
    {
        // 用 OverlapCollider 更“硬”，只看几何重叠，不依赖物理接触状态
        overlapResults.Clear();
        int count = myCol.OverlapCollider(overlapFilter, overlapResults);

        for (int i = 0; i < count; i++)
        {
            if (overlapResults[i] == towelCol)
                return true;
        }

        return false;
    }

    private float GetTowelMovedDistance()
    {
        if (!hasLastPos)
        {
            lastTowelPos = towel.position;
            hasLastPos = true;
            return 0f;
        }

        float dist = Vector3.Distance(towel.position, lastTowelPos);
        lastTowelPos = towel.position;
        return dist;
    }

    private void ResetTrackingAndDecay()
    {
        hasLastPos = false;
        progressDistance = Mathf.Max(0f, progressDistance - scrubLoseDistancePerSecond * Time.deltaTime);
    }
}