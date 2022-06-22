using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace TelAirTextCombination.Controllers
{
	public class HomeController : ApiController
	{

		private readonly IDictionary<int, List<char>> DialPad = new Dictionary<int, List<char>>()
		{
			{2, new List<char> {'a','b','c'} },
			{3, new List<char> {'d','e','f'} },
			{4, new List<char> {'g', 'h', 'i'} },
			{5, new List<char> {'j','k','l' } },
			{6, new List<char> {'m', 'n','o' } },
			{7, new List<char> {'p', 'q', 'r', 's'} },
			{8, new List<char> {'t', 'u', 'v'} },
			{9, new List<char> {'w', 'x', 'y', 'z'} }
		};

		[HttpGet]
		[Route("api/Home/GetCombinationTexts")]
		public IHttpActionResult GetCombinationTexts(string sequenceNo)
		{
			Regex regex = new Regex(@"^[2-9]{1,4}");

			if (!regex.IsMatch(sequenceNo))
			{
				return BadRequest("Invalid number detected");
			}

			if (sequenceNo.Contains("1"))
				return BadRequest("Invalid number detected");
			if (sequenceNo.Length > 4)
				return BadRequest("Maximum length reached");

			return Ok(RecursiveCombination(sequenceNo, 0, new List<string>(), ""));

		}

		private List<string> RecursiveCombination(string sequenceNo, int index, List<string> returnValue, string tempString)
		{
			if (index == sequenceNo.Length)
			{
				returnValue.Add(tempString);
				return returnValue;
			}
			List<char> letters = new List<char>();
			DialPad.TryGetValue(int.Parse(sequenceNo[index].ToString()), out letters);

			foreach(char l in letters)
			{
				tempString += l;
				returnValue = RecursiveCombination(sequenceNo, index + 1, returnValue, tempString);
				tempString = tempString.Substring(0, tempString.Length - 1);
			}

			return returnValue;
		}
	}
}
