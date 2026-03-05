# Flows: FeedbackModule

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_Feedback_ClearForm | 5 | none | none |
| ACT_Feedback_ClearImage | 5 | none | none |
| ACT_Feedback_TriggerScreenshotMode | 14 | none | FeedbackModule.ShareFeedback |
| ACT_Feedback_UploadImage | 26 | none | FeedbackModule.ShareFeedback |
| ACT_SubmitFeedback | 14 | none | FeedbackModule.PopupFailure, FeedbackModule.PopupSuccess |

### Data Sources (DS_*)

| Flow | Nodes | Key Actions | Returns |
|---|---:|---|---|
| DS_Feedback_Populate | 7 | none | inferred from node actions |

### Validation Flows (VAL_*)

| Flow | Nodes | Key Actions |
|---|---:|---|
| VAL_Feedback | 29 | none |

### Other Flows

| Flow | Type | Nodes | Key Actions |
|---|---|---:|---|
| ConvertBase64String | Microflow | 7 | none |
| ConvertUUIDToURL | Microflow | 4 | none |
| OCH_Feedback_SaveToLocalStorage | Nanoflow | 4 | none |
| PopulateUserAttributes | Microflow | 6 | System.User |
| SUB_Feedback_GetOrCreate | Nanoflow | 8 | FeedbackModule.Feedback |
| SUB_Feedback_PostToAppInsights | Microflow | 8 | none |
| SUB_Feedback_ResetLocalStorage | Nanoflow | 6 | none |
| SUB_Feedback_Sanitize | Microflow | 14 | none |
| SUB_Feedback_SendToServer | Microflow | 15 | none |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Tier | Calls Out | Called By |
|---|---|---:|---:|---:|---:|
| ACT_Feedback_ClearForm | Nanoflow | 5 | 3 | 0 | 0 |
| ACT_Feedback_ClearImage | Nanoflow | 5 | 3 | 0 | 0 |
| ACT_Feedback_TriggerScreenshotMode | Nanoflow | 14 | 3 | 0 | 0 |
| ACT_Feedback_UploadImage | Nanoflow | 26 | 3 | 0 | 0 |
| ACT_SubmitFeedback | Nanoflow | 14 | 3 | 3 | 0 |
| ConvertBase64String | Microflow | 7 | 3 | 0 | 0 |
| ConvertUUIDToURL | Microflow | 4 | 3 | 0 | 0 |
| DS_Feedback_Populate | Nanoflow | 7 | 3 | 2 | 0 |
| OCH_Feedback_SaveToLocalStorage | Nanoflow | 4 | 3 | 0 | 0 |
| PopulateUserAttributes | Microflow | 6 | 3 | 0 | 1 |
| SUB_Feedback_GetOrCreate | Nanoflow | 8 | 3 | 0 | 1 |
| SUB_Feedback_PostToAppInsights | Microflow | 8 | 3 | 0 | 1 |
| SUB_Feedback_ResetLocalStorage | Nanoflow | 6 | 3 | 0 | 1 |
| SUB_Feedback_Sanitize | Microflow | 14 | 3 | 0 | 1 |
| SUB_Feedback_SendToServer | Microflow | 15 | 3 | 2 | 1 |
| VAL_Feedback | Microflow | 29 | 3 | 0 | 1 |

## Tier 1 Deep Narratives

No Tier 1 narrative required for this module category.
