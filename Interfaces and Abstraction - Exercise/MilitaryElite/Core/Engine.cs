using MilitaryElite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite
{
    using IO;
    using Core.Models;
    using Models;
    using Core;
    using System.Linq;
    using Core.Models.Enums;

    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private ICollection<ISoldier> soldiersAll;
        public Engine(IWriter writer, IReader reader) :this()
        {
            this.reader = reader;
            this.writer = writer;
            
        }

        private Engine()
        {
            this.soldiersAll = new HashSet<ISoldier>();
        }
        public void Run()
        {
            string command = string.Empty;
            

            while ((command=reader.Readline())!="End")
            {
                string[] CmdArgs = command
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                // < id > < firstName > < lastName >

                string SoldierType = CmdArgs[0];
                int id = int.Parse(CmdArgs[1]);
                string firstName = CmdArgs[2];
                string lastName = CmdArgs[3];
                decimal salary;

                ISoldier soldier = null;

                switch (SoldierType)
                {
                    case "Private": 
                        
                        salary = decimal.Parse(CmdArgs[4]);
                        soldier = new Private(id, firstName, lastName, salary);
                        //soldiersAll.Add(soldier); - Не го забравяй
                    break;

                    case "LieutenantGeneral":

                        salary = decimal.Parse(CmdArgs[4]);
                        ICollection<IPrivate> privitesFound = AddPRivates(CmdArgs);
                        soldier = new LieutenantGeneral(id, firstName, lastName, salary, privitesFound);

                        break;

                    case "Engineer":
                        
                        salary = decimal.Parse(CmdArgs[4]);
                        string coprsText = CmdArgs[5];
                       
                        bool isCorpsValid = Enum.TryParse(coprsText, false, out Corps corp);


                        if (!isCorpsValid)
                        {
                            continue;
                        }
                        ICollection<IRepairs> repairsCreated = AddRepairs(CmdArgs);
                        soldier = new Engineer(id, firstName, lastName, salary, corp, repairsCreated);

                        break;

                    case "Commando":
                        salary = decimal.Parse(CmdArgs[4]);
                        string corpsText = CmdArgs[5];
                        bool isCorpsValid1 = Enum.TryParse(corpsText, false, out Corps corp1);

                        if (!isCorpsValid1)
                        {
                            continue;
                        }

                        ICollection<IMission> missionsDone = AddMissions(CmdArgs);

                        soldier = new Commando(id, firstName, lastName, salary, corp1, missionsDone);

                        break;

                    case "Spy":
                        int CodeNumber = int.Parse(CmdArgs[4]);
                        soldier = new Spy(id, firstName, lastName, CodeNumber);
                        break;

                    default:
                        break;
                }

                this.soldiersAll.Add(soldier);
            }

            this.prinAllSoldiers();

            
        }

        private ICollection<IPrivate> AddPRivates(string[] cmdArgs)
        {
            int[] privateIDs = cmdArgs.Skip(5).Select(int.Parse).ToArray();

            ICollection<IPrivate> result = new HashSet<IPrivate>();

            foreach (int item in privateIDs)
            {
               result.Add((IPrivate)soldiersAll.FirstOrDefault(x => x.Id == item));

            }

            return result;
        }

        private ICollection<IRepairs> AddRepairs(string[] cmdArgs)
        {
            string[] rep = cmdArgs.Skip(6).ToArray();

            ICollection<IRepairs> result = new HashSet<IRepairs>();

            for (int i = 0; i < rep.Length; i += 2)
            {
                IRepairs repair = new Repairs(rep[i], int.Parse(rep[i + 1]));
                result.Add(repair);
            }

            return result;
        }

        private ICollection<IMission> AddMissions(string[] cmdArgs)
        {
            
            
            ICollection<IMission> result = new HashSet<IMission>();

            string[] missionsInfo = cmdArgs.Skip(6).ToArray();

            for (int i = 0; i < missionsInfo.Length; i +=2)
            {
                string missionStateText = missionsInfo[i + 1];

                bool isValidMissionState = Enum.TryParse(missionStateText, false, out State state);

                if (!isValidMissionState)
                {
                    continue;
                }
                
                IMission mission = new Mission(missionsInfo[i], state);
                result.Add(mission);
            }

            return result;

        }

        private void prinAllSoldiers()
        {
            foreach (ISoldier item in soldiersAll)
            {
                writer.WriteLine(item.ToString());
            }
        }


    }
}
