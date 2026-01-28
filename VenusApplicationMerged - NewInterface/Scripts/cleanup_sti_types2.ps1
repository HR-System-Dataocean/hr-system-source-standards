$ErrorActionPreference = 'Stop'
$path = 'd:\OcApp\DownLoad\Standard Source Code 05-11-2025\Standard\VenusApplicationMerged - NewInterface\ClsProject\Venus.Application.SystemFiles.System\ClsSys_ReportsDriverSti.vb'
$content = Get-Content -LiteralPath $path -Raw -Encoding Default

# 1) Collapse any repeated Sti sequences robustly
while ($content -match 'StiSti') { $content = $content -replace 'StiSti','Sti' }

# 2) Fix pluralization accidents (GroupStis -> GroupsSti, etc.)
$content = $content -replace 'GroupStis','GroupsSti'
$content = $content -replace 'SectionStis','SectionsSti'
$content = $content -replace 'FieldStis','FieldsSti'

# 3) Ensure As New <Type> and New <Type> forms use Sti suffix
$types = 'Report','Reports','DataSource','Layout','Font','Group','Groups','Section','Sections','Field','Fields','Picture'
foreach ($t in $types) {
  $content = [regex]::Replace($content, "\bAs\s+New\s+" + [regex]::Escape($t) + "\b", 'As New ' + $t + 'Sti', 'IgnoreCase')
  $content = [regex]::Replace($content, "\bNew\s+" + [regex]::Escape($t) + "\b", 'New ' + $t + 'Sti', 'IgnoreCase')
}

Set-Content -LiteralPath $path -Value $content -Encoding Default
