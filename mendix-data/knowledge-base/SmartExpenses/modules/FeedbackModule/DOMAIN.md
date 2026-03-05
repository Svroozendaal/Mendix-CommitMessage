# Domain: FeedbackModule

## Entities

| Entity | Persistable | Attribute count | Access rule count |
|---|---|---:|---:|
| FeedbackModule.Feedback | False | 16 | 1 |
| FeedbackModule.ResponseHelper | False | 1 | 1 |

Confidence: Export-backed

## Entity Lifecycle Matrix

| Entity | Create flows | Update flows | Delete flows | Read flows |
|---|---|---|---|---|
| FeedbackModule.Feedback | FeedbackModule.SUB_Feedback_GetOrCreate | none | none | none |
| FeedbackModule.ResponseHelper | none | none | none | none |

Confidence: Inferred

## Role impacts per sensitive entity

| Entity | Module roles | Default member rights | XPath constraint |
|---|---|---|---|
| FeedbackModule.Feedback | FeedbackModule.User | ReadWrite | none |
| FeedbackModule.ResponseHelper | FeedbackModule.User | None | none |

Confidence: Export-backed

## Associations

| Association | Parent | Child | Cardinality | Type | Owner |
|---|---|---|---|---|---|
| none | none | none | none | none | none |

## Enumerations

| Enumeration | Value count | Sample values |
|---|---:|---|
| FeedbackModule.LogNodes | 1 | FeedbackModule |
| FeedbackModule.TranslationLanguages | 8 | Dutch, English, French, German |
