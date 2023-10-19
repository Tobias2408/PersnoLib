using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersnoLib
{
    public class PhoneNumberValidator
    {
        // Her er den givede liste over gyldige præfikser for telefonnumre.
        public readonly string[] ValidPrefixes =
        {
            "2", "30", "31", "40", "41", "42", "50", "51", "52", "53", "60", "61", "71", "81",
            "91", "92", "93", "342", "344", "345", "346", "347", "348", "349", "356", "357",
            "359", "362", "365", "366", "389", "398", "431", "441", "462", "466", "468", "472",
            "474", "476", "478", "485", "486", "488", "489", "493", "494", "495", "496", "498",
            "499", "542", "543", "545", "551", "552", "556", "571", "572", "573", "574", "577",
            "579", "584", "586", "587", "589", "597", "598", "627", "629", "641", "649", "658",
            "662", "663", "664", "665", "667", "692", "693", "694", "697", "771", "772", "782",
            "783", "785", "786", "788", "789", "826", "827", "829"
        };

        // Denne funktion tjekker, om et telefonnummer er gyldigt.
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            // Først tjekker vi, om nummeret er tomt eller for kort.
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length < 8)
            {
                return false; // hopper ud hvis det ikke gyldigt.
            }

            // Vi tjekker, om telefonnumrene har et præfiks på 1 til 3 cifre og altid er 8 cifre efter præfikset.
            for (int prefixLength = 1; prefixLength <= 3; prefixLength++)
            {
                // Hvis telefonnummeret er længere end summen af præfikslængden og 8 cifre, ignorerer vi det og tjekker næste.
                if (phoneNumber.Length > (prefixLength + 8))
                {
                    // Vi kan ikke bare sige, at telefonnummeret er ugyldigt, 
                    // fordi det ikke passer til den første præfikslængde, vi tjekker.
                    // Derfor fortsætter vi løkken for at prøve de andre præfikslængder.
                    continue;
                }

                // Vi deler telefonnummeret i to dele: præfikset og hoveddelen.
                string prefix = phoneNumber.Substring(0, prefixLength); // Præfikset
                string mainPart = phoneNumber.Substring(prefixLength);  // Hoveddelen

                // Nu tjekker vi, om præfikset er på listen over gyldige præfikser, og om hoveddelen består af 8 cifre.
                if (ValidPrefixes.Contains(prefix) && mainPart.Length == 8 && mainPart.All(char.IsDigit))
                {
                    return true; // Telefonnummeret er gyldigt.
                }
            }

            // Hvis ingen af betingelserne ovenfor er opfyldt, er telefonnummeret ikke gyldigt.
            return false;
        }
    }
}