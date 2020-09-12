param(
    [string] $VersionPrefix = "4.0.0",
    [string] $VersionSuffix
)

$buildArgs = @(
    "publish", 
    "-c"
    "Release"
    ".\src\TEdit.sln", 
    '/p:signcert="BC Code Signing"')

if ($null -ne $VersionPrefix) {
    $buildArgs += "/p:VersionPrefix=""$VersionPrefix"""
}

if ($null -ne $VersionSuffix) {
    $buildArgs += "/p:VersionSuffix=""$VersionSuffix"""
}

& dotnet $buildArgs

Remove-Item -Path ".\TEdit*.zip"
Compress-Archive -Path ".\src\TEdit\bin\Release\net462\publish\*" -DestinationPath ".\TEdit$VersionPrefix-$VersionSuffix.zip"