﻿using System.Collections.Generic;

public class BattleManager
{
    private static BattleManager _Instance;

    public static BattleManager Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = new BattleManager();
            }

            return _Instance;
        }
    }

    private const bool isVsAi = true;

    private Dictionary<IUnit, BattleUnit> battleListWithPlayer = new Dictionary<IUnit, BattleUnit>();

    private IUnit lastPlayer = null;
    
    public void PlayerEnter(IUnit _playerUnit)
    {
        if (battleListWithPlayer.ContainsKey(_playerUnit))
        {
            battleListWithPlayer[_playerUnit].RefreshData(_playerUnit);
        }
        else if(_playerUnit == lastPlayer)
        {

        }
        else
        {
            if (lastPlayer == null && !isVsAi)
            {
                lastPlayer = _playerUnit;
            }
            else
            {
                BattleUnit unit = new BattleUnit();

                List<int> mCards = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

                List<int> oCards = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

                unit.Init(_playerUnit, lastPlayer, mCards, oCards, 1, isVsAi);

                battleListWithPlayer.Add(_playerUnit, unit);

                if (lastPlayer != null)
                {
                    battleListWithPlayer.Add(lastPlayer, unit);

                    lastPlayer = null;
                }
            }
        }
    }

    public void ReceiveData(PlayerUnit _playerUnit,byte[] _bytes)
    {
        battleListWithPlayer[_playerUnit].ReceiveData(_playerUnit, _bytes);
    }
}

