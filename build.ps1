$msbuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
if (-not (Test-Path $msbuildPath)) {
    Write-Host "Error: MSBuild not found at standard location ($msbuildPath)." -ForegroundColor Red
    Write-Host "Please use Visual Studio 2022 Developer Command Prompt or adjust the path in this script."
    exit 1
}

Write-Host "Building TrafficDesktopApp with MSBuild..." -ForegroundColor Cyan
& $msbuildPath "TrafficDesktopApp.csproj" /t:Rebuild /p:Configuration=Debug /v:n
if ($LASTEXITCODE -eq 0) {
    Write-Host "Build Succeeded!" -ForegroundColor Green
} else {
    Write-Host "Build Failed." -ForegroundColor Red
}
