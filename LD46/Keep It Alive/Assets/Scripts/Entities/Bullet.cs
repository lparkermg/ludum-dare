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

        private float _bulletTimeout = 10f;

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

            if (_bulletTimeout <= 0.0f)
            {
                Destroy(gameObject);
            }
            else
            {
                _bulletTimeout -= Time.deltaTime;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Carrier"))
            {
                collision.gameObject.GetComponent<Carrier>().Damage(0.5f);
            }

            if (collision.gameObject.CompareTag("Barrier"))
            {
                collision.gameObject.GetComponent<Barrier>().Damage(0.5f, Element);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Damage(0.5f);
            }

            Destroy(gameObject);
        }

        private void Move()
        {
            transform.position = transform.position + transform.up * (_bulletSpeed * Time.deltaTime);
        }
    }
}
