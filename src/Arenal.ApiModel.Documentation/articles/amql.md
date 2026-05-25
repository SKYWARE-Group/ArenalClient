## Preface

AQL (Arenal Query Language) is a SQL-like language for defining filters. In the endpoints which return list of objects, there is a AQL expression, represented as query string. Here is the example:
```
GET https://base-arenal-url.com/api/orders?where=(providerId,eq,'G-123456')
```

## Implementation

AQL is used only for definition of a WHERE clause. Property names are **case-insensitive**.

### Predicate

<img src="/images/amql-value.png" width="60%"/>

<img src="/images/amql-predicate.png" width="70%"/>


Examples
```
(providerId,eq,'G-123456')
(created,gt,2023-03-01)
```