# Pages: FeedbackModule

## Page Inventory

| Page | Title | Allowed roles | Parameters | Popup |
|---|---|---|---|---|
| FeedbackModule.PopupFailure | Something went wrong | FeedbackModule.User | none | True |
| FeedbackModule.PopupFailure_Logo | Something went wrong | FeedbackModule.User | none | True |
| FeedbackModule.PopupSuccess | Feedback Submitted | FeedbackModule.User | Response:FeedbackModule.ResponseHelper | True |
| FeedbackModule.PopupSuccess_Logo | Feedback Submitted | FeedbackModule.User | Response:FeedbackModule.ResponseHelper | True |
| FeedbackModule.ShareFeedback | Share your feedback | FeedbackModule.User | none | True |
| FeedbackModule.ShareFeedback_Logo | Share your feedback | FeedbackModule.User | none | True |

## Page-Flow Links

| Page | Shown by flows |
|---|---|
| FeedbackModule.PopupFailure | FeedbackModule.ACT_SubmitFeedback |
| FeedbackModule.PopupFailure_Logo | none (no show-page evidence) |
| FeedbackModule.PopupSuccess | FeedbackModule.ACT_SubmitFeedback |
| FeedbackModule.PopupSuccess_Logo | none (no show-page evidence) |
| FeedbackModule.ShareFeedback | FeedbackModule.ACT_Feedback_TriggerScreenshotMode, FeedbackModule.ACT_Feedback_UploadImage |
| FeedbackModule.ShareFeedback_Logo | none (no show-page evidence) |

## Journey Fragments

| User intent group | Pages |
|---|---|
| General | FeedbackModule.PopupFailure, FeedbackModule.PopupSuccess, FeedbackModule.ShareFeedback |
| PopupFailure | FeedbackModule.PopupFailure_Logo |
| PopupSuccess | FeedbackModule.PopupSuccess_Logo |
| ShareFeedback | FeedbackModule.ShareFeedback_Logo |

## Snippets

Snippet-level page widget behaviour is not exported in current overview contract.
