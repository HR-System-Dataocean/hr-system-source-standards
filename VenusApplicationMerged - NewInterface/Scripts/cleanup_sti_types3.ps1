$ErrorActionPreference = 'Stop'
$path = 'd:\OcApp\DownLoad\Standard Source Code 05-11-2025\Standard\VenusApplicationMerged - NewInterface\ClsProject\Venus.Application.SystemFiles.System\ClsSys_ReportsDriverSti.vb'
$content = Get-Content -LiteralPath $path -Raw -Encoding Default

# Fix bad plural/type leftovers from automated renames
$content = $content -replace '\bReportStisSti\b','ReportsSti'
$content = $content -replace '\bReportStis\b','ReportsSti'
$content = $content -replace '\bGroupsStiSti\b','GroupsSti'
$content = $content -replace '\bSectionsStiSti\b','SectionsSti'
$content = $content -replace '\bFieldsStiSti\b','FieldsSti'

# Rename helper class to avoid conflict with non-Sti file
$content = $content -replace '(?m)^\s*Public\s+Class\s+ReportHelper\b','Public Class ReportHelperSti'
$content = $content -replace '\bAs\s+ReportHelper\b','As ReportHelperSti'
$content = $content -replace '\bNew\s+ReportHelper\b','New ReportHelperSti'

Set-Content -LiteralPath $path -Value $content -Encoding Default
