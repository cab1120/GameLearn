using System.Collections.Generic;
using UnityEngine;

public class ConnectedController : MonoBehaviour
{
    public Transform PlayerA;
    public Transform PlayerB;
    public Transform PlayerC;

    public Transform InteractorA;
    public Transform InteractorB;
    public Transform InteractorC;
    public List<Connection> Connections = new();

    private void Start()
    {
        Connections.Clear();
        GetConnections();
    }

    private void Update()
    {
        CheckandChange();
    }

    private void GetConnections()
    {
        var c = new Connection(PlayerA, PlayerB, PlayerC);
        var c1 = new Connection(InteractorA, InteractorB, InteractorC);
        Connections.Add(c);
        Connections.Add(c1);
    }

    private void CheckandChange()
    {
        foreach (var connection in Connections)
        {
            if (connection.TransformB.position != connection.PositionB)
            {
                //Debug.Log("changed position");
                if (connection.TransformB.GetComponent<Rigidbody>())
                {
                    var rb1 = connection.TransformA.GetComponent<Rigidbody>();
                    var rb2 = connection.TransformC.GetComponent<Rigidbody>();
                    //Debug.Log(connection.PositionB.y);
                    rb1.MovePosition(connection.TransformA.parent.parent.TransformPoint(connection.PositionB));
                    rb2.MovePosition(connection.TransformC.parent.parent.TransformPoint(connection.PositionB));
                    connection.PositionB =
                        connection.TransformB.parent.parent.InverseTransformDirection(connection.TransformB.position);
                }
                else
                {
                    connection.TransformA.position = connection.TransformA.parent.TransformPoint(connection.PositionB);
                    connection.TransformC.position = connection.TransformC.parent.TransformPoint(connection.PositionB);
                }

                connection.PositionB =
                    connection.TransformB.parent.InverseTransformDirection(connection.TransformB.position);
            }

            if (connection.TransformB.localScale != connection.ScaleB)
            {
                connection.TransformA.localScale = connection.TransformB.localScale;
                connection.TransformC.localScale = connection.TransformB.localScale;
                connection.ScaleB = connection.TransformB.localScale;
            }

            if (connection.TransformB.rotation != connection.RotationB)
            {
                connection.TransformA.rotation = connection.TransformB.rotation;
                connection.TransformC.rotation = connection.TransformB.rotation;
                connection.RotationB = connection.TransformB.rotation;
            }
        }
    }

    public class Connection
    {
        public Vector3 PositionB;
        public Quaternion RotationB;

        public float Scale = 5f;
        public Vector3 ScaleB;
        public Transform TransformA;
        public Transform TransformB;
        public Transform TransformC;

        public Connection(Transform A, Transform B, Transform C)
        {
            TransformA = A;
            TransformB = B;
            TransformC = C;
            if (B.GetComponent<Rigidbody>())
                PositionB = B.parent.parent.InverseTransformDirection(B.position);
            else
                PositionB = B.parent.InverseTransformDirection(B.position);
            ScaleB = B.localScale;
            RotationB = B.rotation;
        }

        public Connection()
        {
        }
    }
}