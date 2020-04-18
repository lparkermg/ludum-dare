using LPSoft.LD46.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Entities
{
    public sealed partial class Enemy : MonoBehaviour
    {
        [SerializeField]
        private Element[] _attackElements;

        private float _entryExitSpeed = 0.005f;
        private Vector2 _exitLocation;

        private Vector2[] _attackPath;
        private int _pathIndex = 1;
        private float _waitAtNodesForSeconds = 10.0f;
        private float _moveSpeed = 0.005f;

        private float _moveCurrentTime = 0.0f;
        private float _waitCurrentTime = 0.0f;

        private bool _attacking = false;
        private bool _exiting = false;

        private float _attackRate = 0.075f;
        private float _currentAttackTime = 0.25f;

        [SerializeField]
        private Transform[] _easyModeSpawners;

        [SerializeField]
        private Transform[] _mediumModeSpawners;

        [SerializeField]
        private Transform _spawnerHolder;

        private GameObject _bulletPrefab;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Move();
            SpawnRotate();
            Attack();
        }

        public void Initialize(int maxElements, List<Element> availableElements, List<Vector2> attackPath, Vector2 offScreenLocation, GameObject bulletPrefab)
        {
            var elements = new List<Element>();
            for(var i = 0; i < maxElements; i++)
            {
                var toAdd = Random.Range(0, availableElements.Count);
                elements.Add(availableElements[toAdd]);
                availableElements.RemoveAt(toAdd);
            }

            _attackElements = elements.ToArray();
            _attackPath = attackPath.ToArray();
            transform.position = attackPath[0];
            _exitLocation = offScreenLocation;
            _bulletPrefab = bulletPrefab;
        }

        private void SpawnRotate()
        {
            _spawnerHolder.Rotate(Vector3.forward, 1f);
        }

        private void Attack()
        {
            if (_attacking)
            {
                _currentAttackTime += Time.deltaTime;
                if (_currentAttackTime >= _attackRate)
                {
                    foreach (var easySpawner in _easyModeSpawners)
                    {
                        var bullet = GameObject.Instantiate(_bulletPrefab, easySpawner.position, easySpawner.rotation);
                        bullet.GetComponent<Bullet>().Initialize(5f, _attackElements[Random.Range(0, _attackElements.Length)]);
                    }

                    _currentAttackTime = 0.0f;
                }
            }
        }

        private void Move()
        {
            if (_moveCurrentTime >= 1.0f)
            {
                if (_exiting)
                {
                    gameObject.SetActive(false);
                }

                if (_waitCurrentTime >= _waitAtNodesForSeconds)
                {
                    if (_pathIndex + 1 < _attackPath.Length)
                    {
                        _pathIndex++;
                    }
                    else
                    {
                        _exiting = true;
                    }
                    _waitCurrentTime = 0.0f;
                    _moveCurrentTime = 0.0f;
                    _attacking = false;
                }
                else
                {
                    _attacking = true;
                    _waitCurrentTime += Time.deltaTime;
                }
            }
            else
            {
                if (!_exiting)
                {
                    _moveCurrentTime += _moveSpeed;
                    transform.position = Vector2.Lerp(_attackPath[_pathIndex - 1], _attackPath[_pathIndex], _moveCurrentTime);
                }
                else
                {
                    _moveCurrentTime += _entryExitSpeed;
                    transform.position = Vector2.Lerp(_attackPath[_pathIndex], _exitLocation, _moveCurrentTime);
                }
            }
        }
    }
}
