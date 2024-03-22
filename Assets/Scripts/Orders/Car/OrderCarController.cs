using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Orders;
using UnityEngine;
using Utils;
using Zenject;

public class OrderCarController : MonoBehaviour, IOrderAgent
{
    [SerializeField] private GameObject[] _pathPoint;
    [SerializeField] private float _pathDuration = 5f;
    private OrderSystem _orderSystem;

    [Inject]
    public void Construct(OrderSystem orderSystem)
    {
        _orderSystem = orderSystem;
    }

    private void Start()
    {
        _orderSystem.AddAgent(this);
    }

    public async UniTask CompleteOrderAndReturn()
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < _pathPoint.Length; i++)
            points.Add(_pathPoint[i].transform.position);

        transform.DOPath(points.ToArray(), _pathDuration).SetEase(Ease.Linear).SetLookAt(0.02f);

        await UniTask
            .Delay(TimeSpan.FromSeconds(15f), DelayType.DeltaTime, PlayerLoopTiming.Update,
                gameObject.GetCancellationTokenOnDestroy());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Constants.PlayerTag))
        {
            if (_orderSystem.TargetOrder != null)
            {
                _orderSystem.TryCompleteOrder();
            }
        }
    }

    private void OnDestroy()
    {
        _orderSystem.RemoveAgent(this);
    }
}
