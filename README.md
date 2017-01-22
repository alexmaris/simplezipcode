# simplezipcode
Simple US Zip Code collection for .NET with a light in memory zip code data store, all under 3MB.

To get started use the instance API:

```csharp
// This is the zip code repo builder, you'll need only one instance per app
var source = new ZipCodeSource();

var zipCodes = source.FromMemory().GetRepository();

var chicagoMagnificientMileZipCode = zipCodes.Get("60611");
```

# Search US Zip Codes

By zip code property:
```csharp
var illinoisZipCodes = zipCodes.Search(x => x.State == "Illinois");
```

By zip code postal code:
```csharp
var magnificentMile = zipCodes.Get("60611");
```

By radius of an existing zip code (in miles)
```csharp
var zipCodesNearTheLoop = zipCodes.RadiusSearch(chicagoLoop, 5);
```
