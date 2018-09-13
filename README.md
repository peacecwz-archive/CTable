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
    u => u.FirstName,
    u => u.MiddleName,
    u => u.LastName,
    u => u.CreatedOn,
    u => u.LastModifiedOn,
    u => u.Age
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
var customTable = users.ToStringTable(new[] { "First Name", "Last Name" },
    u => u.FirstName,
    u => u.LastName
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
