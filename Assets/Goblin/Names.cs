using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Names
{
    public class Goblin
    {
        string[] male = {
            "Brung","Glukx","Jeakz","Wrikz","Slags","Yvaz","Clisbisb","Lilgeakt","Vroibtisb","Cryklekx","Bleek","Dirm","Pegz","Guizz","Cort","Gusaags","Cribzect","Kursaags","Prealygs","Wuidfukt"
        };

        string[] female = {
            "Glult","Lofs","Gothee","Iart","Fanx","Gnersinea","Qegtolk","Qoinvesx","Soibsaalm","Ekofs","Bhirxee","Iftee","Trezz","Cofs","Kal","Slogiazz","Sreasvih","Chylbefze","Nekzaanx","Swiehdunk"
        };

        string[] last =
        {
            "Horseride","Bignose","Dungrake","Manychild","Onebrow","Whitetooth","Woodleg","Highprofit","Smalleye"
        };

        public string RandomMale()
        {
            var randMaleName = Random.Range(0, male.Length - 1);
            return male[randMaleName];
        }

        public string RandomFemale()
        {
            var randFemaleName = Random.Range(0, female.Length - 1);
            return female[randFemaleName];
        }


        public string RandomLast()
        {
            var randLastName = Random.Range(0, last.Length - 1);
            return last[randLastName];
        }


    }

}
