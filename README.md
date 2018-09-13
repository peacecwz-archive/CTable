# CTable

Write collections as a table to console

## Get started

Install CTable with Nuget

```
Install-Package CTable
```

or

```
dotnet add package CTable
```

## How to use

### Basic Usage
Format the collection to table with ToStringTable extension method

````csharp
var persons = new List<Person>
{
    new Person { },
    new Person
    {
        FirstName = "Baris",
        MiddleName = "Savas",
        LastName = "Ceviz",
        CreatedOn = DateTime.Now,
        Age = 21
    }
};

var table = persons.ToStringTable(
    p => p.FirstName,
    p => p.MiddleName,
    p => p.LastName,
    p => p.CreatedOn,
    p => p.LastModifiedOn,
    p => p.Age
);

Console.WriteLine(table);
````

````csharp
 | FirstName | MiddleName | LastName | CreatedOn          | LastModifiedOn     | Age |
 |-----------------------------------------------------------------------------------|
 | null      | null       | null     | 9/13/18 4:04:35 AM | null               | 0   |
 | Baris     | Savas      | Ceviz    | 9/13/18 1:04:35 AM | 9/13/18 1:04:35 AM | 21  |
````

### Custom Column Headers

````csharp
var customTable = persons.ToStringTable(new[] { "First Name", "Last Name" },
    p => p.FirstName,
    p => p.LastName
);

Console.WriteLine(customTable);
````

````
 | First Name  | Last Name | 
 |-------------------------| 
 | null        | null      | 
 | Baris       | Ceviz     | 
````

## Contributing

* If you want to contribute to codes, create pull request
* If you find any bugs or error, create an issue

## License

This project is licensed under the MIT License
