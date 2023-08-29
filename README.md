<br />
<div align="center">
<img src="media/IBDR-logo.png" />
    <h3 align="center">NhsNumber Data Type</h3>

  <p align="center">
    A datatype which can be used to validate NHS numbers from string
    <br />
    <!-- 
    <a href="https://github.com/othneildrew/Best-README-Template"><strong>Explore the docs »</strong></a>
    <br /> -->
    <br />
    <a href="https://github.com/IBDRegistry/NHSNumberDatatype/issues">Report Bug</a>
    ·
    <a href="https://github.com/IBDRegistry/NHSNumberDatatype/issues">Request Feature</a>
  </p>
</div>

## Installation
It is recommended that you install this packgage as a NuGet dependency.
   ```shell
   dotnet add package NHSNumberDatatype
   ```

## Usage

 ```csharp
const string str = "123 456 7890";
if (NhsNumber.TryParse(str, null, out var nhsNumber))
{
    Console.WriteLine("Valid NHS Number");
}
else
{
    Console.WriteLine("Invalid NHS Number");
}
```

<!-- CONTRIBUTING -->
## Contributing

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request