using DeckOfCards.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckOfCards.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Project is running";
        }
        

        [HttpPost]
        public Object Post([FromBody] Inputs request)
        {
            try
            {
                string input = request.body.ToString();
                if (input[input.Length - 1] == ',')
                    input = input.Substring(0, input.Length - 1);
                string[] inputArray = input.Split(",");
                Array.Sort(inputArray, new CardsClass());
                Array.Sort(inputArray, new NumbersClass());

                var result = JsonConvert.SerializeObject(inputArray);

                return result;
            }
            catch(Exception e)
            {
                return e;
            }
        }

        public class CardsClass : IComparer
        {
            public char[] cards = new char[] { 'T', 'D', 'S', 'C', 'H' };

            int IComparer.Compare(Object x, Object y)
            {
                var s1 = x.ToString();
                var s2 = y.ToString();

                if (Array.IndexOf(cards, s1[s1.Length - 1]) > Array.IndexOf(cards, s2[s2.Length - 1]))
                    return +1;
                else if (Array.IndexOf(cards, s1[s1.Length - 1]) < Array.IndexOf(cards, s2[s2.Length - 1]))
                    return -1;
                else
                    return 0;

            }
        }

        public class NumbersClass : IComparer
        {
            string[] numbers = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] specialcard = new string[] { "4", "2", "S", "P", "R" };
            int IComparer.Compare(Object x, Object y)
            {
                var s1 = x.ToString();
                var s2 = y.ToString();

                if (s1[s1.Length - 1] == s2[s2.Length - 1])
                {
                    if (s1[s1.Length - 1] == 'T')
                    {
                        if (Array.IndexOf(specialcard, s1.Substring(0, s1.Length - 1)) > Array.IndexOf(specialcard, s2.Substring(0, s2.Length - 1)))
                            return +1;
                        else if (Array.IndexOf(specialcard, s1.Substring(0, s1.Length - 1)) < Array.IndexOf(specialcard, s2.Substring(0, s2.Length - 1)))
                            return -1;
                        else
                            return 0;
                    }
                    else
                    {
                        if (Array.IndexOf(numbers, s1.Substring(0, s1.Length - 1)) > Array.IndexOf(numbers, s2.Substring(0, s2.Length - 1)))
                            return +1;
                        else if (Array.IndexOf(numbers, s1.Substring(0, s1.Length - 1)) < Array.IndexOf(numbers, s2.Substring(0, s2.Length - 1)))
                            return -1;
                        else
                            return 0;
                    }

                }
                else
                    return 0;
            }
        }
    }
}
