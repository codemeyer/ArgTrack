Import-Module -Name "$PSScriptRoot\Invoke-MsBuild\Invoke-MsBuild.psm1"


Write-Host "Retrieving base version number from AssemblyInfo.cs..." -ForegroundColor Yellow
$assemblyInfoPath = "$PSScriptRoot/../Source/ArgTrack/Properties/AssemblyInfo.cs"
$assemblyData = Get-Content -Path $assemblyInfoPath
$pattern = "AssemblyVersion\(`"([0-9]+.[0-9]+.[0-9]+).[0-9]+`"\)"
$regex = new-object System.Text.RegularExpressions.Regex ($pattern)
$matches = $regex.Matches($assemblyData)
$currentMajorMinor = $matches.Groups[1]
Write-Host ""


Write-Host "Figuring out the new version number..." -ForegroundColor Yellow
$buildNumberPath = "$PSScriptRoot\latest-build.txt"
$newBuildNumber = 0    # default...

if (Test-Path -Path $buildNumberPath)
{
  # read latest build number from file
  $fileData = Get-Content -Path "$PSScriptRoot\latest-build.txt"
  $fileData = $fileData.Trim()

  $latestBuildNumber = [int]$fileData
  $newBuildNumber = $latestBuildNumber + 1
}


$newVersion = "$currentMajorMinor.$newBuildNumber"
Write-Host "New version number: $newVersion"
Write-Host ""


Write-Host "Building solution..." -ForegroundColor Yellow
Invoke-MsBuild -Path "$PSScriptRoot/../Source/ArgTrack.sln" -MsBuildParameters "/target:Clean;Build /p:Configuration=Release;VersionAssembly=$newVersion"
Write-Host ""


Write-Host "Zipping exe file and placing it in the Artifacts folder..." -ForegroundColor Yellow
$zipFileName = "ArgTrack-$newVersion.zip"
Write-Host "File name: $zipFileName"
Compress-Archive -Path "$PSScriptRoot/../Source/ArgTrack/bin/Release/ArgTrack.exe" -Update -DestinationPath "$PSScriptRoot/Artifacts/$zipFileName"
Write-Host ""


Write-Host "Updating latest build number in file..." -ForegroundColor Yellow
Set-Content -Path "$PSScriptRoot\latest-build.txt" -Value $newBuildNumber
