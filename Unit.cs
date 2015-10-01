namespace BFCalc{
    //TODO: force update
    using System.Collections.Generic;
    using System.Linq;
    using Improved;
    using Newtonsoft.Json;
    using static Improved.ListClass;
    using static System.IO.File;
    using static Data;
    using static Enums;
    using static Stat;
    public class Unit{
        public Decision[] Ai{get;set;}
        public BraveBurst Bb{get;set;}
        public BraveBurst Sbb{get;set;}
        public BraveBurst Ubb{get;set;}
        public int Category{get;set;}//TODO: rearrange
        public int Cost{get;set;}
        public Element Element{get;set;}
        [JsonProperty("exp_pattern")] public int ExpPattern{get;set;}
        public Gender Gender{get;set;}
        [JsonProperty("getting type")] public GettingType GettingType{get;set;}
        [JsonProperty("guide_id")] public int GuideId{get;set;}
        [JsonProperty("hit dmg% distribution")] public int[] HitDmgDistribution{get;set;}
        [JsonProperty("hit dmg% distribution (total)")] public int TotalHitDmgDistribution{get;set;}
        public int Hits{get;set;}
        public int Id{get;set;}
        [JsonProperty("imp")] public ImpCaps ImpCaps{get;set;}
        public Kind Kind{get;set;}
        [JsonProperty("leader skill")] public Skill Ls{get;set;}
        [JsonProperty("extra skill")] public Skill Es{get;set;}
        [JsonProperty("lord damage range")] public Range LordDmgRange{get;set;}
        [JsonProperty("max bc generated")] public int Dc{get;set;}
        public string Name{get;set;}
        public int Rarity{get;set;}
        [JsonProperty("sell caution")] public bool SellCaution{get;set;}
        public TypeStats Stats{get;set;}
    }
    public enum Kind{
        Normal,
        Evo,
        Enhancing,
        Sale
    }
    public class TypeStats{
        [JsonProperty("_base")] public Stats Base{get;set;}
        [JsonProperty("_lord")] public Stats Lord{get;set;}
        public Stats Anima{get;set;}
        public Stats Breaker{get;set;}
        public Stats Guardian{get;set;}
        public Stats Oracle{get;set;}
    }
    public class Stats{
        public int Atk{get;set;}
        public int Def{get;set;}
        public int Hp{get;set;}
        public int Rec{get;set;}
        [JsonProperty("atk max")] public int AtkMax{get;set;}
        [JsonProperty("def max")] public int DefMax{get;set;}
        [JsonProperty("hp max")] public int HpMax{get;set;}
        [JsonProperty("rec max")] public int RecMax{get;set;}
        [JsonProperty("atk min")] public int AtkMin{get;set;}
        [JsonProperty("def min")] public int DefMin{get;set;}
        [JsonProperty("hp min")] public int HpMin{get;set;}
        [JsonProperty("rec min")] public int RecMin{get;set;}
    }
    public class ImpCaps{
        [JsonProperty("max atk")] public int MaxAtk{get;set;}
        [JsonProperty("max def")] public int MaxDef{get;set;}
        [JsonProperty("max hp")] public int MaxHp{get;set;}
        [JsonProperty("max rec")] public int MaxRec{get;set;}
    }
    public class Skill{
        [JsonProperty("desc")] public string Description{get;set;}
        public Effect[] Effects{get;set;}
        public List<string> EffectStrings =>Effects[0].GetValues();//TODO: 2nd and 3rd
        public int Id{get;set;}
        public string Name{get;set;}
        public TargetType Target{get;set;}
    }
    public class Decision{
        public Action Action{get;set;}
        [JsonProperty("chance%")] public decimal Chance{get;set;}
        [JsonProperty("target conditions")] public TargetConditions TargetConditions{get;set;}
        [JsonProperty("target type")] public TargetType TargetType{get;set;}
    }
    public enum Action{
        Skill,
        Attack
    }
    public enum Stat{
        Random,
        Guard,
        Weakness,
        Paralysis,
        Poison,
        Sickness,
        Injury,
        Curse,
        StupBuff,
        Non,
        [EnumMember("elem")] Element,
        Atk,
        Def,
        Hp,
        Rec,
        Bb
    }
    public enum Comparison{
        Min,
        Max,
        Under,
        Over
    }
    public enum BbType{
        Attack,
        Heal,
        Support
    }
    public class TargetConditions{
        public TargetConditions(string s){
            var parts=s.Split('_');
            Stat=Parse<Stat>(parts[0])??Stat;
            switch(Stat){
                case Random:
                case Guard:
                case Non:
                case Weakness:
                case Paralysis:
                case Poison:
                case Sickness:
                case Injury:
                case Curse:
                    return;
                case Stat.Element:
                    Element=Parse<Element>(parts[1])??Element;
                    return;
            }
            var percentText=parts[1].Replace("pr","");
            int percent;
            if(int.TryParse(percentText,out percent)){
                Percent=percent;
                Comparison=Parse<Comparison>(parts[2])??Comparison;
            } else{
                Comparison=Parse<Comparison>(parts[1])??Comparison;
                BbType=Parse<BbType>(parts[1])??BbType;
            }
        }
        public Stat Stat{get;set;}
        public int Percent{get;set;}
        public Comparison Comparison{get;set;}
        public BbType BbType{get;set;}
        public Element Element{get;set;}
        public static implicit operator TargetConditions(string s) =>new TargetConditions(s);
    }
    public enum Gender{
        Male,
        Female,
        Genderless,
        Other
    }
    [JsonConverter(typeof(EnumConverter<GettingType>))] public enum GettingType{
        NotEligibleForAchievement,
        Farmable,
        RareSummon
    }
    public enum Attribute{
        Hp,
        Attack,
        Defense,
        Recovery
    }
    public static class UnitData{
        public static Dict<int,Unit> Units{get;set;}
        public static Dict<string,Unit> UnitsByName{get;set;}
        public static List<string> UnitNames{get;set;}
        public static List<string> LsEs=new List<string>(){"Leader Skill","Extra Skill"};
        public static List<string> BbType=new List<string>{"Brave Burst","Super Brave Burst","Ultimate Brave Burst"};
        public static Unit CurrentUnit{get;set;}
        public static Skill CurrentSkill{get;set;}
        public static void Initialise(){
            Units=JsonConvert.DeserializeObject<Dict<int,Unit>>(ReadAllText($"{AppData}info.json")
                /*,new JsonSerializerSettings{MissingMemberHandling = MissingMemberHandling.Error}*/);
            UnitsByName=Units.Values.ToList().ToDict(u=>u.Name,u=>u,s=>$"{s} ");
            UnitNames=UnitsByName.Keys.ToList();
        }
        public static void Update(MainWindow mw,string name){//TODO: binary search if possible
            //var unit=UnitsByName[name];
        }
    }
}
