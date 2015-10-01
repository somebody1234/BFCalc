namespace BFCalc{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Improved;
    using Newtonsoft.Json;
    using static System.IO.File;
    using static Data;
    using static Improved.ListClass;
    using static Improved.Lists;
    public enum Type:byte{
        Material,
        Consumable,
        Sphere,
        EvoMat
    }
    public enum TargetArea:byte{
        Single,
        AoE
    }
    public enum TargetType:byte{
        Self,
        Party,
        Enemy
    }
    [JsonConverter(typeof(EnumConverter<Ailment>))] public enum Ailment:byte{
        Poison,
        Weaken,
        Sick,
        Injury,
        Curse,
        Paralysis,
        AtkDown,
        DefDown,
        RecDown
    }
    public enum Element:byte{
        Fire,
        Earth,
        Thunder,
        Water,
        Light,
        Dark,
        All
    }
    [JsonConverter(typeof(EnumConverter<SphereType>))] public enum SphereType:byte{
        StatusBoost,
        Critical,
        Drop,
        StatusAilment,
        StatusAilmentsResistant,
        BBGauge,
        HPRecovery,
        ExposeTarget,
        DamageReflecting,
        DamageReducing,
        Spark,
        DefensePenetrating
    }
    public class Item{
        [JsonProperty("desc")] public string Description{get;set;}
        public int Id{get;set;}
        [JsonProperty("max equipped")] public int MaxOnQuest{get;set;}
        [JsonProperty("max_stack")] public int MaxStack{get;set;}
        public string Name{get;set;}
        public bool Raid{get;set;}
        public int Rarity{get;set;}
        [JsonProperty("sell_price")] public int SellPrice{get;set;}
        [JsonProperty("sphere type")] public int SphereTypeId{get;set;}
        [JsonProperty("sphere type text")] public SphereType SphereType{get;set;}
        public string Thumbnail{get;set;}
        public Type Type{get;set;}
        public MainEffect Effect{get;set;}
        public Effect[] Effects{get;set;}
        public int MaxEquipped{get;set;}
        public Recipe Recipe{get;set;}
        public IEnumerable<string> MaterialStrings =>Recipe?.Materials.Select(m=>$"{m.Count}\t{ItemData.Items[m.Id].Name}");
        public List<string> UsedIn{get;set;} 
    }
    public class MainEffect{
        public Effect[] Effect{get;set;}
        [JsonProperty("target_area")] public TargetArea TargetArea{get;set;}
        [JsonProperty("target_type")] public TargetType TargetType{get;set;}
    }
    public class Conditions{
        [JsonProperty("item required")] public int[] ItemIds{get;set;}
        [JsonProperty("unit required")] public MiniUnit[] UnitIds{get;set;}
    }
    public class MiniUnit{
        public int Id{get;set;}
        public string Name{get;set;}
    }
    public class Effect{
        [JsonProperty("bb atk%")] public int BbAtk{get;set;}
        [JsonProperty("bb flat atk")] public int BbFlatAtk{get;set;}
        [JsonProperty("bb crit%")] public int BbCritChance{get;set;}
        [JsonProperty("bb bc%")] public int BbBc{get;set;}
        [JsonProperty("bb hc%")] public int BbHc{get;set;}
        [JsonProperty("bb elements")] public Element[] BbElements{get;set;}
        [JsonProperty("hits")] public int Hits{get;set;}
        [JsonProperty("random attack")] public bool RandomAttack{get;set;}
        [JsonProperty("conditions")] public Conditions[] Conditions{get;set;}
        [JsonProperty("buff")] public Effect Buff{get;set;}
        [JsonProperty("buff #1")] public Effect Buff1{get;set;}
        [JsonProperty("buff #2")] public Effect Buff2{get;set;}
        [JsonProperty("trigger on bb")] public bool TriggerOnBb{get;set;}
        [JsonProperty("trigger on sbb")] public bool TriggerOnSbb{get;set;}
        [JsonProperty("trigger on ubb")] public bool TriggerOnUbb{get;set;}
        [JsonProperty("triggered effect")] public Effect[] TriggeredEffects{get;set;}
        [JsonProperty("effect delay time(ms)/frame")] public EffectDelay EffectDelay{get;set;}
        [JsonProperty("heal high")] public int HealMax{get;set;}
        [JsonProperty("heal low")] public int HealMin{get;set;}
        [JsonProperty("proc chance%")] public decimal ProcChance{get;set;}
        [JsonProperty("proc id")] public int ProcId{get;set;}
        [JsonProperty("unknown proc id")] public int UnknownProcId{get;set;}
        [JsonProperty("unknown proc param")] public ProcParam UnknownProcParam{get;set;}
        [JsonProperty("rec added% (from healer)")] public decimal HealerRec{get;set;}
        [JsonProperty("rec added% (from target)")] public decimal TargetRec{get;set;}
        [JsonProperty("remove poison")] public bool RemovePoison{get;set;}
        [JsonProperty("remove injury")] public bool RemoveInjury{get;set;}
        [JsonProperty("remove sick")] public bool RemoveSick{get;set;}
        [JsonProperty("remove weaken")] public bool RemoveWeaken{get;set;}
        [JsonProperty("remove curse")] public bool RemoveCurse{get;set;}
        [JsonProperty("remove paralysis")] public bool RemoveParalysis{get;set;}
        [JsonProperty("ailments cured")] public Ailment[] AilmentsCured{get;set;}
        [JsonProperty("atk% buff")] public int MaxAtkUp{get;set;}
        [JsonProperty("def% buff")] public int MaxDefUp{get;set;}
        [JsonProperty("rec% buff")] public int MaxRecUp{get;set;}
        [JsonProperty("hp% buff")] public int MaxHpUp{get;set;}
        [JsonProperty("crit% buff")] public int CritUp{get;set;}
        [JsonProperty("poison%")] public int Poison{get;set;}
        [JsonProperty("weaken%")] public int Weaken{get;set;}
        [JsonProperty("sick%")] public int Sick{get;set;}
        [JsonProperty("injury%")] public int Injury{get;set;}
        [JsonProperty("curse%")] public int Curse{get;set;}
        [JsonProperty("paralysis%")] public int Paralysis{get;set;}
        [JsonProperty("dmg reduction chance%")] public int DmgReduceChance{get;set;}
        [JsonProperty("dmg reduction%")] public int DmgReduce{get;set;}
        [JsonProperty("poison resist%")] public int PoisonResist{get;set;}
        [JsonProperty("weaken resist%")] public int WeakenResist{get;set;}
        [JsonProperty("sick resist%")] public int SickResist{get;set;}
        [JsonProperty("injury resist%")] public int InjuryResist{get;set;}
        [JsonProperty("curse resist%")] public int CurseResist{get;set;}
        [JsonProperty("paralysis resist%")] public int ParalysisResist{get;set;}
        [JsonProperty("poison% buff")] public int PoisonOnAtk{get;set;}
        [JsonProperty("weaken% buff")] public int WeakenOnAtk{get;set;}
        [JsonProperty("sick% buff")] public int SickOnAtk{get;set;}
        [JsonProperty("injury% buff")] public int InjuryOnAtk{get;set;}
        [JsonProperty("curse% buff")] public int CurseOnAtk{get;set;}
        [JsonProperty("paralysis% buff")] public int ParalysisOnAtk{get;set;}
        [JsonProperty("increase bb gauge gradual")] public int BcFillGradual{get;set;}
        [JsonProperty("hp below % buff activation")] public int AngelIdolMaxHp{get;set;}
        [JsonProperty("angel idol recover hp%")] public int AngelIdolHpRecover{get;set;}
        [JsonProperty("gradual heal high")] public int GradualHealHigh{get;set;}
        [JsonProperty("gradual heal low")] public int GradualHealLow{get;set;}
        [JsonProperty("dmg% mitigation")] public int SkillMit{get;set;}
        [JsonProperty("dmg% reduction")] public int EffectMit{get;set;}
        [JsonProperty("bc fill when attacked high")] public int BcAtkedMax{get;set;}
        [JsonProperty("bc fill when attacked low")] public int BcAtkedMin{get;set;}
        [JsonProperty("bc fill when attacked%")] public int BcAtked{get;set;}
        [JsonProperty("defense% ignore")] public int DefenseIgnore{get;set;}
        [JsonProperty("% converted turns")] public int AttConvertTurns{get;set;}
        [JsonProperty("converted attribute")] public Attribute ConvertedAtt{get;set;}
        [JsonProperty("extra hits dmg%")] public int ExtraHitDmg{get;set;}
        [JsonProperty("hit increase/hit")] public int ExtraHitsPerHit{get;set;}
        [JsonProperty("dot atk%")] public int DotAtk{get;set;}
        [JsonProperty("dot element affected")] public bool DotElemAffected{get;set;}
        [JsonProperty("dot flat atk")] public int DotFlatAtk{get;set;}
        [JsonProperty("dot unit index")] public bool DotUnitIndex{get;set;}
        [JsonProperty("stat% debuff turns")] public int StatDownTurns{get;set;}
        [JsonProperty("inflict atk% debuff (2)")] public int AtkDownOnAtk{get;set;}
        [JsonProperty("bb gauge fill rate% buff")] public decimal BbRateUpBuff{get;set;}//TODO one is ls right?
        [JsonProperty("counter inflict ailment turns")] public int AilmentWhenAtkedTurns{get;set;}
        [JsonProperty("crit multiplier%")] public decimal CritMultiplier{get;set;}
        [JsonProperty("angel idol recover chance%")] public int AngelIdolChance{get;set;}
        [JsonProperty("shield def")] public int ShieldDef{get;set;}
        [JsonProperty("shield element")] public Element ShieldElement{get;set;}
        [JsonProperty("shield hp")] public int ShieldHp{get;set;}
        [JsonProperty("first x turns atk% (1)")] public int FirstXTurnsAtk{get;set;}
        [JsonProperty("first x turns def% (3)")] public int FirstXTurnsDef{get;set;}
        [JsonProperty("first x turns rec% (5)")] public int FirstXTurnsRec{get;set;}
        [JsonProperty("first x turns crit% (7)")] public int FirstXTurnsCrit{get;set;}
        [JsonProperty("buff turns (12)")] public int AngelIdolUpTurns{get;set;}//TODO: -1=forever
        [JsonProperty("atk% buff (1)")] public int AtkUpSingle{get;set;}
        [JsonProperty("atk% buff (2)")] public int AtkUpEnemy{get;set;}
        [JsonProperty("def% buff (3)")] public int DefUpSingle{get;set;}
        [JsonProperty("def% buff (4)")] public int DefUpEnemy{get;set;}
        [JsonProperty("rec% buff (5)")] public int RecUpSingle{get;set;}
        [JsonProperty("rec% buff (6)")] public int RecUpEnemy{get;set;}
        [JsonProperty("crit% buff (7)")] public int CritUpSingle{get;set;}
        [JsonProperty("gradual heal turns (8)")] public int GradualHealTurns{get;set;}
        [JsonProperty("hc drop rate% buff (9)")] public int HcDropRateUp{get;set;}
        [JsonProperty("bc drop rate% buff (10)")] public int BcDropRateUp{get;set;}
        [JsonProperty("item drop rate% buff (11)")] public int ItemDropRateUp{get;set;}
        [JsonProperty("angel idol buff (12)")] public bool AngelIdolUp{get;set;}
        [JsonProperty("atk% buff (13)")] public int AtkUpElement{get;set;}
        [JsonProperty("def% buff (14)")] public int DefUpElement{get;set;}
        [JsonProperty("rec% buff (15)")] public int RecUpElement{get;set;}
        [JsonProperty("mitigate fire attacks (21)")] public int MitFireAtkPct{get;set;}
        [JsonProperty("mitigate water attacks (22)")] public int MitWaterAtkPct{get;set;}
        [JsonProperty("mitigate earth attacks (23)")] public int MitEarthAtkPct{get;set;}
        [JsonProperty("mitigate thunder attacks (24)")] public int MitThunderAtkPct{get;set;}
        [JsonProperty("mitigate light attacks (25)")] public int MitLightAtkPct{get;set;}
        [JsonProperty("mitigate dark attacks (26)")] public int MitDarkAtkPct{get;set;}
        [JsonProperty("resist poison% (30)")] public decimal PoisonResistTimed{get;set;}
        [JsonProperty("resist weaken% (31)")] public decimal WeakenResistTimed{get;set;}
        [JsonProperty("resist sick% (32)")] public decimal SickResistTimed{get;set;}
        [JsonProperty("resist injury% (33)")] public decimal InjuryResistTimed{get;set;}
        [JsonProperty("resist curse% (34)")] public decimal CurseResistTimed{get;set;}
        [JsonProperty("resist paralysis% (35)")] public decimal ParalysisResistTimed{get;set;}
        [JsonProperty("dmg% reduction turns (36)")] public int MitTurns{get;set;}
        [JsonProperty("increase bb gauge gradual turns (37)")] public int BcFillGradualTurns{get;set;}
        [JsonProperty("bc fill when attacked turns (38)")] public int BcWhenAttacked{get;set;}
        [JsonProperty("defense% ignore turns (39)")] public int DefenseIgnoreTurns{get;set;}
        [JsonProperty("spark dmg% buff (40)")] public int SparkDmgUp{get;set;}
        //TODO: move paren stuff to just before parens
        [JsonProperty("atk% buff (46)")] public int AtkConvertUp{get;set;}
        [JsonProperty("def% buff (47)")] public int DefConvertUp{get;set;}
        [JsonProperty("rec% buff (48)")] public int RecConvertUp{get;set;}
        [JsonProperty("hit increase buff turns (50)")] public int HitIncreaseUpTurns{get;set;}
        [JsonProperty("dot turns (71)")] public int DotTurns{get;set;}
        [JsonProperty("buff turns (72)")] public int AtkUpTurns{get;set;}
        [JsonProperty("inflict atk% debuff chance% (74)")] public decimal AtkDownChance{get;set;}
        [JsonProperty("buff turns (77)")] public int BbRateUpTurns{get;set;}
        [JsonProperty("counter inflict poison% (78)")] public int PoisonWhenAtked{get;set;}
        [JsonProperty("counter inflict weaken% (79)")] public int WeakenWhenAtked{get;set;}
        [JsonProperty("counter inflict sick% (80)")] public int SickWhenAtked{get;set;}
        [JsonProperty("counter inflict injury% (81)")] public int InjuryWhenAtked{get;set;}
        [JsonProperty("counter inflict curse% (82)")] public int CurseWhenAtked{get;set;}
        [JsonProperty("counter inflict paralysis% (83)")] public int ParalysisWhenAtked{get;set;}
        [JsonProperty("buff turns (84)")] public int CritMultiplierUpTurns{get;set;}
        [JsonProperty("angel idol buff turns (91)")] public int PossibleAngelIdolTurns{get;set;}
        [JsonProperty("atk% buff (100)")] public int RaidAtkUp{get;set;}
        [JsonProperty("taunt turns (10000)")] public int TauntTurns{get;set;}
        [JsonProperty("stealth turns (10001)")] public int StealthTurns{get;set;}
        [JsonProperty("shield turns (10002)")] public int ShieldTurns{get;set;}
        [JsonProperty("resist status ails turns")] public int PreventTurns{get;set;}
        [JsonProperty("drop rate buff turns")] public int DropUpTurns{get;set;}
        [JsonProperty("buff turns")] public int UpTurns{get;set;}
        [JsonProperty("element buffed")] public Element ElementUped{get;set;}
        [JsonProperty("elements buffed")] public Element[] ElementsUped{get;set;}
        [JsonProperty("revive to hp%")] public int ReviveToHp{get;set;}
        [JsonProperty("increase od gauge%")] public decimal OdIncrease{get;set;}
        [JsonProperty("passive id")] public string PassiveId{get;set;}
        [JsonProperty("first x turns")] public int FirstXTurns{get;set;}
        [JsonProperty("hp below % buff requirement")] public int MaxHp{get;set;}
        [JsonProperty("hp above % buff requirement")] public int MinHp{get;set;}
        [JsonProperty("sphere type text")] public SphereType SphereTypeText{get;set;}
        [JsonProperty("bc fill on enemy defeat high")] public int BcOnKillMax{get;set;}
        [JsonProperty("bc fill on enemy defeat low")] public int BcOnKillMin{get;set;}
        [JsonProperty("bc fill on enemy defeat%")] public int BcOnKillChance{get;set;}
        [JsonProperty("bc fill when attacking high")] public int BcFillAtkMax{get;set;}
        [JsonProperty("bc fill when attacking low")] public int BcAtkMin{get;set;}
        [JsonProperty("bc fill when attacking%")] public int BcAtk{get;set;}
        [JsonProperty("bc fill on spark high")] public int SparkMaxBc{get;set;}
        [JsonProperty("bc fill on spark low")] public int SparkMinBc{get;set;}
        [JsonProperty("bc fill on spark%")] public decimal SparkBcChance{get;set;}
        [JsonProperty("bc fill on crit max")] public int CritMaxBc{get;set;}//TODO: tell DM high/low, not max/min
        [JsonProperty("bc fill on crit min")] public int CritMinBc{get;set;}
        [JsonProperty("bc fill on crit%")] public decimal CritBcChance{get;set;}
        [JsonProperty("hp% recover on battle win high")] public int HpRecBattleMax{get;set;}
        [JsonProperty("hp% recover on battle win low")] public int HpRecBattleMin{get;set;}
        [JsonProperty("battle end bc fill high")] public int BattleBcMax{get;set;}
        [JsonProperty("battle end bc fill low")] public int BattleBcMin{get;set;}
        [JsonProperty("hp% recover on enemy defeat high")] public int HpOnKillMax{get;set;}
        [JsonProperty("hp% recover on enemy defeat low")] public int HpOnKillMin{get;set;}
        [JsonProperty("hp drain chance%")] public int HpDrainChance{get;set;}
        [JsonProperty("hp drain% high")] public int HpDrainMax{get;set;}
        [JsonProperty("hp drain% low")] public int HpDrainMin{get;set;}
        [JsonProperty("hc drop rate% buff")] public int HcRate{get;set;}
        [JsonProperty("zel drop rate% buff")] public int ZelRate{get;set;}
        [JsonProperty("karma drop rate% buff")] public int KarmaRate{get;set;}
        [JsonProperty("item drop rate% buff")] public int ItemRate{get;set;}
        [JsonProperty("bc drop rate% buff")] public int BcRate{get;set;}
        [JsonProperty("dmg% to hp% when attacked chance%")] public int DmgToHpAtkedChance{get;set;}
        [JsonProperty("dmg% to hp% when attacked high")] public int DmgToHpMax{get;set;}
        [JsonProperty("dmg% to hp% when attacked low")] public int DmgToHpMin{get;set;}
        [JsonProperty("hp below % passive requirement")] public int MaxHpPassive{get;set;}
        [JsonProperty("target% chance")] public int TargetChance{get;set;}
        [JsonProperty("dmg% reflect chance%")] public int ReflectChance{get;set;}
        [JsonProperty("dmg% reflect high")] public int ReflectMax{get;set;}
        [JsonProperty("dmg% reflect low")] public int ReflectMin{get;set;}
        [JsonProperty("damage% for spark")] public int DamagePctForSpark{get;set;}
        [JsonProperty("ignore def%")] public int IgnoreDefChance{get;set;}
        [JsonProperty("bb gauge above % buff requirement")] public int MinBb{get;set;}
        [JsonProperty("bb gauge below % buff requirement")] public int MaxBb{get;set;}
        [JsonProperty("target area")] public TargetArea TargetArea{get;set;}
        [JsonProperty("target type")] public TargetType TargetType{get;set;}
        [JsonProperty("fire units do extra elemental weakness dmg")] public bool FireDoElemWeak{get;set;}
        [JsonProperty("water units do extra elemental weakness dmg")] public bool WaterDoElemWeak{get;set;}
        [JsonProperty("earth units do extra elemental weakness dmg")] public bool EarthDoElemWeak{get;set;}
        [JsonProperty("thunder units do extra elemental weakness dmg")] public bool ThunderDoElemWeak{get;set;}
        [JsonProperty("light units do extra elemental weakness dmg")] public bool LightDoElemWeak{get;set;}
        [JsonProperty("dark units do extra elemental weakness dmg")] public bool DarkDoElemWeak{get;set;}
        [JsonProperty("elemental weakness multiplier%")] public decimal ElemWeakMultiplier{get;set;}
        [JsonProperty("elemental weakness buff turns")] public int ElemWeakUpTurns{get;set;}
        [JsonProperty("crit chance base resist%")] public int CritResist{get;set;}
        [JsonProperty("crit chance buffed resist%")] public int UpedCritResist{get;set;}
        [JsonProperty("strong base element damage resist%")] public int ElemWeakResist{get;set;}
        [JsonProperty("strong buffed element damage resist%")] public int UpedElemWeakResist{get;set;}
        [JsonProperty("fire resist%")] public int FireResist{get;set;}
        [JsonProperty("water resist%")] public int WaterResist{get;set;}
        [JsonProperty("earth resist%")] public int EarthResist{get;set;}
        [JsonProperty("thunder resist%")] public int ThunderResist{get;set;}
        [JsonProperty("light resist%")] public int LightResist{get;set;}
        [JsonProperty("dark resist%")] public int DarkResist{get;set;}
        [JsonProperty("bc drop% for spark")] public int SparkBcUp{get;set;}
        [JsonProperty("hc drop% for spark")] public int SparkHcUp{get;set;}
        [JsonProperty("zel drop% for spark")] public int SparkZelUp{get;set;}
        [JsonProperty("karma drop% for spark")] public int SparkKarmaUp{get;set;}
        [JsonProperty("hc effectiveness%")] public int HcPowerUp{get;set;}
        [JsonProperty("bc fill per turn")] public int BcPerTurn{get;set;}
        [JsonProperty("bb atk% buff")] public int BbAtkUp{get;set;}
        [JsonProperty("sbb atk% buff")] public int SbbAtkUp{get;set;}
        [JsonProperty("ubb atk% buff")] public int UbbAtkUp{get;set;}
        [JsonProperty("increase bb gauge")] public int BcFilll{get;set;}
        [JsonProperty("gender required")] public Gender GenderRequired{get;set;}
        [JsonProperty("elements added")] public Element[] ElementsAdded{get;set;}
        [JsonProperty("elements added turns")] public int ElementsAdddTurns{get;set;}
        [JsonProperty("unique elements required")] public int AllyUniqueElemsReq{get;set;}
        //TODO: in squadbuilder check for five lights/6 lights, damage calc - SBB effects etc
        [JsonProperty("bb gauge fill rate%")] public int BbRateUp{get;set;}
        [JsonProperty("reduced bb bc cost%")] public int BcNeededDown{get;set;}
        [JsonProperty("reduced bb bc use chance%")] public int BcSavedChance{get;set;}
        [JsonProperty("reduced bb bc use% high")] public int BcSavedMaxPct{get;set;}
        [JsonProperty("reduced bb bc use% low")] public int BcSavedMinPct{get;set;}
        [JsonProperty("bb bc fill%")] public int BbFill{get;set;}
        [JsonProperty("bb bc fill")] public int BcFill{get;set;}
        [JsonProperty("dmg% mitigation for elemental attacks")] public decimal ElemMit{get;set;}
        [JsonProperty("mitigate fire attacks")] public bool MitFireAtks{get;set;}
        [JsonProperty("mitigate water attacks")] public bool MitWaterAtks{get;set;}
        [JsonProperty("mitigate earth attacks")] public bool MitEarthAtks{get;set;}
        [JsonProperty("mitigate thunder attacks")] public bool MitThunderAtks{get;set;}
        [JsonProperty("mitigate light attacks")] public bool MitLightAtks{get;set;}
        [JsonProperty("mitigate dark attacks")] public bool MitDarkAtks{get;set;}
        [JsonProperty("rec% added (turn heal)")] public decimal HealPerTurn{get;set;}
        [JsonProperty("turn heal high")] public int HealPerTurnMax{get;set;}
        [JsonProperty("turn heal low")] public int HealPerTurnMin{get;set;}
        [JsonProperty("hp% damage chance%")] public int HpPctDmgChance{get;set;}
        [JsonProperty("hp% damage high")] public int HpPctDmgHigh{get;set;}
        [JsonProperty("hp% damage low")] public int HpPctDmgLow{get;set;}
        [JsonProperty("take 1 dmg%")] public int FullMit{get;set;}
        [JsonProperty("base crit% resist")] public int BaseCritResist{get;set;}
        [JsonProperty("buff crit% resist")] public int UpCritResist{get;set;}
        [JsonProperty("atk% base buff")] public int AtkBaseUp{get;set;}
        [JsonProperty("def% base buff")] public int DefBaseUp{get;set;}
        [JsonProperty("rec% base buff")] public int RecBaseUp{get;set;}
        [JsonProperty("atk% extra buff based on hp")] public int AtkUpAtFullHp{get;set;}
        [JsonProperty("def% extra buff based on hp")] public int DefUpAtFullHp{get;set;}
        [JsonProperty("rec% extra buff based on hp")] public int RecUpAtFullHp{get;set;}
        [JsonProperty("buff proportional to hp")] public UpBasedOnHp UpBasedOnHp{get;set;}
        [JsonProperty("bb base atk%")] public int BbBaseAtk{get;set;}
        [JsonProperty("bb added atk% based on hp")] public int BbAddedAtkBasedOnHp{get;set;}
        [JsonProperty("bb added atk% proportional to hp")] public UpBasedOnHp BbUpBasedOnHp{get;set;}
        [JsonProperty("bc filled when attacked while guarded")] public int BcWhenAtkedOnGuard{get;set;}
        [JsonProperty("guard increase mitigation%")] public int MitOnGuard{get;set;}
        [JsonProperty("max hp% increase")] public decimal MaxHpIncrease{get;set;}
        [JsonProperty("remove all status ailments")] public bool Cleanse{get;set;}
        [JsonProperty("bc filled on guard")] public int BcOnGuard{get;set;}//TODO: regions
        [JsonProperty("buff timer (seconds)")] public int Seconds{get;set;}
        public static Effect operator-(Effect left,Effect right){
            //TODO: double click goes to Effects tab, which disables clicking on mainwindow, metal/jewel tab, squad tab, animation tab with test dummy
            var result=new Effect();
            foreach(var p in typeof(Effect).GetProperties()){
                dynamic difference=0;
                if(p.PropertyType==typeof(int)) difference=((int)p.GetValue(left))-((int)p.GetValue(right));
                else if(p.PropertyType==typeof(decimal)) difference=((decimal)p.GetValue(left))-((decimal)p.GetValue(right));
                if(difference!=0) p.SetValue(result,difference);
            }//TODO: IIsNotDefault
            return result;
        }
        public List<string> GetValues(){
            var result=new List<string>();
            foreach(var p in typeof(Effect).GetProperties()){
                var value=p.GetValue(this);
                if(p.PropertyType.IsEnum){
                    if(p.GetValue(this)!=
                        p.PropertyType.InvokeMember(null,
                            System.Reflection.BindingFlags.CreateInstance,
                            null,
                            null,
                            null,
                            null)) result.Add($"{p.Name}\t{value}");
                    continue;
                }
                if(p.PropertyType.IsArray) continue;//TODO
                var notDefault=ItemData.IsNotDefault[p.PropertyType](value);
                if(notDefault.Item1)result.Add($"{p.Name}\t{notDefault.Item2}");
            }
            return result;
        }
    }
    public class ProcParam{
        public ProcParam(string s){
            var nums=s.Split(',');
            Params=new int[nums.Length];
            for(var n=0;n<nums.Length;n++) int.TryParse(nums[n],out Params[n]);
        }
        public int[] Params{get;set;}
        public static implicit operator ProcParam(string s) =>new ProcParam(s);
    }//TODO:temporary unknown param system - reflection, new window, submit, Dict<int,List<string>>
    public enum UpBasedOnHp{
        Lost,
        Remaining
    }
    public class Recipe{
        [JsonProperty("karma")] public string Karma{get;set;}
        [JsonProperty("materials")] public List<Material> Materials{get;set;}
    }
    public class Material{
        [JsonProperty("count")] public int Count{get;set;}
        [JsonProperty("id")] public int Id{get;set;}
    }
    public class ItemData{
        public static Dict<int,Item> Items{get;set;}
        public static Dict<string,Item> ItemsByName{get;set;}
        public static List<string> ItemNames{get;set;}
        public static Dict<int,Item> Craftables{get;set;}
        public static Dict<string,Item> CraftablesByName{get;set;}
        public static List<string> CraftableNames{get;set;}
        public static Item CurrentItem{get;set;}
        public static Dict<System.Type,Func<object,Tuple<bool,string>>> IsNotDefault=new Dict<System.Type,Func<object,Tuple<bool,string>>>(){
            [typeof(int)]=o=>Tuple((int)o!=0,((int)o).ToString()),
            [typeof(decimal)]=o=>Tuple((decimal)o!=0,((decimal)o).ToString(CultureInfo.InvariantCulture)),
            [typeof(bool)]=o=>Tuple((bool)o,"true"),
            [typeof(string)]=o=>Tuple(!string.IsNullOrEmpty((string)o),(string)o)
        }; 
        public static void Initialise(){
            Items=JsonConvert.DeserializeObject<Dict<int,Item>>(ReadAllText($"{AppData}items.json"),
                new JsonSerializerSettings{MissingMemberHandling=MissingMemberHandling.Error});
            ItemsByName=Items.Values.ToList().ToDict(i=>i.Name,i=>i,s=>$"{s} ");
            ItemNames=ItemsByName.Keys.ToList();
            Craftables=Items.Where(i=>i.Value.Recipe!=null).ToList().ToDict(i=>i.Key,i=>i.Value);
            CraftablesByName=Craftables.Values.ToList().ToDict(i=>i.Name,i=>i,s=>$"{s} ");
            CraftableNames=CraftablesByName.Keys.ToList();
            //TODO: serialise usedin, change recipe to id?
            foreach(var c in Craftables.Values)
                foreach(var id in c.Recipe.Materials.Select(m=>m.Id))
                    if(Items[id].UsedIn!=null)Items[id].UsedIn.Add(c.Name);
                    else Items[id].UsedIn=new List<string>{c.Name};
        }
    }
}
