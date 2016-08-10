﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FinalWar;

namespace FinalWar_server
{
    class Program
    {
        private static void WriteLog(string _str)
        {
            Console.WriteLine(_str);
        }

        static void Main(string[] args)
        {
            Log.Init(WriteLog);

            ConfigDictionary.Instance.LoadLocalConfig("local.xml");

            StaticData.path = ConfigDictionary.Instance.table_path;

            StaticData.Load<MapSDS>("map");

            Map.Init();

            StaticData.Load<HeroSDS>("hero");

            Dictionary<int, HeroSDS> heroDic = StaticData.GetDic<HeroSDS>();

            Dictionary<int, IHeroSDS> newHeroDic = new Dictionary<int, IHeroSDS>();

            foreach(KeyValuePair<int,HeroSDS> pair in heroDic)
            {
                newHeroDic.Add(pair.Key, pair.Value);
            }
            Battle.Init(newHeroDic, Map.mapDataDic);

            Server<PlayerUnit> server = new Server<PlayerUnit>();

            server.Start("0.0.0.0", 1983, 100);

            while (true)
            {
                server.Update();

                Thread.Sleep(10);
            }
        }
    }
}
