$ErrorActionPreference = 'Stop'
$path = 'd:\OcApp\DownLoad\Standard Source Code 05-11-2025\Standard\VenusApplicationMerged - NewInterface\ClsProject\Venus.Application.SystemFiles.System\ClsSys_ReportsDriverSti.vb'
$content = Get-Content -LiteralPath $path -Raw -Encoding Default

# Collapse duplicate Sti
$dups = @(
  'Report','Reports','DataSource','Layout','Font','Group','Groups','Section','Sections','Field','Fields','Picture'
)
foreach ($t in $dups) {
  $content = $content -replace ("$t" + 'StiSti'), ($t + 'Sti')
}

# Fix plural over-replacements like GroupStisStiSti -> GroupsSti
$content = $content -replace 'GroupStisStiSti','GroupsSti'
$content = $content -replace 'SectionStisStiSti','SectionsSti'
$content = $content -replace 'FieldStisStiSti','FieldsSti'

# Ensure New <Type>( becomes New <Type>Sti(
$news = 'Field','Group','Section','Report','DataSource','Layout','Font','Picture'
foreach ($n in $news) {
  $content = [regex]::Replace($content, "\bNew\s+" + [regex]::Escape($n) + "\s*\(", 'New ' + $n + 'Sti(')
}

Set-Content -LiteralPath $path -Value $content -Encoding Default
