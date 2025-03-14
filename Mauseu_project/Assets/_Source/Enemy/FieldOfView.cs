﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EnemyAI
{
    public class FieldOfView : MonoBehaviour
    {

        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;

        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstacleMask;

        [SerializeField] private Enemy enemyComponent;


        [HideInInspector]
        public List<Transform> visibleTargets = new List<Transform>();

        private Vector3 _targetPos;

        void Start()
        {
            StartCoroutine("FindTargetsWithDelay", .2f);
        }


        IEnumerator FindTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
        }

        void FindVisibleTargets()
        {
            visibleTargets.Clear();
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
            if (visibleTargets.Count > 0)
            {
                _targetPos = visibleTargets[0].position;
                enemyComponent.IsPlayerInRange(true, _targetPos);
            }

        }


        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}