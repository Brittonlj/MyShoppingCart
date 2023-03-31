// See https://aka.ms/new-console-template for more information


using Scratch;

var leftList = new List<Person>
{
    new Person
    {
        Id = 1,
        Name = "Foo"
    },
    new Person
    {
        Id = 2,
        Name = "Bar"
    },
    new Person
    {
        Id = 3,
        Name = "Rob"
    }
};

var rightList = new List<Person>
{
    new Person
    {
        Id = 1,
        Name = "Foo"
    },
    new Person
    {
        Id = 2,
        Name = "Zed"
    },
    new Person
    {
        Id = 4,
        Name = "Jake"
    }
};


var join = leftList.Join(rightList,
    leftId => leftId?.Id ?? 0,
    rightId => rightId.Id,
    (left, right) => new
    {
        Left = left,
        Right = right
    });

var updates = join.Where(x =>x.Left.Id == x.Right.Id && x.Left != x.Right);

foreach (var pair in updates)
{
    Console.WriteLine(pair.Left.Name + " != " + pair.Right.Name);
}
Console.WriteLine();

var same = join.Where(x => x.Left == x.Right);

foreach (var pair in same)
{
    Console.WriteLine(pair.Left.Name + " == " + pair.Right.Name);
}

var inserts = rightList.Where(l => !leftList.Any(r => r.Id == l.Id));
foreach (var item in inserts)
{
    Console.WriteLine(item.Name + " ++ ");
}

var deletes = leftList.Where(l => !rightList.Any(r => r.Id == l.Id));
foreach (var item in deletes)
{
    Console.WriteLine(item.Name + " -- ");
}

