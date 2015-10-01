namespace BFCalc{
    using System;
    using System.Diagnostics;
    using Meebey.SmartIrc4net;
    using static Improved.ListClass;
    using static ItemData;
    using static Meebey.SmartIrc4net.SendType;
    internal static class Irc{
        public static Dict<string,string> BfSlang=new Dict<string,string>{["mats"]="recipe",["materials"]="recipe"};
        public static Dict<string,Func<int,string>> LookupById=new Dict<string,Func<int,string>>{
            ["recipe"]=i=>Craftables[i].Recipe.ToString()
        };
        public static Dict<string,Func<string,string>> LookupByName=new Dict<string,Func<string,string>>{
            ["recipe"]=s=>CraftablesByName[s].Recipe.ToString()
        };
        public static IrcFeatures Client;
        public static Channel Channel;
        public static void Initialise(){
            Client=new IrcFeatures{AutoRejoin=true,AutoRejoinOnKick=true};
            Client.Connect("irc.mibbit.net",8888);//TODO
            Client.Login("somebot","somebot");
            Client.RfcJoin("bravefrontier");
            Client.OnReadLine+=Test;
            Client.SendMessage(Message,"somebody","test");
            Client.Listen();
        }
        private static void Test(object sender,ReadLineEventArgs args) {Debug.WriteLine(args.Line);}
    }
}
