namespace BFCalc{
    using static Improved.ListClass;
    internal class Translator{
        public Dict<string,string> Kanji=new Dict<string,string>{["防衛"]="Defense"};
        public Dict<string,string> Katakana=new Dict<string,string>{
            //TODO kanji individual, katakana into words seperated by dot
            ["ランダム"]="Random",
            ["ジャガーノート"]="Juggernaut",
            ["アバドン"]="Abbadon",
            ["マクスウェル"]="Maxwell",
            ["グラントス"]="Grantos",
            ["ゼブラ"]="Zebra",
            ["カルデス"]="Cardes",
            ["システム"]="System",
            ["ザーグ"]="Zurg",
            ["アフラディリス"]="Afla",
            ["ディリス"]="Dilith",
            ["アルマ"]="Armor",
            ["マンティス"]="Mantis",
            ["アント"]="Ant",
            ["タイガー"]="Tiger",
            ["キメラ"]="Chimera",
            ["ダーク"]="Dark",
            ["リッチ"]="Ricchi",
            ["テイルズ"]="Teiruzu",
            ["アーチェ"]="Aache",
            ["ファンキル"]="Fankiru",
            ["ティル"]="Tail",
            ["エスティア"]="Estia",
            ["ゼノティア"]="Zenotia",
            ["パンプキン"]="Pumpkin",
            ["スキル"]="Skill",
            ["ターン"]="Turn",
            ["バフ"]="Buff",
            ["ボス"]="Boss",
            ["ヴァルガス"]="Vargas",
            ["ランセル"]="Lance",//ru
            ["セレナ"]="Selena",
            ["エゼル"]="Eze",//ru
            ["ローランド"]="Lorand",
            ["ディン"]="Dean",
            ["エデア"]="Edea",
            ["ロクス"]="Loch",//su
            ["セフィア"]="Sefia"
        };
    }
}
