using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// KeyCard�� �޴� �Լ�
public class KeyCardReceiver : MonoBehaviour
{
    public UnityEvent OnKeyCardCollision;  // KeyCard �� Collider�� ���� ��� ����Ǵ� �̺�Ʈ
    public bool DestroyedOnTriggered;      // ���� �� ������Ʈ�� �ı����� ����

    public void OnTriggerEnter(Collider other)  // Trigger Collider�� �߰��� �浹 �Ǵ�
    {
        var proj = other.GetComponent<KeyCard>();   // KeyCard ������Ʈ�� �ִ��� Ȯ��

        // KeyCard ������Ʈ�� �ִٸ�(null�� �ƴϸ�) �̺�Ʈ ����.
        if (proj != null)
        {
            OnKeyCardCollision.Invoke();

            // DestroyedOnTriggered Ȱ��ȭ �������� ������Ʈ �ı�(1ȸ�� KeyCard)
            if (DestroyedOnTriggered)
                Destroy(this);
        }
    }
}
