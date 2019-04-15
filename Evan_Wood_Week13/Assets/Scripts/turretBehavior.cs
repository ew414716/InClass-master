using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBehavior : MonoBehaviour
{
    public Transform rotator;
    public float fireRate = 2;
    public float turretDamage = 5;

    private Transform _currentTarget;
    private List<Transform> _enemiesInRange;
    private float _currentReloadTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        _enemiesInRange = new List<Transform>();
        _currentReloadTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_currentTarget != null)
        //{
        //    rotator.LookAt(_currentTarget);
        //}
        float closestDistance = 99999999;
        Transform closestEnemy = null;
        for (int i = 0; i < _enemiesInRange.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, _enemiesInRange[i].position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = _enemiesInRange[i];
            }

            if (closestEnemy != null)
            {
                rotator.LookAt(closestEnemy);

                if (_currentReloadTimer > fireRate)
                {
                    FireTurret(closestEnemy);
                    _currentReloadTimer = 0;
                }
                _currentReloadTimer += Time.deltaTime;
            }
        }

        void FireTurret (Transform enemyShot)
        {
            //DO SOMETHING HERE
            Debug.Log("Shoot");

            enemyShot.GetComponent<enemyBehavior>().OnEnemyShot(turretDamage);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.name);
        //_currentTarget = other.transform;
        if (!_enemiesInRange.Contains(other.transform))
        {
            _enemiesInRange.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_enemiesInRange.Contains (other.transform))
        {
            _enemiesInRange.Remove(other.transform);
        }
    }
}
