using System.Collections;
using UnityEngine;

//���ÿ� ��, ���Ŵ��� ����

public class Dungeon1f : BaseScene
{

    //public DialogueSystem dialog;
    //public PlayerGenerator playerGenerator;
    //public SymbolGenerator symbolGenerator;
    protected override void Awake()
    {
        Debug.Log("Dungeon 1F Scene Init");

        //GameManager.Data.Dungeon.SetUp("Dungeon1f");
        

        //�������ٰ� �������ݴϴ�. 
        GameManager.Pool.Reset();
        GameManager.UI.Reset();

    }

    protected override IEnumerator LoadingRoutine()
    {
     
        //�ʿ��Ѱ� �ε��մϴ�. �߿��Ѱ��� progress ���� �÷���� �ε��̳����ϴ�.
       
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.6f;

    
        yield return new WaitForSecondsRealtime(0.2f);
        progress = 0.8f;

        yield return new WaitForSecondsRealtime(0.2f);
        progress = 1.0f;

        //GameManager.Data.BattleBackGround.Play();
    }


    public override void Clear()
    {

        //���� ������ ���������� �Ѿ�� �ش� �Լ��� ����˴ϴ�.
        //�ʿ��Ѱ� ���ֽø�˴ϴ�.

        //GameManager.Data.BattleBackGround.Stop();

        //GameManager.Data.Dungeon.didBattle = false;

        //GameManager.Data.Dialog.ResetData();
        ////���̾˷α� ������
        ////GameManager.Pool.ReleaseUI(GameManager.Data.Dialog.dialog_obj);

        //GameManager.Pool.erasePoolDicContet(GameManager.Data.Dialog.dialog_obj.name);


        //foreach (var player in GameManager.Data.Dungeon.InBattlePlayers) 
        //{
        //    GameManager.Pool.Release(player);
        //}

        //foreach (var symbol in GameManager.Data.Dungeon.aliveInDungeonSymbols)
        //{
        //    GameManager.Pool.Release(symbol);
        //}

    }
}