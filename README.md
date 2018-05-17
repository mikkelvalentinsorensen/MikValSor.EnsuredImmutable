# MikValSor.EnsuredImmutable
Library build on top of MikValSor.ImmutableValidator that encapsulated immutable objects ensured types for to ease development and minimize redundant validation check.

## Example:
```cs
class MyClass
{
	public string Value;
}

class MyOtherClass
{
	public readonly string Value;
	public MyOtherClass(string value)
	{
		Value = value;
	}
}

void Example()
{
	//Muttable
	var myObject = new MyClass { Value = "Value" };
	try
	{
		MikValSor.Immutable.EnsuredImmutable<MyClass> myEnsuredObject = MikValSor.Immutable.EnsuredImmutable.Create(myObject);
	}
	catch (MikValSor.Immutable.NotImmutableException)
	{
		System.Console.WriteLine($"myObject is mutable.");
	}

	//Immutable
	var myOtherObject = new MyOtherClass("Value");
	MikValSor.Immutable.EnsuredImmutable<MyOtherClass> myOtherEnsuredObject = MikValSor.Immutable.EnsuredImmutable.Create(myOtherObject);
	System.Console.WriteLine($"myOtherEnsuredObject is immutable.");

	System.Console.WriteLine($"myOtherEnsuredObject.Value: {myOtherEnsuredObject.Value}");

	//Using extensions (using MikValSor.Immutable.Extensions)
	MikValSor.Immutable.EnsuredImmutable<MyOtherClass> myOtherEnsuredObject2 = myOtherObject.EnsureImmutable();
	System.Console.WriteLine($"myOtherEnsuredObject2.Value: {myOtherEnsuredObject2.Value}");
}
/**
	Output:
	myObject is mutable.
	myOtherEnsuredObject is immutable.
	myOtherEnsuredObject.Value: MyOtherClass
	myOtherEnsuredObject2.Value: MyOtherClass
**/
```