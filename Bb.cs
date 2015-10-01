namespace BFCalc{
    //TODO: force update
    using System.Collections.Generic;
    using System.Linq;
    using Improved;
    using Newtonsoft.Json;
    using static Improved.ListClass;
    using static System.IO.File;
    using static Data;
    public class BraveBurst{
        [JsonConstructor] public BraveBurst(BbLevel[] levels) {BbLevels=levels;}
        [JsonProperty("desc")] public string Description{get;set;}
        [JsonProperty("hit dmg% distribution")] public int[] HitDmgDistribution{get;set;}
        [JsonProperty("hit dmg% distribution (total)")] public int TotalHitDmgDistribution{get;set;}
        public int Hits{get;set;}
        public int Id{get;set;}
        [JsonProperty("levels")] public BbLevels BbLevels{get;set;}
        [JsonProperty("max bc generated")] public int DropChecks{get;set;}
        public string Name{get;set;}
    }
    public class BbLevel{
        [JsonProperty("bc cost")] public int BcCost{get;set;}
        public Effect[] Effects{get;set;}
        [JsonProperty("max bc generated")] public int Dc{get;set;}
    }
    public class BbLevels{
        public BbLevels(IReadOnlyList<BbLevel> levels){//TODO: check
            var first=levels[0];
            var second=levels[1];
            BcCost=first.BcCost;
            First=first.Effects;
            Change=new Effect[levels[0].Effects.Length];
            for(var i=0;i<levels[0].Effects.Length;i++) Change[i]=second.Effects[i]-first.Effects[i];
            Dc=first.Dc;
        }
        public int BcCost{get;set;}
        public Effect[] First{get;set;}
        public Effect[] Change{get;set;}
        public int Dc{get;set;}
        public static implicit operator BbLevels(BbLevel[] input) =>new BbLevels(input);
    }
    public class EffectDelay{
        public EffectDelay() {}
        [JsonConstructor] public EffectDelay(string s){
            var parts=s.Split('/');
            Ms=decimal.Parse(parts[0]);
            Frames=int.Parse(parts[1]);
        }
        public decimal Ms{get;set;}
        public int Frames{get;set;}
        public static implicit operator EffectDelay(string s) =>new EffectDelay(s);
    }
    public class Range{
        public Range(string s){
            var parts=s.Split('-','~');
            Min=parts[0].ToNum();
            Max=parts[1].ToNum();
        }
        public int Min{get;set;}
        public int Max{get;set;}
        public static implicit operator Range(string s) =>new Range(s);
    }
    public class BbData{
        public static Dict<int,BraveBurst> Bbs{get;set;}
        public static Dict<string,BraveBurst> BbsByName{get;set;}
        public static List<string> BbNames{get;set;}
        public static void Initialise(){
            Bbs=JsonConvert.DeserializeObject<Dict<int,BraveBurst>>(ReadAllText($"{AppData}info.json")
                /*,new JsonSerializerSettings{MissingMemberHandling = MissingMemberHandling.Error}*/);
            BbsByName=Bbs.Values.ToList().ToDict(u=>u.Name,u=>u,s=>$"{s} ");
            BbNames=BbsByName.Keys.ToList();
        }
        public static void Update(MainWindow mw,string name){//TODO: tab
            //var bb=BbsByName[name];
        }
    }
}
