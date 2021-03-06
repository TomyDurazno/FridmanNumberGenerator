<Query Kind="Program" />

void Main()
{
	/*
	These are all prime numbers:
		1 + 42
		1 + 4242
		1 + 424242
		1 + 42424242
		1 + 4242424242
	*/

	Enumerable.Range(2, 8)
			  .Select(n => new { Digits = n, FridmanNumber = FridmanNumber(n) })
			  .Select(o => new { IsPrime =  IsPrime(o.FridmanNumber), val = o})
			  .Where(o => o.IsPrime)
			  .Dump();
}

public bool IsPrime(int n)
	=> n > 1 ? Enumerable.Range(1, n)
						 .Where(x => n % x == 0)
						 .SequenceEqual(new[] { 1, n })
			 : false;

int FridmanNumber(int digits)
{
	IEnumerable<int> FridmanSequenceGenerator(int digits)
	{
		foreach (int n in Enumerable.Range(2, digits).Select(e => e % 2 == 0 ? 4 : 2))
			yield return n;
	}
	
	return int.TryParse(FridmanSequenceGenerator(digits)
							.Aggregate(new StringBuilder(),(acum, n) => acum.Append(n))
							.ToString(), out var value) 
			? 1 + value : throw new Exception();
}