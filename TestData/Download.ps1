# Downloads the testdata zip file and unpacks it in this folder

$url = "http://www.argtools.com/downloads/testdata/argtrack/files-v1.zip"
$file = "files-v1.zip"
$path = (Get-Location).Path + "\" + $file

Write-Host "Downloading $file... " -NoNewLine

$webClient = new-object System.Net.WebClient
$webClient.DownloadFile($url, $path)

Write-Host "Done!"

Write-Host "Unzipping files... " -NoNewLine

Expand-Archive -Path $path -Destination .

Write-Host "Done!"

Write-Host "Deleting zip file... " -NoNewLine
Remove-Item $path
Write-Host "Done!"
