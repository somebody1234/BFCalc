namespace BFCalc{
    using System.Collections.Generic;
    using System.Linq;
    using Improved;
    using Newtonsoft.Json;
    using static System.IO.File;
    using static Data;
    using static Improved.ListClass;
    public class Evolution{//TODO: put evo in unit?
        [JsonProperty("amount")] public int ZelNeeded{get;set;}
        [JsonProperty("evo")] public EvolveToEvolution EvolvesTo{get;set;}
        public List<EvoMat> Mats{get;set;}
        public IEnumerable<string> MatNames =>Mats?.Select(m=>m.Name); 
        public string Name{get;set;}
        public int Rarity{get;set;}
        public string RarityString =>new string('☆',Rarity);
        public static implicit operator Evolution(Unit u) {return new Evolution(){
            Name=u.Name,
            Rarity=u.Rarity
        };}
    }
    public class EvoMat{
        public int Id;
        public string Name;
    }
    public class EvolveToEvolution{
        public int Id{get;set;}
        public string Name{get;set;}
        public int Rarity{get;set;}
    }
    public static class EvoData{
        public static Dict<int,Evolution> Evos{get;set;}
        public static Dict<string,Evolution> EvosByName{get;set;}
        public static List<string> EvoNames{get;set;}
        public static Evolution CurrentEvo{get;set;}
        public static void Initialise(){
            Evos=JsonConvert.DeserializeObject<Dict<int,Evolution>>(ReadAllText($"{AppData}evo_list.json"));
            EvosByName=Evos.Values.ToList().ToDict(u=>u.Name,u=>u);
            EvoNames=EvosByName.Keys.ToList();
        }//batch name - if(not fire) id-- until (fire), unit.EvolvesTo(name)
    }
}
