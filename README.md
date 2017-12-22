# picdown
Command line C# .net (dotnet) core image downloader.  Can be used to download a single image or a batch of images.

## Usage
```
Usage:  [options]

Options:
  --url             Specify the image url to download
  --imagelist       Specify the file that contains the list of images urls to download
  --downloadpath    Specify the path to save the downloaded files too
  -? | -h | --help  Show help information
 ```

## Requirements
Install the .NET SDK ([http://go.microsoft.com/fwlink/?LinkID=798306&clcid=0x409](Getting Started "")).

## Building

Clone the repository

```
git clone https://github.com/jwilliamsnephos/picdown.git
```

```
cd picdown
dotnet restore
dotnet build
```

## Running

```
dotnet run --help
```

or via the generated dll

```
dotnet bin/Debug/netcoreapp2.0/picdown.dll --help
```


To download a single image
```
dotnet run --url https://api.adorable.io/avatars/285/test.png
```

To download a single image
```
dotnet run --imagelist ../pathToListOfImages.txt
```

To save the list to a specific directory
```
dotnet run --url https://api.adorable.io/avatars/285/test.png --downloadpath ../downloads
```

