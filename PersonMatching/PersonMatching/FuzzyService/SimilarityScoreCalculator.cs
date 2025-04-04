﻿using FuzzySharp;
using System;

namespace PersonMatching.FuzzyService
{
    public class SimilarityScoreCalculator
    {
        public static double GetSimilarityScore(string field, string candVal, string recVal)
        {
            if (string.IsNullOrWhiteSpace(candVal) || string.IsNullOrWhiteSpace(recVal))
                return 0.0;


            switch (field)
            {
                case "name":
                    return GetNameScore(candVal, recVal);

                case "address":
                    return GetAddressScore(candVal, recVal);

                case "email":
                    return GetEmailScore(candVal, recVal);

                case "emailPre":
                    return GetEmailPrefixScore(candVal, recVal);

                case "phoneNo":
                    return GetPhoneNoScore(candVal, recVal);

                case "favColour":
                    return GetFavColourScore(candVal, recVal);

                case "age":
                    return GetAgeScore(candVal, recVal);

                case "birthday":
                    return GetBirthdayScore(candVal, recVal);

                default:
                    return 0.0;
            }
        }
        //Set all strings to lower as comparison is case sensitive
        public static double GetNameScore(string name1, string name2)
        {
            return Fuzz.TokenSetRatio(name1.ToLower(), name2.ToLower()) / 100.0;
            //Nuances in the way First name, middle name and last name are displayed are handled e.g John A Smith & John Anthony Smith
        }

        public static double GetAddressScore(string address1, string address2)
        {
            return Fuzz.TokenSortRatio(address1.ToLower(), address2.ToLower()) / 100.0;
            //Similarity score barely affected by differences like street and st and order in which door no.,
            // and street name appear. e.g regtech Street 789 and 789 regtech St returns high similarity
        }

        public static double GetEmailScore(string email1, string email2)
        {
            return (email1 == email2) ? 1.0 : 0.0;
        }
        public static double GetEmailPrefixScore(string email1, string email2)
        {
            return Fuzz.PartialRatio(email1.ToLower(), email2.ToLower()) / 100.0;
            //handles different email domains effectively by still returning high similarity if email prefix
            //is very similar
        }

        public static double GetPhoneNoScore(string num1, string num2)
        {
           
                return num1 == num2 ? 1.0 : 0.0; //string comparison vs int comparison does not matter in this instance. No point in converting


        }

        public static double GetFavColourScore(string colour1, string colour2)
        {
            return Fuzz.Ratio(colour1.ToLower(), colour2.ToLower()) / 100.0;
        }

        public static double GetAgeScore(string age1, string age2)
        {
           
                return age1 == age2 ? 1.0 : 0.0;
            
        }

        public static double GetBirthdayScore(string bday1, string bday2)
        {
            return bday1 == bday2 ? 1.0 : 0.0; //self explanatory
        }
    }
}
