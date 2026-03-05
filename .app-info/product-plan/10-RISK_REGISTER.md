# Risk Register

## Objective

Track delivery and operational risks for the AI-useful KB upgrade.

## Risk Matrix

## R1: Overfitting to SmartExpenses naming

Impact: High  
Likelihood: Medium

Description:

1. Tiering or capability inference could become too app-specific.

Mitigation:

1. Use generic rules based on evidence patterns, not module names.
2. Add fallback handling for unknown prefixes and sparse models.

## R2: False semantic failures

Impact: Medium  
Likelihood: Medium

Description:

1. Semantic thresholds may fail good outputs due to conservative evidence extraction.

Mitigation:

1. Pilot calibration phase with metric diagnostics.
2. Keep threshold values configurable if needed.

## R3: Markdown parsing fragility in validators

Impact: Medium  
Likelihood: Medium

Description:

1. Table parsers in PowerShell can misread unusual markdown formatting.

Mitigation:

1. Keep deterministic markdown layout from composer.
2. Add tolerant parsing and clear error messages.

## R4: Runtime cost increase

Impact: Medium  
Likelihood: Low

Description:

1. Composer + semantic checks may increase generation time on large apps.

Mitigation:

1. Keep linear scans and avoid expensive nested loops where possible.
2. Print timing checkpoints to identify hotspots.

## R5: Missing source metadata

Impact: Medium  
Likelihood: Medium

Description:

1. Some fields (for example schedules) may be absent in source dumps.

Mitigation:

1. Keep explicit `Unknown` policy only for non-derivable fields.
2. Implement optional parser enrichments.

## R6: Existing consumer compatibility

Impact: High  
Likelihood: Low

Description:

1. Richer content might accidentally break existing navigation assumptions.

Mitigation:

1. Preserve file and heading contracts required by current validators.
2. Add new sections as additive only.

## R7: Dirty repository context during rollout

Impact: Medium  
Likelihood: High

Description:

1. Unrelated workspace changes can obscure verification outcomes.

Mitigation:

1. Validate only target scripts/docs for this feature.
2. Report tested commands and exact results explicitly.

## Fallback Strategy

If semantic gates block delivery unexpectedly:

1. Keep structural gate mandatory.
2. Emit semantic diagnostics without relaxing thresholds silently.
3. Iterate extraction rules first; only adjust thresholds with documented rationale.
