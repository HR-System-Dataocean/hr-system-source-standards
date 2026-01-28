$ErrorActionPreference = 'Stop'

$path = 'd:\OcApp\DownLoad\Standard Source Code 05-11-2025\Standard\VenusApplicationMerged - NewInterface\ClsProject\Venus.Application.SystemFiles.System\ClsSys_ReportsDriverSti.vb'

# Load file with system default encoding
$content = Get-Content -LiteralPath $path -Raw -Encoding Default
Add-Type -AssemblyName 'System.Text.RegularExpressions'
 $script:content = $content

function Replace-Types {
  param([string]$from,[string]$to)
  $script:content = [Regex]::Replace($script:content, '(?m)^\s*(Public\s+)?Class\s+' + [Regex]::Escape($from) + '\b', 'Public Class ' + $to)
  $script:content = [Regex]::Replace($script:content, '\)\s+As\s+' + [Regex]::Escape($from) + '\b', ') As ' + $to)
  $script:content = [Regex]::Replace($script:content, '\bAs\s+' + [Regex]::Escape($from) + '(\s*\(\))?', 'As ' + $to + '$1')
  $script:content = [Regex]::Replace($script:content, '\bNew\s+' + [Regex]::Escape($from) + '\s*\(', 'New ' + $to + '(')
  $script:content = [Regex]::Replace($script:content, '\bOf\s*\(\s*' + [Regex]::Escape($from) + '\s*\)', 'Of(' + $to + ')')
}

$types = @('Reports','Report','DataSource','Layout','Font','Groups','Group','Sections','Section','Fields','Field','Picture')
foreach ($t in $types) { Replace-Types -from $t -to ($t + 'Sti') }

# Backing fields to avoid collisions
$script:content = [Regex]::Replace($script:content, '\bmObjReport\b', 'mObjReportSti')
$script:content = [Regex]::Replace($script:content, '\bmObjGroup\b', 'mObjGroupSti')
$script:content = [Regex]::Replace($script:content, '\bmObjSection\b', 'mObjSectionSti')
$script:content = [Regex]::Replace($script:content, '\bmObjField\b', 'mObjFieldSti')

# Save with system default encoding
 $content = $script:content
 Set-Content -LiteralPath $path -Value $content -Encoding Default
