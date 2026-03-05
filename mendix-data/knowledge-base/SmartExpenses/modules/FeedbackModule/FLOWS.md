# Flows: FeedbackModule

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_Feedback_ClearForm | 5 | call javascript action FeedbackModule.SetStorageItemObject -> ReturnValueName, change Feedback (Subject=empty, Description=empty, SubmitterEmail=empty, ImageB64=empty; refreshInClient=true) | Unknown |
| ACT_Feedback_ClearImage | 5 | call javascript action FeedbackModule.SetStorageItemObject -> ReturnValueName, change Feedback (ImageB64=empty; refreshInClient=true) | Unknown |
| ACT_Feedback_TriggerScreenshotMode | 14 | change Feedback (ImageB64=$base64FromWidget; refreshInClient=true), close page | Unknown |
| ACT_Feedback_UploadImage | 26 | change Feedback (ImageB64=$base64ImageFromWidget; refreshInClient=true), close page | Unknown |
| ACT_SubmitFeedback | 14 | call microflow FeedbackModule.SUB_Feedback_SendToServer -> ResponseHelper, close page (pagesToClose=1) | Unknown |

### Data Sources (DS_*)

| Flow | Nodes | Key Actions | Returns |
|---|---:|---|---|
| DS_Feedback_Populate | 7 | call javascript action FeedbackModule.JS_PopulateFeedbackMetadata -> FeedbackWithMetaData, change Feedback (_showEmail=if $Feedback/SubmitterEmail != empty and $Feedback/SubmitterEmail != '' then false else true; refreshInClient=true) | Unknown |

### Validation Flows (VAL_*)

| Flow | Nodes | Key Actions |
|---|---:|---|
| VAL_Feedback | 29 | change variable ValidFeedback=false |

### Other Flows

| Flow | Type | Nodes | Key Actions |
|---|---|---:|---|
| ConvertBase64String | Microflow | 7 | none |
| ConvertUUIDToURL | Microflow | 4 | none |
| OCH_Feedback_SaveToLocalStorage | Nanoflow | 4 | call javascript action FeedbackModule.SetStorageItemObject -> ReturnValueName |
| PopulateUserAttributes | Microflow | 6 | change Feedback (SubmitterUUID=$currentUser/Name, SubmitterDisplayName=$CurrentUser/Name; refreshInClient=false), retrieve CurrentUser from System.User |
| SUB_Feedback_GetOrCreate | Nanoflow | 8 | call javascript action FeedbackModule.GetStorageItemObject -> LocalFeedback, create FeedbackModule.Feedback as NewFeedback |
| SUB_Feedback_PostToAppInsights | Microflow | 8 | create variable ServerLocation='https://feedback-api.mendix.com/v2/feedback-items', LogMessageAction (errorHandlingType=Rollback) |
| SUB_Feedback_ResetLocalStorage | Nanoflow | 6 | call javascript action FeedbackModule.SetStorageItemObject -> ReturnValueName, change Feedback (Subject=empty, Description=empty, SubmitterEmail=empty, SubmitterUUID=empty, SubmitterDisplayName=empty, ImageB64=empty, ActiveUserRoles=empty, PageName=empty, +6 more; refreshInClient=true) |
| SUB_Feedback_Sanitize | Microflow | 14 | call java action FeedbackModule.XSS_Sanitizer -> SanitizedActiveUserRoles, change Feedback (Subject=$SanitizedSubject, Description=$SanitizedDescription, SubmitterUUID=$SanitizedSubmitterUUID, SubmitterEmail=$SanitizedSubmitterEmail, SubmitterDisplayName=$SanitizedSubmitterDisplayName, ActiveUserRoles=$SanitizedActiveUserRoles, PageName=$SanitizedPageName, Browser=$SanitizedBrowser, +1 more; refreshInClient=false) |
| SUB_Feedback_SendToServer | Microflow | 15 | change Feedback (ScreenshotName='screenshot-' + formatDateTime([%CurrentDateTime%],'yyyy-MM-dd-HH-mm-ss')+'.'+ toLowerCase(substring($Feedback/ImageB64,find($Feedback/ImageB64, 'data:image/') + 11,3)); refreshInClient=false), LogMessageAction (errorHandlingType=Rollback) |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Calls Out | Called By |
|---|---|---:|---:|---:|
| ACT_Feedback_ClearForm | Nanoflow | 5 | 0 | 0 |
| ACT_Feedback_ClearImage | Nanoflow | 5 | 0 | 0 |
| ACT_Feedback_TriggerScreenshotMode | Nanoflow | 14 | 0 | 0 |
| ACT_Feedback_UploadImage | Nanoflow | 26 | 0 | 0 |
| ACT_SubmitFeedback | Nanoflow | 14 | 3 | 0 |
| ConvertBase64String | Microflow | 7 | 0 | 0 |
| ConvertUUIDToURL | Microflow | 4 | 0 | 0 |
| DS_Feedback_Populate | Nanoflow | 7 | 2 | 0 |
| OCH_Feedback_SaveToLocalStorage | Nanoflow | 4 | 0 | 0 |
| PopulateUserAttributes | Microflow | 6 | 0 | 1 |
| SUB_Feedback_GetOrCreate | Nanoflow | 8 | 0 | 1 |
| SUB_Feedback_PostToAppInsights | Microflow | 8 | 0 | 1 |
| SUB_Feedback_ResetLocalStorage | Nanoflow | 6 | 0 | 1 |
| SUB_Feedback_Sanitize | Microflow | 14 | 0 | 1 |
| SUB_Feedback_SendToServer | Microflow | 15 | 2 | 1 |
| VAL_Feedback | Microflow | 29 | 0 | 1 |

