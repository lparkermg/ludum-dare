using LPSoft.LD46.Enums;
using LPSoft.LD46.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LPSoft.LD46.Entities
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Bullet : MonoBehaviour
    {
        public Element Element { get; private set; }
        private SpriteRenderer _renderer;
        private Rigidbody2D _rb;

        private float _bulletSpeed;

        private bool _initialized = false;

        void Awake()
        {
            TryGetComponent(out _renderer);
        }

        public void Initialize(float bulletSpeed, Element element)
        {
            _bulletSpeed = bulletSpeed;
            Element = element;
            _renderer.color = element.ToColor();
            _initialized = true;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_initialized)
            {
                Move();
            }
        }

        private void Move()
        {
            transform.position = transform.position + transform.up * (_bulletSpeed * Time.deltaTime);
        }
    }
}
