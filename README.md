## Things to improve
1. Assertion method's statements are sequential and at failure give very little information. Using the library "FluentAssertions" would be great but its license is no longer free for comercial use.
2. Setting and getting  "AddRecordRequestDto" fields is awkwardly constructed because of the support for fields with XML attribute "name" and fields with XML attribute "fid" has to be supported.
3. The assertion of fields with type "Date" is commented for now because a parser for milliseconds has to be created.
4. There are too many constants. It would be better if DTO properties are decorated with more attributes 
5. IQuickbaseApi is coupled to request DTOs like "AuthenticateRequestDto", "AddRecordRequestDto", and "DoQueryRequestDto". It would be better to use some middleware (mediator) that passes raw XML content to the IQuickbaseApi
6. The tests for AddRecord rely on a manually created table. It would be better to create such a table dynamically at the start of a test run
7. Not all namespace usings are moved to GlobalUsings.cs


## The following is the order of code execution in general
![image](https://github.com/user-attachments/assets/356867d7-4d7c-4d7f-b5d5-86935c35b59a)
